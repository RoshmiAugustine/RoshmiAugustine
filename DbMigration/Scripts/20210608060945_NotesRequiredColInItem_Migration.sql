IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210608060945_NotesRequiredColInItem_Migration')
BEGIN
    ALTER TABLE [Item] ADD [NotesRequired] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210608060945_NotesRequiredColInItem_Migration')
BEGIN
    ALTER TABLE [Item] ADD [ShowNotes] bit NOT NULL DEFAULT CAST(1 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210608060945_NotesRequiredColInItem_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210608060945_NotesRequiredColInItem_Migration', N'3.1.4');
END;

GO

