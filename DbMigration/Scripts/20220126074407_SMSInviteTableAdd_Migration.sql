IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126074407_SMSInviteTableAdd_Migration')
BEGIN
    ALTER TABLE [AssessmentEmailLinkDetails] ADD [AssessmentGUID] uniqueidentifier NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126074407_SMSInviteTableAdd_Migration')
BEGIN
    ALTER TABLE [Assessment] ADD [NotifyReminderID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126074407_SMSInviteTableAdd_Migration')
BEGIN
    CREATE TABLE [SMSDetail] (
        [SMSDetailID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NULL,
        [HelperID] int NULL,
        [AgencyID] bigint NULL,
        [PhoneNumber] varchar(255) NOT NULL,
        [SMSAttributes] nvarchar(max) NULL,
        [Status] varchar(50) NOT NULL,
        [UpdateUserID] int NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [URL] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [FKeyValue] int NULL,
        CONSTRAINT [PK_SMSDetail] PRIMARY KEY ([SMSDetailID]),
        CONSTRAINT [FK_SMSDetail_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SMSDetail_Helper_HelperID] FOREIGN KEY ([HelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SMSDetail_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126074407_SMSInviteTableAdd_Migration')
BEGIN
    CREATE INDEX [IX_SMSDetail_AgencyID] ON [SMSDetail] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126074407_SMSInviteTableAdd_Migration')
BEGIN
    CREATE INDEX [IX_SMSDetail_HelperID] ON [SMSDetail] ([HelperID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126074407_SMSInviteTableAdd_Migration')
BEGIN
    CREATE INDEX [IX_SMSDetail_PersonID] ON [SMSDetail] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220126074407_SMSInviteTableAdd_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220126074407_SMSInviteTableAdd_Migration', N'3.1.4');
END;

GO

