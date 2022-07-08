IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211014075544_ADD_ConfigForTimeout_Application')
BEGIN
BEGIN TRY
BEGIN TRANSACTION
  
 DECLARE @ConfigurationValueTypeID INT;
 DECLARE @ConfigurationParameterID INT;
 DECLARE @ConfigurationContextID INT;
 DECLARE @AgencyID INT;
 DECLARE @ConfigurationParameterContextID INT
 DECLARE @ConfigName NVARCHAR(100)
 DECLARE @ConfigValue NVARCHAR(100) 
 DECLARE @ConfigDesc NVARCHAR(100)
 DECLARE @ConfigContextName NVARCHAR(50)
 DECLARE @ConfigContextNameAgency NVARCHAR(50)
 DECLARE @ConfigValueType NVARCHAR(10)

 SET @ConfigContextName = 'Application';
 SET @ConfigContextNameAgency = 'Agency';
 SET @ConfigValueType = 'Text';

SELECT @ConfigName = 'Application_Timeout' ,
       @ConfigDesc = 'Timeout in application (minutes) ',
       @ConfigValue = '5'

 BEGIN
 IF EXISTS(SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName)
 BEGIN
  SET @ConfigurationContextID = (SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName);
  --print @ConfigurationContextID
  IF EXISTS(SELECT ConfigurationValueTypeID FROM [info].[ConfigurationValueType] Where [Name] = @ConfigValueType)
  BEGIN
 	SET @ConfigurationValueTypeID = (SELECT ConfigurationValueTypeID FROM [info].[ConfigurationValueType] Where [Name] = @ConfigValueType);
   -- print @ConfigurationValueTypeID
  
 -----------------------------------[info].[ConfigurationParameter]--------------------------------------------
 	INSERT INTO [info].[ConfigurationParameter]
 		      ([ConfigurationValueTypeID],[AgencyId],[Name],[Description],[IsActive],[Deprecated],[CanModify])
 	VALUES(@ConfigurationValueTypeID,0,@ConfigName,@ConfigDesc,1,1,1);
 
 	SET @ConfigurationParameterID = SCOPE_IDENTITY();

    --print @ConfigurationParameterID
 	IF (@ConfigurationParameterID <> 0)
 	BEGIN 
---------------------------Application Level --- [info].[ConfigurationParameterContext]----------
 	   INSERT INTO [info].[ConfigurationParameterContext]
 	            ([ConfigurationParameterID],[ConfigurationContextID])
 	      VALUES(@ConfigurationParameterID,@ConfigurationContextID)
               
 	   SET @ConfigurationParameterContextID = SCOPE_IDENTITY();
 	   IF (@ConfigurationParameterContextID <> 0)
 	   BEGIN 
       -------------------------[info].[Configuration] for Application--------------------
 		  	INSERT INTO [info].[Configuration]
 		  	 ([Value],[ContextFKValue],[ConfigurationParameterContextID])
 		      VALUES(@ConfigValue,0,@ConfigurationParameterContextID);
 		  END

--------------------------Agency Level ---[info].[ConfigurationParameterContext]------------------
      SET @ConfigurationContextID = (SELECT ConfigurationContextID FROM [info].[ConfigurationContext] 
                                      Where [Name] = @ConfigContextNameAgency);
      IF(@ConfigurationContextID > 0)
        INSERT INTO [info].[ConfigurationParameterContext]
 		              ([ConfigurationParameterID],[ConfigurationContextID])
 	    VALUES(@ConfigurationParameterID,@ConfigurationContextID)
 	END
  END
 END
 END

 -----------------------------------[__EFMigrationsHistory]--------------------------------------------
 INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211014075544_ADD_ConfigForTimeout_Application', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 print ERROR_MESSAGE();
END CATCH
END;
GO

