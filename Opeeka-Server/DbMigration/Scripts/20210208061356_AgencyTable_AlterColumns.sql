IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208061356_AgencyTable_AlterColumns')
BEGIN
   ALTER TABLE [dbo].[Agency] 
ALTER COLUMN   [ContactLastName] varchar(150);

ALTER TABLE [dbo].[Agency] 
ALTER COLUMN   [ContactFirstName] varchar(150);

END;

GO

 

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210208061356_AgencyTable_AlterColumns')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210208061356_AgencyTable_AlterColumns', N'3.1.4');
END;

GO

