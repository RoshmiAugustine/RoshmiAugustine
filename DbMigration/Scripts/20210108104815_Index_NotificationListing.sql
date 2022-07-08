IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210108104815_Index_NotificationListing')
BEGIN
    IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonQuestionnaireSchedule_IsRemoved' AND object_id = OBJECT_ID('dbo.PersonQuestionnaireSchedule'))>0
	BEGIN
		DROP INDEX [IX_PersonQuestionnaireSchedule_IsRemoved] ON [dbo].[PersonQuestionnaireSchedule]
	END
	CREATE NONCLUSTERED INDEX [IX_PersonQuestionnaireSchedule_IsRemoved] ON [dbo].[PersonQuestionnaireSchedule] ([IsRemoved]) INCLUDE ([QuestionnaireWindowID]) WITH (ONLINE = ON)
		
	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotifyReminder_IsLogAdded' AND object_id = OBJECT_ID('dbo.NotifyReminder'))>0
	BEGIN
		DROP INDEX [IX_NotifyReminder_IsLogAdded] ON [dbo].[NotifyReminder]
	END
	CREATE NONCLUSTERED INDEX IX_NotifyReminder_IsLogAdded ON [dbo].[NotifyReminder] ([IsLogAdded]) INCLUDE ([PersonQuestionnaireScheduleID], [QuestionnaireReminderRuleID]) WITH (ONLINE = ON)
	
	
	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotificationLog_NotificationResolutionStatusID_IsRemoved' AND object_id = OBJECT_ID('dbo.NotificationLog'))>0
	BEGIN
		DROP INDEX [IX_NotificationLog_NotificationResolutionStatusID_IsRemoved] ON [dbo].[NotificationLog]
	END
	CREATE NONCLUSTERED INDEX IX_NotificationLog_NotificationResolutionStatusID_IsRemoved ON [dbo].[NotificationLog] ([NotificationResolutionStatusID],[IsRemoved]) INCLUDE ([FKeyValue]) WITH (ONLINE = ON)
	
	
	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotificationLog_FKeyValue_NotificationResolutionStatusID_IsRemoved' AND object_id = OBJECT_ID('dbo.NotificationLog'))>0
	BEGIN
		DROP INDEX [IX_NotificationLog_FKeyValue_NotificationResolutionStatusID_IsRemoved] ON [dbo].[NotificationLog]
	END
	CREATE NONCLUSTERED INDEX IX_NotificationLog_FKeyValue_NotificationResolutionStatusID_IsRemoved ON [dbo].[NotificationLog] ([FKeyValue],[NotificationResolutionStatusID],[IsRemoved]) WITH (ONLINE = ON)
	
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210108104815_Index_NotificationListing', N'3.1.4');
END;

GO

