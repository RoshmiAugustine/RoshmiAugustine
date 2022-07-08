IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210308063254_CollaborationAudit_migration')
BEGIN
		
		if not exists(select * from AuditTableName WHERE TableName = 'Collaboration')
			BEGIN
				INSERT INTO AuditTableName VALUES('Collaboration','Collaboration');
			END
			IF EXISTS (SELECT * FROM sys.tables t INNER JOIN sys.schemas s ON t.schema_id = s.schema_id 
	INNER JOIN sys.indexes i on i.object_id = t.object_id
    WHERE i.is_primary_key = 1 AND s.name = 'dbo' AND t.name = 'AuditFieldName')
	BEGIN
		ALTER TABLE  [dbo].[AuditFieldName] DROP CONSTRAINT PK_AuditFieldName; 
	END

		if not exists(select * from AuditFieldName WHERE TableName = 'Collaboration')
		BEGIN
				INSERT INTO AuditFieldName VALUES('CollaborationIndex','Collaboration','CollaborationIndex');
			
				INSERT INTO AuditFieldName VALUES('ReportingUnitID','Collaboration','ReportingUnitID');
			
				INSERT INTO AuditFieldName VALUES('TherapyTypeID','Collaboration','TherapyTypeID');
			
				INSERT INTO AuditFieldName VALUES('Name','Collaboration','Name');
			
				INSERT INTO AuditFieldName VALUES('Note','Collaboration','Note');
			
				INSERT INTO AuditFieldName VALUES('StartDate','Collaboration','StartDate');
			
				INSERT INTO AuditFieldName VALUES('EndDate','Collaboration','EndDate');
			
				INSERT INTO AuditFieldName VALUES('IntervalDays','Collaboration','IntervalDays');
			
				INSERT INTO AuditFieldName VALUES('IsRemoved','Collaboration','IsRemoved');
			
				INSERT INTO AuditFieldName VALUES('UpdateUserID','Collaboration','UpdateUserID');
			
				INSERT INTO AuditFieldName VALUES('UpdateDate','Collaboration','UpdateDate');
			
				INSERT INTO AuditFieldName VALUES('Abbreviation','Collaboration','Abbreviation');
			
				INSERT INTO AuditFieldName VALUES('AgencyID','Collaboration','AgencyID');
			
				INSERT INTO AuditFieldName VALUES('CollaborationLeadUserID','Collaboration','CollaborationLeadUserID');
			
				INSERT INTO AuditFieldName VALUES('CollaborationLevelID','Collaboration','CollaborationLevelID');
			
				INSERT INTO AuditFieldName VALUES('Code','Collaboration','Code');
			
				INSERT INTO AuditFieldName VALUES('Description','Collaboration','Description');

					INSERT INTO AuditFieldName VALUES('CollaborationID','Collaboration','CollaborationID');
		END

END;

GO


BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210308063254_CollaborationAudit_migration', N'3.1.4');
END;

GO

