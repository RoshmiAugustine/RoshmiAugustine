IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210302183920_ImportType_Lookup_Insert')
BEGIN
IF NOT EXISTS(SELECT * FROM info.importType)
BEGIN
  INSERT INTO info.importType 
	SELECT 'Helper','[{"FirstName": "","LastName": "","Role": "","Email": "","ReviewerEmail":"","StartDate":""}]','',0,getdate(),1
  INSERT INTO info.importType 
	SELECT 'Person','[{ "FirstName": "","LastName": "","DateOfBirth": "","Email": "","Phone1": "","Phone1Code": "","Identified Gender": "","HelperEmail": "","HelperStartDate": "", "CollaborationName": "", "CollaborationStartDate": "", "Race/Ethnicity1": "", "Race/Ethnicity2": "", "Race/Ethnicity3": "", "Race/Ethnicity4": "", "Race/Ethnicity5": "", "IdentifierType1": "", "IdentifierType2": "", "IdentifierType3": "", "IdentifierType4": "", "IdentifierType5": "", "IdentifierTypeID1": "", "IdentifierTypeID2": "", "IdentifierTypeID3": "", "IdentifierTypeID4": "", "IdentifierTypeID5": "" }]','',0,getdate(),2
  INSERT INTO info.importType 
	SELECT 'Assessment','[{"PersonIndex":"","AssessmentDateTaken":"","ReasoningText":"","AssessmentReason":"","TriggeringEventDate":"","TriggeringEventNotes":"","AssessmentStatus":"","VoiceType":"","PersonSupportID":"","HelperEmail":""}]','',0,getdate(),3 
 END
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210302183920_ImportType_Lookup_Insert', N'3.1.4');
END;

GO

