
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201109084146_DataSharing_IsActiveRemoved')
BEGIN
   DECLARE @var0 sysname;
   SELECT @var0 = [d].[name]
   FROM [sys].[default_constraints] [d]
   INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
   WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CollaborationSharing]') AND [c].[name] = N'IsActive');
   IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CollaborationSharing] DROP CONSTRAINT [' + @var0 + '];');
   ALTER TABLE [CollaborationSharing] DROP COLUMN [IsActive];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201109084146_DataSharing_IsActiveRemoved')
BEGIN
   DECLARE @var1 sysname;
   SELECT @var1 = [d].[name]
   FROM [sys].[default_constraints] [d]
   INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
   WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AgencySharing]') AND [c].[name] = N'IsActive');
   IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AgencySharing] DROP CONSTRAINT [' + @var1 + '];');
   ALTER TABLE [AgencySharing] DROP COLUMN [IsActive];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201109084146_DataSharing_IsActiveRemoved')
BEGIN
   INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
   VALUES (N'20201109084146_DataSharing_IsActiveRemoved', N'3.1.4');
END;

GO