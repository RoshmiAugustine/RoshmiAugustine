IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120180636_RegularReminderRecurrenceLookups')
BEGIN
    
 IF NOT EXISTS(SELECT TOP 1* FROM info.RecurrenceEndType) 
 BEGIN
    INSERT INTO info.RecurrenceEndType
    SELECT 'EndByEndDate', 'End by', 1, 0, getdate()
    INSERT INTO info.RecurrenceEndType
    SELECT 'EndByNmbrOfOccurence', 'End after', 2, 0, getdate()
    INSERT INTO info.RecurrenceEndType
    SELECT 'EndByNoEndDate', 'No end date', 3, 0, getdate()  
 END
    
 IF NOT EXISTS(SELECT TOP 1* FROM info.RecurrencePattern) 
 BEGIN       
    INSERT INTO info.RecurrencePattern
    SELECT 'Daily', 'DailyDays', 1, 0, getdate()
    INSERT INTO info.RecurrencePattern
    SELECT 'Daily', 'DailyWeekdays', 2, 0, getdate()
    INSERT INTO info.RecurrencePattern
    SELECT 'Weekly', 'Weekly', 3, 0, getdate()
    INSERT INTO info.RecurrencePattern
    SELECT 'Monthly', 'MonthlyByDay', 4, 0, getdate()
    INSERT INTO info.RecurrencePattern
    SELECT 'Monthly', 'MonthlyByOrdinalDay', 5, 0, getdate()
    INSERT INTO info.RecurrencePattern
    SELECT 'Yearly', 'YearlyByMonth', 6, 0, getdate()
    INSERT INTO info.RecurrencePattern
    SELECT 'Yearly', 'YearlyByOrdinal', 7, 0, getdate()
 END
    
 IF NOT EXISTS(SELECT TOP 1* FROM info.RecurrenceOrdinal) 
 BEGIN

    INSERT INTO info.RecurrenceOrdinal
    SELECT 'First', 1, 0, getdate()
    INSERT INTO info.RecurrenceOrdinal
    SELECT 'Second', 2, 0, getdate()
    INSERT INTO info.RecurrenceOrdinal
    SELECT 'Third', 3, 0, getdate()
    INSERT INTO info.RecurrenceOrdinal
    SELECT 'Fourth', 4, 0, getdate()
    INSERT INTO info.RecurrenceOrdinal
    SELECT 'Last', 5, 0, getdate()
 END
    
 IF NOT EXISTS(SELECT TOP 1* FROM info.RecurrenceDay) 
 BEGIN
    INSERT INTO info.RecurrenceDay
    SELECT 'Day', 0, 1, 0,getdate()
    INSERT INTO info.RecurrenceDay
    SELECT 'WeekDay', 0, 1, 0,getdate()
    INSERT INTO info.RecurrenceDay
    SELECT 'Sunday', 0, 1, 0,getdate()
    INSERT INTO info.RecurrenceDay
    SELECT 'Monday', 1, 2, 0,getdate()
    INSERT INTO info.RecurrenceDay
    SELECT 'Tuesday', 1, 3, 0,getdate()
    INSERT INTO info.RecurrenceDay
    SELECT 'Wednesday', 1, 4, 0,getdate()
    INSERT INTO info.RecurrenceDay
    SELECT 'Thursday', 1, 5, 0,getdate()
    INSERT INTO info.RecurrenceDay
    SELECT 'Friday', 1, 6, 0,getdate()
    INSERT INTO info.RecurrenceDay
    SELECT 'Saturday', 0, 7, 0,getdate()
 END
    
 IF NOT EXISTS(SELECT TOP 1* FROM info.RecurrenceMonth) 
 BEGIN
    INSERT INTO info.RecurrenceMonth
    SELECT 'January', 0, 1, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'February', 0, 2, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'March', 0, 3, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'April', 0, 4, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'May', 0, 5, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'June', 0, 6, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'July', 0, 7, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'August', 0, 8, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'September', 0, 9, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'October', 0, 10, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'November', 0, 11, 0, GETDATE()
    INSERT INTO info.RecurrenceMonth
    SELECT 'December', 0, 12, 0, GETDATE()
 END
    
 IF NOT EXISTS(SELECT TOP 1* FROM info.InviteToCompleteReceiver) 
 BEGIN  
    INSERT INTO info.InviteToCompleteReceiver
    SELECT 'Person in Care', 1, 0, getdate()
    INSERT INTO info.InviteToCompleteReceiver
    SELECT 'Lead Helper', 2, 0, getdate()
    INSERT INTO info.InviteToCompleteReceiver
    SELECT 'All Helpers', 3, 0, getdate()
    INSERT INTO info.InviteToCompleteReceiver
    SELECT 'Supports', 4, 0, getdate()
 END
    
 IF NOT EXISTS(SELECT TOP 1* FROM info.OffsetType) 
 BEGIN 
    INSERT INTO info.OffsetType
    SELECT 'd','Day', 1, 0, getdate()
    INSERT INTO info.OffsetType
    SELECT 'h','Hour', 2, 0, getdate()
 END
 IF NOT EXISTS(SELECT TOP 1* FROM info.TimeZones) 
 BEGIN 
    INSERT INTO info.TimeZones
    SELECT 'Coordinated Universal Time', 'UTC', 1 , 0 , GETDATE()
 END

BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220120180636_RegularReminderRecurrenceLookups', N'3.1.4');
END;
END

GO

