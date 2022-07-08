rm -rf UnitTests/TestResults/*
dotnet test --collect:"XPlat Code Coverage" --output UnitTests/TestResults/
COVERAGE_FILE=`ls UnitTests/TestResults/*/coverage.cobertura.xml`
reportgenerator "-reports:$COVERAGE_FILE" "-targetdir:coveragereport" -reporttypes:Html



