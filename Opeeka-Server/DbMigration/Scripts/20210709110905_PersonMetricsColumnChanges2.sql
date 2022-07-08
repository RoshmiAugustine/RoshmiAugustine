IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210709110905_PersonMetricsColumnChanges2')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[dbo].[PersonAssessmentMetrics]') AND [c].[name] = N'UpdateDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [dbo].[PersonAssessmentMetrics] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [dbo].[PersonAssessmentMetrics] ALTER COLUMN [UpdateDate] datetime2 NOT NULL;
    ALTER TABLE [dbo].[PersonAssessmentMetrics] ADD DEFAULT (getdate()) FOR [UpdateDate];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210709110905_PersonMetricsColumnChanges2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210709110905_PersonMetricsColumnChanges2', N'3.1.4');
END;

GO

