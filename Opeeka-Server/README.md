In VSCode, hit F5 to debug and run... 

Using docker compose - docker-compose up --build

VSCode bebug and run URL : https://localhost:5001/weatherforecast
Docker compose run URL : http://localhost:5000/weatherforecast


To Create migrations : dotnet ef migrations add InitialMigrations -o Data\Migrations -c opeekadbcontext -p Infrastructure\Infrastructure.csproj -s Api\Api.csproj

To run migrations :  dotnet ef database update -c opeekadbcontext -p Infrastructure\Infrastructure.csproj -s Api\Api.csproj



To re-run the migrations after dropping the database : dotnet ef database drop -p Api\Api.csproj --force && dotnet ef migrations remove -c opeekadbcontext -p Infrastructure\Infrastructure.csproj -s Api\Api.csproj && dotnet ef migrations add InitialMigrations -o Data\Migrations -c opeekadbcontext -p Infrastructure\Infrastructure.csproj -s Api\Api.csproj && dotnet ef database update -c opeekadbcontext -p Infrastructure\Infrastructure.csproj -s Api\Api.csproj

------------------------------------------------------------------------------------------------------------------------------------------------
# Code Analysis Tool		

## StyleCopAnalyzers

An implementation of StyleCop rules using the .NET Compiler Platform. It helps in enforcing standard style for code.

Referencce Link : https://medium.com/@michaelparkerdev/linting-c-in-2019-stylecop-sonar-resharper-and-roslyn-73e88af57ebd
                : https://medium.com/@bharatdwarkani/top-10-code-quality-static-analysers-for-asp-net-core-1660ad7a8d61
                : https://blog.submain.com/stylecop-detailed-guide/
                : https://github.com/DotNetAnalyzers/StyleCopAnalyzers/tree/master/documentation

### Installation package

	package : StyleCop.Analyzers 1.1.118

### Configuring StyleCop
	
	Configuring StyleCop is done in two optional steps.

	1) First, you can use rule set files to configure which rules are checked and how strongly you feel about them.
	2) Second, you can add a stylecop.json file to your project to fine-tune some rules.

	1. Rule Sets

	Create a stylecop.json file next to your solution, looks like this:

	{  
      "$schema": "https://raw.githubusercontent.com/DotNetAnalyzers/StyleCopAnalyzers/master/StyleCop.Analyzers/StyleCop.Analyzers/Settings/stylecop.schema.json",
      "settings": {
        "documentationRules": {
          "companyName": "Naicoits",
          "copyrightText": "Copyright (c) Naicoits. All rights reserved.",
          "headerDecoration": "-----------------------------------------------------------------------",
          "documentInterfaces": true,
          "documentExposedElements": false,
          "documentInternalElements": true,
          "documentPrivateElements": false,
          "documentPrivateFields": false
        },
        "indentation": {
          "indentationSize": 4,
          "tabSize": 4,
          "useTabs": false
        },
        "orderingRules": {
          "elementOrder": [
            "kind",
            "constant",
            "accessibility",
            "static",
            "readonly"
          ]
        }
      }
    } 


    ***Add location in .csproj

    <ItemGroup>     
	  <AdditionalFiles Include="..\stylecop.json" />
    </ItemGroup>

    2. .editorconfig

    Microsoft have said that .editorconfig files are the future and will potentially take over from .ruleset files. 
    For now it doesnt seem the ecosystem is set up for .editorconfig files to completely take over all the .ruleset file functionality.
    In the meantime it’s probably possible for .editorconfig rules to state something different from the .ruleset rules and have them clash, but I am yet to see it.

    Example : 

    [*.cs]

    # IDE0065: Misplaced using directive
    csharp_using_directive_placement = inside_namespace:silent // avoid warning of Inside namespace 

## SonarAnalyzer
    
    Analyzers which spot bugs and code smells in your code.

### Installation package

    Package : SonarAnalyzer.CSharp 8.7.0.17535



To upload to Server :  dotnet publish -f netcoreapp3.1 -c Release --self-contained false -r linux-x64 && scp -r Api\bin\Release\netcoreapp3.1\linux-x64\* opeekadevserver:/home/AzureUser/Code