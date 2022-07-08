DECLARE @name VARCHAR(100)='/api/identified-gender/{identifiedGenderID}'
DECLARE @desc VARCHAR(100)='Delete an Identifier Gender'
DECLARE @aoID INT
DECLARE @permID INT
DECLARE @opID INT=4
DECLARE @roles TABLE (RoleID INT)
INSERT INTO @roles 
SELECT 1 UNION
SELECT 2 

INSERT INTO [dbo].[ApplicationObject]
           ([ApplicationObjectTypeID]
           ,[Name]
           ,[Description]
           ,[ListOrder]
           ,[IsRemoved]
           ,[UpdateDate]
           ,[UpdateUserID])
VALUES
    (3,@name,@desc,3,0,GETDATE(),1)

SELECT @aoID=[ApplicationObjectID] FROM ApplicationObject WHERE [Name]=@name

PRINT CAST(@aoID AS VARCHAR(10))

INSERT INTO [info].[Permission]
           ([ApplicationObjectID]
           ,[OperationTypeID]
           ,[Description]
           ,[ListOrder]
           ,[UpdateDate]
           ,[UpdateUserID]
           ,[IsRemoved])
VALUES
    (@aoID,@opID,@desc,1,GETDATE(),1,0)

SELECT @permID=PermissionID FROM [info].[Permission] WHERE [ApplicationObjectID]=@aoID

PRINT CAST(@permID AS VARCHAR(10))

Insert into info.SystemRolePermission(SystemRoleID,PermissionID)
Select RoleID, @permID FROM @roles



IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200915164110_API_PERM_DeleteIdentifiedGender')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200915164110_API_PERM_DeleteIdentifiedGender', N'3.1.4');
END;

GO

