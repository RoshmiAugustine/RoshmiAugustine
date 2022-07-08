IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonHelper_EndDate_PersonID' AND object_id = OBJECT_ID('dbo.PersonHelper'))>0
BEGIN
	DROP INDEX [IX_PersonHelper_EndDate_PersonID] ON [dbo].[PersonHelper]
END
CREATE NONCLUSTERED INDEX IX_PersonHelper_EndDate_PersonID ON [dbo].[PersonHelper] ([HelperID], [IsRemoved], [StartDate]) INCLUDE ([EndDate], [PersonID]) WITH (ONLINE = ON)

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200926110854_NEW_INDEX_PersonHelper_EndDate_PersonID')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200926110854_NEW_INDEX_PersonHelper_EndDate_PersonID', N'3.1.4');
END;

GO

