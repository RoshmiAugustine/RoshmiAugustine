

INSERT INTO [info].[VoiceType]
           ([Name]
           ,[Abbrev]
           ,[Description]
           ,[ListOrder]
           ,[IsRemoved]
           ,[UpdateDate]
           ,[UpdateUserID])
     VALUES
           ('Helper',null,null,4,0,getdate(),1)


IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930133007_Assessment_VoiceTypeFKID_Column')
BEGIN
    ALTER TABLE [Assessment] ADD [VoiceTypeFKID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200930133007_Assessment_VoiceTypeFKID_Column')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200930133007_Assessment_VoiceTypeFKID_Column', N'3.1.4');
END;

GO

