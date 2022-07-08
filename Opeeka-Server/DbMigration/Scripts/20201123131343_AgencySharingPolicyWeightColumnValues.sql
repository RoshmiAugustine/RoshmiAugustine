IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201123131343_AgencySharingPolicyWeightColumnValues')
BEGIN
IF EXISTS(SELECT * FROM [AgencySharingPolicy] WHERE Abbreviation IN ('RO','RW'))
    BEGIN
        Update [AgencySharingPolicy] SET [Weight] = 0 WHERE Abbreviation = 'RO'
        Update [AgencySharingPolicy] SET [Weight] = 1 WHERE Abbreviation = 'RW'
    END
END
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201123131343_AgencySharingPolicyWeightColumnValues', N'3.1.4');
END;

GO

