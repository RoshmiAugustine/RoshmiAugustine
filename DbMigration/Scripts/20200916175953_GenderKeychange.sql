IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200916175953_GenderKeychange')
BEGIN
    ALTER TABLE [Person] DROP CONSTRAINT [FK_Person_Gender_GenderID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200916175953_GenderKeychange')
BEGIN
    DROP INDEX [IX_Person_GenderID] ON [Person];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200916175953_GenderKeychange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200916175953_GenderKeychange', N'3.1.4');
END;

GO

