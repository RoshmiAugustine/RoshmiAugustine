IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200918072505_Genderdatapopulation')
BEGIN

    BEGIN
        TRUNCATE TABLE info.IdentifiedGender

        SET IDENTITY_INSERT info.IdentifiedGender ON
        INSERT INTO [info].[IdentifiedGender]
        (
        [IdentifiedGenderID]
        ,[Name]
        ,[Abbrev]
        ,[Description]
        ,[ListOrder]
        ,[IsRemoved]
        ,[UpdateDate]
        ,[UpdateUserID]
        ,[AgencyID]
        )
        SELECT
        [GenderID]
        ,[Name]
        ,[Abbrev]
        ,[Description]
        ,[ListOrder]
        ,[IsRemoved]
        ,[UpdateDate]
        ,[UpdateUserID]
        ,[AgencyID]
        FROM [info].[Gender]
        SET IDENTITY_INSERT info.IdentifiedGender OFF

        END
    
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200918072505_Genderdatapopulation', N'3.1.4');
END;

GO

