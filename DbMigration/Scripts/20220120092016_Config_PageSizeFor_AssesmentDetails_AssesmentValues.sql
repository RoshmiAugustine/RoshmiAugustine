IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120092016_Config_PageSizeFor_AssesmentDetails_AssesmentValues')
BEGIN
DECLARE @ConfigurationValueTypeID INT;
 DECLARE @ConfigurationParameterID INT;
 DECLARE @ConfigurationContextID INT;
 DECLARE @ConfigurationParameterContextID INT
 DECLARE @ConfigName NVARCHAR(100)
 DECLARE @ConfigValue NVARCHAR(100) 
 DECLARE @ConfigDesc NVARCHAR(100)
 DECLARE @ConfigContextName NVARCHAR(50)
 DECLARE @ConfigValueType NVARCHAR(10)
 DECLARE @GET int = 1;
 DECLARE @POST int = 2;
 DECLARE @PUT int = 3;
 DECLARE @appObjId int;
 DECLARE @appObjId1 int;

 SET @ConfigContextName = 'Application';
 SET @ConfigValueType = 'Number';
 SET @ConfigName = 'Assesment_PageSize';
 SET @ConfigValue = 7;
 SET @ConfigDesc = 'PageSize for assessment-details and assessment-values';

 BEGIN
 IF EXISTS(SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName)
 BEGIN
  SET @ConfigurationContextID = (SELECT ConfigurationContextID FROM [info].[ConfigurationContext] Where [Name] = @ConfigContextName);
  print @ConfigurationContextID
  IF EXISTS(SELECT ConfigurationValueTypeID FROM [info].[ConfigurationValueType] Where [Name] = @ConfigValueType)
  BEGIN
 	SET @ConfigurationValueTypeID = (SELECT ConfigurationValueTypeID FROM [info].[ConfigurationValueType] Where [Name] = @ConfigValueType);
    print @ConfigurationValueTypeID
 
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
 			     VALUES(@ConfigValue,1,@ConfigurationParameterContextID);
 			 END
 		END
 	END
END
 -----------------------------------Modifying api endpoints--------------------------------------------------------
   IF  EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/assessment-details/{personIndex}/{questionnaireId}')
      BEGIN

        SET @appObjId = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/assessment-details/{personIndex}/{questionnaireId}');
        UPDATE  ApplicationObject set [Name] = '/api/assessment-details/{personIndex}/{questionnaireId}/{pageNumber}/{totalCount}' where 
        [Name] = '/api/assessment-details/{personIndex}/{questionnaireId}' AND IsRemoved = 0 AND ApplicationObjectID=@appObjId;

      END

   IF  EXISTS(select ApplicationObjectID from ApplicationObject where [Name] = '/api/assessment-values/{personIndex}/{questionnaireId}')
      BEGIN
         SET @appObjId1 = (select ApplicationObjectID from ApplicationObject where [Name] = '/api/assessment-values/{personIndex}/{questionnaireId}');

 	     UPDATE  ApplicationObject set [Name] = '/api/assessment-values' where 
         [Name] = '/api/assessment-values/{personIndex}/{questionnaireId}' AND IsRemoved = 0 AND   ApplicationObjectID=@appObjId1;

         update info.Permission  set OperationTypeID= @POST where ApplicationObjectID=@appObjId1
     END
      -----------------------------------Modifying api endpoints--------------------------------------------------------
END
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220120092016_Config_PageSizeFor_AssesmentDetails_AssesmentValues', N'3.1.4');
END;

GO



