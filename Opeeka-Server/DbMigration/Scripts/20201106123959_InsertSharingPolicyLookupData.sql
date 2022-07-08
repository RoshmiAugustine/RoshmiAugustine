IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201106123959_InsertSharingPolicyLookupData')
BEGIN

---------------------------------[dbo].[AgencySharingPolicy]--------------------------------------------------
    DELETE FROM [dbo].[AgencySharingPolicy]
    DBCC CHECKIDENT ('[dbo].[AgencySharingPolicy]',RESEED, 0)

    INSERT INTO [dbo].[AgencySharingPolicy]([Name],[Abbreviation],[Description],[IsActive])
         VALUES('ReadOnly','RO','Read Only',1);
    INSERT INTO [dbo].[AgencySharingPolicy]([Name],[Abbreviation],[Description],[IsActive])
         VALUES('Read/Write','RW','Both Read and Write',1);
         
---------------------------------[dbo].[CollaborationSharingPolicy]-------------------------------------------
    DELETE FROM [dbo].[CollaborationSharingPolicy]
    DBCC CHECKIDENT ('[dbo].[CollaborationSharingPolicy]',RESEED, 0)

    INSERT INTO [dbo].[CollaborationSharingPolicy]
               ([Name],[Abbreviation] ,[Description],[IsActive],[Weight])
         VALUES('ReadOnly','RO','Read Only',1,0);
    INSERT INTO [dbo].[CollaborationSharingPolicy]
               ([Name],[Abbreviation] ,[Description],[IsActive],[Weight])
         VALUES('Read/Write','RW','Both Read and Write',1,1);
END

BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201106123959_InsertSharingPolicyLookupData', N'3.1.4');
END;

GO

