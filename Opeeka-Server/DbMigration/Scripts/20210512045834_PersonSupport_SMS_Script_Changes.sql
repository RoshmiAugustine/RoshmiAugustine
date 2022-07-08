IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210512045834_PersonSupport_SMS_Script_Changes')
BEGIN
    ALTER TABLE [PersonSupport] ADD [EmailPermission] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210512045834_PersonSupport_SMS_Script_Changes')
BEGIN
    ALTER TABLE [PersonSupport] ADD [TextPermission] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210512045834_PersonSupport_SMS_Script_Changes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210512045834_PersonSupport_SMS_Script_Changes', N'3.1.4');
END;

GO

