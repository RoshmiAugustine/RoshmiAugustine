IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200824124240_AddEmailAssessmentStatus')
BEGIN
    IF NOT EXISTS(SELECT * FROM [info].[AssessmentStatus] WHERE Name = 'Email Sent')
    BEGIN
    DECLARE @TotalCount INT = 0;
    SET @TotalCount = (SELECT COUNT(*) FROM [info].[AssessmentStatus])
    INSERT INTO [info].[AssessmentStatus]
           ([Name],[ListOrder],[IsRemoved],[UpdateDate],[UpdateUserID])
     VALUES('Email Sent',@TotalCount+1,0,getdate(),1);
    END

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200824124240_AddEmailAssessmentStatus')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200824124240_AddEmailAssessmentStatus', N'3.1.4');
END;

GO

