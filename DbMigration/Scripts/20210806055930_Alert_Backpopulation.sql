IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210806055930_Alert_Backpopulation')
BEGIN

	select * into QuestionnaireNotifyRiskRuleCondition_bkp_2021_08_05_Bugfix from QuestionnaireNotifyRiskRuleCondition

    update  qnr set
 qnr.ListOrder =  isnull((
	select top 1 ListOrder from Response where itemID = 
		(select top 1 itemID from Questionnaireitem where QuestionnaireitemID in 
				(select  QuestionnaireItemID from QuestionnaireNotifyRiskRuleCondition 
				where QuestionnaireNotifyRiskRuleConditionID=qnr.QuestionnaireNotifyRiskRuleConditionID  
				)
		) 
        and Value = (select top 1 ComparisonValue from QuestionnaireNotifyRiskRuleCondition 
			where QuestionnaireNotifyRiskRuleConditionID=qnr.QuestionnaireNotifyRiskRuleConditionID
			)  order by ListOrder desc
			),1)  from QuestionnaireNotifyRiskRuleCondition qnr;

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210806055930_Alert_Backpopulation', N'3.1.4');
END;

GO

