IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210407130958_AddResendSignupPermissionSupervisor')
BEGIN
 --Declaration of variables starts
 DECLARE @appObjId int;
 DECLARE @Supervisor int;
 DECLARE @PermissionID int;
 DECLARE @PUT int = 3;
  --Declaration of variables ends
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  SET @Supervisor = (SELECT SystemRoleID FROM info.SystemRole WHERE [Name] ='Supervisor');
  SET @appObjId = (SELECT ApplicationObjectID FROM ApplicationObject WHERE Name='/api/helpers/resendhelperinvitation/{helperIndex}');
  SET @PermissionID=(SELECT PermissionID FROM info.Permission WHERE ApplicationObjectID=@appObjId AND OperationTypeID = @PUT
									  AND  Description='resend signup invitation' AND IsRemoved=0 AND ListOrder=1); 

  INSERT INTO info.SystemRolePermission VALUES(@Supervisor,@PermissionID);
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210407130958_AddResendSignupPermissionSupervisor', N'3.1.4');
END;

GO

