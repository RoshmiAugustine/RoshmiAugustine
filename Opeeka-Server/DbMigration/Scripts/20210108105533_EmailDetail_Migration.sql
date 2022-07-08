IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210108105533_EmailDetail_Migration')
BEGIN
    CREATE TABLE [EmailDetail] (
        [EmailDetailID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NULL,
        [HelperID] int NULL,
        [AgencyID] bigint NULL,
        [Email] varchar(255) NOT NULL,
        [EmailAttributes] nvarchar(max) NULL,
        [Status] varchar(50) NOT NULL,
        [UpdateUserID] int NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_EmailDetail] PRIMARY KEY ([EmailDetailID]),
        CONSTRAINT [FK_EmailDetail_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_EmailDetail_Helper_HelperID] FOREIGN KEY ([HelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_EmailDetail_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210108105533_EmailDetail_Migration')
BEGIN
    CREATE INDEX [IX_EmailDetail_AgencyID] ON [EmailDetail] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210108105533_EmailDetail_Migration')
BEGIN
    CREATE INDEX [IX_EmailDetail_HelperID] ON [EmailDetail] ([HelperID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210108105533_EmailDetail_Migration')
BEGIN
    CREATE INDEX [IX_EmailDetail_PersonID] ON [EmailDetail] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210108105533_EmailDetail_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210108105533_EmailDetail_Migration', N'3.1.4');
END;

GO

