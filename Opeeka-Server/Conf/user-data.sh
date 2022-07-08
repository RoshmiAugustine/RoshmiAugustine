#!/bin/sh
NEW_RELIC_LICENSE_KEY=$1
NEW_RELIC_APP_NAME=$2
AZURE_KEY_VAULT_URL=$3
AZURE_KEY_VAULT_CLIENT_ID=$4
AZURE_KEY_VAULT_SECRET_KEY=$5
ENV_NAME=$6
wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo add-apt-repository -y universe
sudo apt-get update
sudo apt-get install  -y apt-transport-https
sudo apt-get update
sudo apt-get install -y aspnetcore-runtime-3.1
mkdir Code
sudo apt-get -y install nginx
sudo tee /etc/nginx/sites-available/default >/dev/null <<'EOF'
upstream dotnet {
        server 127.0.0.1:5000;
}

server {
    listen 80 default_server;
    listen [::]:80 default_server;
    server_name _;
    location / {
        proxy_pass         http://dotnet;
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
EOF

sudo service nginx restart

echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | sudo tee /etc/apt/sources.list.d/newrelic.list
wget -O- https://download.newrelic.com/548C16BF.gpg | sudo apt-key add -
sudo apt-get update
sudo apt-get install -y newrelic-netcore20-agent

sudo cat <<EOT >> ~/.profile
export CORECLR_ENABLE_PROFILING=1
export CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A}
export CORECLR_NEWRELIC_HOME=/usr/local/newrelic-netcore20-agent
export CORECLR_PROFILER_PATH="/usr/local/newrelic-netcore20-agent/libNewRelicProfiler.so"
export NEW_RELIC_LICENSE_KEY=$NEW_RELIC_LICENSE_KEY
export NEW_RELIC_APP_NAME=$NEW_RELIC_APP_NAME
EOT

sudo tee /usr/local/newrelic-netcore20-agent/newrelic.config >/dev/null <<EOF
<?xml version="1.0"?>
<!-- Copyright (c) 2008-2020 New Relic, Inc.  All rights reserved. -->
<!-- For more information see: https://newrelic.com/docs/dotnet/dotnet-agent-configuration -->
<configuration xmlns="urn:newrelic-config" agentEnabled="true">
        <service licenseKey="$NEW_RELIC_LICENSE_KEY" />
        <application>
                <name>$NEW_RELIC_APP_NAME</name>
        </application>
        <log level="info"/>
        <transactionTracer enabled="true"
                transactionThreshold="apdex_f"
                stackTraceThreshold="500"
                recordSql="obfuscated"
                explainEnabled="false"
                explainThreshold="500"/>
        <crossApplicationTracer enabled="true"/>
        <errorCollector enabled="true">
                <ignoreErrors>
                        <exception>System.IO.FileNotFoundException</exception>
                        <exception>System.Threading.ThreadAbortException</exception>
                </ignoreErrors>
                <ignoreStatusCodes>
                        <code>401</code>
                        <code>404</code>
                </ignoreStatusCodes>
        </errorCollector>
        <browserMonitoring autoInstrument="true" />
        <threadProfiling>
                <ignoreMethod>System.Threading.WaitHandle:InternalWaitOne</ignoreMethod>
                <ignoreMethod>System.Threading.WaitHandle:WaitAny</ignoreMethod>
        </threadProfiling>
</configuration>
EOF

sudo tee /etc/systemd/system/pcis-app.service >/dev/null <<EOF
[Unit]
Description=PCIS Web Api App

[Service]
WorkingDirectory=/home/AzureUser/Code
ExecStart=/usr/local/newrelic-netcore20-agent/run.sh /usr/bin/dotnet /home/AzureUser/Code/Api.dll --KeyVault:Url "https://pcis-dev-testing.vault.azure.net/" --KeyVault:ClientId "$AZURE_KEY_VAULT_CLIENT_ID" --KeyVault:SecretId "$AZURE_KEY_VAULT_SECRET_KEY"
Restart=always

RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=pcis-app
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=$ENV_NAME

[Install]
WantedBy=multi-user.target
EOF

sudo systemctl enable pcis-app.service
sudo systemctl start pcis-app.service

