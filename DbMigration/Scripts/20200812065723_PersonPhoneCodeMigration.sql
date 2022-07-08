IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200812065723_PersonPhoneCodeMigration')
BEGIN
    ALTER TABLE [PersonSupport] ADD [PhoneCode] varchar(10) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200812065723_PersonPhoneCodeMigration')
BEGIN
    ALTER TABLE [Person] ADD [Phone1Code] varchar(10) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200812065723_PersonPhoneCodeMigration')
BEGIN
    ALTER TABLE [Person] ADD [Phone2Code] varchar(10) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200812065723_PersonPhoneCodeMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200812065723_PersonPhoneCodeMigration', N'3.1.4');
END;

GO

