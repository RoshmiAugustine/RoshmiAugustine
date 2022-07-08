IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210318072745_SliderChangeAssessmentResponse')
BEGIN
    ALTER TABLE [AssessmentResponse] ADD [Value] nvarchar(max) NULL;
    

    INSERT INTO [info].[ResponseValueType]
                ([Name]
                ,[Abbrev]
                ,[Description]
                ,[ListOrder]
                ,[IsRemoved]
                ,[UpdateDate]
                ,[UpdateUserID])
            VALUES
                ('Slider'
                ,null
                ,null
                ,3
                ,0
                ,GetDate()
                ,1)
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210318072745_SliderChangeAssessmentResponse')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210318072745_SliderChangeAssessmentResponse', N'3.1.4');
END;

GO

