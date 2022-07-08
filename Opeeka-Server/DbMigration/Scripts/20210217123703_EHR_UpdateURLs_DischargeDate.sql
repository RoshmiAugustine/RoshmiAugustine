IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210217123703_EHR_UpdateURLs_DischargeDate')
--the below block of script changes automaticaly appeared on generating migration File
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FileImport]') AND [c].[name] = N'ImportType');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [FileImport] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [FileImport] ALTER COLUMN [ImportType] nvarchar(100) NULL;
END;
---------------------------------------------------------
BEGIN 

Update info.configuration set value = 'https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_PROVIDER&query=FH_HomeStatus=%22{status}%22&returnfields=FH_Name_First_A,FH_Name_First_B,FH_Name_Last_A,FH_Name_Last_B,FH_Name_Middle_A,FH_Name_Middle_B,FH_Phone_Home,FH_EMail,FH_FullName,FH_EMail2,FH_Worker_UNID,FH_SecWorker_UNID,FH_IntakeDate,FH_Member_UNIDs,FH_DOB_A,FH_ProviderType,FH_Race_A,FH_Race_B,FH_CloseDate' 
WHERE ConfigurationParameterContextID IN (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'EHR_HomeURL'))

Update info.configuration set value = 'https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_CLIENT&query=CC_CaseStatus=%22{status}%22&returnfields=doc_UNID,CC_LastName,CC_FirstName,CC_MiddleInitial,CC_DOB,CC_Gender,CC_Race,CC_PrimaryLanguage,CC_PersonalPhone,CC_PersonalEmail,CC_Referral_Date,CC_Therapist_UNID,CC_HOME_UNID,CC_Relationship_UNIDs,CC_States,CC_Close' 
WHERE ConfigurationParameterContextID IN (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'EHR_ClientURL'))

Update info.configuration set value = 'https://fb.extendedreach.com/Clients/FaithBridge/fbfc.nsf/A_JSON?Open&form=F_CLIENT&query=CC_AdoptionCaseStatus=%22{status}%22&returnfields=doc_UNID,CC_LastName,CC_FirstName,CC_MiddleInitial,CC_DOB,CC_Gender,CC_Race,CC_PrimaryLanguage,CC_PersonalPhone,CC_PersonalEmail,CC_Referral_Date,CC_Therapist_UNID,CC_HOME_UNID,CC_Relationship_UNIDs,CC_States,CC_Close' 
WHERE ConfigurationParameterContextID IN (SELECT ConfigurationParameterContextID FROM info.ConfigurationParameterContext 
    WHERE ConfigurationParameterID 
    in (SELECT ConfigurationParameterID FROM info.ConfigurationParameter WHERE NAME = 'EHR_AdoptionURL'))
END

BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210217123703_EHR_UpdateURLs_DischargeDate', N'3.1.4');
END;

GO

