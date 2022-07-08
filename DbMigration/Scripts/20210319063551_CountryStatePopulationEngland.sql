IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210319063551_CountryStatePopulationEngland')
BEGIN
Declare @CountryID INT;
Declare @MaxListCount INT;
----------------------------------------------------------------------------------------------------
SET @MaxListCount=(SELECT max(ListOrder)+1 from info.Country where IsRemoved=0)
----------------------------------------------------------------------------------------------------
INSERT INTO info.Country ( Abbrev, Name, Description, ListOrder, IsRemoved, UpdateDate, UpdateUserID, CountryCode) VALUES
( 'GB', 'ENGLAND', 'England', @MaxListCount, 0, getdate(),1, +44);
----------------------------------------------------------------------------------------------------
SET @CountryID = (select countryID from info.country where name ='ENGLAND' and isremoved=0);
----------------------------------------------------------------------------------------------------

INSERT INTO info.CountryState( CountryID,Name,Abbrev,Description,ListOrder,IsRemoved,UpdateDate,UpdateUserID)VALUES
( @CountryID, 'Bedfordshire', 'Beds','	County of Bedford',1, 0, getdate(),1),
( @CountryID, 'Berkshire', 'Berks','County of Berks',2, 0,getdate(),1 ),
( @CountryID, 'Buckinghamshire', 'Bucks','County of Buckingham',3, 0, getdate(),1),
( @CountryID, 'Cambridgeshire', 'Cambs','County of Cambridge',4, 0,getdate(),1),
( @CountryID, 'Cheshire', 'Ches','County of Chester',5, 0,getdate(),1),
( @CountryID, 'Cornwall', 'Corn','County of Cornwall',6, 0, getdate(),1),
( @CountryID, 'Cumberland', 'Cumb','County of Cumberland',7, 0,getdate(),1 ),
( @CountryID, 'Derbyshire', 'Derbs','County of Derby', 8, 0, getdate(),1),
( @CountryID, 'Devon', 'Dev','Devonshire', 9, 0,getdate(),1),
( @CountryID, 'Dorset', 'Dor','Dorsetshire', 10, 0,getdate(),1),
( @CountryID, 'Durham', 'Co Dur','County of Durham',11, 0, getdate(),1),
( @CountryID, 'Essex', 'Essex','County of Essex',12, 0,getdate(),1 ),
( @CountryID, 'Gloucestershire', 'Gloucs','County of Gloucester',13, 0, getdate(),1),
( @CountryID, 'Hampshire', 'Hants','County of Southampton
Southamptonshire',14, 0,getdate(),1),
( @CountryID, 'Herefordshire','Here','County of Hereford',15, 0,getdate(),1),
( @CountryID, 'Hertfordshire','Herts',	'County of Hertford', 16, 0, getdate(),1),
( @CountryID, 'Huntingdonshire'	,'Hunts','County of Huntingdon'	,17, 0,getdate(),1 ),
( @CountryID, 'Kent','kent','County of kent', 18, 0, getdate(),1),
( @CountryID,'Leicestershire','Leics','County of Leicester',19, 0,getdate(),1),
( @CountryID, 'Middlesex','Mx',	'England Genealogy',20, 0, getdate(),1),
( @CountryID, 'Norfolk'	,'Norf','County of Norfolk',21, 0,getdate(),1 ),
( @CountryID, 'Northamptonshire','Northants','County of Northampton', 22, 0, getdate(),1),
( @CountryID, 'Northumberland','Northd','Northumb',23, 0, getdate(),1),
( @CountryID, 'Nottinghamshire','Notts','County of Nottingham', 24, 0, getdate(),1),
( @CountryID, 'Oxfordshire','Oxon','County of Oxford',25, 0, getdate(),1),
( @CountryID, 'Rutland','Rut','Rutlandshire',26, 0, getdate(),1),
( @CountryID, 'Shropshire','Salop','County of Salop	Shrops',27, 0, getdate(),1),
( @CountryID, 'Somerset','Som','Somersetshire',28, 0, getdate(),1),
( @CountryID, 'Staffordshire','Staf','County of Stafford',29, 0, getdate(),1),
( @CountryID, 'Suffolk','Suff','Suffolk',30, 0, getdate(),1),
( @CountryID, 'Surrey','Sy','Surrey',31, 0, getdate(),1),
( @CountryID, 'Sussex','Sx','Sussex',32, 0, getdate(),1),
( @CountryID, 'Warwickshire','Warw','County of Warwick	Warks',33, 0, getdate(),1),
( @CountryID, 'Westmorland','Westm','Westmorland',34, 0, getdate(),1),
( @CountryID, 'Wiltshire','Wilts','County of Wilts',35, 0, getdate(),1),
( @CountryID, 'Worcestershire','Worsts','County of Worcester',36, 0, getdate(),1),
( @CountryID, 'Yorkshire','Yorks','County of York',37, 0, getdate(),1);
		
----------------------------------------------------------------------------------------------------------------------
update info.countrystate set  countryID= (select countryID from info.country where name ='UNITED STATES' and isremoved=0) where countryID=226
----------------------------------------------------------------------------------------------------------------------

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210319063551_CountryStatePopulationEngland', N'3.1.4');
END;

GO

