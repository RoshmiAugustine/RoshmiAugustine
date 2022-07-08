IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210610064610_ItemRowVersion_Migration')
BEGIN

IF EXISTS (SELECT *  FROM sys.columns WHERE Name IN (N'RowVersion')  AND Object_ID = Object_ID(N'dbo.Item'))
BEGIN
   ALTER TABLE [Item] DROP COLUMN [RowVersion];
END
ALTER TABLE [Item] ADD [RowVersion] rowversion NULL;
END;

GO
BEGIN
	  DECLARE @SYS_ROLE_PERM_ID int;
	  DECLARE @AG_RW int;
	  DECLARE @COLL_RW int;
	  DECLARE @APPOBJECTID int=0;

	  SET @APPOBJECTID  = (select ApplicationObjectID  from ApplicationObject where NAme = '/api/helpers/SetSuperAdminDefaultAgency');
	  SET @AG_RW  = (select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write');
	  SET @COLL_RW =(select AgencySharingPolicyID from AgencySharingPolicy where Description = 'Both Read and Write');
	  IF(@APPOBJECTID <> 0)
	  BEGIN
	   SET @SYS_ROLE_PERM_ID = (select TOP 1 SystemRolePermissionID from info.SystemRolePermission WHERE PermissionID = (
									SELECT PermissionID FROM info.Permission where ApplicationObjectID = @APPOBJECTID) AND SystemRoleID = 1)
	   INSERT INTO info.SharingRolePermission values( @SYS_ROLE_PERM_ID, @AG_RW, @COLL_RW, 1) 
       END
END
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210610064610_ItemRowVersion_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210610064610_ItemRowVersion_Migration', N'3.1.4');
END;

GO

