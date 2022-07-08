IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813113100_CaregiverCategoryMigration')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD [CaregiverCategory] nvarchar(50) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200813113100_CaregiverCategoryMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200813113100_CaregiverCategoryMigration', N'3.1.4');
END;

GO

