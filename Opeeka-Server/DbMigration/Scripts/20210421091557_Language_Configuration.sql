IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210421091557_Language_Configuration')
BEGIN
BEGIN TRY
BEGIN TRANSACTION
  
 DECLARE @ConfigurationValueTypeID INT;
 DECLARE @ConfigurationParameterID INT;
 DECLARE @ConfigurationContextID INT;
 DECLARE @ConfigurationParameterContextID INT
 DECLARE @ConfigName NVARCHAR(100)
 DECLARE @ConfigValue NVARCHAR(MAX) 
 DECLARE @ConfigDesc NVARCHAR(100)
 DECLARE @ConfigContextName NVARCHAR(50)
 DECLARE @ConfigValueType NVARCHAR(10)
 DECLARE @AgencyID INT
 DECLARE @table_temp TABLE
	(    
	    ID INT,
	    ConfigName Varchar(100),
		ConfigDesc Varchar(100),
		ConfigValue Varchar(MAX),
		AgencyID INT
	);
 
 SET @ConfigContextName = 'Agency';
 SET @ConfigValueType = 'Text';
 
 INSERT INTO @table_temp 
	SELECT 1,'Language_Localization','Language for the localization','it-CH', 1
  
  --SELECT * FROM @table_temp;

 BEGIN
 IF EXISTS(SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName)
 BEGIN
  SET @ConfigurationContextID = (SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName);
  print @ConfigurationContextID
  IF EXISTS(SELECT ConfigurationValueTypeID FROM [info].[ConfigurationValueType] Where [Name] = @ConfigValueType)
  BEGIN
 	SET @ConfigurationValueTypeID = (SELECT ConfigurationValueTypeID FROM [info].[ConfigurationValueType] Where [Name] = @ConfigValueType);
    print @ConfigurationValueTypeID
 	DECLARE cursor_configData CURSOR LOCAL FOR SELECT ConfigName,ConfigDesc,ConfigValue,AgencyID FROM @table_temp order by ID
 	OPEN cursor_configData	
 	FETCH NEXT FROM cursor_configData INTO @ConfigName,@ConfigDesc,@ConfigValue,@AgencyID;
 	WHILE @@FETCH_STATUS = 0
 	BEGIN	    
 -----------------------------------[info].[ConfigurationParameter]--------------------------------------------
 		INSERT INTO [info].[ConfigurationParameter]
 		      ([ConfigurationValueTypeID],[AgencyId],[Name],[Description],[IsActive],[Deprecated],[CanModify])
 		VALUES(@ConfigurationValueTypeID,0,@ConfigName,@ConfigDesc,1,1,1);
 
 		SET @ConfigurationParameterID = SCOPE_IDENTITY();
 		print @ConfigurationParameterID
 		IF (@ConfigurationParameterID <> 0)
 		BEGIN 
-----------------------------------[info].[ConfigurationParameterContext]-----------------------------------------
 			INSERT INTO [info].[ConfigurationParameterContext]
 		          ([ConfigurationParameterID],[ConfigurationContextID])
 		    VALUES(@ConfigurationParameterID,@ConfigurationContextID)
 
 			SET @ConfigurationParameterContextID = SCOPE_IDENTITY();
 			IF (@ConfigurationParameterContextID <> 0)
 			 BEGIN 
 -----------------------------------[info].[Configuration]--------------------------------------------------------
 			 	INSERT INTO [info].[Configuration]
 			 	 ([Value],[ContextFKValue],[ConfigurationParameterContextID])
 			     VALUES(@ConfigValue,@AgencyID,@ConfigurationParameterContextID);
 			 END
 		END
         FETCH NEXT FROM cursor_configData INTO @ConfigName,@ConfigDesc,@ConfigValue,@AgencyID;
 	END
  END
 END
 END
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210421091557_Language_Configuration', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 --print ERROR_MESSAGE();
END CATCH;
END
GO


