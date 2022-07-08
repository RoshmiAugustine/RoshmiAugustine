IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211011061726_ADD_ChildItemGroupNumber')
BEGIN
    ALTER TABLE [Item] ADD [ChildItemGroupNumber] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211011061726_ADD_ChildItemGroupNumber')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211011061726_ADD_ChildItemGroupNumber', N'3.1.4');
END;

GO

