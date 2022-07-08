IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005143823_GetLastAssessmentAPIChange')
BEGIN
    UPDATE [dbo].ApplicationObject SET NAME = '/api/latest-submitted-assessment/{personIndex}/{questionnaireID}/{voiceTypeID}' 
    WHERE NAME like '/api/latest-submitted-assessment/{personIndex}/{questionnaireID}';

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201005143823_GetLastAssessmentAPIChange', N'3.1.4');
END;

GO

