IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210621085841_ADD_HelperCollaborationId_Migration')
BEGIN
    ALTER TABLE [PersonHelper] ADD [CollaborationID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210621085841_ADD_HelperCollaborationId_Migration')
BEGIN
    CREATE INDEX [IX_PersonHelper_CollaborationID] ON [PersonHelper] ([CollaborationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210621085841_ADD_HelperCollaborationId_Migration')
BEGIN
    ALTER TABLE [PersonHelper] ADD CONSTRAINT [FK_PersonHelper_Collaboration_CollaborationID] FOREIGN KEY ([CollaborationID]) REFERENCES [Collaboration] ([CollaborationID]) ON DELETE NO ACTION;
END;

GO

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotificationTypeID_IsRemoved' AND object_id = OBJECT_ID('dbo.NotificationLog'))>0
BEGIN
	DROP INDEX [IX_NotificationTypeID_IsRemoved] ON [dbo].[NotificationLog]
END
CREATE NONCLUSTERED INDEX IX_NotificationTypeID_IsRemoved ON [dbo].[NotificationLog] ([NotificationTypeID],[NotificationResolutionStatusID],[IsRemoved])
INCLUDE ([NotificationDate], [PersonID], [FKeyValue], [QuestionnaireID]) WITH (ONLINE = ON)
GO

 IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonID_NotificationTypeID_IsRemoved' AND object_id = OBJECT_ID('dbo.NotificationLog'))>0
BEGIN
	DROP INDEX [IX_PersonID_NotificationTypeID_IsRemoved] ON [dbo].[NotificationLog]
END
CREATE NONCLUSTERED INDEX IX_PersonID_NotificationTypeID_IsRemoved 
ON [dbo].[NotificationLog] ([PersonID],[NotificationTypeID],[NotificationResolutionStatusID],[IsRemoved],[QuestionnaireID])
INCLUDE ([NotificationDate], [FKeyValue]) WITH (ONLINE = ON)


IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210621085841_ADD_HelperCollaborationId_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210621085841_ADD_HelperCollaborationId_Migration', N'3.1.4');
END;

GO

