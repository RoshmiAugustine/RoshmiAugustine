IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211029132351_NotificationViewTimeAdded_Add_Column')
BEGIN
    ALTER TABLE [User] ADD [NotificationViewedOn] datetime2 NULL DEFAULT (getdate());
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20211029132351_NotificationViewTimeAdded_Add_Column')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20211029132351_NotificationViewTimeAdded_Add_Column', N'3.1.4');
END;

GO

