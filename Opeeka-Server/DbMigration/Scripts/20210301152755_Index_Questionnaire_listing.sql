IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210301152755_Index_Questionnaire_listing')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210301152755_Index_Questionnaire_listing', N'3.1.4');
END;

GO

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_CollaborationQuestionnaire_QuestionnaireID_Removed' AND object_id = OBJECT_ID('dbo.CollaborationQuestionnaire'))>0
BEGIN
	DROP INDEX [IX_CollaborationQuestionnaire_QuestionnaireID_Removed] ON [dbo].[CollaborationQuestionnaire]
END
CREATE NONCLUSTERED INDEX [IX_CollaborationQuestionnaire_QuestionnaireID_Removed]
ON [dbo].[CollaborationQuestionnaire] ([IsRemoved])
INCLUDE ([QuestionnaireID])

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_PersonQuestionnaire_QuestionnaireID_Removed' AND object_id = OBJECT_ID('dbo.PersonQuestionnaire'))>0
BEGIN
	DROP INDEX [IX_PersonQuestionnaire_QuestionnaireID_Removed] ON [dbo].[PersonQuestionnaire]
END
CREATE NONCLUSTERED INDEX [IX_PersonQuestionnaire_QuestionnaireID_Removed]
ON [dbo].[PersonQuestionnaire] ([IsRemoved])
INCLUDE ([QuestionnaireID])

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123150039_INDEX_ADD_PersonListing')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210301152755_Index_Questionnaire_listing', N'3.1.4');
END;

GO

