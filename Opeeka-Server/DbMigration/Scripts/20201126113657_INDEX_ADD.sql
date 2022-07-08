IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201126113657_INDEX_ADD')
BEGIN
	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_TimeFrame_DaysInService' AND object_id = OBJECT_ID('info.TimeFrame'))>0
	BEGIN
		DROP INDEX [IX_TimeFrame_DaysInService] ON [info].[TimeFrame]
	END
	CREATE NONCLUSTERED INDEX IX_TimeFrame_DaysInService ON [info].[TimeFrame] ([DaysInService]) INCLUDE ([TimeFrame_Std]) WITH (ONLINE = ON)
	
	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_Person_AgencyID_1' AND object_id = OBJECT_ID('dbo.Person'))>0
	BEGIN
		DROP INDEX [IX_Person_AgencyID_1] ON [dbo].[Person]
	END
	CREATE NONCLUSTERED INDEX IX_Person_AgencyID_1 ON [dbo].[Person] ([AgencyID]) INCLUDE ([PersonIndex], [FirstName], [MiddleName], [LastName], [IsActive], [StartDate], [EndDate], [IsRemoved]) WITH (ONLINE = ON)
	
	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonQuestionnaire_IsActive_IsRemoved' AND object_id = OBJECT_ID('dbo.PersonQuestionnaire'))>0
	BEGIN
		DROP INDEX [IX_PersonQuestionnaire_IsActive_IsRemoved] ON [dbo].[PersonQuestionnaire]
	END
	CREATE NONCLUSTERED INDEX IX_PersonQuestionnaire_IsActive_IsRemoved ON [dbo].[PersonQuestionnaire] ([IsActive], [IsRemoved]) INCLUDE ([personId]) WITH (ONLINE = ON)
	
	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_Person_AgencyID_IsRemoved' AND object_id = OBJECT_ID('dbo.Person'))>0
	BEGIN
		DROP INDEX [IX_Person_AgencyID_IsRemoved] ON [dbo].[Person]
	END
	CREATE NONCLUSTERED INDEX IX_Person_AgencyID_IsRemoved ON [dbo].[Person] ([IsRemoved],[AgencyID]) INCLUDE ([PersonIndex], [FirstName], [MiddleName], [LastName], [IsActive], [StartDate], [EndDate]) WITH (ONLINE = ON)
END

BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201126113657_INDEX_ADD', N'3.1.4');
END;

GO

