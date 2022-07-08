IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121081056_ReminderReccurenceColumnChanges')
BEGIN
    DECLARE  @ConstraintName nvarchar(200) 
    IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireWindow' AND COLUMN_NAME = 'OpenOffsetTypeID')
    BEGIN
    SET  @ConstraintName = '';
    SELECT @ConstraintName = CONSTRAINT_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireWindow' AND COLUMN_NAME = 'OpenOffsetTypeID'
    EXEC('ALTER TABLE QuestionnaireWindow DROP CONSTRAINT ' + @ConstraintName)
    END
    
    IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireWindow' AND COLUMN_NAME = 'CloseOffsetTypeID')
    BEGIN
    SET  @ConstraintName = '';
    SELECT @ConstraintName = CONSTRAINT_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireWindow' AND COLUMN_NAME = 'CloseOffsetTypeID'
    EXEC('ALTER TABLE QuestionnaireWindow DROP CONSTRAINT ' + @ConstraintName)
    END
    IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireReminderRule' AND COLUMN_NAME = 'ReminderOffsetTypeID')
    BEGIN
    SET  @ConstraintName = '';
    SELECT @ConstraintName = CONSTRAINT_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireReminderRule' AND COLUMN_NAME = 'ReminderOffsetTypeID'
    EXEC('ALTER TABLE QuestionnaireReminderRule DROP CONSTRAINT ' + @ConstraintName)
    END
END
GO

BEGIN
 IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_QuestionnaireWindow_CloseOffsetTypeID' AND object_id = OBJECT_ID('dbo.QuestionnaireWindow'))>0
 BEGIN
	DROP INDEX [IX_QuestionnaireWindow_CloseOffsetTypeID] ON [dbo].[QuestionnaireWindow]
 END
END

BEGIN
 IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_QuestionnaireWindow_OpenOffsetTypeID' AND object_id = OBJECT_ID('dbo.QuestionnaireWindow'))>0
 BEGIN
	DROP INDEX [IX_QuestionnaireWindow_OpenOffsetTypeID] ON [dbo].[QuestionnaireWindow]
 END
END

BEGIN
 IF (SELECT COUNT(*) FROM sys.indexes  WHERE name='IX_QuestionnaireReminderRule_ReminderOffsetTypeID' AND object_id = OBJECT_ID('dbo.QuestionnaireReminderRule'))>0
 BEGIN
	DROP INDEX [IX_QuestionnaireReminderRule_ReminderOffsetTypeID] ON [dbo].[QuestionnaireReminderRule]
 END
END

ALTER TABLE [info].[OffsetType] DROP CONSTRAINT PK_OffsetType

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121081056_ReminderReccurenceColumnChanges')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[info].[OffsetType]') AND [c].[name] = N'OffsetTypeID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [info].[OffsetType] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [info].[OffsetType] ALTER COLUMN [OffsetTypeID] char(1) NOT NULL;
    ALTER TABLE [info].[OffsetType] ADD DEFAULT 'd' FOR [OffsetTypeID];
END;

ALTER TABLE [info].[OffsetType] ADD CONSTRAINT PK_OffsetType PRIMARY KEY ([OffsetTypeID]);
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121081056_ReminderReccurenceColumnChanges')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireWindow]') AND [c].[name] = N'OpenOffsetTypeID');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireWindow] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [QuestionnaireWindow] ALTER COLUMN [OpenOffsetTypeID] char(1) NULL;
    ALTER TABLE [QuestionnaireWindow] ADD DEFAULT 'd' FOR [OpenOffsetTypeID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121081056_ReminderReccurenceColumnChanges')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireWindow]') AND [c].[name] = N'CloseOffsetTypeID');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireWindow] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [QuestionnaireWindow] ALTER COLUMN [CloseOffsetTypeID] char(1) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121081056_ReminderReccurenceColumnChanges')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionnaireReminderRule]') AND [c].[name] = N'ReminderOffsetTypeID');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [QuestionnaireReminderRule] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [QuestionnaireReminderRule] ALTER COLUMN [ReminderOffsetTypeID] char(1) NULL;
    ALTER TABLE [QuestionnaireReminderRule] ADD DEFAULT 'd' FOR [ReminderOffsetTypeID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireWindow_CloseOffsetTypeID] ON [QuestionnaireWindow] ([CloseOffsetTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireWindow_OpenOffsetTypeID] ON [QuestionnaireWindow] ([OpenOffsetTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220120175743_RegularReminderRecurrenceDBChanges_Migration')
BEGIN
    CREATE INDEX [IX_QuestionnaireReminderRule_ReminderOffsetTypeID] ON [QuestionnaireReminderRule] ([ReminderOffsetTypeID]);
END;



IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121081056_ReminderReccurenceColumnChanges')
BEGIN
    IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireWindow' AND COLUMN_NAME = 'OpenOffsetTypeID')
    BEGIN
      ALTER TABLE [QuestionnaireWindow] ADD CONSTRAINT [FK_QuestionnaireWindow_OffsetType_OpenOffsetTypeID] FOREIGN KEY ([OpenOffsetTypeID]) REFERENCES [info].[OffsetType] ([OffsetTypeID]) ON DELETE NO ACTION;
    END
    
    IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireWindow' AND COLUMN_NAME = 'CloseOffsetTypeID')
    BEGIN
       ALTER TABLE [QuestionnaireWindow] ADD CONSTRAINT [FK_QuestionnaireWindow_OffsetType_CloseOffsetTypeID] FOREIGN KEY ([CloseOffsetTypeID]) REFERENCES [info].[OffsetType] ([OffsetTypeID]) ON DELETE NO ACTION;
    END
    
    IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE where TABLE_NAME = 'QuestionnaireReminderRule' AND COLUMN_NAME = 'ReminderOffsetTypeID')
    BEGIN
      ALTER TABLE [QuestionnaireReminderRule] ADD CONSTRAINT [FK_QuestionnaireReminderRule_OffsetType_ReminderOffsetTypeID] FOREIGN KEY ([ReminderOffsetTypeID]) REFERENCES [info].[OffsetType] ([OffsetTypeID]) ON DELETE NO ACTION;
    END
END
GO

IF EXISTS( SELECT c.name FROM  sys.columns c JOIN sys.tables  t   ON c.object_id = t.object_id WHERE t.name LIKE 'PersonQuestionnaireSchedule' and c.name = 'UniqueCounter')
BEGIN
EXEC sp_RENAME 'PersonQuestionnaireSchedule.UniqueCounter' , 'OccurrenceCounter', 'COLUMN'
END


IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220121081056_ReminderReccurenceColumnChanges')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220121081056_ReminderReccurenceColumnChanges', N'3.1.4');
END;

GO

