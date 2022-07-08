IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210209154622_Audit_Update_Script')
BEGIN

INSERT INTO [dbo].[AuditTableName]
           ([TableName]
           ,[Label])
     VALUES
           ('User'
           ,'User')

INSERT INTO [dbo].[AuditFieldName]
           ([FieldName]
           ,[TableName]
           ,[Label])
     VALUES
           ('LastLogin'
           ,'User'
           ,'Last Login')
insert into AuditFieldName values('UserID', 'User','UserID')
insert into AuditFieldName values('PersonID', 'Person','PersonID')
END;
GO
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210209154622_Audit_Update_Script', N'3.1.4');
END;

GO

