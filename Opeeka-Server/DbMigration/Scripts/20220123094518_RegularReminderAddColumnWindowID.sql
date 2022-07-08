IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220123094518_RegularReminderAddColumnWindowID')
BEGIN
    ALTER TABLE [QuestionnaireRegularReminderRecurrence] ADD [QuestionnaireWindowID] int NULL DEFAULT null;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220123094518_RegularReminderAddColumnWindowID')
BEGIN
    CREATE INDEX [IX_QuestionnaireRegularReminderRecurrence_QuestionnaireWindowID] ON [QuestionnaireRegularReminderRecurrence] ([QuestionnaireWindowID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220123094518_RegularReminderAddColumnWindowID')
BEGIN
    ALTER TABLE [QuestionnaireRegularReminderRecurrence] ADD CONSTRAINT [FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID] FOREIGN KEY ([QuestionnaireWindowID]) REFERENCES [QuestionnaireWindow] ([QuestionnaireWindowID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220123094518_RegularReminderAddColumnWindowID')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220123094518_RegularReminderAddColumnWindowID', N'3.1.4');
END;

GO

