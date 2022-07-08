IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [CollaborationSharingPolicy] DROP CONSTRAINT [FK_CollaborationSharingPolicy_CollaborationSharing_CollaborationSharingID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [CollaborationSharingPolicy] DROP CONSTRAINT [FK_CollaborationSharingPolicy_SharingPolicy_SharingPolicyID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    DROP INDEX [IX_CollaborationSharingPolicy_CollaborationSharingID] ON [CollaborationSharingPolicy];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    DROP INDEX [IX_CollaborationSharingPolicy_SharingPolicyID] ON [CollaborationSharingPolicy];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CollaborationSharingPolicy]') AND [c].[name] = N'CollaborationSharingID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CollaborationSharingPolicy] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [CollaborationSharingPolicy] DROP COLUMN [CollaborationSharingID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CollaborationSharingPolicy]') AND [c].[name] = N'SharingPolicyID');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CollaborationSharingPolicy] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [CollaborationSharingPolicy] DROP COLUMN [SharingPolicyID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencySharingPolicy]') AND [c].[name] = N'AgencySharingID');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AgencySharingPolicy] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AgencySharingPolicy] DROP COLUMN [AgencySharingID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencySharingPolicy]') AND [c].[name] = N'SharingPolicyID');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AgencySharingPolicy] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [AgencySharingPolicy] DROP COLUMN [SharingPolicyID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [CollaborationSharingPolicy] ADD [Abbreviation] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [CollaborationSharingPolicy] ADD [Description] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [CollaborationSharingPolicy] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [CollaborationSharingPolicy] ADD [Name] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [CollaborationSharingPolicy] ADD [Weight] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [AgencySharingPolicy] ADD [Abbreviation] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [AgencySharingPolicy] ADD [Description] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [AgencySharingPolicy] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    ALTER TABLE [AgencySharingPolicy] ADD [Name] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106110101_SharingPolicyLookupsMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201106110101_SharingPolicyLookupsMigration', N'3.1.4');
END;

GO

