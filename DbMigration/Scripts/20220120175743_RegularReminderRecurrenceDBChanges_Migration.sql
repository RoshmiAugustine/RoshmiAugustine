IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [QuestionnaireWindow] ADD [CloseOffsetTypeID] nvarchar(1) NULL DEFAULT N'd';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [QuestionnaireWindow] ADD [OpenOffsetTypeID] nvarchar(1) NULL DEFAULT N'd';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [QuestionnaireWindow] ADD [UpdateDate] datetime2 NOT NULL DEFAULT (getdate());
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [QuestionnaireReminderRule] ADD [ReminderOffsetTypeID] nvarchar(1) NULL DEFAULT N'd';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [QuestionnaireReminderRule] ADD [UpdateDate] datetime2 NOT NULL DEFAULT (getdate());
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [Questionnaire] ADD [IsEmailInviteToCompleteReminders] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [NotifyReminder] ADD [InviteToCompleteSentAt] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [info].[InviteToCompleteReceiver] (
        [InviteToCompleteReceiverID] int NOT NULL IDENTITY,
        [Name] nvarchar(15) NOT NULL,
        [Abbrev] nvarchar(15) NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_InviteToCompleteReceiver] PRIMARY KEY ([InviteToCompleteReceiverID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [info].[OffsetType] (
        [OffsetTypeID] nvarchar(1) NOT NULL,
        [Name] nvarchar(15) NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_OffsetType] PRIMARY KEY ([OffsetTypeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [info].[RecurrenceDay] (
        [RecurrenceDayID] int NOT NULL IDENTITY,
        [Name] nvarchar(15) NOT NULL,
        [IsWeekDay] bit NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_RecurrenceDay] PRIMARY KEY ([RecurrenceDayID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [info].[RecurrenceEndType] (
        [RecurrenceEndTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(15) NOT NULL,
        [DisplayLabel] nvarchar(15) NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_RecurrenceEndType] PRIMARY KEY ([RecurrenceEndTypeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [info].[RecurrenceMonth] (
        [RecurrenceMonthID] int NOT NULL IDENTITY,
        [Name] nvarchar(15) NOT NULL,
        [NoOfDays] int NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_RecurrenceMonth] PRIMARY KEY ([RecurrenceMonthID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [info].[RecurrenceOrdinal] (
        [RecurrenceOrdinalID] int NOT NULL IDENTITY,
        [Name] nvarchar(15) NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_RecurrenceOrdinal] PRIMARY KEY ([RecurrenceOrdinalID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [info].[RecurrencePattern] (
        [RecurrencePatternID] int NOT NULL IDENTITY,
        [GroupName] nvarchar(15) NOT NULL,
        [Name] nvarchar(15) NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_RecurrencePattern] PRIMARY KEY ([RecurrencePatternID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [info].[TimeZones] (
        [TimeZonesID] int NOT NULL IDENTITY,
        [Name] nvarchar(15) NOT NULL,
        [Abbrev] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_TimeZones] PRIMARY KEY ([TimeZonesID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [QuestionnaireInviteToCompleteReceiver] (
        [QuestionnaireInviteToCompleteReceiverID] int NOT NULL IDENTITY,
        [QuestionnaireID] int NOT NULL,
        [InviteToCompleteReceiverID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_QuestionnaireInviteToCompleteReceiver] PRIMARY KEY ([QuestionnaireInviteToCompleteReceiverID]),
        CONSTRAINT [FK_QuestionnaireInviteToCompleteReceiver_InviteToCompleteReceiver_InviteToCompleteReceiverID] FOREIGN KEY ([InviteToCompleteReceiverID]) REFERENCES [info].[InviteToCompleteReceiver] ([InviteToCompleteReceiverID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireInviteToCompleteReceiver_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [QuestionnaireRegularReminderRecurrence] (
        [QuestionnaireRegularReminderRecurrenceID] int NOT NULL IDENTITY,
        [QuestionnaireID] int NOT NULL,
        [RecurrencePatternID] int NOT NULL,
        [RecurrenceInterval] int NOT NULL,
        [RecurrenceOrdinalIDs] nvarchar(15) NOT NULL,
        [RecurrenceDayNameIDs] nvarchar(15) NOT NULL,
        [RecurrenceDayNoOfMonth] int NULL,
        [RecurrenceMonthIDs] nvarchar(15) NOT NULL,
        [RecurrenceRangeStartDate] datetime2 NOT NULL,
        [RecurrenceRangeEndTypeID] int NOT NULL,
        [RecurrenceRangeEndDate] datetime2 NULL,
        [RecurrenceRangeEndInNumber] int NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_QuestionnaireRegularReminderRecurrence] PRIMARY KEY ([QuestionnaireRegularReminderRecurrenceID]),
        CONSTRAINT [FK_QuestionnaireRegularReminderRecurrence_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE CASCADE,
        CONSTRAINT [FK_QuestionnaireRegularReminderRecurrence_RecurrencePattern_RecurrencePatternID] FOREIGN KEY ([RecurrencePatternID]) REFERENCES [info].[RecurrencePattern] ([RecurrencePatternID]) ON DELETE CASCADE,
        CONSTRAINT [FK_QuestionnaireRegularReminderRecurrence_RecurrenceEndType_RecurrenceRangeEndTypeID] FOREIGN KEY ([RecurrenceRangeEndTypeID]) REFERENCES [info].[RecurrenceEndType] ([RecurrenceEndTypeID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE TABLE [QuestionnaireRegularReminderTimeRule] (
        [QuestionnaireRegularReminderTimeRuleID] int NOT NULL IDENTITY,
        [QuestionnaireID] int NOT NULL,
        [Time] time NOT NULL,
        [TimeZonesID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_QuestionnaireRegularReminderTimeRule] PRIMARY KEY ([QuestionnaireRegularReminderTimeRuleID]),
        CONSTRAINT [FK_QuestionnaireRegularReminderTimeRule_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE CASCADE,
        CONSTRAINT [FK_QuestionnaireRegularReminderTimeRule_TimeZones_TimeZonesID] FOREIGN KEY ([TimeZonesID]) REFERENCES [info].[TimeZones] ([TimeZonesID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireWindow_CloseOffsetTypeID] ON [QuestionnaireWindow] ([CloseOffsetTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireWindow_OpenOffsetTypeID] ON [QuestionnaireWindow] ([OpenOffsetTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireReminderRule_ReminderOffsetTypeID] ON [QuestionnaireReminderRule] ([ReminderOffsetTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireInviteToCompleteReceiver_InviteToCompleteReceiverID] ON [QuestionnaireInviteToCompleteReceiver] ([InviteToCompleteReceiverID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireInviteToCompleteReceiver_QuestionnaireID] ON [QuestionnaireInviteToCompleteReceiver] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireRegularReminderRecurrence_QuestionnaireID] ON [QuestionnaireRegularReminderRecurrence] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireRegularReminderRecurrence_RecurrencePatternID] ON [QuestionnaireRegularReminderRecurrence] ([RecurrencePatternID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireRegularReminderRecurrence_RecurrenceRangeEndTypeID] ON [QuestionnaireRegularReminderRecurrence] ([RecurrenceRangeEndTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireRegularReminderTimeRule_QuestionnaireID] ON [QuestionnaireRegularReminderTimeRule] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireRegularReminderTimeRule_TimeZonesID] ON [QuestionnaireRegularReminderTimeRule] ([TimeZonesID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [QuestionnaireReminderRule] ADD CONSTRAINT [FK_QuestionnaireReminderRule_OffsetType_ReminderOffsetTypeID] FOREIGN KEY ([ReminderOffsetTypeID]) REFERENCES [info].[OffsetType] ([OffsetTypeID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [QuestionnaireWindow] ADD CONSTRAINT [FK_QuestionnaireWindow_OffsetType_CloseOffsetTypeID] FOREIGN KEY ([CloseOffsetTypeID]) REFERENCES [info].[OffsetType] ([OffsetTypeID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    ALTER TABLE [QuestionnaireWindow] ADD CONSTRAINT [FK_QuestionnaireWindow_OffsetType_OpenOffsetTypeID] FOREIGN KEY ([OpenOffsetTypeID]) REFERENCES [info].[OffsetType] ([OffsetTypeID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220120175743_RegularReminderRecurrenceDBChanges_Migration', N'3.1.4');
END;

GO

