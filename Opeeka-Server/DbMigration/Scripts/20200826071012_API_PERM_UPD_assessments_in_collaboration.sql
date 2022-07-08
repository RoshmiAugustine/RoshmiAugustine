UPDATE ApplicationObject 
SET Name='/api/assessments-in-collaboration/{personQuestionnaireID}/{personCollaborationID}/{voiceTypeID}' 
WHERE Name='/api/assessments-in-collaboration/{personQuestionnaireID}/{collaborationID}'

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826071012_API_PERM_UPD_assessments_in_collaboration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200826071012_API_PERM_UPD_assessments_in_collaboration', N'3.1.4');
END;

GO

