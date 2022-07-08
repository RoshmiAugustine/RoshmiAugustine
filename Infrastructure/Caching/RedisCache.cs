// -----------------------------------------------------------------------
// <copyright file="RedisCache.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Extensions.Logging;
using Opeeka.PICS.Domain.Interfaces;
using Redis.StackExchange.Encryption;

namespace Opeeka.PICS.Infrastructure.Caching
{
    public class RedisCache : ICache
    {
        private IConfiguration configuration;

        private RedisClient Redisclient { get; set; }

        public int DatabaseNumber => this.Redisclient.DatabaseNumber;

        public bool IsConnected => this._IsConnected;

        private readonly ILogger<RedisCache> logger;

        private bool _IsConnected { get; set; }
        private bool _CacheEnabled { get; set; }

        public bool Encryption { get => this.Redisclient.Encryption; set => this.Redisclient.Encryption = value; }
        public List<OpenIdConnectConfiguration> openIdConnectConfiguration;
        public RedisCache(IConfiguration iConfig, ILogger<RedisCache> ilogger)
        {
            try
            {
                logger = ilogger;
                openIdConnectConfiguration = new List<OpenIdConnectConfiguration>();
                this.configuration = iConfig;
                string stsDiscoveryEndpoint = this.configuration["OpenIDConnectUrl"];
                ConfigurationManager<OpenIdConnectConfiguration> configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
                openIdConnectConfiguration.Add(configManager.GetConfigurationAsync().Result);

                stsDiscoveryEndpoint = this.configuration["OpenIDConnectROPCUrl"];
                configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
                openIdConnectConfiguration.Add(configManager.GetConfigurationAsync().Result);

                stsDiscoveryEndpoint = this.configuration["openidconnectSSOurl"];
                configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
                openIdConnectConfiguration.Add(configManager.GetConfigurationAsync().Result);

                _IsConnected = false;
                this.configuration = iConfig;                
                var cacheEnabled = this.configuration.GetSection("RedisSettings").GetSection("IsCacheEnabled").Value;
                logger.LogWarning(cacheEnabled);
                _CacheEnabled = cacheEnabled == "true" ? true : false;
                if (_CacheEnabled)
                {
                    string connectionFromConfig = this.configuration["RedisSettings-AzureConnectionString"];
                    logger.LogWarning(connectionFromConfig);
                    this.Redisclient = new RedisClient(connectionFromConfig);
                    if (this.Redisclient.IsConnected)
                    {
                        logger.LogWarning("Redis connected");
                        _IsConnected = true;
                        var encrypt = this.configuration.GetSection("RedisSettings").GetSection("Encryption").Value;
                        this.Encryption = encrypt == "true" ? true : false;
                    }
                }
            }
            catch (Exception ex)
            {
                _IsConnected = false;
            }

        }

        public List<OpenIdConnectConfiguration> GetOpenIDConfig()
        {
            return this.openIdConnectConfiguration;
        }
        //public T Get<T>(string key) => this.IsConnected ? this.Redisclient.Get<T>(key) : default(T);

        public T Get<T>(string key)
        {
            try
            {
                return this.IsConnected ? this.Redisclient.GetAsync<T>(key).Result : default(T);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        //public string Get(string key) => this.IsConnected ? this.Redisclient.GetAsync(key).Result : null;

        public string Get(string key)
        {
            try
            {
                return this.IsConnected ? this.Redisclient.GetAsync(key).Result : null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys) => this.IsConnected ? this.Redisclient.GetAll<T>(keys) : null;

        //public bool Post<T>(string key, T objectToCache) => this.IsConnected ? this.Redisclient.Post<T>(key, objectToCache) : false;

        public bool Post<T>(string key, T objectToCache)
        {
            try
            {
                return this.IsConnected ? this.Redisclient.Post<T>(key, objectToCache) : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Post<T>(string key, T objectToCache, TimeSpan expiresAt) => this.IsConnected ? this.Redisclient.Post<T>(key, objectToCache, expiresAt) : false;

        public bool Post(string key, string objectToCache) => this.IsConnected ? this.Redisclient.Post(key, objectToCache) : false;

        public bool Post(string key, string objectToCache, TimeSpan expiresAt) => this.IsConnected ? this.Redisclient.Post(key, objectToCache, expiresAt) : false;

        public bool PostAll<T>(IDictionary<string, T> values) => this.IsConnected ? this.Redisclient.PostAll<T>(values) : false;

        public bool PostAll<T>(IDictionary<string, T> values, TimeSpan expiresAt) => this.IsConnected ? this.Redisclient.PostAll<T>(values, expiresAt) : false;

        //public bool Delete(string key) => this.IsConnected ? this.Redisclient.Delete(key) : false;

        public bool Delete(string key)
        {
            try
            {
                return this.IsConnected ? this.Redisclient.DeleteAsync(key).Result : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public bool DeleteAll(IEnumerable<string> keys) => this.IsConnected ? this.Redisclient.DeleteAll(keys) : false;

        public bool DeleteAll(IEnumerable<string> keys)
        {
            try
            {
                return this.IsConnected ? this.Redisclient.DeleteAll(keys) : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool FlushAll() => this.IsConnected ? this.Redisclient.FlushAll() : false;

        public bool IsKeyExists(string key) => this.IsConnected ? this.Redisclient.IsKeyExists(key) : false;

        public Task<bool> IsKeyExistsAsync(string key) => this.IsConnected ? this.Redisclient.IsKeyExistsAsync(key) : null;

        public Task<string> GetAsync(string key) => this.IsConnected ? this.Redisclient.GetAsync(key) : null;

        public Task<T> GetAsync<T>(string key) => this.IsConnected ? this.Redisclient.GetAsync<T>(key) : null;

        public Task<bool> PostAsync<T>(string key, T objectToCache) => this.IsConnected ? this.Redisclient.PostAsync<T>(key, objectToCache) : null;

        public Task<bool> PostAsync<T>(string key, T objectToCache, TimeSpan expiresAt) => this.IsConnected ? this.Redisclient.PostAsync<T>(key, objectToCache, expiresAt) : null;

        public Task<bool> PostAsync(string key, string objectToCache) => this.IsConnected ? this.Redisclient.PostAsync(key, objectToCache) : null;

        public Task<bool> PostAsync(string key, string objectToCache, TimeSpan expiresAt) => this.IsConnected ? this.Redisclient.PostAsync(key, objectToCache, expiresAt) : null;

        public Task<bool> DeleteAsync(string key) => this.IsConnected ? this.Redisclient.DeleteAsync(key) : null;

        public Task<bool> FlushAllAsync() => this.IsConnected ? this.Redisclient.FlushAllAsync() : null;
    }
}
