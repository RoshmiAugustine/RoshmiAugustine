IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117030244_DataSharing_CS_AS_StartDate')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CollaborationSharingHistory]') AND [c].[name] = N'StartDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CollaborationSharingHistory] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [CollaborationSharingHistory] ALTER COLUMN [StartDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117030244_DataSharing_CS_AS_StartDate')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CollaborationSharing]') AND [c].[name] = N'StartDate');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CollaborationSharing] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [CollaborationSharing] ALTER COLUMN [StartDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117030244_DataSharing_CS_AS_StartDate')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencySharingHistory]') AND [c].[name] = N'StartDate');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AgencySharingHistory] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AgencySharingHistory] ALTER COLUMN [StartDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201117030244_DataSharing_CS_AS_StartDate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201117030244_DataSharing_CS_AS_StartDate', N'3.1.4');
END;

GO

