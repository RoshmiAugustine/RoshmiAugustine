IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308144053_Index_Getting_Latest_response')
BEGIN

IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='[IX_AssessmentResponse_QuestionnaireItemID_AssessmentID]' AND object_id = OBJECT_ID('dbo.[AssessmentResponse]'))>0
BEGIN
	DROP INDEX [IX_AssessmentResponse_QuestionnaireItemID_AssessmentID] ON [dbo].[AssessmentResponse]
END
CREATE NONCLUSTERED INDEX [IX_AssessmentResponse_QuestionnaireItemID_AssessmentID]
ON [dbo].[AssessmentResponse] ([AssessmentID])
INCLUDE ([QuestionnaireItemID])


    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210308144053_Index_Getting_Latest_response', N'3.1.4');
END;

GO

