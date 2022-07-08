IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208112740_Config_Url_Update')
BEGIN

Update info.configuration set value = 'people/details?notificationType={notificationtype}&assessmentID={assessmentid}&questionnaireID={questionnaireid}&comeFrom=Email&personIndex={personindex}' 
WHERE ConfigurationParameterContextID IN (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'PeopleQuestionnaireURL'))

END;
GO
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210208112740_Config_Url_Update', N'3.1.4');
END;

GO

