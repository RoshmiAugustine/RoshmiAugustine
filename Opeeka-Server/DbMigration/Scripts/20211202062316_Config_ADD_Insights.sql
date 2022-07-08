IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211202062316_Config_ADD_Insights')
BEGIN
BEGIN TRY
BEGIN TRANSACTION  
 DECLARE @ConfigurationValueTypeID INT;
 DECLARE @ConfigurationParameterID INT;
 DECLARE @ConfigurationContextID INT;
 DECLARE @ConfigurationParameterContextID INT
 DECLARE @ConfigValueTypeID INT
 DECLARE @ConfigName NVARCHAR(100) 
 DECLARE @ConfigDesc NVARCHAR(500)
 DECLARE @ConfigValue NVARCHAR(MAX) 
 DECLARE @ConfigContextName NVARCHAR(50)
 DECLARE @ConfigValueTypeText INT
 DECLARE @ConfigValueTypeJSON INT
 DECLARE @ConfigContextNameAgency NVARCHAR(50)
 DECLARE @table_temp TABLE
	(    
	    ID INT,
	    ConfigValueType INT,
	    ConfigName NVarchar(100),
		ConfigDesc NVarchar(500),
		ConfigValue NVarchar(MAX)  
	);
 
 SET @ConfigContextName = 'Application';
 SET @ConfigContextNameAgency = 'Agency';
 SET @ConfigValueTypeText = 1;--Text type
 SET @ConfigValueTypeJSON = 4;--JSON type
 
 INSERT INTO @table_temp 
	SELECT 1,@ConfigValueTypeText,'Insights_Filters','Filters for Insight','daterange,Age_at_Last_Assessment,Gender,Sex,Sexual_Orientation,Races_and_Ethnicities,Language_Preferred,Language_Primary,Assessment_Instruments,Any_Indicated_Items,Item_Types,Min_Times_Assessed,Collaborations,Collaboration_Level,Collaboration_Category,Active_or_Discharged,Min_LOS,Success_Needs_Resolved,Success_Strengths_Built,Success_Support_Needs_Resolved,Helper,Person'
	UNION
	SELECT 2,@ConfigValueTypeText,'Insights_SisenseAPIURL','API URL for Insight','https://www.periscopedata.com{0}&signature={1}'
	UNION
	SELECT 3,@ConfigValueTypeText,'Insights_SisenseURLDataPath','Data path in API URL for Insight','/api/embedded_dashboard?data='
	UNION
	SELECT 4,@ConfigValueTypeText,'Insights_LifeInSeconds','Expiration for Request In seconds','20'
	UNION
    SELECT 5,@ConfigValueTypeText,'Insights_SisenseDashboardID','Sisense DashboardId for Insight','752293'
	UNION
    SELECT 6,@ConfigValueTypeText,'Insights_SisenseAPISecretKey','Sisense Secret key for signature','24a67fad-ab52-458b-8080-d63c21930'
	UNION
	SELECT 7,@ConfigValueTypeJSON,'Insights_CustomFilters','Custom Filters for Insight','[
	{
		"name":"AgencyId",
		"value":"{{AgencyId}}"
	},
	{
		"name":"UserRole",
		"value":"{{UserRole}}"
	},
	{
		"name":"UserId",
		"value":"{{UserId}}"
	},
	{
		"name":"Collaborations",
		"value":"0"
	},
	{
		"name":"Active_or_Discharged",
		"value":"1"
	},
	{
		"name":"Success_Needs_Resolved",
		"value":"50"
	}
]'
  
 --SELECT * FROM @table_temp;

 BEGIN
 IF EXISTS(SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName)
 BEGIN
  SET @ConfigurationContextID = (SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName);
  print @ConfigurationContextID 
  BEGIN
 	DECLARE cursor_configData CURSOR LOCAL FOR SELECT ConfigValueType,ConfigName,ConfigDesc,ConfigValue FROM @table_temp order by ID
 	OPEN cursor_configData	
 	FETCH NEXT FROM cursor_configData INTO @ConfigValueTypeID,@ConfigName,@ConfigDesc,@ConfigValue;
 	WHILE @@FETCH_STATUS = 0
 	BEGIN	    
 -----------------------------------[info].[ConfigurationParameter]--------------------------------------------
 		INSERT INTO [info].[ConfigurationParameter]
 		      ([ConfigurationValueTypeID],[AgencyId],[Name],[Description],[IsActive],[Deprecated],[CanModify])
 		VALUES(@ConfigValueTypeID,0,@ConfigName,@ConfigDesc,1,1,1);
 
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
 			     VALUES(@ConfigValue,0,@ConfigurationParameterContextID);
 			 END 
			 
-------------- For AgencyLevel ConfigurationParameterContext for Filters and customFilters 
			IF(@ConfigName = 'Insights_Filters' OR @ConfigName = 'Insights_CustomFilters')
			BEGIN
			    SET @ConfigurationContextID = (SELECT ConfigurationContextID FROM [info].[ConfigurationContext] 
                                      Where [Name] = @ConfigContextNameAgency);
			    INSERT INTO [info].[ConfigurationParameterContext]
 		              ([ConfigurationParameterID],[ConfigurationContextID])
			    VALUES(@ConfigurationParameterID,@ConfigurationContextID)
			END
 		 END
         FETCH NEXT FROM cursor_configData INTO @ConfigValueTypeID,@ConfigName,@ConfigDesc,@ConfigValue;
 	END
  END

 END

 -----------------------------------[__EFMigrationsHistory]--------------------------------------------
 INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211202062316_Config_ADD_Insights', N'3.1.4');
END
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 --print ERROR_MESSAGE();
END CATCH
END;

GO

