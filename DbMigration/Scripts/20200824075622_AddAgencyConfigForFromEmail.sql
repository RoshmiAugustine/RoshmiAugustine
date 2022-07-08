IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200824075622_AddAgencyConfigForFromEmail')
BEGIN
BEGIN TRY
BEGIN TRANSACTION
 
 DECLARE @ConfigurationValueTypeID INT;
 DECLARE @ConfigurationParameterID INT;
 DECLARE @ConfigurationContextID INT;
 DECLARE @ConfigurationParameterContextID INT
 DECLARE @ConfigParamValue NVARCHAR(100) 
 DECLARE @ConfigContextName NVARCHAR(50)
 DECLARE @ConfigValueType NVARCHAR(10)
 DECLARE @AgencyAbbrev NVARCHAR(50)
 DECLARE @AgencyID INT
 DECLARE @ConfigParameter NVARCHAR(50)
 DECLARE @ConfigParameterDesc NVARCHAR(50)

 DECLARE @table_temp TABLE
	(    
	    ID INT,
		AgencyAbbrev Varchar(100),
		ConfigParamValue Varchar(100) 
	);
-----------------------------------------------------------------------------------------------------
 
 SET @ConfigContextName = 'Agency';
 SET @ConfigValueType = 'Email';
 SET @ConfigParameter = 'FromEmailID';
 SET @ConfigParameterDesc = 'From Email ID';

 INSERT INTO @table_temp 
	SELECT 1,'agency1','ajib.ck@naicoits.com'
	UNION
	SELECT 2,'agency2','ajib.ck@naicoits.com'
 -----------------------------------------------------------------------------------------------------
 --SELECT * FROM @table_temp;
 BEGIN
 IF EXISTS(SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName)
 BEGIN
  --print Reading ConfigContextID
  SET @ConfigurationContextID = (SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName);

  IF EXISTS(SELECT ConfigurationValueTypeID FROM [info].[ConfigurationValueType] Where [Name] = @ConfigValueType)
  BEGIN
    --print Reading ConfigurationValueTypeID
    SET @ConfigurationValueTypeID = (SELECT ConfigurationValueTypeID FROM [info].[ConfigurationValueType] Where [Name] = @ConfigValueType);
    print @ConfigurationValueTypeID
    --print Insert ConfigParameter
    INSERT INTO [info].[ConfigurationParameter]
 		        ([ConfigurationValueTypeID],[AgencyId],[Name],[Description],[IsActive],[Deprecated],[CanModify])
 		  VALUES(@ConfigurationValueTypeID,0,@ConfigParameter,@ConfigParameterDesc,1,1,1);
	--print Reading ConfigurationParameterID
    SET @ConfigurationParameterID = SCOPE_IDENTITY();	
 	
 	DECLARE cursor_configData CURSOR LOCAL FOR SELECT AgencyAbbrev,ConfigParamValue FROM @table_temp order by ID
 	OPEN cursor_configData	
 	FETCH NEXT FROM cursor_configData INTO @AgencyAbbrev,@ConfigParamValue;
 	WHILE @@FETCH_STATUS = 0
 	BEGIN
	    IF EXISTS(SELECT * from [dbo].[Agency] WHERE Abbrev = @AgencyAbbrev COLLATE SQL_Latin1_General_CP1_CS_AS)
		  BEGIN
		   --Print Read AgencyID
		   SET @AgencyID = (SELECT AgencyID from [dbo].[Agency] WHERE Abbrev = @AgencyAbbrev COLLATE SQL_Latin1_General_CP1_CS_AS)
		   
		   IF (@ConfigurationParameterID <> 0)
		   BEGIN		    
            --print Insert ConfigurationParameter
 		   	INSERT INTO [info].[ConfigurationParameterContext]
 		             ([ConfigurationParameterID],[ConfigurationContextID])
 		       VALUES(@ConfigurationParameterID,@ConfigurationContextID)
 		    --print Read ConfigurationParameterContextID
 		   	SET @ConfigurationParameterContextID = SCOPE_IDENTITY();
 		   	IF (@ConfigurationParameterContextID <> 0)
 		   	 BEGIN 
		   	    --print Insert Configuration
 		   	 	INSERT INTO [info].[Configuration]
 		   	 	 ([Value],[ContextFKValue],[ConfigurationParameterContextID])
 		   	     VALUES(@ConfigParamValue,@AgencyID,@ConfigurationParameterContextID);
 		   	 END
 		   END
		 END
         FETCH NEXT FROM cursor_configData INTO @AgencyAbbrev,@ConfigParamValue;
 	END
  END
 END
 END
 ---------insert into __EFMigrationsHistory--------------------------------------------------------
 INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200824075622_AddAgencyConfigForFromEmail', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 --print ERROR_MESSAGE();
END CATCH
END;

GO

