IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210303133510_IndexForReminderTriggering')
BEGIN

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotifyReminder_NotifyDate_IsLogAdded' AND object_id = OBJECT_ID('dbo.NotifyReminder'))>0
BEGIN
	DROP INDEX [IX_NotifyReminder_NotifyDate_IsLogAdded] ON [dbo].[NotifyReminder]
END
CREATE NONCLUSTERED INDEX [IX_NotifyReminder_NotifyDate_IsLogAdded]
ON [dbo].[NotifyReminder] ([NotifyDate],[IsLogAdded])
INCLUDE ([PersonQuestionnaireScheduleID])

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_CollaborationQuestionnaire_IsRemoved' AND object_id = OBJECT_ID('dbo.CollaborationQuestionnaire'))>0
BEGIN
	DROP INDEX [IX_CollaborationQuestionnaire_IsRemoved] ON [dbo].[CollaborationQuestionnaire]
END
CREATE NONCLUSTERED INDEX [IX_CollaborationQuestionnaire_IsRemoved]
ON [dbo].[CollaborationQuestionnaire] ([IsRemoved])
INCLUDE ([CollaborationID],[QuestionnaireID])

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_CollaborationQuestionnaire_CollaborationID_IsRemoved' AND object_id = OBJECT_ID('dbo.CollaborationQuestionnaire'))>0
BEGIN
	DROP INDEX [IX_CollaborationQuestionnaire_CollaborationID_IsRemoved] ON [dbo].[CollaborationQuestionnaire]
END
CREATE NONCLUSTERED INDEX [IX_CollaborationQuestionnaire_CollaborationID_IsRemoved]
ON [dbo].[CollaborationQuestionnaire] ([CollaborationID],[IsRemoved])
INCLUDE ([QuestionnaireID])

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210303133510_IndexForReminderTriggering', N'3.1.4');
END;

GO

