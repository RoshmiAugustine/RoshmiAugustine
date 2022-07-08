IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200917052612_AssessmentEmailOtpEntity')
BEGIN
    CREATE TABLE [AssessmentEmailOtp] (
        [AssessmentEmailOtpID] int NOT NULL IDENTITY,
        [AssessmentEmailLinkDetailsID] int NOT NULL,
        [Otp] nvarchar(max) NULL,
        [ExpiryTime] datetime2 NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [AssessmentEmailOtpID] PRIMARY KEY ([AssessmentEmailOtpID]),
        CONSTRAINT [FK_AssessmentEmailOtp_AssessmentEmailLinkDetails_AssessmentEmailLinkDetailsID] FOREIGN KEY ([AssessmentEmailLinkDetailsID]) REFERENCES [AssessmentEmailLinkDetails] ([AssessmentEmailLinkDetailsID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200917052612_AssessmentEmailOtpEntity')
BEGIN
    CREATE INDEX [IX_AssessmentEmailOtp_AssessmentEmailLinkDetailsID] ON [AssessmentEmailOtp] ([AssessmentEmailLinkDetailsID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200917052612_AssessmentEmailOtpEntity')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200917052612_AssessmentEmailOtpEntity', N'3.1.4');
END;

GO

