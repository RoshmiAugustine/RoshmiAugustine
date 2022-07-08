IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonSupport]') AND [c].[name] = N'MiddleName');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PersonSupport] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [PersonSupport] ALTER COLUMN [MiddleName] varchar(150) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonSupport]') AND [c].[name] = N'LastName');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [PersonSupport] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [PersonSupport] ALTER COLUMN [LastName] varchar(150) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PersonSupport]') AND [c].[name] = N'FirstName');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [PersonSupport] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [PersonSupport] ALTER COLUMN [FirstName] varchar(150) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Person]') AND [c].[name] = N'MiddleName');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Person] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Person] ALTER COLUMN [MiddleName] varchar(150) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Person]') AND [c].[name] = N'LastName');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Person] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Person] ALTER COLUMN [LastName] varchar(150) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Person]') AND [c].[name] = N'FirstName');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Person] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Person] ALTER COLUMN [FirstName] varchar(150) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Helper]') AND [c].[name] = N'MiddleName');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Helper] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Helper] ALTER COLUMN [MiddleName] varchar(150) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Helper]') AND [c].[name] = N'LastName');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Helper] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Helper] ALTER COLUMN [LastName] varchar(150) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Helper]') AND [c].[name] = N'FirstName');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Helper] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Helper] ALTER COLUMN [FirstName] varchar(150) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Agency]') AND [c].[name] = N'Name');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Agency] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Agency] ALTER COLUMN [Name] varchar(150) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210115093726_ColumnSizeChange150_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210115093726_ColumnSizeChange150_Migration', N'3.1.4');
END;

GO

