IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200821135243_DashboardIndex')
BEGIN
    IF NOT EXISTS (SELECT name FROM sys.indexes  
               WHERE name = N'IX_Person_AgencyID_Removed')  
       CREATE NONCLUSTERED INDEX IX_Person_AgencyID_Removed
    ON [dbo].[Person] ([IsRemoved],[AgencyID]);
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200821135243_DashboardIndex', N'3.1.4');
END;

GO

