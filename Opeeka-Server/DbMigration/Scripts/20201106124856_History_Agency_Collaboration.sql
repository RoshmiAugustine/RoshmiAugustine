IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    ALTER TABLE [AgencySharingHistory] DROP CONSTRAINT [FK_AgencySharingHistory_Agency_ReportingUnitAgencyID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    ALTER TABLE [CollaborationSharingHistory] DROP CONSTRAINT [FK_CollaborationSharingHistory_CollaborationSharing_ReportingUnitCollaborationID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    DROP INDEX [IX_CollaborationSharingHistory_ReportingUnitCollaborationID] ON [CollaborationSharingHistory];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    DROP INDEX [IX_AgencySharingHistory_ReportingUnitAgencyID] ON [AgencySharingHistory];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CollaborationSharingHistory]') AND [c].[name] = N'ReportingUnitCollaborationID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CollaborationSharingHistory] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [CollaborationSharingHistory] DROP COLUMN [ReportingUnitCollaborationID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencySharingHistory]') AND [c].[name] = N'ReportingUnitAgencyID');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AgencySharingHistory] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AgencySharingHistory] DROP COLUMN [ReportingUnitAgencyID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    ALTER TABLE [CollaborationSharingHistory] ADD [CollaborationSharingID] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    ALTER TABLE [AgencySharingHistory] ADD [AgencySharingID] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    CREATE INDEX [IX_CollaborationSharingHistory_CollaborationSharingID] ON [CollaborationSharingHistory] ([CollaborationSharingID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    CREATE INDEX [IX_AgencySharingHistory_AgencySharingID] ON [AgencySharingHistory] ([AgencySharingID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    ALTER TABLE [AgencySharingHistory] ADD CONSTRAINT [FK_AgencySharingHistory_AgencySharing_AgencySharingID] FOREIGN KEY ([AgencySharingID]) REFERENCES [AgencySharing] ([AgencySharingID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    ALTER TABLE [CollaborationSharingHistory] ADD CONSTRAINT [FK_CollaborationSharingHistory_CollaborationSharing_CollaborationSharingID] FOREIGN KEY ([CollaborationSharingID]) REFERENCES [CollaborationSharing] ([CollaborationSharingID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106124856_History_Agency_Collaboration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201106124856_History_Agency_Collaboration', N'3.1.4');
END;

GO

