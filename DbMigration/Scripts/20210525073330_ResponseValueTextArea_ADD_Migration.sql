IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210525073330_ResponseValueTextArea_ADD_Migration')
BEGIN

IF NOT EXISTS(SELECT * FROM info.ResponseValueType where Name = 'TextArea')
BEGIN
  INSERT INTO info.ResponseValueType 
    SELECT 'TextArea',null,null,4,0,getdate(),1;
END
IF NOT EXISTS(SELECT * FROM info.ResponseValueType where Name = 'Date')
BEGIN
  INSERT INTO info.ResponseValueType 
    SELECT 'Date',null,null,5,0,getdate(),1;
END
IF NOT EXISTS(SELECT * FROM info.ResponseValueType where Name = 'Checkbox')
BEGIN
  INSERT INTO info.ResponseValueType 
    SELECT 'Checkbox',null,null,6,0,getdate(),1;
END
END

BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210525073330_ResponseValueTextArea_ADD_Migration', N'3.1.4');
END;

GO

