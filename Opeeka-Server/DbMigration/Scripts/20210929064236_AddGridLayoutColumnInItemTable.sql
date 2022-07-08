IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929064236_AddGridLayoutColumnInItemTable')
BEGIN
    ALTER TABLE [Item] ADD [GridLayoutInFormView] bit NOT NULL DEFAULT CAST(1 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210929064236_AddGridLayoutColumnInItemTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210929064236_AddGridLayoutColumnInItemTable', N'3.1.4');
END;

GO

