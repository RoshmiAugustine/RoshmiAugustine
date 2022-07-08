IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210406061559_EHR_Instruments_UpdateDate')
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
	SELECT 1,'EHR_InstrumentsToUpdate','Instrument Details to Update Assessment Dates','[{
      "typeName":"CANS",
      "typeID":"22D5199653E3004185258615005433EB",    
	  "urlBodyParam":"RP_Date={0}&RP_Therapist_UNID={1}",
	  "urlToPost":"https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/F_REPORT?CreateDocument&ParentUNID={0}&type={1}",
      "urlToPostComplete":"https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_REPORT_RETURN?OpenAgent&UNID={0}&ACTION=complete&PARM2="
   },
     {
      "typeName":"CHAPS",
      "typeID":"F7C947323C5519AF852586A7005A974A",  
	  "urlBodyParam":"RP_Date={0}&RP_Therapist_UNID={1}",
      "urlToPost":"https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/F_HOME_REPORT?CreateDocument&ParentUNID={0}&type={1}",
      "urlToPostComplete":"https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_HOMEREPORT_RETURN?OpenAgent&UNID={0}&ACTION=complete&PARM2="
   },
   {
      "typeName":"SAFE",
      "typeID":"342397F8192F884A852586A70059FAE3",
	  "urlBodyParam":"AC_Date={0}&AC_Therapist_UNID={1}",
      "urlToPost":"https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/F_HOME_ACTIVITY?CreateDocument&ParentUNID={0}&type={1}",
      "urlToPostComplete":"https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_HOMEACTIVITY_RETURN?OpenAgent&UNID={0}&ACTION=complete&PARM2="
   }]', 1
   UNION
   SELECT 1,'EHR_TimeZone','Timezone used to update Assessmnet date to EHR', 'Eastern Standard Time', 1
	
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
    VALUES (N'20210406061559_EHR_Instruments_UpdateDate', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 --print ERROR_MESSAGE();
END CATCH;
END
GO
