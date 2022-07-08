IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210062958_InstrumentLogo_updateDB')
BEGIN
UPDATE info.Instrument SET InstrumentUrl = '../../../../assets/icons/CANS_Logo.png' WHERE InstrumentID IN ( select InstrumentID from info.Instrument where Abbrev LIKE '%CANS%')

UPDATE info.Instrument SET InstrumentUrl = '../../../../assets/icons/Logo.png' WHERE InstrumentID IN ( select InstrumentID from info.Instrument where Abbrev NOT LIKE '%CANS%')
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210210062958_InstrumentLogo_updateDB', N'3.1.4');
END;

GO

