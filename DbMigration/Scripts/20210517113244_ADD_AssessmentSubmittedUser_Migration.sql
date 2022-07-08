IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517113244_ADD_AssessmentSubmittedUser_Migration')
BEGIN
    ALTER TABLE [NotificationLog] ADD [HelperName] varchar(500) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517113244_ADD_AssessmentSubmittedUser_Migration')
BEGIN
    ALTER TABLE [Assessment] ADD [SubmittedUserID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517113244_ADD_AssessmentSubmittedUser_Migration')
BEGIN
    CREATE INDEX [IX_Assessment_SubmittedUserID] ON [Assessment] ([SubmittedUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517113244_ADD_AssessmentSubmittedUser_Migration')
BEGIN
    ALTER TABLE [Assessment] ADD CONSTRAINT [FK_Assessment_User_SubmittedUserID] FOREIGN KEY ([SubmittedUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210517113244_ADD_AssessmentSubmittedUser_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210517113244_ADD_AssessmentSubmittedUser_Migration', N'3.1.4');
END;

GO

