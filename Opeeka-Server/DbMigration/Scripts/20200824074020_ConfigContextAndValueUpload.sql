IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200824074020_ConfigContextAndValueUpload')
BEGIN
------------------------------------[info].[ConfigurationContext]----------------------------------------
 IF NOT EXISTS(SELECT Name FROM [info].[ConfigurationContext] Where [Name] IN (SELECT 'Application' UNION SELECT 'Agency' UNION SELECT 'User'))
 BEGIN
  INSERT INTO [info].[ConfigurationContext]
	           ([Name],[Description],[ParentContextID],[EntityName],[FKValueRequired])
	     VALUES('Application','Application Level Configurations',null,null,0)

  INSERT INTO [info].[ConfigurationContext]
	           ([Name],[Description],[ParentContextID],[EntityName],[FKValueRequired])
	     VALUES('Agency','Agency Level Configurations',null,null,0)
 
  INSERT INTO [info].[ConfigurationContext]
	           ([Name],[Description],[ParentContextID],[EntityName],[FKValueRequired])
	     VALUES('User','User Level Configurations',null,null,0)
 END
 -----------------------------------[info].[ConfigurationValueType]-------------------------------------------
 IF NOT EXISTS(SELECT Name FROM [info].[ConfigurationValueType] Where [Name] IN 
     (SELECT 'Text' UNION SELECT 'Email' UNION SELECT 'Number' UNION SELECT 'json'))
 BEGIN
 INSERT INTO [info].[ConfigurationValueType]
	           ([Name],[Description])
	     VALUES('Text','Any Text Values')

 INSERT INTO [info].[ConfigurationValueType]
	           ([Name],[Description])
	     VALUES('Email','Valid Email ID')


 INSERT INTO [info].[ConfigurationValueType]
	           ([Name],[Description])
	     VALUES('Number','Number Type')

 INSERT INTO [info].[ConfigurationValueType]
	           ([Name],[Description])
	     VALUES('json','JSON Type')
 END
 -------------------------------------------------------------------------------------------------------------
 INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
  VALUES (N'20200824074020_ConfigContextAndValueUpload', N'3.1.4');
END;

GO

