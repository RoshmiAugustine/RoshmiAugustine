IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210108080854_HelperList_INDEX_ADD')
BEGIN
	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_Assessment_IsRemoved' AND object_id = OBJECT_ID('dbo.Assessment'))>0
	BEGIN
		DROP INDEX [IX_Assessment_IsRemoved] ON [dbo].[Assessment]
	END
	CREATE NONCLUSTERED INDEX IX_Assessment_IsRemoved ON [dbo].[Assessment] ([IsRemoved]) INCLUDE ([personQuestionnaireID],[AssessmentStatusID]) WITH (ONLINE = ON)


	IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonCollaboration_IsPrimary_IsRemoved' AND object_id = OBJECT_ID('dbo.PersonCollaboration'))>0
	BEGIN
		DROP INDEX [IX_PersonCollaboration_IsPrimary_IsRemoved] ON [dbo].[PersonCollaboration]
	END
	CREATE NONCLUSTERED INDEX IX_PersonCollaboration_IsPrimary_IsRemoved ON [dbo].[PersonCollaboration] ([IsPrimary],[IsCurrent],[IsRemoved]) INCLUDE ([personID],[EnrollDate],[EndDate]) WITH (ONLINE = ON)

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210108080854_HelperList_INDEX_ADD', N'3.1.4');
END;

GO

