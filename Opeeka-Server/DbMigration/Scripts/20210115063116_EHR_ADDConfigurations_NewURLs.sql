--Please Note-File added to DBMigration.csproj BUt the migrationID here differs.Since applied in DB.
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210111055536_EHR_ADDConfigurations_NewURLs')
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
	SELECT 1,'EHR_HomeURL','Home Persons URL','https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_PROVIDER&query=FH_HomeStatus=%22O%22&returnfields=FH_Name_First_A,FH_Name_First_B,FH_Name_Last_A,FH_Name_Last_B,FH_Name_Middle_A,FH_Name_Middle_B,FH_Phone_Home,FH_EMail,FH_FullName,FH_EMail2,FH_Worker_UNID,FH_SecWorker_UNID,FH_IntakeDate,FH_Member_UNIDs,FH_DOB_A', 1
	UNION
	SELECT 2,'EHR_HouseMemberURL','HouseMember/Support For Home persons URL','https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_HOUSEMEMBER&returnfields=HM_FirstName,HM_MiddleInitial,HM_LastName,HM_EMail,HM_Phone_Home,HM_Relationship_Detail', 1

  
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
    VALUES (N'20210111055536_EHR_ADDConfigurations_NewURLs', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 --print ERROR_MESSAGE();
END CATCH
Update info.configuration set value = 'https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_CLIENT&query=CC_CaseStatus=%22O%22&returnfields=doc_UNID,CC_LastName,CC_FirstName,CC_MiddleInitial,CC_DOB,CC_Gender,CC_Race,CC_PrimaryLanguage,CC_PersonalPhone,CC_PersonalEmail,CC_Referral_Date,CC_Therapist_UNID,CC_HOME_UNID,CC_Relationship_UNIDs,CC_States' 
WHERE ConfigurationParameterContextID IN (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'EHR_ClientURL'))
--New change as on 15-01-2021
Update info.configuration set value = 'https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_PROVIDER&query=FH_HomeStatus=%22O%22&returnfields=FH_Name_First_A,FH_Name_First_B,FH_Name_Last_A,FH_Name_Last_B,FH_Name_Middle_A,FH_Name_Middle_B,FH_Phone_Home,FH_EMail,FH_FullName,FH_EMail2,FH_Worker_UNID,FH_SecWorker_UNID,FH_IntakeDate,FH_Member_UNIDs,FH_DOB_A,FH_ProviderType' 
WHERE ConfigurationParameterContextID IN (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'EHR_HomeURL'))
END

GO

