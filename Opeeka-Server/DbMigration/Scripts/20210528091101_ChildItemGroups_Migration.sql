IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    ALTER TABLE [Response] ADD [DisplayChildItem] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    ALTER TABLE [Item] ADD [ParentItemID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AssessmentResponse]') AND [c].[name] = N'QuestionnaireItemID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AssessmentResponse] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AssessmentResponse] ALTER COLUMN [QuestionnaireItemID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD [GroupNumber] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD [ItemID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    CREATE INDEX [IX_Item_ParentItemID] ON [Item] ([ParentItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    CREATE INDEX [IX_AssessmentResponse_ItemID] ON [AssessmentResponse] ([ItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD CONSTRAINT [FK_AssessmentResponse_Item_ItemID] FOREIGN KEY ([ItemID]) REFERENCES [Item] ([ItemID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    ALTER TABLE [Item] ADD CONSTRAINT [FK_Item_Item_ParentItemID] FOREIGN KEY ([ParentItemID]) REFERENCES [Item] ([ItemID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210528091101_ChildItemGroups_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210528091101_ChildItemGroups_Migration', N'3.1.4');
END;

GO

