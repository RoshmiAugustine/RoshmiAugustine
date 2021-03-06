IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220518115505_columnNameChanges')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssessmentResponseAttachment]') AND [c].[name] = N'BlobURL');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AssessmentResponseAttachment] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AssessmentResponseAttachment] DROP COLUMN [BlobURL];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220518115505_columnNameChanges')
BEGIN
    ALTER TABLE [AssessmentResponseAttachment] ADD [FileURL] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220518115505_columnNameChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220518115505_columnNameChanges', N'3.1.4');
END;

GO

