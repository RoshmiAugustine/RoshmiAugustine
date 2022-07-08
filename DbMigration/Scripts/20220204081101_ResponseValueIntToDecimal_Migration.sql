IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220204081101_ResponseValueIntToDecimal_Migration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Response]') AND [c].[name] = N'Value');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Response] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Response] ALTER COLUMN [Value] decimal(16,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220204081101_ResponseValueIntToDecimal_Migration')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireSkipLogicRuleCondition]') AND [c].[name] = N'ComparisonValue');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireSkipLogicRuleCondition] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [QuestionnaireSkipLogicRuleCondition] ALTER COLUMN [ComparisonValue] decimal(16,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220204081101_ResponseValueIntToDecimal_Migration')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireNotifyRiskRuleCondition]') AND [c].[name] = N'ComparisonValue');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireNotifyRiskRuleCondition] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [QuestionnaireNotifyRiskRuleCondition] ALTER COLUMN [ComparisonValue] decimal(16,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220204081101_ResponseValueIntToDecimal_Migration')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireDefaultResponseRuleCondition]') AND [c].[name] = N'ComparisonValue');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireDefaultResponseRuleCondition] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [QuestionnaireDefaultResponseRuleCondition] ALTER COLUMN [ComparisonValue] decimal(16,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220204081101_ResponseValueIntToDecimal_Migration')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NotifyRiskValue]') AND [c].[name] = N'AssessmentResponseValue');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [NotifyRiskValue] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [NotifyRiskValue] ALTER COLUMN [AssessmentResponseValue] decimal(16,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220204081101_ResponseValueIntToDecimal_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220204081101_ResponseValueIntToDecimal_Migration', N'3.1.4');
END;

GO

