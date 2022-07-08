IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530073321_AddColumn_powerbiRDLReport')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencyPowerBIReport]') AND [c].[name] = N'Filters');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AgencyPowerBIReport] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AgencyPowerBIReport] DROP COLUMN [Filters];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530073321_AddColumn_powerbiRDLReport')
BEGIN
    ALTER TABLE [AgencyPowerBIReport] ADD [FiltersOrParameters] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530073321_AddColumn_powerbiRDLReport')
BEGIN
    ALTER TABLE [AgencyPowerBIReport] ADD [IsRDLReport] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530073321_AddColumn_powerbiRDLReport')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220530073321_AddColumn_powerbiRDLReport', N'3.1.4');
END;

GO

