IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210429053142_WeatherForecastRemoved_Migration')
BEGIN
    DROP TABLE [WeatherForecasts];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210429053142_WeatherForecastRemoved_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210429053142_WeatherForecastRemoved_Migration', N'3.1.4');
END;

GO

