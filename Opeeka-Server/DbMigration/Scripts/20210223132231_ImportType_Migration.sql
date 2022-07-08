IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210223132231_ImportType_Migration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FileImport]') AND [c].[name] = N'ImportType');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [FileImport] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [FileImport] DROP COLUMN [ImportType];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210223132231_ImportType_Migration')
BEGIN
    ALTER TABLE [FileImport] ADD [ImportTypeID] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210223132231_ImportType_Migration')
BEGIN
    CREATE TABLE [info].[ImportType] (
        [ImportTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NULL,
        [TemplateJson] nvarchar(max) NULL,
        [TemplateURL] nvarchar(250) NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_ImportType] PRIMARY KEY ([ImportTypeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210223132231_ImportType_Migration')
BEGIN
    CREATE INDEX [IX_FileImport_ImportTypeID] ON [FileImport] ([ImportTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210223132231_ImportType_Migration')
BEGIN
    ALTER TABLE [FileImport] ADD CONSTRAINT [FK_FileImport_ImportType_ImportTypeID] FOREIGN KEY ([ImportTypeID]) REFERENCES [info].[ImportType] ([ImportTypeID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210223132231_ImportType_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210223132231_ImportType_Migration', N'3.1.4');
END;

GO

