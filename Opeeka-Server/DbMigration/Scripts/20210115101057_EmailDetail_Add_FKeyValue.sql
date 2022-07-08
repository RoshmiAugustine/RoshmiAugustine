IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115101057_EmailDetail_Add_FKeyValue')
BEGIN
    ALTER TABLE [EmailDetail] ADD [FKeyValue] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115101057_EmailDetail_Add_FKeyValue')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210115101057_EmailDetail_Add_FKeyValue', N'3.1.4');
END;

GO

