IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210330120234_PersonHelperIndex')
BEGIN
-----------------------------------------------------------------------------------------------------------------------------------
 IF NOT EXISTS (SELECT name FROM sys.indexes  
			   WHERE name = N'IX_PersonHelper_HelperID_IsLead_IsRemoved')  
CREATE NONCLUSTERED INDEX [IX_PersonHelper_HelperID_IsLead_IsRemoved]
ON [dbo].[PersonHelper] ([HelperID],[IsLead],[IsRemoved],[StartDate])
INCLUDE ([PersonID],[EndDate])
-----------------------------------------------------------------------------------------------------------------------------------
	INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
	VALUES (N'20210330120234_PersonHelperIndex', N'3.1.4');
END;

GO

