IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201222050852_EHR_ADDConfigurations_URL')
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
	SELECT 1,'EHR_LoginURL','Login URL','https://www.extendedreach.com/names.nsf?login', 1
	UNION
	SELECT 2,'EHR_ClientURL','Client/Person URL','https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_CLIENT&returnfields=doc_UNID,CC_LastName,CC_FirstName,CC_MiddleInitial,CC_DOB,CC_Gender,CC_Race,CC_PrimaryLanguage,CC_PersonalPhone,CC_PersonalEmail,CC_Referral_Date,CC_Therapist_UNID,CC_HOME_UNID,CC_Relationship_UNIDs', 1
	UNION
	SELECT 3,'EHR_RelationshipURL','PersonSupport Relationship URL','https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_RELATIONSHIP&returnfields=doc_UNID,R_FullName,R_Relationship_Detail,R_EMail,R_Phone_Work,R_Address1,R_Address2,R_City,R_State,R_Zip', 1
	UNION
	SELECT 4,'EHR_ProviderURL','PersonSupport Provider URL','https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_PROVIDER&returnfields=FH_Name_First_A,FH_Name_First_B,FH_Name_Last_A,FH_Name_Last_B,FH_Name_Middle_A,FH_Name_Middle_B,FH_Phone_Home,FH_EMail', 1
	UNION
	SELECT 5,'EHR_Username','Username for Login URL','dmeagher@opeeka.com', 1
	UNION
	SELECT 6,'EHR_Password','Password for Login URL','Fred22@@fr', 1
  
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
 -----------------------------------[__EFMigrationsHistory]--------------------------------------------
  INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201222050852_EHR_ADDConfigurations_URL', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 --print ERROR_MESSAGE();
END CATCH
END

GO

