#! /bin/bash

dotnet run -p DbMigration/DbMigration.csproj $1

LASTCREATEDMIGRATION=$(ls Infrastructure/Data/Migrations | tail -3 | head -n 1 | sed 's/.\{3\}$//')
echo "The last created migration is $LASTCREATEDMIGRATION"

echo "Enter the name of the new migration."
read NEWMIGRATION

ADDMIGRATIONCOMMAND=$(dotnet ef migrations add $NEWMIGRATION -c opeekadbcontext -p Infrastructure/Infrastructure.csproj -s Api/Api.csproj)
echo "Adding migration : $ADDMIGRATIONCOMMAND"

NEWLYCREATEDMIGRATION=$(ls Infrastructure/Data/Migrations | tail -3 | head -n 1 | sed 's/.\{3\}$//')
echo "Newly Generated MigrationScript file : $NEWLYCREATEDMIGRATION"

MIGRATIONSCRIPTGENERATION=$(dotnet ef migrations script $LASTCREATEDMIGRATION -c opeekadbcontext -p Infrastructure/Infrastructure.csproj -s Api/Api.csproj -o DbMigration/Scripts/$NEWLYCREATEDMIGRATION.sql -i)
echo "Generating Migration Script: $MIGRATIONSCRIPTGENERATION"

$(node Node/modify_migration_project.js $LASTCREATEDMIGRATION $NEWLYCREATEDMIGRATION)

dotnet ef database update -c opeekadbcontext -p Infrastructure/Infrastructure.csproj -s Api/Api.csproj

dotnet run -p DbMigration/DbMigration.csproj $1
