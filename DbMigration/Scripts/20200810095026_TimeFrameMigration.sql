IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810095026_TimeFrameMigration')
BEGIN
    CREATE TABLE [info].[TimeFrame] (
        [TimeFrameID] int NOT NULL IDENTITY,
        [DaysInService] int NOT NULL,
        [Month_Exact] decimal(18,10) NOT NULL,
        [Months_Int] int NOT NULL,
        [Qrts_Exact] decimal(18,10) NOT NULL,
        [Qrts_Int] int NOT NULL,
        [Qrt_Current] int NOT NULL,
        [Yrs_Exact] decimal(18,10) NOT NULL,
        [Years_Int] int NOT NULL,
        [Timeframe_Std] nvarchar(max) NULL,
        CONSTRAINT [PK_TimeFrame] PRIMARY KEY ([TimeFrameID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810095026_TimeFrameMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200810095026_TimeFrameMigration', N'3.1.4');
END;

GO

