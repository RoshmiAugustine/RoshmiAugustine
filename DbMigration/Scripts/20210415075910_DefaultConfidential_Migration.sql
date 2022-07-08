IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210415075910_DefaultConfidential_Migration')
BEGIN
    ALTER TABLE [QuestionnaireItem] ADD [DefaultOtherConfidentiality] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210415075910_DefaultConfidential_Migration')
BEGIN
    ALTER TABLE [QuestionnaireItem] ADD [DefaultPersonRequestedConfidentiality] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210415075910_DefaultConfidential_Migration')
BEGIN
    ALTER TABLE [QuestionnaireItem] ADD [DefaultRequiredConfidentiality] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210415075910_DefaultConfidential_Migration')
BEGIN
    ALTER TABLE [Item] ADD [DefaultOtherConfidentiality] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210415075910_DefaultConfidential_Migration')
BEGIN
    ALTER TABLE [Item] ADD [DefaultPersonRequestedConfidentiality] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210415075910_DefaultConfidential_Migration')
BEGIN
    ALTER TABLE [Item] ADD [DefaultRequiredConfidentiality] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210415075910_DefaultConfidential_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210415075910_DefaultConfidential_Migration', N'3.1.4');
END;

GO

