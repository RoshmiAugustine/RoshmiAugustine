IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201125120210_person_questionnaires_APIChange')
BEGIN
   IF EXISTS(SELECT * FROM ApplicationObject where Name like '/api/person-questionnaires/{personIndex}/{pageNumber}/{pageSize}')
   BEGIN
     UPDATE ApplicationObject SET  Name = '/api/person-questionnaires/{personIndex}/{questionnaireID}/{pageNumber}/{pageSize}'  
     WHERE NAME like '/api/person-questionnaires/{personIndex}/{pageNumber}/{pageSize}';
   END
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201125120210_person_questionnaires_APIChange', N'3.1.4');
END;

GO

