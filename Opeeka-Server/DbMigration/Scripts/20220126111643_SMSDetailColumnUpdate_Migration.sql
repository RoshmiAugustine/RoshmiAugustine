IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126111643_SMSDetailColumnUpdate_Migration')
BEGIN
    ALTER TABLE [SMSDetail] ADD [PersonSupportID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126111643_SMSDetailColumnUpdate_Migration')
BEGIN
    ALTER TABLE [EmailDetail] ADD [PersonSupportID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126111643_SMSDetailColumnUpdate_Migration')
BEGIN
    CREATE INDEX [IX_SMSDetail_PersonSupportID] ON [SMSDetail] ([PersonSupportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126111643_SMSDetailColumnUpdate_Migration')
BEGIN
    CREATE INDEX [IX_EmailDetail_PersonSupportID] ON [EmailDetail] ([PersonSupportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126111643_SMSDetailColumnUpdate_Migration')
BEGIN
    ALTER TABLE [EmailDetail] ADD CONSTRAINT [FK_EmailDetail_PersonSupport_PersonSupportID] FOREIGN KEY ([PersonSupportID]) REFERENCES [PersonSupport] ([PersonSupportID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126111643_SMSDetailColumnUpdate_Migration')
BEGIN
    ALTER TABLE [SMSDetail] ADD CONSTRAINT [FK_SMSDetail_PersonSupport_PersonSupportID] FOREIGN KEY ([PersonSupportID]) REFERENCES [PersonSupport] ([PersonSupportID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126111643_SMSDetailColumnUpdate_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220126111643_SMSDetailColumnUpdate_Migration', N'3.1.4');
END;

GO

