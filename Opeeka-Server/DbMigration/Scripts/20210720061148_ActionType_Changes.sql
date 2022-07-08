IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210720061148_ActionType_Changes')
BEGIN
    if not exists(select * from [info].[ActionType] WHERE [Name] = 'Child Item')
	BEGIN
        INSERT INTO [info].[ActionType]
               ([Name]
               ,[ListOrder]
               ,[IsRemoved])
         VALUES
               ('Child Item'
               ,3
               ,0)
    END;

END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090813_SkipLogicLookupValues')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210401090813_SkipLogicLookupValues', N'3.1.4');
END;

GO

