rm Infrastructure/Data/Migrations/*.cs
dotnet ef migrations Add InitialMigration -c opeekadbcontext -p Infrastructure/Infrastructure.csproj -s Api/Api.csproj -o Data/Migrations
rm DbMigration/Scripts/*.sql
LASTCREATEDMIGRATION=$(ls Infrastructure/Data/Migrations | tail -3 | head -n 1 | sed 's/.\{3\}$//')
MIGRATIONSCRIPTGENERATION=$(dotnet ef migrations script -c opeekadbcontext -p Infrastructure/Infrastructure.csproj -s Api/Api.csproj -o DbMigration/Scripts/$LASTCREATEDMIGRATION.sql -i)
dotnet ef database update -c opeekadbcontext -p Infrastructure/Infrastructure.csproj -s Api/Api.csproj