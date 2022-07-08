IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220123134534_InviteToCompleteAbbrevRemoved')
BEGIN
    ALTER TABLE [QuestionnaireRegularReminderRecurrence] DROP CONSTRAINT [FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220123134534_InviteToCompleteAbbrevRemoved')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[info].[InviteToCompleteReceiver]') AND [c].[name] = N'Abbrev');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [info].[InviteToCompleteReceiver] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [info].[InviteToCompleteReceiver] DROP COLUMN [Abbrev];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220123134534_InviteToCompleteAbbrevRemoved')
BEGIN
    ALTER TABLE [QuestionnaireRegularReminderRecurrence] ADD CONSTRAINT [FK_QuestionnaireRegularReminderRecurrence_QuestionnaireWindow_QuestionnaireWindowID] FOREIGN KEY ([QuestionnaireWindowID]) REFERENCES [QuestionnaireWindow] ([QuestionnaireWindowID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220123134534_InviteToCompleteAbbrevRemoved')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220123134534_InviteToCompleteAbbrevRemoved', N'3.1.4');
END;

GO

