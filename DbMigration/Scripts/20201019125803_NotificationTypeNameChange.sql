IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201019125803_NotificationTypeNameChange')
BEGIN
    UPDATE info.NotificationType SET Name = 'Risk' where Name = 'Danger'
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201019125803_NotificationTypeNameChange', N'3.1.4');
END;

GO

