IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511134006_ExportTemplateADD_Type_Migration')
BEGIN
    ALTER TABLE [ExportTemplate] ADD [TemplateType] varchar(15) NOT NULL DEFAULT '';
END;
GO
--Changing Get method to Post
IF EXISTS(SELECT * FROM ApplicationObject WHERE name like '/api/export-template')
BEGIN   
	DECLARE @PERMID INT;
	DECLARE @OperationTypeID INT;
	SET @OperationTypeID = (SELECT OperationTypeID FROM info.OperationType WHERE Name = 'POST');
	SET @PERMID = (SELECT PermissionID FROM info.Permission WHERE ApplicationObjectID = (SELECT ApplicationObjectID FROM ApplicationObject
	WHERE name like '/api/export-template'))
	UPDATE info.Permission SET OperationTypeID = @OperationTypeID WHERE PermissionID = @PERMID
END
GO
--Updating Assement Query
IF EXISTS(SELECT * FROM ExportTemplate WHERE DisplayName = 'Assessment Details' AND ISNULL(TemplateType,'') ='')
BEGIN
 UPDATE ExportTemplate SET TemplateType = 'Assessment' WHERE DisplayName = 'Assessment Details';
 UPDATE ExportTemplate SET TemplateSourceText = 
       'DECLARE @cols  AS NVARCHAR(MAX)=''''; 
		DECLARE @query AS NVARCHAR(MAX)='''';  
	    DECLARE @cols2  AS NVARCHAR(MAX)=''''; 
		SELECT * INTO #AssessmentCTE FROM
		(
		 SELECT Agency.AgencyId,    Agency.Name, 
			Person.PersonID,    Person.FirstName,    
			ISNULL(Person.MiddleName,'''') AS MiddleName,    Person.LastName,  
			CONVERT(varchar,Person.DateOfBirth,101) AS DateOfBirth,PQ.QuestionnaireID,A.AssessmentID, 
			ASR.Name AS AssessmentReason,AST.Name AssessmentStatus,CONVERT(varchar,DateTaken,101) AS DateTaken
		 FROM Assessment A 
			JOIn info.AssessmentStatus AST ON AST.AssessmentStatusID = A.AssessmentStatusID   
			JOIN info.AssessmentReason ASR ON ASR.AssessmentReasonID = A.AssessmentReasonID 
			JOIN PersonQuestionnaire PQ ON A.PersonQuestionnaireID = PQ.PersonQuestionnaireID
			JOIN Person ON PQ.PersonID = Person.PersonID  
			JOIN Agency ON Person.AgencyID = Agency.AgencyID 
		 WHERE Person.AgencyID = {agencyId} AND A.IsRemoved = 0  AND PQ.IsRemoved = 0 {assessmentFilterConditions}
		) AS A1
		SELECT * INTO #AssessmentResponseCTE FROM(
		 SELECT ISNULL(Ins.Abbrev + ''_''+ C.Abbrev + ''_'' + I.Label + ISNULL(''-'' + ps.FirstName, '''') ,'''') AS ResponseDescription,
		     CASE WHEN AR.ResponseID IS NULL THEN AR.Value
			 ELSE R.Label END AS ResponseValue ,
			 AR.AssessmentID
		 FROM #AssessmentCTE A
		    LEFT JOIN AssessmentResponse AR ON A.AssessmentID = AR.AssessmentID
			LEFT JOIN PersonSupport PS ON AR.PersonSupportID = PS.PersonSupportID
		    LEFT JOIN Response R ON R.ResponseID = AR.ResponseID  
		    JOIN  QuestionnaireItem QI ON QI.QuestionnaireItemID = AR.QuestionnaireItemID  
		    JOIN info.Category C ON QI.CategoryID = C.CategoryID  
		    JOIN Item I ON I.ItemID = QI.ItemID  
		    JOIN  Questionnaire Q ON Q.QuestionnaireID = QI.QuestionnaireID 
		    JOIN  Info.Instrument Ins ON Ins.InstrumentID = Q.InstrumentID  
		 WHERE AR.IsRemoved = 0 
		 )AS AR1 
		SELECT * INTO #CTE FROM (SELECT A.*,AR.ResponseDescription,AR.ResponseValue FROM #AssessmentCTE A
		 LEFT JOIN  #AssessmentResponseCTE AR ON A.AssessmentID = AR.AssessmentID)AS A
		
		        SET @Cols = ''[-],'';				
				SELECT @cols = ISNULL(@cols,'''') + QUOTENAME(ResponseDescription) + '',''
				FROM (SELECT DISTINCT ResponseDescription AS ResponseDescription FROM #CTE ) AS tmp ;
				SELECT @cols = substring(@cols, 0, len(@cols));			

				SELECT @cols2 = ISNULL(@cols2,'''') + ResponseDescription
				FROM (SELECT DISTINCT '',ISNULL([''+ResponseDescription+''],'''''''') AS [''+ResponseDescription+'']'' AS ResponseDescription 
				      FROM #CTE ) AS tmp ;
				SET @query = ''IF('''''' + @Cols + '''''' = ''''[-]'''')
							     SELECT * FROM #AssessmentCTE
							  ELSE
							  SELECT AgencyId,Name,PersonID,FirstName,MiddleName,LastName,  
									DateOfBirth,QuestionnaireID,AssessmentID,AssessmentReason,AssessmentStatus,DateTaken
									''+ @Cols2 +'' FROM  (SELECT * FROM #CTE )src  
							  	         PIVOT ( MAX(ResponseValue) for ResponseDescription in ('' + @cols + '')  )piv ORDER BY firstname ASC, DateTaken DESC'';
				EXECUTE(@query);'
	 WHERE DisplayName = 'Assessment Details';
END
GO
--Applyng index1
IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_AssessmentResponse_IsRemoved' AND object_id = OBJECT_ID('dbo.AssessmentResponse'))>0
BEGIN
	DROP INDEX [IX_AssessmentResponse_IsRemoved] ON [dbo].[AssessmentResponse]
END
CREATE NONCLUSTERED INDEX [IX_AssessmentResponse_IsRemoved]
ON [dbo].[AssessmentResponse] ([IsRemoved])
INCLUDE ([AssessmentID],[ResponseID],[QuestionnaireITemID])
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210511134006_ExportTemplateADD_Type_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210511134006_ExportTemplateADD_Type_Migration', N'3.1.4');
END;

GO

