IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] DROP CONSTRAINT [FK_AssessmentEmailLinkDetails_Helper_HelperID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] DROP CONSTRAINT [FK_AssessmentEmailLinkDetails_PersonSupport_PersonSupportID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssessmentEmailLinkDetails]') AND [c].[name] = N'PersonSupportEmail');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AssessmentEmailLinkDetails] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AssessmentEmailLinkDetails] DROP COLUMN [PersonSupportEmail];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssessmentEmailLinkDetails]') AND [c].[name] = N'PersonSupportID');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AssessmentEmailLinkDetails] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AssessmentEmailLinkDetails] ALTER COLUMN [PersonSupportID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssessmentEmailLinkDetails]') AND [c].[name] = N'HelperID');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AssessmentEmailLinkDetails] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AssessmentEmailLinkDetails] ALTER COLUMN [HelperID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] ADD [PersonOrSupportEmail] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] ADD [VoiceTypeID] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    CREATE INDEX [IX_AssessmentEmailLinkDetails_VoiceTypeID] ON [AssessmentEmailLinkDetails] ([VoiceTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] ADD CONSTRAINT [FK_AssessmentEmailLinkDetails_Helper_HelperID] FOREIGN KEY ([HelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] ADD CONSTRAINT [FK_AssessmentEmailLinkDetails_PersonSupport_PersonSupportID] FOREIGN KEY ([PersonSupportID]) REFERENCES [PersonSupport] ([PersonSupportID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] ADD CONSTRAINT [FK_AssessmentEmailLinkDetails_VoiceType_VoiceTypeID] FOREIGN KEY ([VoiceTypeID]) REFERENCES [info].[VoiceType] ([VoiceTypeID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826111029_EmailAssessmentEntityChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200826111029_EmailAssessmentEntityChanges', N'3.1.4');
END;

GO

