IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210112050124_NotificationTypeRiskToAlert')
BEGIN
    UPDATE info.NotificationType SET Name = 'Alert' where Name = 'Risk'
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210112050124_NotificationTypeRiskToAlert', N'3.1.4');
END;

GO

