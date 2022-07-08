IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201119080801_UI_PERM_Admin_ReportingUnit')
BEGIN

		if not exists(select * from ApplicationObject WHERE Name = 'Settings/Admin/ReportingUnitButton')
	BEGIN
		INSERT INTO ApplicationObject VALUES(1,'Settings/Admin/ReportingUnitButton', 'Reporting Unit', 134,0, GETDATE(), 1)
	END
	if not exists(select * from info.Permission where Description = 'Add Reporting Unit' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Add'))
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'Settings/Admin/ReportingUnitButton'),(select OperationTypeID from info.OperationType where Name = 'Add'), 'Add Reporting Unit', 134, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Add Reporting Unit' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Add')))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Add Reporting Unit' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Add')
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Add Reporting Unit' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Add')

	end
	if not exists(select * from info.Permission where Description = 'Edit Reporting Unit')
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'Settings/Admin/ReportingUnitButton'),(select OperationTypeID from info.OperationType where Name = 'Edit'), 'Edit Reporting Unit', 134, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Edit Reporting Unit'))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Edit Reporting Unit'
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Edit Reporting Unit'

	end

	
	if not exists(select * from ApplicationObject WHERE Name = 'Settings/Admin/PartnerAgency')
	BEGIN
		INSERT INTO ApplicationObject VALUES(1,'Settings/Admin/PartnerAgency', 'Partner Agency', 134,0, GETDATE(), 1)
	END
	if not exists(select * from info.Permission where Description = 'Add Partner Agency Button' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Add'))
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'Settings/Admin/PartnerAgency'),(select OperationTypeID from info.OperationType where Name = 'Add'), 'Add Partner Agency Button', 134, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Add Partner Agency Button' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Add')))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Add Partner Agency Button' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Add')
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Add Partner Agency Button' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Add')

	end
	if not exists(select * from info.Permission where Description = 'Edit Partner Agency' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Edit'))
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'Settings/Admin/PartnerAgency'),(select OperationTypeID from info.OperationType where Name = 'Edit'), 'Edit Partner Agency', 134, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Edit Partner Agency'  and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Edit')))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Edit Partner Agency' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Edit')
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Edit Partner Agency' and OperationTypeID = (select OperationTypeID from info.OperationType where Name = 'Edit')

	end

	if not exists(select * from ApplicationObject WHERE Name = 'Settings/Admin/RUCollaboration')
	BEGIN
		INSERT INTO ApplicationObject VALUES(1,'Settings/Admin/RUCollaboration', 'RU Collaboration', 134,0, GETDATE(), 1)
	END
	if not exists(select * from info.Permission where Description = 'Add RU Collaboration')
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'Settings/Admin/RUCollaboration'),(select OperationTypeID from info.OperationType where Name = 'Add'), 'Add RU Collaboration', 134, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Add RU Collaboration'))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Add RU Collaboration'
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Add RU Collaboration'

	end
	if not exists(select * from info.Permission where Description = 'Edit RU Collaboration')
	begin
		insert into info.Permission values((select ApplicationObjectID from ApplicationObject where Name= 'Settings/Admin/RUCollaboration'),(select OperationTypeID from info.OperationType where Name = 'Edit'), 'Edit RU Collaboration', 134, getdate(), 1,0)
	end
	if not exists(select * from info.SystemrolePermission where PermissionID = (select PermissionID from info.Permission where Description = 'Edit RU Collaboration'))
	begin
		insert into info.SystemrolePermission select 1, PermissionID from info.Permission where Description = 'Edit RU Collaboration'
		insert into info.SystemrolePermission select 2, PermissionID from info.Permission where Description = 'Edit RU Collaboration'

	end
	insert into info.RolePermission select ur.UserRoleID  ,  srp.PermissionID from info.SystemRolePermission srp  JOIN UserRole ur ON srp.SystemRoleID = ur.SystemRoleID WHERE srp.PermissionID not in (select PermissionId from info.RolePermission where UserRoleId = ur.UserRoleID)


    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201119080801_UI_PERM_Admin_ReportingUnit', N'3.1.4');
END;

GO

