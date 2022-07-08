IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220511060810_Add_AssessmentAttachmentTable')
BEGIN
    CREATE TABLE [AssessmentResponseAttachment] (
        [AssessmentResponseAttachmentID] int NOT NULL IDENTITY,
        [AssessmentResponseID] int NOT NULL,
        [BlobURL] nvarchar(max) NULL,
        [FileName] nvarchar(max) NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AddedByVoiceTypeID] int NULL,
        [VoiceTypeFKID] bigint NULL,
        [AssessmentResponseFileGUID] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_AssessmentResponseAttachment] PRIMARY KEY ([AssessmentResponseAttachmentID]),
        CONSTRAINT [FK_AssessmentResponseAttachment_AssessmentResponse_AssessmentResponseID] FOREIGN KEY ([AssessmentResponseID]) REFERENCES [AssessmentResponse] ([AssessmentResponseID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentResponseAttachment_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220511060810_Add_AssessmentAttachmentTable')
BEGIN
    CREATE INDEX [IX_AssessmentResponseAttachment_AssessmentResponseID] ON [AssessmentResponseAttachment] ([AssessmentResponseID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220511060810_Add_AssessmentAttachmentTable')
BEGIN
    CREATE INDEX [IX_AssessmentResponseAttachment_UpdateUserID] ON [AssessmentResponseAttachment] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220511060810_Add_AssessmentAttachmentTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220511060810_Add_AssessmentAttachmentTable', N'3.1.4');
END;

GO

