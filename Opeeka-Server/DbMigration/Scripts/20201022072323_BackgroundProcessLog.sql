IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201022072323_BackgroundProcessLog')
BEGIN
    ALTER TABLE [NotifyReminder] ADD [IsLogAdded] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201022072323_BackgroundProcessLog')
BEGIN
    CREATE TABLE [BackgroundProcessLog] (
        [BackgroundProcessLogID] int NOT NULL IDENTITY,
        [ProcessName] nvarchar(max) NULL,
        [LastProcessedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_BackgroundProcessLog] PRIMARY KEY ([BackgroundProcessLogID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201022072323_BackgroundProcessLog')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201022072323_BackgroundProcessLog', N'3.1.4');
END;

GO

