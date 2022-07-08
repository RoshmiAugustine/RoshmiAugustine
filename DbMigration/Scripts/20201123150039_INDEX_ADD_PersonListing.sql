IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonCollaboration_IsRemoved' AND object_id = OBJECT_ID('dbo.PersonCollaboration'))>0
BEGIN
	DROP INDEX [IX_PersonCollaboration_IsRemoved] ON [dbo].[PersonCollaboration]
END
CREATE NONCLUSTERED INDEX IX_PersonCollaboration_IsRemoved ON [dbo].[PersonCollaboration] ([IsRemoved]) INCLUDE ([PersonID], [CollaborationID]) WITH (ONLINE = ON)

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonCollaboration_CollaborationID_IsRemoved' AND object_id = OBJECT_ID('dbo.PersonCollaboration'))>0
BEGIN
	DROP INDEX [IX_PersonCollaboration_CollaborationID_IsRemoved] ON [dbo].[PersonCollaboration]
END
CREATE NONCLUSTERED INDEX IX_PersonCollaboration_CollaborationID_IsRemoved ON [dbo].[PersonCollaboration] ([CollaborationID],[IsRemoved]) INCLUDE ([PersonID], [EndDate]) WITH (ONLINE = ON)

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123150039_INDEX_ADD_PersonListing')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201123150039_INDEX_ADD_PersonListing', N'3.1.4');
END;

GO

