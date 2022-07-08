IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308070639_PersonAuditNew_migration')

BEGIN
		if not exists(select * from AuditTableName WHERE TableName = 'Person')
			BEGIN
				INSERT INTO AuditTableName VALUES('Person','Person');
			END
			IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.schemas s ON t.schema_id = s.schema_id 
	INNER JOIN sys.indexes i on i.object_id = t.object_id
    WHERE i.is_primary_key = 1 AND s.name = 'dbo' AND t.name = 'AuditFieldName')
	BEGIN
		ALTER TABLE  [dbo].[AuditFieldName] DROP CONSTRAINT PK_AuditFieldName; 
	END

			BEGIN
				if not exists(select * from AuditFieldName WHERE FieldName = 'FirstName' and TableName='Person')
					BEGIN
						INSERT INTO AuditFieldName VALUES('FirstName','Person','FirstName');
					END
				if not exists(select * from AuditFieldName WHERE FieldName = 'Email' and TableName='Person')
					BEGIN
						INSERT INTO AuditFieldName VALUES('Email','Person','Email');
					END
				if not exists(select * from AuditFieldName WHERE FieldName = 'LastName' and TableName='Person')
					BEGIN
						INSERT INTO AuditFieldName VALUES('LastName','Person','LastName');
					END
					if not exists(select * from AuditFieldName WHERE FieldName = 'GenderID' and TableName='Person')
					BEGIN
				INSERT INTO AuditFieldName VALUES('GenderID','Person','GenderID');
				END
			if not exists(select * from AuditFieldName WHERE FieldName = 'MiddleName' and TableName='Person')
			BEGIN
				INSERT INTO AuditFieldName VALUES('MiddleName','Person','MiddleName');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'Suffix' and TableName='Person')
			BEGIN
			INSERT INTO AuditFieldName VALUES('Suffix','Person','Suffix');
				END
			if not exists(select * from AuditFieldName WHERE FieldName = 'PrimaryLanguageID' and TableName='Person')
			BEGIN
			INSERT INTO AuditFieldName VALUES('PrimaryLanguageID','Person','PrimaryLanguageID');
				END
			if not exists(select * from AuditFieldName WHERE FieldName = 'PreferredLanguageID' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('PreferredLanguageID','Person','PreferredLanguageID');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'DateOfBirth' and TableName='Person')
			BEGIN
				INSERT INTO AuditFieldName VALUES('DateOfBirth','Person','DateOfBirth');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'SexualityID' and TableName='Person')
			BEGIN
				INSERT INTO AuditFieldName VALUES('SexualityID','Person','SexualityID');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'BiologicalSexID' and TableName='Person')
			BEGIN
				INSERT INTO AuditFieldName VALUES('BiologicalSexID','Person','BiologicalSexID');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'PreferredLanguageID' and TableName='Person')
			BEGIN
				INSERT INTO AuditFieldName VALUES('PreferredLanguageID','Person','PreferredLanguageID');
			END
			if not exists(select * from AuditFieldName WHERE FieldName = 'Phone1' and TableName='Person')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Phone1','Person','Phone1');
			END

			if not exists(select * from AuditFieldName WHERE FieldName = 'Phone2' and TableName='Person')
			BEGIN
				INSERT INTO AuditFieldName VALUES('Phone2','Person','Phone2');
			END

			if not exists(select * from AuditFieldName WHERE FieldName = 'StartDate' and TableName='Person')
			BEGIN
			INSERT INTO AuditFieldName VALUES('StartDate','Person','StartDate');
			END

			if not exists(select * from AuditFieldName WHERE FieldName = 'EndDate' and TableName='Person')
			BEGIN
				INSERT INTO AuditFieldName VALUES('EndDate','Person','EndDate');
			END

			if not exists(select * from AuditFieldName WHERE FieldName = 'AgencyID' and TableName='Person')
			BEGIN
		INSERT INTO AuditFieldName VALUES('AgencyID','Person','AgencyID');
			END

			if not exists(select * from AuditFieldName WHERE FieldName = 'Phone1Code' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('Phone1Code','Person','Phone1Code');
			END


				if not exists(select * from AuditFieldName WHERE FieldName = 'Phone2Code' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('Phone2Code','Person','Phone2Code');
			END

				if not exists(select * from AuditFieldName WHERE FieldName = 'IsActive' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('IsActive','Person','IsActive');
			END

				if not exists(select * from AuditFieldName WHERE FieldName = 'PersonScreeningStatusID' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('PersonScreeningStatusID','Person','PersonScreeningStatusID');
			END

				if not exists(select * from AuditFieldName WHERE FieldName = 'IsRemoved' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('IsRemoved','Person','IsRemoved');
			END

				if not exists(select * from AuditFieldName WHERE FieldName = 'UpdateUserID' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('UpdateUserID','Person','UpdateUserID');
			END

				if not exists(select * from AuditFieldName WHERE FieldName = 'UpdateDate' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('UpdateDate','Person','UpdateDate');
			END
			
			if not exists(select * from AuditFieldName WHERE FieldName = 'UniversalID' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('UniversalID','Person','UniversalID');
			END
				if not exists(select * from AuditFieldName WHERE FieldName = 'PersonIndex' and TableName='Person')
			BEGIN
					INSERT INTO AuditFieldName VALUES('PersonIndex','Person','PersonIndex');
			END
			END

	END;

GO





BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210308070639_PersonAuditNew_migration', N'3.1.4');
END;

GO

