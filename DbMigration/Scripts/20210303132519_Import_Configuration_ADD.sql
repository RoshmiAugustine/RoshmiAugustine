﻿IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210303132519_Import_Configuration_ADD')

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
 DECLARE @table_temp TABLE
	(    
	    ID INT,
	    ConfigName Varchar(100),
		ConfigDesc Varchar(100),
		ConfigValue Varchar(MAX) 
	);
 
 SET @ConfigContextName = 'Application';
 SET @ConfigValueType = 'Text';
 
 INSERT INTO @table_temp 
	SELECT 1,'ImportEmailSubject','Import Email Link Subject','Import Process Result'
	UNION
	SELECT 1,'ImportEmailText','Import Email Text','<!DOCTYPE html PUBLIC “-//W3C//DTD XHTML 1.0 Transitional//EN” “https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd”>  <html xmlns=“https://www.w3.org/1999/xhtml”>  <head>  <title>Import Process</title>  <meta http–equiv=“Content-Type” content=“text/html; charset=UTF-8” />  <meta http–equiv=“X-UA-Compatible” content=“IE=edge” />  <meta name=“viewport” content=“width=device-width, initial-scale=1.0 “ />  </head>  <body>  <table style="width: 100%; border-collapse: collapse;" cellspacing="0" cellpadding="0">      <tbody>          <tr style="background-color: #227793; height: 80px;">              <td style="width: 30%;"></td>              <td style="width: 40%"></td>              <td style="width: 30%"></td>          </tr>          <tr style="background-color: #227793; height: 70px;">              <td style="width: 30%"></td>              <th                  style="width: 40%; background-color: white; text-align: center; font-family: Montserrat-bold; font-size: 30px; padding: 30px 0 20px 0;">                  Import Process              </th>              <td style="width: 30%;"></td>          </tr>          <tr style="background-color: #f4f4f4;">              <td style="width: 30%;"></td>              <td style="width: 40%; background-color: #f4f4f4;">                  <table style="background-color: white; width: 100%;  padding: 0 30px;">                      <tbody>                          <tr>                              <td style="text-align: center; padding: 30px 0  30px 0;">                                  Hi {{helpername}}, Please find the status of your import request below for the file "{{importfilename}}".                           </td>                          </tr>                          <tr>                              <td style="text-align: center; padding-bottom: 30px;">                                  {{importresultmessage}}                              </td>                          </tr>                                                                      </tbody>                  </table>                  <table cellspacing="0" cellpadding="0"                      style="background-color: #e5b008; width: 100%; padding: 25px 0px; margin-top:15px;">                      <tbody>                          <tr>                              <td style="text-align: center;">                                  If you need assistance please                                  contact                                  our                              </td>                          </tr>                          <tr>                              <td style="text-align: center;">                                  <a href="mailto:support@p-cis.com" style="color: #227793; text-decoration: underline; cursor:pointer; pointer-events: auto;">                                      Customer                                      Support Team                                  </a>                              </td>                          </tr>                      </tbody>                  </table>                    <table cellspacing="0" cellpadding="0" style="width: 100%; padding:0 0 20px 0">                      <tbody>                          <tr>                              <td style="text-align: center; padding:10px 0 0 0">                                  <img style="height: 32px;" src="{{applicationUrl}}/assets/icons/email-logo.png"                                      alt="Assessment" />                              </td>                          </tr>                          <tr>                              <td style="text-align: center; color: #a19f9f; font-size: 10px; margin-bottom: 4px;">                                  &#169;                                  2020                                  All rights                                  reserved Opeeka Inc.                              </td>                          </tr>                          <tr>                              <td style="text-align: center; color: #a19f9f; font-size: 10px;">                                 81 Blue Ravine Road Suite 120, Folsom, CA 95630                              </td>                          </tr>                      </tbody>                  </table>              </td>              <td style="width: 30%;"></td>          </tr>      </tbody>  </table>  </body>  </html>'
  
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
 	DECLARE cursor_configData CURSOR LOCAL FOR SELECT ConfigName,ConfigDesc,ConfigValue FROM @table_temp order by ID
 	OPEN cursor_configData	
 	FETCH NEXT FROM cursor_configData INTO @ConfigName,@ConfigDesc,@ConfigValue;
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
 			     VALUES(@ConfigValue,0,@ConfigurationParameterContextID);
 			 END
 		END
         FETCH NEXT FROM cursor_configData INTO @ConfigName,@ConfigDesc,@ConfigValue;
 	END
  END
 END
 END
 -----------------------------------[__EFMigrationsHistory]--------------------------------------------

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210303132519_Import_Configuration_ADD', N'3.1.4');
COMMIT TRANSACTION
END TRY
BEGIN CATCH 
 ROLLBACK TRANSACTION;
 --print ERROR_MESSAGE();
END CATCH
END;

GO

