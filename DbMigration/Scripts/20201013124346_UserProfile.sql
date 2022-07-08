





IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013124346_UserProfile')
BEGIN
    CREATE TABLE [File] (
        [FileID] bigint NOT NULL IDENTITY,
        [AgencyID] bigint NOT NULL,
        [FileName] nvarchar(1000) NULL,
        [AzureFileName] nvarchar(max) NULL,
        [Path] nvarchar(1000) NULL,
        [AzureID] uniqueidentifier NOT NULL,
        [IsTemp] bit NOT NULL,
        CONSTRAINT [PK_File] PRIMARY KEY ([FileID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013124346_UserProfile')
BEGIN
    CREATE TABLE [UserProfile] (
        [UserProfileID] int NOT NULL IDENTITY,
        [UserID] int NOT NULL,
        [ImageFileID] bigint NOT NULL,
        CONSTRAINT [PK_UserProfile] PRIMARY KEY ([UserProfileID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201013124346_UserProfile')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201013124346_UserProfile', N'3.1.4');
END;

GO

