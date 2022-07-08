IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201118053549_PersonCollaborationsAPIParamChange')
BEGIN
    UPDATE [dbo].ApplicationObject SET NAME = 'api/person-collaborations/{personIndex}/{questionnaireID}' 
    WHERE NAME like '/api/person-collaborations/{personIndex}';
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201118053549_PersonCollaborationsAPIParamChange', N'3.1.4');
END;

GO

