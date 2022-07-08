IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317121641_Add_CountryId_Address_Migration')
BEGIN
    ALTER TABLE [Address] ADD [CountryId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317121641_Add_CountryId_Address_Migration')
BEGIN
    CREATE INDEX [IX_Address_CountryId] ON [Address] ([CountryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317121641_Add_CountryId_Address_Migration')
BEGIN
    ALTER TABLE [Address] ADD CONSTRAINT [FK_Address_Country_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [info].[Country] ([CountryID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210317121641_Add_CountryId_Address_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210317121641_Add_CountryId_Address_Migration', N'3.1.4');
END;

GO

