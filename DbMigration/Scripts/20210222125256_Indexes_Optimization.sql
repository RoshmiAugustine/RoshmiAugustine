IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210222125256_Indexes_Optimization')
BEGIN

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonQuestionnaireMetrics_NeedsEver' AND object_id = OBJECT_ID('dbo.PersonQuestionnaireMetrics'))>0
	BEGIN
		DROP INDEX [IX_PersonQuestionnaireMetrics_NeedsEver] ON [dbo].[PersonQuestionnaireMetrics]
	END
	CREATE NONCLUSTERED INDEX IX_PersonQuestionnaireMetrics_NeedsEver ON [dbo].[PersonQuestionnaireMetrics] ([NeedsEver]) INCLUDE ([PersonID],[InstrumentID],[ItemID],[NeedsAddressed],[NeedsAddressing]) WITH (ONLINE = ON)

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonQuestionnaireMetrics_StrengthsEver' AND object_id = OBJECT_ID('dbo.PersonQuestionnaireMetrics'))>0
	BEGIN
		DROP INDEX [IX_PersonQuestionnaireMetrics_StrengthsEver] ON [dbo].[PersonQuestionnaireMetrics]
	END
	CREATE NONCLUSTERED INDEX IX_PersonQuestionnaireMetrics_StrengthsEver ON [dbo].[PersonQuestionnaireMetrics] ([StrengthsEver]) INCLUDE ([PersonID],[InstrumentID],[ItemID],[StrengthsBuilt],[StrengthsBuilding]) WITH (ONLINE = ON)

	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_NotificationLog_FKeyValue_NotificationTypeID' AND object_id = OBJECT_ID('dbo.NotificationLog'))>0
	BEGIN
		DROP INDEX [IX_NotificationLog_FKeyValue_NotificationTypeID] ON [dbo].[NotificationLog]
	END
	CREATE NONCLUSTERED INDEX IX_NotificationLog_FKeyValue_NotificationTypeID ON [dbo].[NotificationLog] ([FKeyValue], [IsRemoved], [NotificationTypeID]) INCLUDE ([NotificationDate]) WITH (ONLINE = ON)

END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210222125256_Indexes_Optimization', N'3.1.4');
END;

GO

