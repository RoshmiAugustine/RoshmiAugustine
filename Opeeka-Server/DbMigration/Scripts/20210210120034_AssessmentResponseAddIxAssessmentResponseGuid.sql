IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210210120034_AssessmentResponseAddIxAssessmentResponseGuid')
BEGIN
   CREATE INDEX [IX_AssessmentResponse_AssessmentResponseGuid] ON [AssessmentResponse] ([AssessmentResponseGuid]);
    CREATE INDEX [IX_Note_NoteGuid] ON [Note] ([NoteGuid]);

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210210120034_AssessmentResponseAddIxAssessmentResponseGuid', N'3.1.4');
END;

GO

