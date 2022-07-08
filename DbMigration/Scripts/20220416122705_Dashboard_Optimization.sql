IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220416122705_Dashboard_Optimization')
BEGIN
IF NOT EXISTS(SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotifyReminder_IsLogAdded' AND object_id = OBJECT_ID('dbo.NotifyReminder'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_NotifyReminder_IsLogAdded ON [dbo].[NotifyReminder] ([IsLogAdded]) INCLUDE ([PersonQuestionnaireScheduleID], [QuestionnaireReminderRuleID]) WITH (ONLINE = ON)
END


IF NOT EXISTS(SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonQuestionnaireMetrics_NeedsEver' AND object_id = OBJECT_ID('dbo.PersonQuestionnaireMetrics'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_PersonQuestionnaireMetrics_NeedsEver ON [dbo].[PersonQuestionnaireMetrics] ([NeedsEver]) INCLUDE ([PersonID],[InstrumentID],[ItemID],[NeedsAddressed],[NeedsAddressing]) WITH (ONLINE = ON)
END

IF NOT EXISTS(SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonQuestionnaireMetrics_StrengthsEver' AND object_id = OBJECT_ID('dbo.PersonQuestionnaireMetrics'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_PersonQuestionnaireMetrics_StrengthsEver ON [dbo].[PersonQuestionnaireMetrics] ([StrengthsEver]) INCLUDE ([PersonID],[InstrumentID],[ItemID],[StrengthsBuilt],[StrengthsBuilding]) WITH (ONLINE = ON)
END

IF NOT EXISTS(SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotificationLog_FKeyValue_NotificationTypeID' AND object_id = OBJECT_ID('dbo.NotificationLog'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_NotificationLog_FKeyValue_NotificationTypeID ON [dbo].[NotificationLog] ([FKeyValue], [IsRemoved], [NotificationTypeID]) INCLUDE ([NotificationDate]) WITH (ONLINE = ON)
END

IF NOT EXISTS(SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotificationLog_FKeyValue_NotificationResolutionStatusID_IsRemoved' AND object_id = OBJECT_ID('dbo.NotificationLog'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_NotificationLog_FKeyValue_NotificationResolutionStatusID_IsRemoved ON [dbo].[NotificationLog] ([FKeyValue],[NotificationResolutionStatusID],[IsRemoved]) WITH (ONLINE = ON)
END

IF NOT EXISTS(SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_CollaborationQuestionnaire_QuestionnaireID_Removed' AND object_id = OBJECT_ID('dbo.CollaborationQuestionnaire'))
BEGIN
	CREATE NONCLUSTERED INDEX [IX_CollaborationQuestionnaire_QuestionnaireID_Removed] ON [dbo].[CollaborationQuestionnaire] ([IsRemoved]) INCLUDE ([QuestionnaireID])
END

IF NOT EXISTS (SELECT name FROM sys.indexes  WHERE name = N'IX_PersonHelper_Date')  
BEGIN
	CREATE NONCLUSTERED INDEX [IX_PersonHelper_Date] ON [dbo].[PersonHelper] ([IsRemoved],[StartDate]) INCLUDE ([PersonID],[HelperID],[EndDate])
END

END


BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220416122705_Dashboard_Optimization', N'3.1.4');
END;

GO

