IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201012131707_FKValueTypeChange')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Assessment]') AND [c].[name] = N'VoiceTypeFKID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Assessment] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Assessment] ALTER COLUMN [VoiceTypeFKID] bigint NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201012131707_FKValueTypeChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201012131707_FKValueTypeChange', N'3.1.4');
END;

GO

