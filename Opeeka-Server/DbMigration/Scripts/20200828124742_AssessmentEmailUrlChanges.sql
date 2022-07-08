IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200828124742_AssessmentEmailUrlChanges')
BEGIN 
    IF EXISTS(select * from info.Configuration C where Value like '%https:%pcis%/email-asssessment')
    BEGIN
    UPDATE  info.Configuration SET Value = REPLACE(value,'email-asssessment','email-assessment')
       WHERE Value LIKE '%https:%pcis%/email-asssessment'
    END

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200828124742_AssessmentEmailUrlChanges', N'3.1.4');
END;

GO

