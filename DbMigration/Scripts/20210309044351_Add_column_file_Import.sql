IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210309044351_Add_column_file_Import')
BEGIN
    ALTER TABLE [FileImport] ADD [ImportFileName] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210309044351_Add_column_file_Import')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210309044351_Add_column_file_Import', N'3.1.4');
END;

GO

