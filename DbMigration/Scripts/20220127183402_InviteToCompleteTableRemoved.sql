IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220127183402_InviteToCompleteTableRemoved')
BEGIN
    DROP TABLE [QuestionnaireInviteToCompleteReceiver];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220127183402_InviteToCompleteTableRemoved')
BEGIN
    ALTER TABLE [ReminderInviteToComplete] ADD [QuestionnaireID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220127183402_InviteToCompleteTableRemoved')
BEGIN
    ALTER TABLE [Questionnaire] ADD [InviteToCompleteReceiverIds] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220127183402_InviteToCompleteTableRemoved')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220127183402_InviteToCompleteTableRemoved', N'3.1.4');
END;

GO

