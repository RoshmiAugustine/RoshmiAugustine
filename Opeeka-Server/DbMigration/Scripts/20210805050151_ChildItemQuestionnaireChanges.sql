IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805050151_ChildItemQuestionnaireChanges')
BEGIN
    ALTER TABLE [QuestionnaireSkipLogicRuleAction] ADD [ParentItemID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805050151_ChildItemQuestionnaireChanges')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleAction_ParentItemID] ON [QuestionnaireSkipLogicRuleAction] ([ParentItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805050151_ChildItemQuestionnaireChanges')
BEGIN
    ALTER TABLE [QuestionnaireSkipLogicRuleAction] ADD CONSTRAINT [FK_QuestionnaireSkipLogicRuleAction_Item_ParentItemID] FOREIGN KEY ([ParentItemID]) REFERENCES [Item] ([ItemID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210805050151_ChildItemQuestionnaireChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210805050151_ChildItemQuestionnaireChanges', N'3.1.4');
END;

GO

