IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200823080814_dashboardindexscript')
BEGIN

    
    IF NOT EXISTS (SELECT name FROM sys.indexes  
          WHERE name = N'IX_PersonCollaboration_Details')  
BEGIN
  CREATE NONCLUSTERED INDEX [IX_PersonCollaboration_Details]
ON [dbo].[PersonCollaboration] ([IsPrimary],[IsRemoved])
INCLUDE ([PersonID],[CollaborationID],[EnrollDate],[EndDate])
END

IF NOT EXISTS (SELECT name FROM sys.indexes  
          WHERE name = N'IX_Person_Name_Details')  
BEGIN
  CREATE NONCLUSTERED INDEX [IX_Person_Name_Details]
ON [dbo].[Person] ([IsRemoved],[AgencyID])
INCLUDE ([PersonIndex],[FirstName],[MiddleName],[LastName],[StartDate],[EndDate])
END


IF NOT EXISTS (SELECT name FROM sys.indexes  
          WHERE name = N'IX_PersonHelper_Details')  
BEGIN
CREATE NONCLUSTERED INDEX [IX_PersonHelper_Details]
ON [dbo].[PersonHelper] ([IsLead],[IsRemoved],[StartDate])
INCLUDE ([PersonID],[HelperID],[EndDate])
END

IF NOT EXISTS (SELECT name FROM sys.indexes  
          WHERE name = N'IX_PersonHelper_Date')  
BEGIN
CREATE NONCLUSTERED INDEX [IX_PersonHelper_Date]
ON [dbo].[PersonHelper] ([IsRemoved],[StartDate])
INCLUDE ([PersonID],[HelperID],[EndDate])
END


    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200823080814_dashboardindexscript', N'3.1.4');
END;

GO

