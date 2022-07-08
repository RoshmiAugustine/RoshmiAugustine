IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117041318_DataSharing_CS_AS_StartDate_2')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencySharing]') AND [c].[name] = N'StartDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AgencySharing] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AgencySharing] ALTER COLUMN [StartDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117041318_DataSharing_CS_AS_StartDate_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201117041318_DataSharing_CS_AS_StartDate_2', N'3.1.4');
END;

GO

