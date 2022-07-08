IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200923131549_EmailAssessmentConfigChanges')
BEGIN
--------FromEmailID and From DisplayName Changes in Configuration for ApplicationLevel Entry------------
    update info.[Configuration] SET value = 'info@pcis-dev.com' where Value = 'ajib.ck@naicoits.com';
    update info.[Configuration] SET value = 'P-CIS' where Value = 'PCIS-Agency1';
--------------------------------------------------------------------------------------------------------

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200923131549_EmailAssessmentConfigChanges', N'3.1.4');
END;

GO

