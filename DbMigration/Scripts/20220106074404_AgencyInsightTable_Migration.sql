IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220106074404_AgencyInsightTable_Migration')
BEGIN
    CREATE TABLE [AgencyInsightDashboard] (
        [AgencyInsightDashboardId] int NOT NULL IDENTITY,
        [DashboardId] int NOT NULL,
        [AgencyId] bigint NOT NULL,
        [Name] nvarchar(500) NULL,
        [Filters] nvarchar(max) NULL,
        [CustomFilters] nvarchar(max) NULL,
        [ShortDescription] nvarchar(500) NULL,
        [LongDescription] nvarchar(max) NULL,
        [IconURL] nvarchar(500) NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_AgencyInsightDashboard] PRIMARY KEY ([AgencyInsightDashboardId]),
        CONSTRAINT [FK_AgencyInsightDashboard_Agency_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220106074404_AgencyInsightTable_Migration')
BEGIN
    CREATE INDEX [IX_AgencyInsightDashboard_AgencyId] ON [AgencyInsightDashboard] ([AgencyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220106074404_AgencyInsightTable_Migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220106074404_AgencyInsightTable_Migration', N'3.1.4');
END;

GO

