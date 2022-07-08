IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308064053_AuditAgency_migration')

BEGIN
		if not exists(select * from AuditTableName WHERE TableName = 'Agency')
			BEGIN
				INSERT INTO AuditTableName VALUES('Agency','Agency');
			END
			IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.schemas s ON t.schema_id = s.schema_id 
	INNER JOIN sys.indexes i on i.object_id = t.object_id
    WHERE i.is_primary_key = 1 AND s.name = 'dbo' AND t.name = 'AuditFieldName')
	BEGIN
		ALTER TABLE  [dbo].[AuditFieldName] DROP CONSTRAINT PK_AuditFieldName; 
	END

			BEGIN
				if not exists(select * from AuditFieldName WHERE FieldName = 'Name' and TableName='Agency')
					BEGIN
						INSERT INTO AuditFieldName VALUES('Name','Agency','Name');
					END
				if not exists(select * from AuditFieldName WHERE FieldName = 'AgencyIndex' and TableName='Agency')
					BEGIN
						INSERT INTO AuditFieldName VALUES('AgencyIndex','Agency','AgencyIndex');
					END
				if not exists(select * from AuditFieldName WHERE FieldName = 'UpdateUserID' and TableName='Agency')
					BEGIN
						INSERT INTO AuditFieldName VALUES('UpdateUserID','Agency','UpdateUserID');
					END
					if not exists(select * from AuditFieldName WHERE FieldName = 'IsCustomer' and TableName='Agency')
					BEGIN
				INSERT INTO AuditFieldName VALUES('IsCustomer','Agency','IsCustomer');
				END
			if not exists(select * from AuditFieldName WHERE FieldName = 'IsRemoved' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('IsRemoved','Agency','IsRemoved');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'UpdateDate' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('UpdateDate','Agency','UpdateDate');
				END
			if not exists(select * from AuditFieldName WHERE FieldName = 'Note' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Note','Agency','Note');
				END
			if not exists(select * from AuditFieldName WHERE FieldName = 'Abbrev' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Abbrev','Agency','Abbrev');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'Phone1' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Phone1','Agency','Phone1');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'Phone2' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Phone2','Agency','Phone2');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'Email' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Email','Agency','Email');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'ContactLastName' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('ContactLastName','Agency','ContactLastName');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'ContactFirstName' and TableName='Agency')
			BEGIN
				INSERT INTO AuditFieldName VALUES('ContactFirstName','Agency','ContactFirstName');
			END

			END

	END;

GO



BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210308064053_AuditAgency_migration', N'3.1.4');
END;

GO

