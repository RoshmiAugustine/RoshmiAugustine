IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909061052_IdentifiedGender')
BEGIN
    CREATE TABLE [info].[IdentifiedGender] (
        [IdentifiedGenderID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_IdentifiedGender] PRIMARY KEY ([IdentifiedGenderID]),
        CONSTRAINT [FK_IdentifiedGender_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_IdentifiedGender_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909061052_IdentifiedGender')
BEGIN
    CREATE INDEX [IX_IdentifiedGender_AgencyID] ON [info].[IdentifiedGender] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909061052_IdentifiedGender')
BEGIN
    CREATE INDEX [IX_IdentifiedGender_UpdateUserID] ON [info].[IdentifiedGender] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909061052_IdentifiedGender')
BEGIN
	DECLARE @name VARCHAR(100)='/api/identified-gender'
	DECLARE @desc VARCHAR(100)='Get Identified gender lookup values'
	DECLARE @aoID INT
	DECLARE @permID INT
	DECLARE @opID INT=1
	DECLARE @roles TABLE (RoleID INT)
	INSERT INTO @roles 
	SELECT 1 UNION
	SELECT 2 UNION
	SELECT 3 UNION
	SELECT 4 UNION
	SELECT 5 UNION
	SELECT 6 

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

END

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909061052_IdentifiedGender')
BEGIN
	SET @name ='/api/identified-gender'
	SET @desc ='Add new Identified gender'
	SET @opID=2
	DELETE FROM @roles 
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

END

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909061052_IdentifiedGender')
BEGIN
	SET @name='/api/identified-gender'
	SET @desc ='Update existing Identified gender'
	SET @opID =3
	DELETE FROM @roles 
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

END

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909061052_IdentifiedGender')
BEGIN
	SET @name ='/api/identified-gender/{pageNumber}/{pageSize}'
	SET @desc ='Get list of Identified genders'
	SET @opID =1
	DELETE FROM @roles 
	INSERT INTO @roles 
	SELECT 1 UNION
	SELECT 2 UNION
	SELECT 3 

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

END

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200909061052_IdentifiedGender')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200909061052_IdentifiedGender', N'3.1.4');
END;

GO

