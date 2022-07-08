IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126084455_EmailLinkColumnUpdate_Migration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssessmentEmailLinkDetails]') AND [c].[name] = N'AssessmentGUID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AssessmentEmailLinkDetails] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AssessmentEmailLinkDetails] DROP COLUMN [AssessmentGUID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126084455_EmailLinkColumnUpdate_Migration')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] ADD [AssessmentEmailLinkGUID] uniqueidentifier NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126084455_EmailLinkColumnUpdate_Migration')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] ADD [PhoneNumber] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126084455_EmailLinkColumnUpdate_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220126084455_EmailLinkColumnUpdate_Migration', N'3.1.4');
END;

GO

