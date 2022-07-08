IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525112905_ADD_Config_SSORedirectUrls')
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
---------------------------//Assessment_FileTypes//-------------------------------------
 SET @ConfigContextName = 'Application';
 SET @ConfigValueType = 'Text';
 SET @ConfigName = 'SSO_RedirectURL';
 SET @ConfigValue = '{ 
  "SSOBaserUrl" : "https://opeekab2cdev.b2clogin.com/opeekab2cdev.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1A_SIGNIN_SIGNUP_FRP_SSO&client_id=9994db7f-2dc7-4bac-bf8a-93bf04067890&nonce=defaultNonce&scope=openid&response_type=id_token&redirect_uri={{redirecturl}}",
  "RedirectUrls": 
  [
	{
	  "name":"safy", 
	  "value" : "https://agency1.pcis-dev.com"
	}
  ]
}';
 SET @ConfigDesc = 'SSO Redirect URLs for different clients';

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
 			     VALUES(@ConfigValue,0,@ConfigurationParameterContextID);
 			 END
 		END
 	END
 END
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220525112905_ADD_Config_SSORedirectUrls', N'3.1.4');
END;

GO

