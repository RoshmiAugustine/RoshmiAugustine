IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200826130504_UpdateInConfigTable_BaseURLKey')
BEGIN

    IF EXISTS(SELECT * FROM [info].[ConfigurationParameter] WHERE Name = 'EmailAssessmentURL_Development')
    BEGIN
      UPDATE [info].[ConfigurationParameter] set Name = 'EmailAssessmentURL_Dev' WHERE Name = 'EmailAssessmentURL_Development'
    END

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200826130504_UpdateInConfigTable_BaseURLKey', N'3.1.4');
END;

GO

