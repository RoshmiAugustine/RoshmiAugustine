IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211007043545_ScheduleUpdateDateAndRemovedValueUpdations')
BEGIN 
   IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'UpdateDate'
                AND Object_ID = Object_ID(N'[dbo].[PersonQuestionnaireSchedule]'))
    BEGIN
        UPDATE [PersonQuestionnaireSchedule] set [UpdateDate] = [WindowDueDate];
    END

    IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'IsRemoved'
                AND Object_ID = Object_ID(N'[dbo].[NotifyReminder]'))
    BEGIN
        UPDATE NR set NR.IsRemoved = PQS.IsRemoved 
	    FROM NotifyReminder NR
	    JOIN  PersonQuestionnaireSchedule PQS ON NR.PersonQuestionnaireScheduleID = PQS.PersonQuestionnaireScheduleID
	    WHERE PQS.IsRemoved = 1;
    END
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211007043545_ScheduleUpdateDateAndRemovedValueUpdations', N'3.1.4');
END;

GO

