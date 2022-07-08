IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210318092751_AuditPersonProfile_Migration')
BEGIN
    CREATE TABLE [AuditPersonProfile] (
        [AuditPersonProfileID] int NOT NULL IDENTITY,
        [TypeName] varchar(20) NOT NULL,
        [ParentID] bigint NOT NULL,
        [ChildRecordID] int NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_AuditPersonProfile] PRIMARY KEY ([AuditPersonProfileID]),
        CONSTRAINT [FK_AuditPersonProfile_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210318092751_AuditPersonProfile_Migration')
BEGIN
    CREATE INDEX [IX_AuditPersonProfile_UpdateUserID] ON [AuditPersonProfile] ([UpdateUserID]);
END;

GO
update info.Instrument set InstrumentUrl = '' where InstrumentUrl = '../../../../assets/icons/Logo.png';

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210318092751_AuditPersonProfile_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210318092751_AuditPersonProfile_Migration', N'3.1.4');
END;

GO

