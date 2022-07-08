IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211018124806_GroupWithPrevious')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Item]') AND [c].[name] = N'GroupWithPrevious');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Item] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Item] DROP COLUMN [GroupWithPrevious];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211018124806_GroupWithPrevious')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211018124806_GroupWithPrevious', N'3.1.4');
END;

GO

