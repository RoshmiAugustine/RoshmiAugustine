IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210602192941_ResponseIDNullInChild_Migration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssessmentResponse]') AND [c].[name] = N'ResponseID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AssessmentResponse] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AssessmentResponse] ALTER COLUMN [ResponseID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210602192941_ResponseIDNullInChild_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210602192941_ResponseIDNullInChild_Migration', N'3.1.4');
END;

GO

