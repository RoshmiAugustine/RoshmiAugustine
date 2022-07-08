IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    ALTER TABLE [Questionnaire] ADD [HasDefaultResponseRule] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE TABLE [QuestionnaireDefaultResponseRule] (
        [QuestionnaireDefaultResponseRuleID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [QuestionnaireID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [ClonedQuestionnaireDefaultResponseRuleID] int NULL,
        CONSTRAINT [PK_QuestionnaireDefaultResponseRule] PRIMARY KEY ([QuestionnaireDefaultResponseRuleID]),
        CONSTRAINT [FK_QuestionnaireDefaultResponseRule_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireDefaultResponseRule_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE TABLE [QuestionnaireDefaultResponseRuleAction] (
        [QuestionnaireDefaultResponseRuleActionID] int NOT NULL IDENTITY,
        [QuestionnaireDefaultResponseRuleID] int NOT NULL,
        [QuestionnaireItemID] int NULL,
        [DefaultResponseID] int NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_QuestionnaireDefaultResponseRuleAction] PRIMARY KEY ([QuestionnaireDefaultResponseRuleActionID]),
        CONSTRAINT [FK_QuestionnaireDefaultResponseRuleAction_Response_DefaultResponseID] FOREIGN KEY ([DefaultResponseID]) REFERENCES [Response] ([ResponseID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireDefaultResponseRuleAction_QuestionnaireDefaultResponseRule_QuestionnaireDefaultResponseRuleID] FOREIGN KEY ([QuestionnaireDefaultResponseRuleID]) REFERENCES [QuestionnaireDefaultResponseRule] ([QuestionnaireDefaultResponseRuleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireDefaultResponseRuleAction_QuestionnaireItem_QuestionnaireItemID] FOREIGN KEY ([QuestionnaireItemID]) REFERENCES [QuestionnaireItem] ([QuestionnaireItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireDefaultResponseRuleAction_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE TABLE [QuestionnaireDefaultResponseRuleCondition] (
        [QuestionnaireDefaultResponseRuleConditionID] int NOT NULL IDENTITY,
        [QuestionnaireID] int NOT NULL,
        [QuestionnaireItemID] int NOT NULL,
        [ComparisonOperator] nvarchar(max) NULL,
        [ComparisonValue] int NOT NULL,
        [QuestionnaireDefaultResponseRuleID] int NOT NULL,
        [ListOrder] int NOT NULL,
        [JoiningOperator] nvarchar(max) NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_QuestionnaireDefaultResponseRuleCondition] PRIMARY KEY ([QuestionnaireDefaultResponseRuleConditionID]),
        CONSTRAINT [FK_QuestionnaireDefaultResponseRuleCondition_QuestionnaireDefaultResponseRule_QuestionnaireDefaultResponseRuleID] FOREIGN KEY ([QuestionnaireDefaultResponseRuleID]) REFERENCES [QuestionnaireDefaultResponseRule] ([QuestionnaireDefaultResponseRuleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireDefaultResponseRuleCondition_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireDefaultResponseRuleCondition_QuestionnaireItem_QuestionnaireItemID] FOREIGN KEY ([QuestionnaireItemID]) REFERENCES [QuestionnaireItem] ([QuestionnaireItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireDefaultResponseRuleCondition_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRule_QuestionnaireID] ON [QuestionnaireDefaultResponseRule] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRule_UpdateUserID] ON [QuestionnaireDefaultResponseRule] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRuleAction_DefaultResponseID] ON [QuestionnaireDefaultResponseRuleAction] ([DefaultResponseID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRuleAction_QuestionnaireDefaultResponseRuleID] ON [QuestionnaireDefaultResponseRuleAction] ([QuestionnaireDefaultResponseRuleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRuleAction_QuestionnaireItemID] ON [QuestionnaireDefaultResponseRuleAction] ([QuestionnaireItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRuleAction_UpdateUserID] ON [QuestionnaireDefaultResponseRuleAction] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRuleCondition_QuestionnaireDefaultResponseRuleID] ON [QuestionnaireDefaultResponseRuleCondition] ([QuestionnaireDefaultResponseRuleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRuleCondition_QuestionnaireID] ON [QuestionnaireDefaultResponseRuleCondition] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRuleCondition_QuestionnaireItemID] ON [QuestionnaireDefaultResponseRuleCondition] ([QuestionnaireItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireDefaultResponseRuleCondition_UpdateUserID] ON [QuestionnaireDefaultResponseRuleCondition] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210609042646_QuestionnaireDefaultResponse_Changes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210609042646_QuestionnaireDefaultResponse_Changes', N'3.1.4');
END;

GO

