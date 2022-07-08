IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516115837_Add_Migration_AgencyPowerBIReport')
BEGIN
    CREATE TABLE [AgencyPowerBIReport] (
        [AgencyPowerBIReportId] int NOT NULL IDENTITY,
        [AgencyId] bigint NOT NULL,
        [InstrumentId] int NOT NULL,
        [ReportName] nvarchar(500) NULL,
        [ReportId] uniqueidentifier NOT NULL,
        [Filters] nvarchar(max) NULL,
        [IsRemoved] bit NOT NULL,
        [ListOrder] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_AgencyPowerBIReport] PRIMARY KEY ([AgencyPowerBIReportId]),
        CONSTRAINT [FK_AgencyPowerBIReport_Agency_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AgencyPowerBIReport_Instrument_InstrumentId] FOREIGN KEY ([InstrumentId]) REFERENCES [info].[Instrument] ([InstrumentID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516115837_Add_Migration_AgencyPowerBIReport')
BEGIN
    CREATE INDEX [IX_AgencyPowerBIReport_AgencyId] ON [AgencyPowerBIReport] ([AgencyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516115837_Add_Migration_AgencyPowerBIReport')
BEGIN
    CREATE INDEX [IX_AgencyPowerBIReport_InstrumentId] ON [AgencyPowerBIReport] ([InstrumentId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220516115837_Add_Migration_AgencyPowerBIReport')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220516115837_Add_Migration_AgencyPowerBIReport', N'3.1.4');
END;

GO

