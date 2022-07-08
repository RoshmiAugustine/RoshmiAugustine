IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210610074902_RowVersionColumn_ADD_Migration')
BEGIN
    IF EXISTS (SELECT *  FROM sys.columns WHERE Name IN (N'RowVersion')  AND Object_ID = Object_ID(N'info.Instrument'))
    BEGIN
       ALTER TABLE [info].[Instrument] DROP COLUMN [RowVersion];
    END
    
    IF EXISTS (SELECT *  FROM sys.columns WHERE Name IN (N'RowVersion')  AND Object_ID = Object_ID(N'info.Category'))
    BEGIN
       ALTER TABLE [info].[Category] DROP COLUMN [RowVersion];
    END
    
    IF EXISTS (SELECT *  FROM sys.columns WHERE Name IN (N'RowVersion')  AND Object_ID = Object_ID(N'dbo.Response'))
    BEGIN
       ALTER TABLE [Response] DROP COLUMN [RowVersion];
    END
    
    IF EXISTS (SELECT *  FROM sys.columns WHERE Name IN (N'RowVersion')  AND Object_ID = Object_ID(N'dbo.QuestionnaireItem'))
    BEGIN
       ALTER TABLE [QuestionnaireItem] DROP COLUMN [RowVersion];
    END
    
    IF EXISTS (SELECT *  FROM sys.columns WHERE Name IN (N'RowVersion')  AND Object_ID = Object_ID(N'dbo.Questionnaire'))
    BEGIN
       ALTER TABLE [Questionnaire] DROP COLUMN [RowVersion];
    END


    ALTER TABLE [info].[Instrument] ADD [RowVersion] rowversion NULL;
    
    ALTER TABLE [info].[Category] ADD [RowVersion] rowversion NULL;
    
    ALTER TABLE [Response] ADD [RowVersion] rowversion NULL;
    
    ALTER TABLE [QuestionnaireItem] ADD [RowVersion] rowversion NULL;
    
    ALTER TABLE [Questionnaire] ADD [RowVersion] rowversion NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210610074902_RowVersionColumn_ADD_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210610074902_RowVersionColumn_ADD_Migration', N'3.1.4');
END;

GO

