IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210720063520_QuestionnaireSkipLogicRuleAction_Changes')
BEGIN
    ALTER TABLE [QuestionnaireSkipLogicRuleAction] ADD [ChildItemID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210720063520_QuestionnaireSkipLogicRuleAction_Changes')
BEGIN
    CREATE INDEX [IX_QuestionnaireSkipLogicRuleAction_ChildItemID] ON [QuestionnaireSkipLogicRuleAction] ([ChildItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210720063520_QuestionnaireSkipLogicRuleAction_Changes')
BEGIN
    ALTER TABLE [QuestionnaireSkipLogicRuleAction] ADD CONSTRAINT [FK_QuestionnaireSkipLogicRuleAction_Item_ChildItemID] FOREIGN KEY ([ChildItemID]) REFERENCES [Item] ([ItemID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210720063520_QuestionnaireSkipLogicRuleAction_Changes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210720063520_QuestionnaireSkipLogicRuleAction_Changes', N'3.1.4');
END;

GO

