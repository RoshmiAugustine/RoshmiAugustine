DECLARE @aoID INT = (SELECT ApplicationObjectID FROM ApplicationObject WHERE NAME='/api/assessment-review-status')

IF ISNULL(@aoID,0)>0
BEGIN
	DECLARE @permID INT = (SELECT PermissionID FROM info.Permission WHERE ApplicationObjectID=@aoID)
	IF ISNULL(@permID,0)>0
	BEGIN
		INSERT INTO info.SystemRolePermission(SystemRoleID,PermissionID)
		SELECT 3,@permID UNION SELECT 6,@permID
	END
END

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200924174912_API_PERM_AssessmentApproveReject_RORoles')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200924174912_API_PERM_AssessmentApproveReject_RORoles', N'3.1.4');
END;

GO

