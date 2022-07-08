IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201008210308_ReportsAPI_NameChange')
BEGIN
    UPDATE [dbo].ApplicationObject SET NAME = '/api/latest-submitted-assessment' 
    WHERE NAME like '/api/latest-submitted-assessment/{personIndex}/{questionnaireID}/{voiceTypeID}';

    UPDATE [dbo].ApplicationObject SET NAME = '/api/assessments-in-collaboration/{personQuestionnaireID}/{personCollaborationID}/{voiceTypeID}/{voiceTypeFKID}' 
    WHERE NAME like '/api/assessments-in-collaboration/{personQuestionnaireID}/{personCollaborationID}/{voiceTypeID}';

    UPDATE [dbo].ApplicationObject SET NAME = '/api/assessed-questionnaires/{personIndex}/{personCollaborationID}/{voicetypeID}/{voiceTypeFKID}/{pageNumber}/{pageSize}' 
    WHERE NAME like '/api/assessed-questionnaires/{personIndex}/{personCollaborationID}/{voicetypeID}/{pageNumber}/{pageSize}';

    UPDATE info.Permission SET OperationTypeID = 2 WHERE ApplicationObjectID in (select ApplicationObjectID from ApplicationObject 
    where name = '/api/latest-submitted-assessment')

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201008210308_ReportsAPI_NameChange', N'3.1.4');
END;

GO

