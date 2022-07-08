IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517060315_DefaultResponseIDInItem_Migration')
BEGIN
    ALTER TABLE [Item] ADD [DefaultResponseID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517060315_DefaultResponseIDInItem_Migration')
BEGIN
    CREATE INDEX [IX_Item_DefaultResponseID] ON [Item] ([DefaultResponseID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517060315_DefaultResponseIDInItem_Migration')
BEGIN
    ALTER TABLE [Item] ADD CONSTRAINT [FK_Item_Response_DefaultResponseID] FOREIGN KEY ([DefaultResponseID]) REFERENCES [Response] ([ResponseID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517060315_DefaultResponseIDInItem_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210517060315_DefaultResponseIDInItem_Migration', N'3.1.4');
END;

GO

