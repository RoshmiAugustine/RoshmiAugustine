IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210322074033_Api_Publish')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210322074033_Api_Publish', N'3.1.4');
END;
BEGIN
--------------------------------------------------------Updating UNITED STATES ------------------------------------------------------------
update info.country set  Name = 'United States of America'  where  Name='UNITED STATES' AND Abbrev='US' AND IsRemoved=0

--------------------------------------------------------Removing UNITED KINGDOM--------------------------------------------------------
update info.country set  IsRemoved = 1  where  Name='UNITED KINGDOM' AND Abbrev='GB' AND IsRemoved=0

--------------------------------------------------------Updating Country Name--------------------------------------------------------------
Update Info.Country set Name=(
select stuff((
	   select ' '+upper(left(T3.V, 1))+lower(stuff(T3.V, 1, 1, ''))
	   from (select cast(replace((select (Name) as '*' for xml path('')), ' ', '<X/>') as xml).query('.')) as T1(X)
		 cross apply T1.X.nodes('text()') as T2(X)
		 cross apply (select T2.X.value('.', 'varchar(max)')) as T3(V)
	   for xml path(''), type
	   ).value('text()[1]', 'varchar(max)'), 1, 1, '')) where IsRemoved=0;
--------------------------------------------------------------------------------------------------------------------------------------------------

END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210324203707_CountryNameUpdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210324203707_CountryNameUpdate', N'3.1.4');
END;

GO

