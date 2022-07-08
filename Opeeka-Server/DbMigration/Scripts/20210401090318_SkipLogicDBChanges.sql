IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    ALTER TABLE [Questionnaire] ADD [HasSkipLogic] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE TABLE [QuestionnaireSkipLogicRule] (
        [QuestionnaireSkipLogicRuleID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [QuestionnaireID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [ClonedQuestionnaireSkipLogicRuleID] int NULL,
        CONSTRAINT [PK_QuestionnaireSkipLogicRule] PRIMARY KEY ([QuestionnaireSkipLogicRuleID]),
        CONSTRAINT [FK_QuestionnaireSkipLogicRule_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireSkipLogicRule_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE TABLE [info].[ActionLevel] (
        [ActionLevelID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_ActionLevel] PRIMARY KEY ([ActionLevelID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE TABLE [info].[ActionType] (
        [ActionTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_ActionType] PRIMARY KEY ([ActionTypeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE TABLE [QuestionnaireSkipLogicRuleCondition] (
        [QuestionnaireSkipLogicRuleConditionID] int NOT NULL IDENTITY,
        [QuestionnaireItemID] int NOT NULL,
        [ComparisonOperator] nvarchar(max) NULL,
        [ComparisonValue] int NOT NULL,
        [QuestionnaireSkipLogicRuleID] int NOT NULL,
        [ListOrder] int NOT NULL,
        [JoiningOperator] nvarchar(max) NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_QuestionnaireSkipLogicRuleCondition] PRIMARY KEY ([QuestionnaireSkipLogicRuleConditionID]),
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleCondition_QuestionnaireItem_QuestionnaireItemID] FOREIGN KEY ([QuestionnaireItemID]) REFERENCES [QuestionnaireItem] ([QuestionnaireItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleCondition_QuestionnaireSkipLogicRule_QuestionnaireSkipLogicRuleID] FOREIGN KEY ([QuestionnaireSkipLogicRuleID]) REFERENCES [QuestionnaireSkipLogicRule] ([QuestionnaireSkipLogicRuleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleCondition_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE TABLE [QuestionnaireSkipLogicRuleAction] (
        [QuestionnaireSkipLogicRuleActionID] int NOT NULL IDENTITY,
        [QuestionnaireSkipLogicRuleID] int NOT NULL,
        [ActionLevelID] int NOT NULL,
        [QuestionnaireItemID] int NULL,
        [CategoryID] int NULL,
        [ActionTypeID] int NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_QuestionnaireSkipLogicRuleAction] PRIMARY KEY ([QuestionnaireSkipLogicRuleActionID]),
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleAction_ActionLevel_ActionLevelID] FOREIGN KEY ([ActionLevelID]) REFERENCES [info].[ActionLevel] ([ActionLevelID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleAction_ActionType_ActionTypeID] FOREIGN KEY ([ActionTypeID]) REFERENCES [info].[ActionType] ([ActionTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleAction_Category_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [info].[Category] ([CategoryID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleAction_QuestionnaireItem_QuestionnaireItemID] FOREIGN KEY ([QuestionnaireItemID]) REFERENCES [QuestionnaireItem] ([QuestionnaireItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleAction_QuestionnaireSkipLogicRule_QuestionnaireSkipLogicRuleID] FOREIGN KEY ([QuestionnaireSkipLogicRuleID]) REFERENCES [QuestionnaireSkipLogicRule] ([QuestionnaireSkipLogicRuleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireSkipLogicRuleAction_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRule_QuestionnaireID] ON [QuestionnaireSkipLogicRule] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRule_UpdateUserID] ON [QuestionnaireSkipLogicRule] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleAction_ActionLevelID] ON [QuestionnaireSkipLogicRuleAction] ([ActionLevelID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleAction_ActionTypeID] ON [QuestionnaireSkipLogicRuleAction] ([ActionTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleAction_CategoryID] ON [QuestionnaireSkipLogicRuleAction] ([CategoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleAction_QuestionnaireItemID] ON [QuestionnaireSkipLogicRuleAction] ([QuestionnaireItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleAction_QuestionnaireSkipLogicRuleID] ON [QuestionnaireSkipLogicRuleAction] ([QuestionnaireSkipLogicRuleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleAction_UpdateUserID] ON [QuestionnaireSkipLogicRuleAction] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleCondition_QuestionnaireItemID] ON [QuestionnaireSkipLogicRuleCondition] ([QuestionnaireItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleCondition_QuestionnaireSkipLogicRuleID] ON [QuestionnaireSkipLogicRuleCondition] ([QuestionnaireSkipLogicRuleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleCondition_UpdateUserID] ON [QuestionnaireSkipLogicRuleCondition] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090318_SkipLogicDBChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210401090318_SkipLogicDBChanges', N'3.1.4');
END;

GO

