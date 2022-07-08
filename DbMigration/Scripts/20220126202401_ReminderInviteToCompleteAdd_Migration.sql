IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    ALTER TABLE [EmailDetail] DROP CONSTRAINT [FK_EmailDetail_PersonSupport_PersonSupportID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    DROP TABLE [SMSDetail];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    DROP INDEX [IX_EmailDetail_PersonSupportID] ON [EmailDetail];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EmailDetail]') AND [c].[name] = N'PersonSupportID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [EmailDetail] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [EmailDetail] DROP COLUMN [PersonSupportID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    CREATE TABLE [ReminderInviteToComplete] (
        [ReminderInviteToCompleteID] bigint NOT NULL IDENTITY,
        [NotifyReminderID] int NOT NULL,
        [AssessmentID] int NOT NULL,
        [InviteToCompleteReceiverID] int NOT NULL,
        [PersonID] bigint NULL,
        [HelperID] int NULL,
        [PersonSupportID] int NULL,
        [Attributes] nvarchar(max) NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [CreatedDate] datetime2 NOT NULL DEFAULT (getdate()),
        [AssessmentURL] nvarchar(max) NULL,
        [TypeOfInviteSend] nvarchar(max) NULL,
        [Email] nvarchar(255) NULL,
        [PhoneNumber] varchar(255) NULL,
        [Status] varchar(50) NOT NULL,
        [UpdateUserId] int NULL,
        CONSTRAINT [PK_ReminderInviteToComplete] PRIMARY KEY ([ReminderInviteToCompleteID]),
        CONSTRAINT [FK_ReminderInviteToComplete_Assessment_AssessmentID] FOREIGN KEY ([AssessmentID]) REFERENCES [Assessment] ([AssessmentID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReminderInviteToComplete_Helper_HelperID] FOREIGN KEY ([HelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReminderInviteToComplete_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReminderInviteToComplete_PersonSupport_PersonSupportID] FOREIGN KEY ([PersonSupportID]) REFERENCES [PersonSupport] ([PersonSupportID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    CREATE INDEX [IX_ReminderInviteToComplete_AssessmentID] ON [ReminderInviteToComplete] ([AssessmentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    CREATE INDEX [IX_ReminderInviteToComplete_HelperID] ON [ReminderInviteToComplete] ([HelperID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    CREATE INDEX [IX_ReminderInviteToComplete_PersonID] ON [ReminderInviteToComplete] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    CREATE INDEX [IX_ReminderInviteToComplete_PersonSupportID] ON [ReminderInviteToComplete] ([PersonSupportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126202401_ReminderInviteToCompleteAdd_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220126202401_ReminderInviteToCompleteAdd_Migration', N'3.1.4');
END;

GO

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

