

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308055230_AuditHelper_migration')
BEGIN
		
		if not exists(select * from AuditTableName WHERE TableName = 'Helper')
			BEGIN
				INSERT INTO AuditTableName VALUES('Helper','Helper');
			END
	
	IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.schemas s ON t.schema_id = s.schema_id 
	INNER JOIN sys.indexes i on i.object_id = t.object_id
    WHERE i.is_primary_key = 1 AND s.name = 'dbo' AND t.name = 'AuditFieldName')
	BEGIN
		ALTER TABLE  [dbo].[AuditFieldName] DROP CONSTRAINT PK_AuditFieldName; 
	END


			BEGIN
			if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='FirstName')
			BEGIN
				INSERT INTO AuditFieldName VALUES('FirstName','Helper','FirstName');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='MiddleName')
			BEGIN
				INSERT INTO AuditFieldName VALUES('MiddleName','Helper','MiddleName');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='LastName')
			BEGIN
				INSERT INTO AuditFieldName VALUES('LastName','Helper','LastName');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='Email')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Email','Helper','Email');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='Phone')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Phone','Helper','Phone');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='IsRemoved')
			BEGIN
				INSERT INTO AuditFieldName VALUES('IsRemoved','Helper','IsRemoved');
		END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='UpdateDate')
			BEGIN
				INSERT INTO AuditFieldName VALUES('UpdateDate','Helper','UpdateDate');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper'and FieldName='FirstName')
			BEGIN
				INSERT INTO AuditFieldName VALUES('UpdateUserID','Helper','UpdateUserID');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='AgencyID')
			BEGIN
				INSERT INTO AuditFieldName VALUES('AgencyID','Helper','AgencyID');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='HelperTitleID')
			BEGIN
				INSERT INTO AuditFieldName VALUES('HelperTitleID','Helper','HelperTitleID');
		END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='Phone2')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Phone2','Helper','Phone2');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='SupervisorHelperID')
			BEGIN
				INSERT INTO AuditFieldName VALUES('SupervisorHelperID','Helper','SupervisorHelperID');
			END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='ReviewerID')
			BEGIN
				INSERT INTO AuditFieldName VALUES('ReviewerID','Helper','ReviewerID');
	END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='StartDate')
			BEGIN
				INSERT INTO AuditFieldName VALUES('StartDate','Helper','StartDate');
		END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='EndDate')
			BEGIN
				INSERT INTO AuditFieldName VALUES('EndDate','Helper','EndDate');
		END
				if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='HelperExternalID')
			BEGIN
				INSERT INTO AuditFieldName VALUES('HelperExternalID','Helper','HelperExternalID');
				END

					if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='HelperIndex')
			BEGIN
				INSERT INTO AuditFieldName VALUES('HelperIndex','Helper','HelperIndex');
				END
					if not exists(select * from AuditFieldName WHERE TableName = 'Helper' and FieldName='UserID')
			BEGIN
				INSERT INTO AuditFieldName VALUES('UserID','Helper','UserID');
				END
			END	
END;

GO

BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210308055230_AuditHelper_migration', N'3.1.4');
END;

GO

