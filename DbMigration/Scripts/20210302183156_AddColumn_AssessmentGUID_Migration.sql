IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210302183156_AddColumn_AssessmentGUID_Migration')
BEGIN
    ALTER TABLE [Assessment] ADD [AssessmentGUID] uniqueidentifier NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210302183156_AddColumn_AssessmentGUID_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210302183156_AddColumn_AssessmentGUID_Migration', N'3.1.4');
END;

GO

