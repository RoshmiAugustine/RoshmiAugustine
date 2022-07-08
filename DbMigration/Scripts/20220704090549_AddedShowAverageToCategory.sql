IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220704090549_AddedShowAverageToCategory')
BEGIN
    ALTER TABLE [info].[Category] ADD [ShowAverage] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220704090549_AddedShowAverageToCategory')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220704090549_AddedShowAverageToCategory', N'3.1.4');
END;

GO

