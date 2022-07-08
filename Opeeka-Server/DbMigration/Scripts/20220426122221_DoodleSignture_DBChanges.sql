IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220426122221_DoodleSignture_DBChanges')
BEGIN
    IF NOT EXISTS(SELECT * FROM info.ResponseValueType where Name = 'Doodle')
    BEGIN
      INSERT INTO info.ResponseValueType 
        SELECT 'Doodle',null,null,7,0,getdate(),1;
    END
    IF NOT EXISTS(SELECT * FROM info.ResponseValueType where Name = 'Signature')
    BEGIN
      INSERT INTO info.ResponseValueType 
        SELECT 'Signature',null,null,8,0,getdate(),1;
    END
END

BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220426122221_DoodleSignture_DBChanges', N'3.1.4');
END;

GO

