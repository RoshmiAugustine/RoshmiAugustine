IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220107061901_InsightDashboard_listOrder')
BEGIN
    ALTER TABLE [AgencyInsightDashboard] ADD [ListOrder] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220107061901_InsightDashboard_listOrder')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220107061901_InsightDashboard_listOrder', N'3.1.4');
END;

GO

