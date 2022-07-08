IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090813_SkipLogicLookupValues')
BEGIN
    if not exists(select * from [info].[ActionLevel] WHERE [Name] = 'Hide')
	BEGIN
        INSERT INTO [info].[ActionLevel]
           ([Name]
           ,[ListOrder]
           ,[IsRemoved])
     VALUES
           ('Hide'
           ,1
           ,0)
    END;
    if not exists(select * from [info].[ActionType] WHERE [Name] = 'Category')
	BEGIN
         INSERT INTO [info].[ActionType]
               ([Name]
               ,[ListOrder]
               ,[IsRemoved])
         VALUES
               ('Category'
               ,1
               ,0)
    END;
    if not exists(select * from [info].[ActionType] WHERE [Name] = 'Item')
	BEGIN
        INSERT INTO [info].[ActionType]
               ([Name]
               ,[ListOrder]
               ,[IsRemoved])
         VALUES
               ('Item'
               ,2
               ,0)
    END;

END;

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210401090813_SkipLogicLookupValues')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210401090813_SkipLogicLookupValues', N'3.1.4');
END;

