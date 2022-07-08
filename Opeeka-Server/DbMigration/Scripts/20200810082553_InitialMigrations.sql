IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    IF SCHEMA_ID(N'info') IS NULL EXEC(N'CREATE SCHEMA [info];');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AgencySharing] (
        [AgencySharingID] int NOT NULL IDENTITY,
        [AgencySharingIndex] uniqueidentifier NOT NULL DEFAULT (newid()),
        [ReportingUnitID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        [AgencySharingPolicyID] int NULL,
        [HistoricalView] bit NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_AgencySharing] PRIMARY KEY ([AgencySharingID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AgencySharingPolicy] (
        [AgencySharingPolicyID] int NOT NULL IDENTITY,
        [AgencySharingID] int NOT NULL,
        [SharingPolicyID] int NOT NULL,
        CONSTRAINT [PK_AgencySharingPolicy] PRIMARY KEY ([AgencySharingPolicyID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AuditDetails] (
        [Id] int NOT NULL IDENTITY,
        [TableName] nvarchar(max) NOT NULL,
        [DateTime] datetime2 NOT NULL DEFAULT (getdate()),
        [KeyValues] nvarchar(max) NULL,
        [OldValues] nvarchar(max) NULL,
        [NewValues] nvarchar(max) NULL,
        [AuditUser] nvarchar(max) NULL,
        [ReferenceKeyValues] nvarchar(max) NULL,
        [EntityState] nvarchar(max) NULL,
        [Tenant] nvarchar(max) NULL,
        CONSTRAINT [PK_AuditDetails] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AuditFieldName] (
        [FieldName] nvarchar(450) NOT NULL,
        [TableName] nvarchar(max) NOT NULL,
        [Label] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AuditFieldName] PRIMARY KEY ([FieldName])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AuditTableName] (
        [TableName] nvarchar(450) NOT NULL,
        [Label] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_AuditTableName] PRIMARY KEY ([TableName])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [IdentityRole] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Discriminator] nvarchar(max) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        CONSTRAINT [PK_IdentityRole] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [User] (
        [UserID] int NOT NULL IDENTITY,
        [UserIndex] uniqueidentifier NOT NULL DEFAULT (NEWID ( )),
        [UserName] nvarchar(max) NULL,
        [LastLogin] datetime2 NULL DEFAULT (getdate()),
        [Name] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [AgencyID] bigint NULL,
        [AspNetUserID] nvarchar(max) NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([UserID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [WeatherForecasts] (
        [Id] int NOT NULL IDENTITY,
        [WeatherForecastIndex] uniqueidentifier NOT NULL DEFAULT (newid()),
        [Date] datetime2 NOT NULL DEFAULT (getdate()),
        [TemperatureC] int NOT NULL,
        [TemperatureF] int NOT NULL,
        [Summary] nvarchar(max) NOT NULL,
        [Summary1] nvarchar(max) NULL,
        CONSTRAINT [PK_WeatherForecasts] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [dbo].[PersonQuestionnaireMetrics] (
        [PersonQuestionnaireMetricsID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [InstrumentID] int NOT NULL,
        [PersonQuestionnaireID] int NOT NULL,
        [ItemID] int NOT NULL,
        [NeedsEver] int NOT NULL,
        [NeedsIdentified] int NOT NULL,
        [NeedsAddressed] int NOT NULL,
        [NeedsAddressing] int NOT NULL,
        [NeedsImproved] int NOT NULL,
        [StrengthsEver] int NOT NULL,
        [StrengthsIdentified] int NOT NULL,
        [StrengthsBuilt] int NOT NULL,
        [StrengthsBuilding] int NOT NULL,
        [StrengthsImproved] int NOT NULL,
        CONSTRAINT [PK_PersonQuestionnaireMetrics] PRIMARY KEY ([PersonQuestionnaireMetricsID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Attachment] (
        [AttachmentId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Attachments] nvarchar(max) NULL,
        [ContextType] int NOT NULL,
        CONSTRAINT [PK_Attachment] PRIMARY KEY ([AttachmentId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ConfigurationContext] (
        [ConfigurationContextID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ParentContextID] int NULL,
        [EntityName] nvarchar(max) NULL,
        [FKValueRequired] bit NOT NULL,
        CONSTRAINT [PK_ConfigurationContext] PRIMARY KEY ([ConfigurationContextID]),
        CONSTRAINT [FK_ConfigurationContext_ConfigurationContext_ParentContextID] FOREIGN KEY ([ParentContextID]) REFERENCES [info].[ConfigurationContext] ([ConfigurationContextID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ConfigurationValueType] (
        [ConfigurationValueTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_ConfigurationValueType] PRIMARY KEY ([ConfigurationValueTypeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[NotificationMode] (
        [NotificationModeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_NotificationMode] PRIMARY KEY ([NotificationModeID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[SharingPolicy] (
        [SharingPolicyID] int NOT NULL IDENTITY,
        [AccessName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_SharingPolicy] PRIMARY KEY ([SharingPolicyID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [IdentityRoleClaim] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_IdentityRoleClaim] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_IdentityRoleClaim_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [IdentityRole] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Address] (
        [AddressID] bigint NOT NULL IDENTITY,
        [AddressIndex] uniqueidentifier NOT NULL DEFAULT (newid()),
        [Address1] nvarchar(max) NULL,
        [Address2] nvarchar(max) NULL,
        [City] nvarchar(max) NULL,
        [CountryStateId] int NULL,
        [Zip] nvarchar(max) NULL,
        [Zip4] nvarchar(max) NULL,
        [IsPrimary] bit NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_Address] PRIMARY KEY ([AddressID]),
        CONSTRAINT [FK_Address_User_CountryStateId] FOREIGN KEY ([CountryStateId]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Address_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Agency] (
        [AgencyID] bigint NOT NULL IDENTITY,
        [Name] varchar(100) NOT NULL,
        [AgencyIndex] uniqueidentifier NOT NULL DEFAULT (newid()),
        [UpdateUserID] int NOT NULL,
        [IsCustomer] bit NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [Note] varchar(max) NULL,
        [Abbrev] varchar(50) NULL,
        [Phone1] varchar(50) NULL,
        [Phone2] varchar(50) NULL,
        [Email] varchar(255) NULL,
        [ContactLastName] varchar(100) NULL,
        [ContactFirstName] varchar(100) NULL,
        CONSTRAINT [PK_Agency] PRIMARY KEY ([AgencyID]),
        CONSTRAINT [FK_Agency_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [ApplicationObjectType] (
        [ApplicationObjectTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_ApplicationObjectType] PRIMARY KEY ([ApplicationObjectTypeID]),
        CONSTRAINT [FK_ApplicationObjectType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Note] (
        [NoteID] int NOT NULL IDENTITY,
        [NoteText] nvarchar(max) NULL,
        [IsConfidential] bit NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_Note] PRIMARY KEY ([NoteID]),
        CONSTRAINT [FK_Note_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Response] (
        [ResponseID] int NOT NULL IDENTITY,
        [ResponseIndex] uniqueidentifier NOT NULL DEFAULT (NEWID ( )),
        [ItemID] int NOT NULL,
        [BackgroundColorPaletteID] int NULL,
        [Label] nvarchar(max) NULL,
        [KeyCodes] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Value] int NOT NULL,
        [MaxRangeValue] int NULL,
        [ListOrder] int NOT NULL,
        [DefaultItemResponseBehaviorID] int NULL,
        [IsItemResponseBehaviorDisabled] bit NOT NULL DEFAULT CAST(0 AS bit),
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_Response] PRIMARY KEY ([ResponseID]),
        CONSTRAINT [FK_Response_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[AssessmentReason] (
        [AssessmentReasonID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_AssessmentReason] PRIMARY KEY ([AssessmentReasonID]),
        CONSTRAINT [FK_AssessmentReason_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[AssessmentStatus] (
        [AssessmentStatusID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_AssessmentStatus] PRIMARY KEY ([AssessmentStatusID]),
        CONSTRAINT [FK_AssessmentStatus_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Category] (
        [CategoryID] int NOT NULL IDENTITY,
        [CategoryFocusID] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryID]),
        CONSTRAINT [FK_Category_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[CategoryFocus] (
        [CategoryFocusID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_CategoryFocus] PRIMARY KEY ([CategoryFocusID]),
        CONSTRAINT [FK_CategoryFocus_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ColorPalette] (
        [ColorPaletteID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [RGB] nvarchar(max) NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_ColorPalette] PRIMARY KEY ([ColorPaletteID]),
        CONSTRAINT [FK_ColorPalette_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ContactType] (
        [ContactTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_ContactType] PRIMARY KEY ([ContactTypeID]),
        CONSTRAINT [FK_ContactType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Country] (
        [CountryID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [CountryCode] nvarchar(max) NULL,
        CONSTRAINT [PK_Country] PRIMARY KEY ([CountryID]),
        CONSTRAINT [FK_Country_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Instrument] (
        [InstrumentID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Instructions] nvarchar(max) NULL,
        [Authors] nvarchar(max) NULL,
        [Source] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [InstrumentUrl] nvarchar(max) NULL,
        [IconUrl] nvarchar(max) NULL,
        CONSTRAINT [PK_Instrument] PRIMARY KEY ([InstrumentID]),
        CONSTRAINT [FK_Instrument_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ItemResponseType] (
        [ItemResponseTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_ItemResponseType] PRIMARY KEY ([ItemResponseTypeID]),
        CONSTRAINT [FK_ItemResponseType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Language] (
        [LanguageID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_Language] PRIMARY KEY ([LanguageID]),
        CONSTRAINT [FK_Language_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[NotificationResolutionStatus] (
        [NotificationResolutionStatusID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_NotificationResolutionStatus] PRIMARY KEY ([NotificationResolutionStatusID]),
        CONSTRAINT [FK_NotificationResolutionStatus_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[NotificationType] (
        [NotificationTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_NotificationType] PRIMARY KEY ([NotificationTypeID]),
        CONSTRAINT [FK_NotificationType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[OperationType] (
        [OperationTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_OperationType] PRIMARY KEY ([OperationTypeID]),
        CONSTRAINT [FK_OperationType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[PersonScreeningStatus] (
        [PersonScreeningStatusId] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserId] int NOT NULL,
        CONSTRAINT [PK_PersonScreeningStatus] PRIMARY KEY ([PersonScreeningStatusId]),
        CONSTRAINT [FK_PersonScreeningStatus_User_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ResponseValueType] (
        [ResponseValueTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_ResponseValueType] PRIMARY KEY ([ResponseValueTypeID]),
        CONSTRAINT [FK_ResponseValueType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[SystemRole] (
        [SystemRoleID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [IsExternal] bit NOT NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NULL,
        [Weight] int NULL,
        CONSTRAINT [PK_SystemRole] PRIMARY KEY ([SystemRoleID]),
        CONSTRAINT [FK_SystemRole_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[VoiceType] (
        [VoiceTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_VoiceType] PRIMARY KEY ([VoiceTypeID]),
        CONSTRAINT [FK_VoiceType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ConfigurationParameter] (
        [ConfigurationParameterID] int NOT NULL IDENTITY,
        [ConfigurationValueTypeID] int NOT NULL,
        [AgencyId] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [Deprecated] bit NOT NULL,
        [CanModify] bit NOT NULL,
        CONSTRAINT [PK_ConfigurationParameter] PRIMARY KEY ([ConfigurationParameterID]),
        CONSTRAINT [FK_ConfigurationParameter_ConfigurationValueType_ConfigurationValueTypeID] FOREIGN KEY ([ConfigurationValueTypeID]) REFERENCES [info].[ConfigurationValueType] ([ConfigurationValueTypeID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AgencyAddress] (
        [AgencyAddressID] bigint NOT NULL IDENTITY,
        [AgencyID] bigint NOT NULL,
        [AddressID] bigint NOT NULL,
        [UpdateUserID] int NOT NULL,
        [IsPrimary] bit NOT NULL,
        CONSTRAINT [PK_AgencyAddress] PRIMARY KEY ([AgencyAddressID]),
        CONSTRAINT [FK_AgencyAddress_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Address] ([AddressID]) ON DELETE CASCADE,
        CONSTRAINT [FK_AgencyAddress_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AgencyAddress_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AgencySharingHistory] (
        [AgencySharingHistoryID] int NOT NULL IDENTITY,
        [ReportingUnitAgencyID] bigint NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [HistoricalView] bit NOT NULL,
        [RemovedUserID] int NULL,
        [RemovedNoteID] int NULL,
        CONSTRAINT [PK_AgencySharingHistory] PRIMARY KEY ([AgencySharingHistoryID]),
        CONSTRAINT [FK_AgencySharingHistory_Agency_ReportingUnitAgencyID] FOREIGN KEY ([ReportingUnitAgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [IdentityUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [UserID] int NOT NULL,
        [UserIndex] uniqueidentifier NOT NULL,
        [LastLogin] datetime2 NULL,
        [IsActive] bit NOT NULL,
        [AgencyId] bigint NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        CONSTRAINT [PK_IdentityUsers] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_IdentityUsers_Agency_AgencyId] FOREIGN KEY ([AgencyId]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [ReportingUnit] (
        [ReportingUnitID] int NOT NULL IDENTITY,
        [ReportingUnitIndex] uniqueidentifier NOT NULL DEFAULT (newid()),
        [Name] nvarchar(max) NOT NULL,
        [Abbrev] nvarchar(max) NULL,
        [ParentAgencyID] bigint NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateUserID] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        CONSTRAINT [PK_ReportingUnit] PRIMARY KEY ([ReportingUnitID]),
        CONSTRAINT [FK_ReportingUnit_Agency_ParentAgencyID] FOREIGN KEY ([ParentAgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReportingUnit_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[CollaborationLevel] (
        [CollaborationLevelID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_CollaborationLevel] PRIMARY KEY ([CollaborationLevelID]),
        CONSTRAINT [FK_CollaborationLevel_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CollaborationLevel_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[CollaborationTagType] (
        [CollaborationTagTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_CollaborationTagType] PRIMARY KEY ([CollaborationTagTypeID]),
        CONSTRAINT [FK_CollaborationTagType_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CollaborationTagType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Gender] (
        [GenderID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_Gender] PRIMARY KEY ([GenderID]),
        CONSTRAINT [FK_Gender_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Gender_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[HelperTitle] (
        [HelperTitleID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] varchar(50) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_HelperTitle] PRIMARY KEY ([HelperTitleID]),
        CONSTRAINT [FK_HelperTitle_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_HelperTitle_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[IdentificationType] (
        [IdentificationTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_IdentificationType] PRIMARY KEY ([IdentificationTypeID]),
        CONSTRAINT [FK_IdentificationType_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_IdentificationType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[RaceEthnicity] (
        [RaceEthnicityID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_RaceEthnicity] PRIMARY KEY ([RaceEthnicityID]),
        CONSTRAINT [FK_RaceEthnicity_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RaceEthnicity_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Sexuality] (
        [SexualityID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_Sexuality] PRIMARY KEY ([SexualityID]),
        CONSTRAINT [FK_Sexuality_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Sexuality_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[SupportType] (
        [SupportTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_SupportType] PRIMARY KEY ([SupportTypeID]),
        CONSTRAINT [FK_SupportType_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SupportType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[TherapyType] (
        [TherapyTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsResidential] bit NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_TherapyType] PRIMARY KEY ([TherapyTypeID]),
        CONSTRAINT [FK_TherapyType_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_TherapyType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [ApplicationObject] (
        [ApplicationObjectID] int NOT NULL IDENTITY,
        [ApplicationObjectTypeID] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_ApplicationObject] PRIMARY KEY ([ApplicationObjectID]),
        CONSTRAINT [FK_ApplicationObject_ApplicationObjectType_ApplicationObjectTypeID] FOREIGN KEY ([ApplicationObjectTypeID]) REFERENCES [ApplicationObjectType] ([ApplicationObjectTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ApplicationObject_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [ReviewerHistory] (
        [AssessmentReviewHistoryID] int NOT NULL IDENTITY,
        [RecordedDate] datetime2 NULL,
        [StatusFrom] int NOT NULL,
        [StatusTo] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [AssessmentReviewHistoryID] PRIMARY KEY ([AssessmentReviewHistoryID]),
        CONSTRAINT [FK_ReviewerHistory_AssessmentStatus_StatusFrom] FOREIGN KEY ([StatusFrom]) REFERENCES [info].[AssessmentStatus] ([AssessmentStatusID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReviewerHistory_AssessmentStatus_StatusTo] FOREIGN KEY ([StatusTo]) REFERENCES [info].[AssessmentStatus] ([AssessmentStatusID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ReviewerHistory_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Contact] (
        [ContactID] int NOT NULL IDENTITY,
        [ContactTypeID] int NOT NULL,
        [Value] nvarchar(max) NULL,
        [IsPrimary] bit NOT NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_Contact] PRIMARY KEY ([ContactID]),
        CONSTRAINT [FK_Contact_ContactType_ContactTypeID] FOREIGN KEY ([ContactTypeID]) REFERENCES [info].[ContactType] ([ContactTypeID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[CountryState] (
        [CountryStateID] int NOT NULL IDENTITY,
        [CountryID] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_CountryState] PRIMARY KEY ([CountryStateID]),
        CONSTRAINT [FK_CountryState_Country_CountryID] FOREIGN KEY ([CountryID]) REFERENCES [info].[Country] ([CountryID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CountryState_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [InstrumentAgency] (
        [InstrumentAgencyID] int NOT NULL IDENTITY,
        [InstrumentID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_InstrumentAgency] PRIMARY KEY ([InstrumentAgencyID]),
        CONSTRAINT [FK_InstrumentAgency_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_InstrumentAgency_Instrument_InstrumentID] FOREIGN KEY ([InstrumentID]) REFERENCES [info].[Instrument] ([InstrumentID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Questionnaire] (
        [QuestionnaireID] int NOT NULL IDENTITY,
        [InstrumentID] int NOT NULL,
        [AgencyID] bigint NULL,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [ReminderScheduleName] nvarchar(max) NULL,
        [RequiredConfidentialityLanguage] nvarchar(max) NOT NULL DEFAULT N'Confidential',
        [PersonRequestedConfidentialityLanguage] nvarchar(max) NULL,
        [OtherConfidentialityLanguage] nvarchar(max) NULL,
        [IsPubllished] bit NOT NULL,
        [ParentQuestionnaireID] int NULL,
        [IsBaseQuestionnaire] bit NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateUserID] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [OwnerUserID] int NOT NULL,
        CONSTRAINT [PK_Questionnaire] PRIMARY KEY ([QuestionnaireID]),
        CONSTRAINT [FK_Questionnaire_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Questionnaire_Instrument_InstrumentID] FOREIGN KEY ([InstrumentID]) REFERENCES [info].[Instrument] ([InstrumentID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Questionnaire_Questionnaire_ParentQuestionnaireID] FOREIGN KEY ([ParentQuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Questionnaire_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ItemResponseBehavior] (
        [ItemResponseBehaviorID] int NOT NULL IDENTITY,
        [ItemResponseTypeID] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_ItemResponseBehavior] PRIMARY KEY ([ItemResponseBehaviorID]),
        CONSTRAINT [FK_ItemResponseBehavior_ItemResponseType_ItemResponseTypeID] FOREIGN KEY ([ItemResponseTypeID]) REFERENCES [info].[ItemResponseType] ([ItemResponseTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_ItemResponseBehavior_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[NotificationLevel] (
        [NotificationLevelID] int NOT NULL IDENTITY,
        [NotificationTypeID] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [RequireResolution] bit NOT NULL,
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_NotificationLevel] PRIMARY KEY ([NotificationLevelID]),
        CONSTRAINT [FK_NotificationLevel_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationLevel_NotificationType_NotificationTypeID] FOREIGN KEY ([NotificationTypeID]) REFERENCES [info].[NotificationType] ([NotificationTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationLevel_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Item] (
        [ItemID] int NOT NULL IDENTITY,
        [ItemIndex] uniqueidentifier NOT NULL DEFAULT (NEWID ( )),
        [Abbreviation] nvarchar(max) NULL,
        [ItemResponseTypeID] int NOT NULL,
        [Label] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Considerations] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [SupplementalDescription] nvarchar(max) NULL,
        [ResponseValueTypeID] int NOT NULL,
        [ListOrder] int NOT NULL,
        [UseRequiredConfidentiality] bit NOT NULL DEFAULT CAST(0 AS bit),
        [UsePersonRequestedConfidentiality] bit NOT NULL DEFAULT CAST(0 AS bit),
        [UseOtherConfidentiality] bit NOT NULL DEFAULT CAST(0 AS bit),
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_Item] PRIMARY KEY ([ItemID]),
        CONSTRAINT [FK_Item_ItemResponseType_ItemResponseTypeID] FOREIGN KEY ([ItemResponseTypeID]) REFERENCES [info].[ItemResponseType] ([ItemResponseTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Item_ResponseValueType_ResponseValueTypeID] FOREIGN KEY ([ResponseValueTypeID]) REFERENCES [info].[ResponseValueType] ([ResponseValueTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Item_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [UserRole] (
        [UserRoleID] int NOT NULL IDENTITY,
        [UserRoleIndex] uniqueidentifier NOT NULL DEFAULT (NEWID ( )),
        [UserID] int NOT NULL,
        [SystemRoleID] int NOT NULL,
        CONSTRAINT [PK_UserRole] PRIMARY KEY ([UserRoleID]),
        CONSTRAINT [FK_UserRole_SystemRole_SystemRoleID] FOREIGN KEY ([SystemRoleID]) REFERENCES [info].[SystemRole] ([SystemRoleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_UserRole_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ConfigurationParameterContext] (
        [ConfigurationParameterContextID] int NOT NULL IDENTITY,
        [ConfigurationParameterID] int NOT NULL,
        [ConfigurationContextID] int NOT NULL,
        CONSTRAINT [PK_ConfigurationParameterContext] PRIMARY KEY ([ConfigurationParameterContextID]),
        CONSTRAINT [FK_ConfigurationParameterContext_ConfigurationContext_ConfigurationContextID] FOREIGN KEY ([ConfigurationContextID]) REFERENCES [info].[ConfigurationContext] ([ConfigurationContextID]) ON DELETE CASCADE,
        CONSTRAINT [FK_ConfigurationParameterContext_ConfigurationParameter_ConfigurationParameterID] FOREIGN KEY ([ConfigurationParameterID]) REFERENCES [info].[ConfigurationParameter] ([ConfigurationParameterID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [IdentityUserClaim] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_IdentityUserClaim] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_IdentityUserClaim_IdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [IdentityUserLogin] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_IdentityUserLogin] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_IdentityUserLogin_IdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [IdentityUserRole] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_IdentityUserRole] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_IdentityUserRole_IdentityRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [IdentityRole] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_IdentityUserRole_IdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [IdentityUserToken] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_IdentityUserToken] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_IdentityUserToken_IdentityUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [IdentityUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Helper] (
        [HelperID] int NOT NULL IDENTITY,
        [HelperIndex] uniqueidentifier NOT NULL DEFAULT (NEWID ( )),
        [UserID] int NOT NULL,
        [FirstName] varchar(100) NOT NULL,
        [MiddleName] varchar(100) NULL,
        [LastName] varchar(100) NOT NULL,
        [Email] varchar(255) NOT NULL,
        [Phone] varchar(50) NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        [HelperTitleID] int NULL,
        [Phone2] varchar(50) NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [SupervisorHelperID] int NULL,
        [ReviewerID] int NULL,
        CONSTRAINT [PK_Helper] PRIMARY KEY ([HelperID]),
        CONSTRAINT [FK_Helper_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Helper_HelperTitle_HelperTitleID] FOREIGN KEY ([HelperTitleID]) REFERENCES [info].[HelperTitle] ([HelperTitleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Helper_Helper_ReviewerID] FOREIGN KEY ([ReviewerID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Helper_Helper_SupervisorHelperID] FOREIGN KEY ([SupervisorHelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Helper_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Helper_User_UserID] FOREIGN KEY ([UserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Person] (
        [PersonID] bigint NOT NULL IDENTITY,
        [PersonIndex] uniqueidentifier NOT NULL DEFAULT (NEWID ( )),
        [FirstName] varchar(100) NOT NULL,
        [MiddleName] varchar(100) NULL,
        [LastName] varchar(100) NOT NULL,
        [Suffix] varchar(50) NULL,
        [PrimaryLanguageID] int NULL,
        [PreferredLanguageID] int NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [GenderID] int NULL,
        [SexualityID] int NULL,
        [BiologicalSexID] int NULL,
        [Email] varchar(255) NULL,
        [Phone1] varchar(50) NULL,
        [Phone2] varchar(50) NULL,
        [IsActive] bit NULL,
        [PersonScreeningStatusID] int NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateUserID] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [AgencyID] bigint NOT NULL,
        CONSTRAINT [PK_Person] PRIMARY KEY ([PersonID]),
        CONSTRAINT [FK_Person_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Person_Gender_BiologicalSexID] FOREIGN KEY ([BiologicalSexID]) REFERENCES [info].[Gender] ([GenderID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Person_Gender_GenderID] FOREIGN KEY ([GenderID]) REFERENCES [info].[Gender] ([GenderID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Person_PersonScreeningStatus_PersonScreeningStatusID] FOREIGN KEY ([PersonScreeningStatusID]) REFERENCES [info].[PersonScreeningStatus] ([PersonScreeningStatusId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Person_Language_PreferredLanguageID] FOREIGN KEY ([PreferredLanguageID]) REFERENCES [info].[Language] ([LanguageID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Person_Language_PrimaryLanguageID] FOREIGN KEY ([PrimaryLanguageID]) REFERENCES [info].[Language] ([LanguageID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Person_Sexuality_SexualityID] FOREIGN KEY ([SexualityID]) REFERENCES [info].[Sexuality] ([SexualityID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Person_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Collaboration] (
        [CollaborationID] int NOT NULL IDENTITY,
        [CollaborationIndex] uniqueidentifier NOT NULL DEFAULT (newid()),
        [ReportingUnitID] int NOT NULL,
        [TherapyTypeID] int NOT NULL,
        [Name] nvarchar(max) NULL,
        [Note] nvarchar(max) NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [IntervalDays] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateUserID] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [Abbreviation] nvarchar(max) NULL,
        [AgencyID] bigint NOT NULL,
        [CollaborationLeadUserID] int NULL,
        [CollaborationLevelID] int NOT NULL,
        [Code] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Collaboration] PRIMARY KEY ([CollaborationID]),
        CONSTRAINT [FK_Collaboration_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Collaboration_CollaborationLevel_CollaborationLevelID] FOREIGN KEY ([CollaborationLevelID]) REFERENCES [info].[CollaborationLevel] ([CollaborationLevelID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Collaboration_TherapyType_TherapyTypeID] FOREIGN KEY ([TherapyTypeID]) REFERENCES [info].[TherapyType] ([TherapyTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Collaboration_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Permission] (
        [PermissionID] int NOT NULL IDENTITY,
        [ApplicationObjectID] int NOT NULL,
        [OperationTypeID] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_Permission] PRIMARY KEY ([PermissionID]),
        CONSTRAINT [FK_Permission_ApplicationObject_ApplicationObjectID] FOREIGN KEY ([ApplicationObjectID]) REFERENCES [ApplicationObject] ([ApplicationObjectID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Permission_OperationType_OperationTypeID] FOREIGN KEY ([OperationTypeID]) REFERENCES [info].[OperationType] ([OperationTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Permission_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AgencyContact] (
        [AgencyContactID] int NOT NULL IDENTITY,
        [ContactID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        [ListOrder] int NULL,
        CONSTRAINT [PK_AgencyContact] PRIMARY KEY ([AgencyContactID]),
        CONSTRAINT [FK_AgencyContact_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AgencyContact_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [info].[Contact] ([ContactID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [QuestionnaireNotifyRiskSchedule] (
        [QuestionnaireNotifyRiskScheduleID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [QuestionnaireID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_QuestionnaireNotifyRiskSchedule] PRIMARY KEY ([QuestionnaireNotifyRiskScheduleID]),
        CONSTRAINT [FK_QuestionnaireNotifyRiskSchedule_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireNotifyRiskSchedule_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [QuestionnaireWindow] (
        [QuestionnaireWindowID] int NOT NULL IDENTITY,
        [QuestionnaireID] int NOT NULL,
        [AssessmentReasonID] int NOT NULL,
        [DueDateOffsetDays] int NULL,
        [WindowOpenOffsetDays] int NULL,
        [WindowCloseOffsetDays] int NULL,
        [CanRepeat] bit NOT NULL,
        [RepeatIntervalDays] int NULL,
        [IsSelected] bit NOT NULL,
        CONSTRAINT [PK_QuestionnaireWindow] PRIMARY KEY ([QuestionnaireWindowID]),
        CONSTRAINT [FK_QuestionnaireWindow_AssessmentReason_AssessmentReasonID] FOREIGN KEY ([AssessmentReasonID]) REFERENCES [info].[AssessmentReason] ([AssessmentReasonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireWindow_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotificationTemplate] (
        [NotificationTemplateID] int NOT NULL IDENTITY,
        [NotificationLevelID] int NOT NULL,
        [NotificationModeID] int NOT NULL,
        [Description] nvarchar(max) NULL,
        [TemplateText] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        CONSTRAINT [PK_NotificationTemplate] PRIMARY KEY ([NotificationTemplateID]),
        CONSTRAINT [FK_NotificationTemplate_NotificationLevel_NotificationLevelID] FOREIGN KEY ([NotificationLevelID]) REFERENCES [info].[NotificationLevel] ([NotificationLevelID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationTemplate_NotificationMode_NotificationModeID] FOREIGN KEY ([NotificationModeID]) REFERENCES [info].[NotificationMode] ([NotificationModeID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[QuestionnaireReminderType] (
        [QuestionnaireReminderTypeID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Abbrev] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [ListOrder] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [NotificationLevelID] int NOT NULL,
        CONSTRAINT [PK_QuestionnaireReminderType] PRIMARY KEY ([QuestionnaireReminderTypeID]),
        CONSTRAINT [FK_QuestionnaireReminderType_NotificationLevel_NotificationLevelID] FOREIGN KEY ([NotificationLevelID]) REFERENCES [info].[NotificationLevel] ([NotificationLevelID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireReminderType_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [QuestionnaireItem] (
        [QuestionnaireItemID] int NOT NULL IDENTITY,
        [QuestionnaireItemIndex] uniqueidentifier NOT NULL DEFAULT (NEWID ( )),
        [QuestionnaireID] int NOT NULL,
        [CategoryID] int NOT NULL,
        [ItemID] int NOT NULL,
        [IsOptional] bit NOT NULL DEFAULT CAST(0 AS bit),
        [CanOverrideLowerResponseBehavior] bit NOT NULL DEFAULT CAST(1 AS bit),
        [CanOverrideMedianResponseBehavior] bit NOT NULL DEFAULT CAST(1 AS bit),
        [CanOverrideUpperResponseBehavior] bit NOT NULL DEFAULT CAST(1 AS bit),
        [LowerItemResponseBehaviorID] int NULL,
        [MedianItemResponseBehaviorID] int NULL,
        [UpperItemResponseBehaviorID] int NULL,
        [IsActive] bit NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [LowerResponseValue] int NULL,
        [UpperResponseValue] int NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        CONSTRAINT [PK_QuestionnaireItem] PRIMARY KEY ([QuestionnaireItemID]),
        CONSTRAINT [FK_QuestionnaireItem_Category_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [info].[Category] ([CategoryID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireItem_Item_ItemID] FOREIGN KEY ([ItemID]) REFERENCES [Item] ([ItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireItem_ItemResponseBehavior_LowerItemResponseBehaviorID] FOREIGN KEY ([LowerItemResponseBehaviorID]) REFERENCES [info].[ItemResponseBehavior] ([ItemResponseBehaviorID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireItem_ItemResponseBehavior_MedianItemResponseBehaviorID] FOREIGN KEY ([MedianItemResponseBehaviorID]) REFERENCES [info].[ItemResponseBehavior] ([ItemResponseBehaviorID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireItem_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireItem_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE CASCADE,
        CONSTRAINT [FK_QuestionnaireItem_ItemResponseBehavior_UpperItemResponseBehaviorID] FOREIGN KEY ([UpperItemResponseBehaviorID]) REFERENCES [info].[ItemResponseBehavior] ([ItemResponseBehaviorID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[Configuration] (
        [ConfigurationID] int NOT NULL IDENTITY,
        [Value] nvarchar(max) NULL,
        [ContextFKValue] int NOT NULL,
        [ConfigurationParameterContextID] int NOT NULL,
        CONSTRAINT [PK_Configuration] PRIMARY KEY ([ConfigurationID]),
        CONSTRAINT [FK_Configuration_ConfigurationParameterContext_ConfigurationParameterContextID] FOREIGN KEY ([ConfigurationParameterContextID]) REFERENCES [info].[ConfigurationParameterContext] ([ConfigurationParameterContextID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [HelperAddress] (
        [HelperAddressID] int NOT NULL IDENTITY,
        [HelperAddressIndex] uniqueidentifier NOT NULL DEFAULT (NEWID ( )),
        [HelperID] int NOT NULL,
        [AddressID] bigint NOT NULL,
        [IsPrimary] bit NOT NULL,
        CONSTRAINT [PK_HelperAddress] PRIMARY KEY ([HelperAddressID]),
        CONSTRAINT [FK_HelperAddress_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Address] ([AddressID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_HelperAddress_Helper_HelperID] FOREIGN KEY ([HelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [HelperContact] (
        [HelperContactID] int NOT NULL IDENTITY,
        [ContactID] int NOT NULL,
        [HelperID] int NOT NULL,
        [ListOrder] int NULL,
        CONSTRAINT [PK_HelperContact] PRIMARY KEY ([HelperContactID]),
        CONSTRAINT [FK_HelperContact_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [info].[Contact] ([ContactID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_HelperContact_Helper_HelperID] FOREIGN KEY ([HelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotificationLog] (
        [NotificationLogID] int NOT NULL IDENTITY,
        [NotificationDate] datetime2 NOT NULL,
        [PersonID] bigint NOT NULL,
        [NotificationTypeID] int NOT NULL,
        [FKeyValue] int NULL,
        [NotificationData] nvarchar(max) NULL,
        [NotificationResolutionStatusID] int NOT NULL,
        [StatusDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_NotificationLog] PRIMARY KEY ([NotificationLogID]),
        CONSTRAINT [FK_NotificationLog_NotificationResolutionStatus_NotificationResolutionStatusID] FOREIGN KEY ([NotificationResolutionStatusID]) REFERENCES [info].[NotificationResolutionStatus] ([NotificationResolutionStatusID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationLog_NotificationType_NotificationTypeID] FOREIGN KEY ([NotificationTypeID]) REFERENCES [info].[NotificationType] ([NotificationTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationLog_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationLog_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonAddress] (
        [PersonAddressID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [AddressID] bigint NOT NULL,
        [IsPrimary] bit NOT NULL,
        CONSTRAINT [PK_PersonAddress] PRIMARY KEY ([PersonAddressID]),
        CONSTRAINT [FK_PersonAddress_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Address] ([AddressID]) ON DELETE CASCADE,
        CONSTRAINT [FK_PersonAddress_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonContact] (
        [PersonContactID] bigint NOT NULL IDENTITY,
        [ContactID] int NOT NULL,
        [PersonID] bigint NOT NULL,
        [ListOrder] int NULL,
        CONSTRAINT [PK_PersonContact] PRIMARY KEY ([PersonContactID]),
        CONSTRAINT [FK_PersonContact_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [info].[Contact] ([ContactID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonContact_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonHelper] (
        [PersonHelperID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [HelperID] int NOT NULL,
        [IsLead] bit NOT NULL,
        [IsCurrent] bit NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateUserID] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_PersonHelper] PRIMARY KEY ([PersonHelperID]),
        CONSTRAINT [FK_PersonHelper_Helper_HelperID] FOREIGN KEY ([HelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonHelper_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonHelper_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonIdentification] (
        [PersonIdentificationID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [IdentificationTypeID] int NOT NULL,
        [IdentificationNumber] varchar(max) NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_PersonIdentification] PRIMARY KEY ([PersonIdentificationID]),
        CONSTRAINT [FK_PersonIdentification_IdentificationType_IdentificationTypeID] FOREIGN KEY ([IdentificationTypeID]) REFERENCES [info].[IdentificationType] ([IdentificationTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonIdentification_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonIdentification_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonLanguage] (
        [PersonID] bigint NOT NULL,
        [LanguageID] int NOT NULL,
        [IsPrimary] bit NOT NULL,
        [IsPreferred] bit NOT NULL,
        CONSTRAINT [PK_PersonLanguage] PRIMARY KEY ([PersonID], [LanguageID]),
        CONSTRAINT [FK_PersonLanguage_Language_LanguageID] FOREIGN KEY ([LanguageID]) REFERENCES [info].[Language] ([LanguageID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonLanguage_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonNote] (
        [PersonNoteID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [NoteID] int NOT NULL,
        CONSTRAINT [PK_PersonNote] PRIMARY KEY ([PersonNoteID]),
        CONSTRAINT [FK_PersonNote_Note_NoteID] FOREIGN KEY ([NoteID]) REFERENCES [Note] ([NoteID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonNote_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonRaceEthnicity] (
        [PersonRaceEthnicityID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [RaceEthnicityID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_PersonRaceEthnicity] PRIMARY KEY ([PersonRaceEthnicityID]),
        CONSTRAINT [FK_PersonRaceEthnicity_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonRaceEthnicity_RaceEthnicity_RaceEthnicityID] FOREIGN KEY ([RaceEthnicityID]) REFERENCES [info].[RaceEthnicity] ([RaceEthnicityID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonSupport] (
        [PersonSupportID] int NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [SupportTypeID] int NOT NULL,
        [IsCurrent] bit NOT NULL,
        [FirstName] varchar(100) NOT NULL,
        [MiddleName] varchar(100) NULL,
        [LastName] varchar(100) NOT NULL,
        [Suffix] varchar(50) NULL,
        [Email] varchar(255) NULL,
        [Phone] varchar(50) NULL,
        [Note] varchar(max) NULL,
        [StartDate] datetime2 NOT NULL DEFAULT (getdate()),
        [EndDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateUserID] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_PersonSupport] PRIMARY KEY ([PersonSupportID]),
        CONSTRAINT [FK_PersonSupport_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonSupport_SupportType_SupportTypeID] FOREIGN KEY ([SupportTypeID]) REFERENCES [info].[SupportType] ([SupportTypeID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonSupport_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [CollaborationAgencyAddress] (
        [CollaborationID] int NOT NULL,
        [AddressID] bigint NOT NULL,
        CONSTRAINT [PK_CollaborationAgencyAddress] PRIMARY KEY ([CollaborationID], [AddressID]),
        CONSTRAINT [FK_CollaborationAgencyAddress_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Address] ([AddressID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CollaborationAgencyAddress_Collaboration_CollaborationID] FOREIGN KEY ([CollaborationID]) REFERENCES [Collaboration] ([CollaborationID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [CollaborationLeadHistory] (
        [CollaborationLeadHistoryID] int NOT NULL IDENTITY,
        [CollaborationID] int NOT NULL,
        [LeadUserID] int NULL,
        [RemovedUserID] int NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_CollaborationLeadHistory] PRIMARY KEY ([CollaborationLeadHistoryID]),
        CONSTRAINT [FK_CollaborationLeadHistory_Collaboration_CollaborationID] FOREIGN KEY ([CollaborationID]) REFERENCES [Collaboration] ([CollaborationID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [CollaborationQuestionnaire] (
        [CollaborationQuestionnaireID] int NOT NULL IDENTITY,
        [CollaborationID] int NOT NULL,
        [QuestionnaireID] int NOT NULL,
        [IsMandatory] bit NOT NULL,
        [StartDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_CollaborationQuestionnaire] PRIMARY KEY ([CollaborationQuestionnaireID]),
        CONSTRAINT [FK_CollaborationQuestionnaire_Collaboration_CollaborationID] FOREIGN KEY ([CollaborationID]) REFERENCES [Collaboration] ([CollaborationID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CollaborationQuestionnaire_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [CollaborationSharing] (
        [CollaborationSharingID] int NOT NULL IDENTITY,
        [CollaborationSharingIndex] uniqueidentifier NOT NULL DEFAULT (newid()),
        [ReportingUnitID] int NOT NULL,
        [AgencyID] bigint NOT NULL,
        [CollaborationID] int NOT NULL,
        [CollaborationSharingPolicyID] int NULL,
        [HistoricalView] bit NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_CollaborationSharing] PRIMARY KEY ([CollaborationSharingID]),
        CONSTRAINT [FK_CollaborationSharing_Agency_AgencyID] FOREIGN KEY ([AgencyID]) REFERENCES [Agency] ([AgencyID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CollaborationSharing_Collaboration_CollaborationID] FOREIGN KEY ([CollaborationID]) REFERENCES [Collaboration] ([CollaborationID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CollaborationSharing_ReportingUnit_ReportingUnitID] FOREIGN KEY ([ReportingUnitID]) REFERENCES [ReportingUnit] ([ReportingUnitID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [CollaborationTag] (
        [CollaborationTagID] int NOT NULL IDENTITY,
        [CollaborationID] int NOT NULL,
        [CollaborationTagTypeID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        CONSTRAINT [PK_CollaborationTag] PRIMARY KEY ([CollaborationTagID]),
        CONSTRAINT [FK_CollaborationTag_Collaboration_CollaborationID] FOREIGN KEY ([CollaborationID]) REFERENCES [Collaboration] ([CollaborationID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CollaborationTag_CollaborationTagType_CollaborationTagTypeID] FOREIGN KEY ([CollaborationTagTypeID]) REFERENCES [info].[CollaborationTagType] ([CollaborationTagTypeID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonCollaboration] (
        [PersonCollaborationID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [CollaborationID] int NOT NULL,
        [EnrollDate] datetime2 NULL,
        [EndDate] datetime2 NULL,
        [IsPrimary] bit NULL,
        [IsCurrent] bit NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateUserID] int NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_PersonCollaboration] PRIMARY KEY ([PersonCollaborationID]),
        CONSTRAINT [FK_PersonCollaboration_Collaboration_CollaborationID] FOREIGN KEY ([CollaborationID]) REFERENCES [Collaboration] ([CollaborationID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonCollaboration_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonCollaboration_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonQuestionnaire] (
        [PersonQuestionnaireID] bigint NOT NULL IDENTITY,
        [PersonID] bigint NOT NULL,
        [QuestionnaireID] int NOT NULL,
        [CollaborationID] int NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDueDate] datetime2 NULL,
        [IsActive] bit NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL,
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_PersonQuestionnaire] PRIMARY KEY ([PersonQuestionnaireID]),
        CONSTRAINT [FK_PersonQuestionnaire_Collaboration_CollaborationID] FOREIGN KEY ([CollaborationID]) REFERENCES [Collaboration] ([CollaborationID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonQuestionnaire_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonQuestionnaire_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonQuestionnaire_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[RolePermission] (
        [RolePermissionID] int NOT NULL IDENTITY,
        [UserRoleID] int NOT NULL,
        [PermissionID] int NOT NULL,
        CONSTRAINT [PK_RolePermission] PRIMARY KEY ([RolePermissionID]),
        CONSTRAINT [FK_RolePermission_Permission_PermissionID] FOREIGN KEY ([PermissionID]) REFERENCES [info].[Permission] ([PermissionID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_RolePermission_UserRole_UserRoleID] FOREIGN KEY ([UserRoleID]) REFERENCES [UserRole] ([UserRoleID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[SystemRolePermission] (
        [SystemRolePermissionID] int NOT NULL IDENTITY,
        [SystemRoleID] int NOT NULL,
        [PermissionID] int NOT NULL,
        CONSTRAINT [PK_SystemRolePermission] PRIMARY KEY ([SystemRolePermissionID]),
        CONSTRAINT [FK_SystemRolePermission_Permission_PermissionID] FOREIGN KEY ([PermissionID]) REFERENCES [info].[Permission] ([PermissionID]) ON DELETE CASCADE,
        CONSTRAINT [FK_SystemRolePermission_SystemRole_SystemRoleID] FOREIGN KEY ([SystemRoleID]) REFERENCES [info].[SystemRole] ([SystemRoleID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [QuestionnaireNotifyRiskRule] (
        [QuestionnaireNotifyRiskRuleID] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [QuestionnaireNotifyRiskScheduleID] int NOT NULL,
        [NotificationLevelID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_QuestionnaireNotifyRiskRule] PRIMARY KEY ([QuestionnaireNotifyRiskRuleID]),
        CONSTRAINT [FK_QuestionnaireNotifyRiskRule_NotificationLevel_NotificationLevelID] FOREIGN KEY ([NotificationLevelID]) REFERENCES [info].[NotificationLevel] ([NotificationLevelID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireNotifyRiskRule_QuestionnaireNotifyRiskSchedule_QuestionnaireNotifyRiskScheduleID] FOREIGN KEY ([QuestionnaireNotifyRiskScheduleID]) REFERENCES [QuestionnaireNotifyRiskSchedule] ([QuestionnaireNotifyRiskScheduleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireNotifyRiskRule_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [QuestionnaireReminderRule] (
        [QuestionnaireReminderRuleID] int NOT NULL IDENTITY,
        [QuestionnaireID] int NOT NULL,
        [QuestionnaireReminderTypeID] int NOT NULL,
        [ReminderOffsetDays] int NULL,
        [CanRepeat] bit NOT NULL,
        [RepeatInterval] int NULL,
        [IsSelected] bit NOT NULL,
        CONSTRAINT [PK_QuestionnaireReminderRule] PRIMARY KEY ([QuestionnaireReminderRuleID]),
        CONSTRAINT [FK_QuestionnaireReminderRule_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireReminderRule_QuestionnaireReminderType_QuestionnaireReminderTypeID] FOREIGN KEY ([QuestionnaireReminderTypeID]) REFERENCES [info].[QuestionnaireReminderType] ([QuestionnaireReminderTypeID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotifyRiskRule] (
        [NotifyRiskRuleID] int NOT NULL IDENTITY,
        [QuestionnaireItemID] int NOT NULL,
        [NotifyThresholdMinimumListOrder] int NOT NULL,
        [NotificationLevelID] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_NotifyRiskRule] PRIMARY KEY ([NotifyRiskRuleID]),
        CONSTRAINT [FK_NotifyRiskRule_NotificationLevel_NotificationLevelID] FOREIGN KEY ([NotificationLevelID]) REFERENCES [info].[NotificationLevel] ([NotificationLevelID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotifyRiskRule_QuestionnaireItem_QuestionnaireItemID] FOREIGN KEY ([QuestionnaireItemID]) REFERENCES [QuestionnaireItem] ([QuestionnaireItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotifyRiskRule_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [QuestionnaireItemHistory] (
        [QuestionnaireItemHistoryID] int NOT NULL IDENTITY,
        [QuestionnaireItemID] int NOT NULL,
        [InactiveStartDate] datetime2 NOT NULL,
        [InactiveEndDate] datetime2 NULL,
        CONSTRAINT [PK_QuestionnaireItemHistory] PRIMARY KEY ([QuestionnaireItemHistoryID]),
        CONSTRAINT [FK_QuestionnaireItemHistory_QuestionnaireItem_QuestionnaireItemID] FOREIGN KEY ([QuestionnaireItemID]) REFERENCES [QuestionnaireItem] ([QuestionnaireItemID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [info].[ConfigurationAttachment] (
        [ConfigurationAttachmentID] int NOT NULL IDENTITY,
        [ConfigurationID] int NOT NULL,
        [AttachmentID] int NOT NULL,
        CONSTRAINT [PK_ConfigurationAttachment] PRIMARY KEY ([ConfigurationAttachmentID]),
        CONSTRAINT [FK_ConfigurationAttachment_Attachment_AttachmentID] FOREIGN KEY ([AttachmentID]) REFERENCES [info].[Attachment] ([AttachmentId]) ON DELETE CASCADE,
        CONSTRAINT [FK_ConfigurationAttachment_Configuration_ConfigurationID] FOREIGN KEY ([ConfigurationID]) REFERENCES [info].[Configuration] ([ConfigurationID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotificationDelivery] (
        [NotificationDeliveryID] int NOT NULL IDENTITY,
        [NotificationLogID] int NOT NULL,
        [DeliveryDate] datetime2 NOT NULL,
        [NotificationTemplateID] int NOT NULL,
        CONSTRAINT [PK_NotificationDelivery] PRIMARY KEY ([NotificationDeliveryID]),
        CONSTRAINT [FK_NotificationDelivery_NotificationLog_NotificationLogID] FOREIGN KEY ([NotificationLogID]) REFERENCES [NotificationLog] ([NotificationLogID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationDelivery_NotificationTemplate_NotificationTemplateID] FOREIGN KEY ([NotificationTemplateID]) REFERENCES [NotificationTemplate] ([NotificationTemplateID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotificationResolutionHistory] (
        [NotificationResolutionHistoryID] int NOT NULL IDENTITY,
        [NotificationLogID] int NOT NULL,
        [RecordedDate] datetime2 NULL,
        [StatusFrom] int NOT NULL,
        [StatusTo] int NOT NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_NotificationResolutionHistory] PRIMARY KEY ([NotificationResolutionHistoryID]),
        CONSTRAINT [FK_NotificationResolutionHistory_NotificationLog_NotificationLogID] FOREIGN KEY ([NotificationLogID]) REFERENCES [NotificationLog] ([NotificationLogID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationResolutionHistory_NotificationResolutionStatus_StatusFrom] FOREIGN KEY ([StatusFrom]) REFERENCES [info].[NotificationResolutionStatus] ([NotificationResolutionStatusID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationResolutionHistory_NotificationResolutionStatus_StatusTo] FOREIGN KEY ([StatusTo]) REFERENCES [info].[NotificationResolutionStatus] ([NotificationResolutionStatusID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationResolutionHistory_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [SupportAddress] (
        [SupportAddressID] int NOT NULL IDENTITY,
        [SupportID] int NOT NULL,
        [AddressID] bigint NOT NULL,
        [IsPrimary] bit NOT NULL,
        CONSTRAINT [PK_SupportAddress] PRIMARY KEY ([SupportAddressID]),
        CONSTRAINT [FK_SupportAddress_Address_AddressID] FOREIGN KEY ([AddressID]) REFERENCES [Address] ([AddressID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SupportAddress_PersonSupport_SupportID] FOREIGN KEY ([SupportID]) REFERENCES [PersonSupport] ([PersonSupportID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [SupportContact] (
        [SupportContactID] int NOT NULL IDENTITY,
        [ContactID] int NOT NULL,
        [SupportID] int NOT NULL,
        [ListOrder] int NULL,
        CONSTRAINT [PK_SupportContact] PRIMARY KEY ([SupportContactID]),
        CONSTRAINT [FK_SupportContact_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [info].[Contact] ([ContactID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_SupportContact_PersonSupport_SupportID] FOREIGN KEY ([SupportID]) REFERENCES [PersonSupport] ([PersonSupportID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [CollaborationSharingHistory] (
        [CollaborationSharingHistoryID] int NOT NULL IDENTITY,
        [ReportingUnitCollaborationID] int NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NULL,
        [HistoricalView] bit NOT NULL,
        [RemovedUserID] int NULL,
        [RemovedNoteID] int NULL,
        CONSTRAINT [PK_CollaborationSharingHistory] PRIMARY KEY ([CollaborationSharingHistoryID]),
        CONSTRAINT [FK_CollaborationSharingHistory_CollaborationSharing_ReportingUnitCollaborationID] FOREIGN KEY ([ReportingUnitCollaborationID]) REFERENCES [CollaborationSharing] ([CollaborationSharingID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [CollaborationSharingPolicy] (
        [CollaborationSharingPolicyID] int NOT NULL IDENTITY,
        [CollaborationSharingID] int NOT NULL,
        [SharingPolicyID] int NOT NULL,
        CONSTRAINT [PK_CollaborationSharingPolicy] PRIMARY KEY ([CollaborationSharingPolicyID]),
        CONSTRAINT [FK_CollaborationSharingPolicy_CollaborationSharing_CollaborationSharingID] FOREIGN KEY ([CollaborationSharingID]) REFERENCES [CollaborationSharing] ([CollaborationSharingID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_CollaborationSharingPolicy_SharingPolicy_SharingPolicyID] FOREIGN KEY ([SharingPolicyID]) REFERENCES [info].[SharingPolicy] ([SharingPolicyID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [PersonQuestionnaireSchedule] (
        [PersonQuestionnaireScheduleID] bigint NOT NULL IDENTITY,
        [PersonQuestionnaireID] bigint NOT NULL,
        [WindowDueDate] datetime2 NOT NULL,
        [QuestionnaireWindowID] int NOT NULL,
        CONSTRAINT [PK_PersonQuestionnaireSchedule] PRIMARY KEY ([PersonQuestionnaireScheduleID]),
        CONSTRAINT [FK_PersonQuestionnaireSchedule_PersonQuestionnaire_PersonQuestionnaireID] FOREIGN KEY ([PersonQuestionnaireID]) REFERENCES [PersonQuestionnaire] ([PersonQuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_PersonQuestionnaireSchedule_QuestionnaireWindow_QuestionnaireWindowID] FOREIGN KEY ([QuestionnaireWindowID]) REFERENCES [QuestionnaireWindow] ([QuestionnaireWindowID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [QuestionnaireNotifyRiskRuleCondition] (
        [QuestionnaireNotifyRiskRuleConditionID] int NOT NULL IDENTITY,
        [QuestionnaireItemID] int NOT NULL,
        [ComparisonOperator] nvarchar(max) NULL,
        [ComparisonValue] int NOT NULL,
        [QuestionnaireNotifyRiskRuleID] int NOT NULL,
        [ListOrder] int NOT NULL,
        [JoiningOperator] nvarchar(max) NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_QuestionnaireNotifyRiskRuleCondition] PRIMARY KEY ([QuestionnaireNotifyRiskRuleConditionID]),
        CONSTRAINT [FK_QuestionnaireNotifyRiskRuleCondition_QuestionnaireItem_QuestionnaireItemID] FOREIGN KEY ([QuestionnaireItemID]) REFERENCES [QuestionnaireItem] ([QuestionnaireItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireNotifyRiskRuleCondition_QuestionnaireNotifyRiskRule_QuestionnaireNotifyRiskRuleID] FOREIGN KEY ([QuestionnaireNotifyRiskRuleID]) REFERENCES [QuestionnaireNotifyRiskRule] ([QuestionnaireNotifyRiskRuleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_QuestionnaireNotifyRiskRuleCondition_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotificationResolutionNote] (
        [NotificationResolutionNoteID] int NOT NULL IDENTITY,
        [NotificationLogID] int NOT NULL,
        [Note_NoteID] int NOT NULL,
        [NotificationResolutionHistoryID] int NULL,
        CONSTRAINT [PK_NotificationResolutionNote] PRIMARY KEY ([NotificationResolutionNoteID]),
        CONSTRAINT [FK_NotificationResolutionNote_Note_Note_NoteID] FOREIGN KEY ([Note_NoteID]) REFERENCES [Note] ([NoteID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationResolutionNote_NotificationLog_NotificationLogID] FOREIGN KEY ([NotificationLogID]) REFERENCES [NotificationLog] ([NotificationLogID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotificationResolutionNote_NotificationResolutionHistory_NotificationResolutionHistoryID] FOREIGN KEY ([NotificationResolutionHistoryID]) REFERENCES [NotificationResolutionHistory] ([NotificationResolutionHistoryID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [Assessment] (
        [AssessmentID] int NOT NULL IDENTITY,
        [PersonQuestionnaireID] bigint NOT NULL,
        [VoiceTypeID] int NOT NULL,
        [DateTaken] datetime2 NOT NULL,
        [ReasoningText] nvarchar(max) NULL,
        [AssessmentReasonID] int NOT NULL,
        [AssessmentStatusID] int NOT NULL,
        [PersonQuestionnaireScheduleID] bigint NULL,
        [IsUpdate] bit NOT NULL,
        [Approved] int NULL,
        [CloseDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_Assessment] PRIMARY KEY ([AssessmentID]),
        CONSTRAINT [FK_Assessment_AssessmentReason_AssessmentReasonID] FOREIGN KEY ([AssessmentReasonID]) REFERENCES [info].[AssessmentReason] ([AssessmentReasonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Assessment_AssessmentStatus_AssessmentStatusID] FOREIGN KEY ([AssessmentStatusID]) REFERENCES [info].[AssessmentStatus] ([AssessmentStatusID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Assessment_PersonQuestionnaire_PersonQuestionnaireID] FOREIGN KEY ([PersonQuestionnaireID]) REFERENCES [PersonQuestionnaire] ([PersonQuestionnaireID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Assessment_PersonQuestionnaireSchedule_PersonQuestionnaireScheduleID] FOREIGN KEY ([PersonQuestionnaireScheduleID]) REFERENCES [PersonQuestionnaireSchedule] ([PersonQuestionnaireScheduleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Assessment_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Assessment_VoiceType_VoiceTypeID] FOREIGN KEY ([VoiceTypeID]) REFERENCES [info].[VoiceType] ([VoiceTypeID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotifyReminder] (
        [NotifyReminderID] int NOT NULL IDENTITY,
        [PersonQuestionnaireScheduleID] bigint NULL,
        [QuestionnaireReminderRuleID] int NOT NULL,
        [NotifyDate] datetime2 NOT NULL,
        CONSTRAINT [PK_NotifyReminder] PRIMARY KEY ([NotifyReminderID]),
        CONSTRAINT [FK_NotifyReminder_PersonQuestionnaireSchedule_PersonQuestionnaireScheduleID] FOREIGN KEY ([PersonQuestionnaireScheduleID]) REFERENCES [PersonQuestionnaireSchedule] ([PersonQuestionnaireScheduleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotifyReminder_QuestionnaireReminderRule_QuestionnaireReminderRuleID] FOREIGN KEY ([QuestionnaireReminderRuleID]) REFERENCES [QuestionnaireReminderRule] ([QuestionnaireReminderRuleID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AssessmentEmailLinkDetails] (
        [AssessmentEmailLinkDetailsID] int NOT NULL IDENTITY,
        [EmailLinkDetailsIndex] uniqueidentifier NOT NULL DEFAULT (newid()),
        [PersonIndex] uniqueidentifier NOT NULL,
        [AssessmentID] int NOT NULL,
        [QuestionnaireID] int NOT NULL,
        [PersonSupportID] int NOT NULL,
        [HelperID] int NOT NULL,
        [PersonSupportEmail] nvarchar(max) NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [AssessmentEmailLinkDetailsID] PRIMARY KEY ([AssessmentEmailLinkDetailsID]),
        CONSTRAINT [FK_AssessmentEmailLinkDetails_Assessment_AssessmentID] FOREIGN KEY ([AssessmentID]) REFERENCES [Assessment] ([AssessmentID]) ON DELETE CASCADE,
        CONSTRAINT [FK_AssessmentEmailLinkDetails_Helper_HelperID] FOREIGN KEY ([HelperID]) REFERENCES [Helper] ([HelperID]) ON DELETE CASCADE,
        CONSTRAINT [FK_AssessmentEmailLinkDetails_PersonSupport_PersonSupportID] FOREIGN KEY ([PersonSupportID]) REFERENCES [PersonSupport] ([PersonSupportID]) ON DELETE CASCADE,
        CONSTRAINT [FK_AssessmentEmailLinkDetails_Questionnaire_QuestionnaireID] FOREIGN KEY ([QuestionnaireID]) REFERENCES [Questionnaire] ([QuestionnaireID]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AssessmentNote] (
        [AssessmentNoteID] int NOT NULL IDENTITY,
        [AssessmentID] int NOT NULL,
        [NoteID] int NOT NULL,
        [AssessmentReviewHistoryID] int NULL,
        CONSTRAINT [PK_AssessmentNote] PRIMARY KEY ([AssessmentNoteID]),
        CONSTRAINT [FK_AssessmentNote_Assessment_AssessmentID] FOREIGN KEY ([AssessmentID]) REFERENCES [Assessment] ([AssessmentID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentNote_ReviewerHistory_AssessmentReviewHistoryID] FOREIGN KEY ([AssessmentReviewHistoryID]) REFERENCES [ReviewerHistory] ([AssessmentReviewHistoryID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentNote_Note_NoteID] FOREIGN KEY ([NoteID]) REFERENCES [Note] ([NoteID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AssessmentResponse] (
        [AssessmentResponseID] int NOT NULL IDENTITY,
        [AssessmentID] int NOT NULL,
        [PersonSupportID] int NULL,
        [ResponseID] int NOT NULL,
        [ItemResponseBehaviorID] int NULL,
        [IsRequiredConfidential] bit NOT NULL,
        [IsPersonRequestedConfidential] bit NULL,
        [IsOtherConfidential] bit NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        [QuestionnaireItemID] int NOT NULL,
        [IsCloned] bit NOT NULL,
        CONSTRAINT [PK_AssessmentResponse] PRIMARY KEY ([AssessmentResponseID]),
        CONSTRAINT [FK_AssessmentResponse_Assessment_AssessmentID] FOREIGN KEY ([AssessmentID]) REFERENCES [Assessment] ([AssessmentID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentResponse_ItemResponseBehavior_ItemResponseBehaviorID] FOREIGN KEY ([ItemResponseBehaviorID]) REFERENCES [info].[ItemResponseBehavior] ([ItemResponseBehaviorID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentResponse_PersonSupport_PersonSupportID] FOREIGN KEY ([PersonSupportID]) REFERENCES [PersonSupport] ([PersonSupportID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentResponse_QuestionnaireItem_QuestionnaireItemID] FOREIGN KEY ([QuestionnaireItemID]) REFERENCES [QuestionnaireItem] ([QuestionnaireItemID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentResponse_Response_ResponseID] FOREIGN KEY ([ResponseID]) REFERENCES [Response] ([ResponseID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentResponse_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotifyRisk] (
        [NotifyRiskID] int NOT NULL IDENTITY,
        [QuestionnaireNotifyRiskRuleID] int NOT NULL,
        [PersonID] bigint NOT NULL,
        [AssessmentID] int NOT NULL,
        [NotifyDate] datetime2 NULL,
        [CloseDate] datetime2 NULL,
        [IsRemoved] bit NOT NULL,
        [UpdateDate] datetime2 NOT NULL DEFAULT (getdate()),
        [UpdateUserID] int NOT NULL,
        CONSTRAINT [PK_NotifyRisk] PRIMARY KEY ([NotifyRiskID]),
        CONSTRAINT [FK_NotifyRisk_Assessment_AssessmentID] FOREIGN KEY ([AssessmentID]) REFERENCES [Assessment] ([AssessmentID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotifyRisk_Person_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [Person] ([PersonID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotifyRisk_QuestionnaireNotifyRiskRule_QuestionnaireNotifyRiskRuleID] FOREIGN KEY ([QuestionnaireNotifyRiskRuleID]) REFERENCES [QuestionnaireNotifyRiskRule] ([QuestionnaireNotifyRiskRuleID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotifyRisk_User_UpdateUserID] FOREIGN KEY ([UpdateUserID]) REFERENCES [User] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AssessmentResponseNote] (
        [AssessmentResponseNoteID] int NOT NULL IDENTITY,
        [AssessmentResponseID] int NOT NULL,
        [NoteID] int NOT NULL,
        CONSTRAINT [PK_AssessmentResponseNote] PRIMARY KEY ([AssessmentResponseNoteID]),
        CONSTRAINT [FK_AssessmentResponseNote_AssessmentResponse_AssessmentResponseID] FOREIGN KEY ([AssessmentResponseID]) REFERENCES [AssessmentResponse] ([AssessmentResponseID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_AssessmentResponseNote_Note_NoteID] FOREIGN KEY ([NoteID]) REFERENCES [Note] ([NoteID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [AssessmentResponseText] (
        [AssessmentResponseTextID] int NOT NULL IDENTITY,
        [ResponseText] varchar(max) NULL,
        [AssessmentResponseID] int NOT NULL,
        CONSTRAINT [PK_AssessmentResponseText] PRIMARY KEY ([AssessmentResponseTextID]),
        CONSTRAINT [FK_AssessmentResponseText_AssessmentResponse_AssessmentResponseID] FOREIGN KEY ([AssessmentResponseID]) REFERENCES [AssessmentResponse] ([AssessmentResponseID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE TABLE [NotifyRiskValue] (
        [NotifyRiskValueID] int NOT NULL IDENTITY,
        [NotifyRiskID] int NOT NULL,
        [QuestionnaireNotifyRiskRuleConditionID] int NOT NULL,
        [AssessmentResponseID] int NOT NULL,
        [AssessmentResponseValue] int NOT NULL,
        CONSTRAINT [PK_NotifyRiskValue] PRIMARY KEY ([NotifyRiskValueID]),
        CONSTRAINT [FK_NotifyRiskValue_AssessmentResponse_AssessmentResponseID] FOREIGN KEY ([AssessmentResponseID]) REFERENCES [AssessmentResponse] ([AssessmentResponseID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotifyRiskValue_NotifyRisk_NotifyRiskID] FOREIGN KEY ([NotifyRiskID]) REFERENCES [NotifyRisk] ([NotifyRiskID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_NotifyRiskValue_QuestionnaireNotifyRiskRuleCondition_QuestionnaireNotifyRiskRuleConditionID] FOREIGN KEY ([QuestionnaireNotifyRiskRuleConditionID]) REFERENCES [QuestionnaireNotifyRiskRuleCondition] ([QuestionnaireNotifyRiskRuleConditionID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Address_AddressIndex] ON [Address] ([AddressIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Address_CountryStateId] ON [Address] ([CountryStateId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Address_UpdateUserID] ON [Address] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Agency_AgencyIndex] ON [Agency] ([AgencyIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Agency_UpdateUserID] ON [Agency] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AgencyAddress_AddressID] ON [AgencyAddress] ([AddressID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AgencyAddress_AgencyID] ON [AgencyAddress] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AgencyAddress_UpdateUserID] ON [AgencyAddress] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AgencyContact_AgencyID] ON [AgencyContact] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AgencyContact_ContactID] ON [AgencyContact] ([ContactID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_AgencySharing_AgencySharingIndex] ON [AgencySharing] ([AgencySharingIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AgencySharingHistory_ReportingUnitAgencyID] ON [AgencySharingHistory] ([ReportingUnitAgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ApplicationObject_ApplicationObjectTypeID] ON [ApplicationObject] ([ApplicationObjectTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ApplicationObject_UpdateUserID] ON [ApplicationObject] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ApplicationObjectType_UpdateUserID] ON [ApplicationObjectType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Assessment_AssessmentReasonID] ON [Assessment] ([AssessmentReasonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Assessment_AssessmentStatusID] ON [Assessment] ([AssessmentStatusID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Assessment_PersonQuestionnaireID] ON [Assessment] ([PersonQuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Assessment_PersonQuestionnaireScheduleID] ON [Assessment] ([PersonQuestionnaireScheduleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Assessment_UpdateUserID] ON [Assessment] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Assessment_VoiceTypeID] ON [Assessment] ([VoiceTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentEmailLinkDetails_AssessmentID] ON [AssessmentEmailLinkDetails] ([AssessmentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentEmailLinkDetails_HelperID] ON [AssessmentEmailLinkDetails] ([HelperID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentEmailLinkDetails_PersonSupportID] ON [AssessmentEmailLinkDetails] ([PersonSupportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentEmailLinkDetails_QuestionnaireID] ON [AssessmentEmailLinkDetails] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentNote_AssessmentID] ON [AssessmentNote] ([AssessmentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentNote_AssessmentReviewHistoryID] ON [AssessmentNote] ([AssessmentReviewHistoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentNote_NoteID] ON [AssessmentNote] ([NoteID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponse_AssessmentID] ON [AssessmentResponse] ([AssessmentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponse_ItemResponseBehaviorID] ON [AssessmentResponse] ([ItemResponseBehaviorID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponse_PersonSupportID] ON [AssessmentResponse] ([PersonSupportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponse_QuestionnaireItemID] ON [AssessmentResponse] ([QuestionnaireItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponse_ResponseID] ON [AssessmentResponse] ([ResponseID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponse_UpdateUserID] ON [AssessmentResponse] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponseNote_AssessmentResponseID] ON [AssessmentResponseNote] ([AssessmentResponseID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponseNote_NoteID] ON [AssessmentResponseNote] ([NoteID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentResponseText_AssessmentResponseID] ON [AssessmentResponseText] ([AssessmentResponseID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Collaboration_AgencyID] ON [Collaboration] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Collaboration_CollaborationIndex] ON [Collaboration] ([CollaborationIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Collaboration_CollaborationLevelID] ON [Collaboration] ([CollaborationLevelID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Collaboration_TherapyTypeID] ON [Collaboration] ([TherapyTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Collaboration_UpdateUserID] ON [Collaboration] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationAgencyAddress_AddressID] ON [CollaborationAgencyAddress] ([AddressID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationLeadHistory_CollaborationID] ON [CollaborationLeadHistory] ([CollaborationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationQuestionnaire_CollaborationID] ON [CollaborationQuestionnaire] ([CollaborationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationQuestionnaire_QuestionnaireID] ON [CollaborationQuestionnaire] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationSharing_AgencyID] ON [CollaborationSharing] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationSharing_CollaborationID] ON [CollaborationSharing] ([CollaborationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_CollaborationSharing_CollaborationSharingIndex] ON [CollaborationSharing] ([CollaborationSharingIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationSharing_ReportingUnitID] ON [CollaborationSharing] ([ReportingUnitID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationSharingHistory_ReportingUnitCollaborationID] ON [CollaborationSharingHistory] ([ReportingUnitCollaborationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationSharingPolicy_CollaborationSharingID] ON [CollaborationSharingPolicy] ([CollaborationSharingID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationSharingPolicy_SharingPolicyID] ON [CollaborationSharingPolicy] ([SharingPolicyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationTag_CollaborationID] ON [CollaborationTag] ([CollaborationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationTag_CollaborationTagTypeID] ON [CollaborationTag] ([CollaborationTagTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Helper_AgencyID] ON [Helper] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Helper_HelperIndex] ON [Helper] ([HelperIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Helper_HelperTitleID] ON [Helper] ([HelperTitleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Helper_ReviewerID] ON [Helper] ([ReviewerID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Helper_SupervisorHelperID] ON [Helper] ([SupervisorHelperID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Helper_UpdateUserID] ON [Helper] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Helper_UserID] ON [Helper] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_HelperAddress_AddressID] ON [HelperAddress] ([AddressID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_HelperAddress_HelperAddressIndex] ON [HelperAddress] ([HelperAddressIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_HelperAddress_HelperID] ON [HelperAddress] ([HelperID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_HelperContact_ContactID] ON [HelperContact] ([ContactID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_HelperContact_HelperID] ON [HelperContact] ([HelperID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [IdentityRole] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_IdentityRoleClaim_RoleId] ON [IdentityRoleClaim] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_IdentityUserClaim_UserId] ON [IdentityUserClaim] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_IdentityUserLogin_UserId] ON [IdentityUserLogin] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_IdentityUserRole_RoleId] ON [IdentityUserRole] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_IdentityUsers_AgencyId] ON [IdentityUsers] ([AgencyId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [EmailIndex] ON [IdentityUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [IdentityUsers] ([NormalizedUserName], [AgencyId]) WHERE [NormalizedUserName] IS NOT NULL AND [AgencyId] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_InstrumentAgency_AgencyID] ON [InstrumentAgency] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_InstrumentAgency_InstrumentID] ON [InstrumentAgency] ([InstrumentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Item_ItemIndex] ON [Item] ([ItemIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Item_ItemResponseTypeID] ON [Item] ([ItemResponseTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Item_ResponseValueTypeID] ON [Item] ([ResponseValueTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Item_UpdateUserID] ON [Item] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Note_UpdateUserID] ON [Note] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationDelivery_NotificationLogID] ON [NotificationDelivery] ([NotificationLogID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationDelivery_NotificationTemplateID] ON [NotificationDelivery] ([NotificationTemplateID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationLog_NotificationResolutionStatusID] ON [NotificationLog] ([NotificationResolutionStatusID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationLog_NotificationTypeID] ON [NotificationLog] ([NotificationTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationLog_PersonID] ON [NotificationLog] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationLog_UpdateUserID] ON [NotificationLog] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationResolutionHistory_NotificationLogID] ON [NotificationResolutionHistory] ([NotificationLogID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationResolutionHistory_StatusFrom] ON [NotificationResolutionHistory] ([StatusFrom]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationResolutionHistory_StatusTo] ON [NotificationResolutionHistory] ([StatusTo]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationResolutionHistory_UpdateUserID] ON [NotificationResolutionHistory] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationResolutionNote_Note_NoteID] ON [NotificationResolutionNote] ([Note_NoteID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationResolutionNote_NotificationLogID] ON [NotificationResolutionNote] ([NotificationLogID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationResolutionNote_NotificationResolutionHistoryID] ON [NotificationResolutionNote] ([NotificationResolutionHistoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationTemplate_NotificationLevelID] ON [NotificationTemplate] ([NotificationLevelID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationTemplate_NotificationModeID] ON [NotificationTemplate] ([NotificationModeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyReminder_PersonQuestionnaireScheduleID] ON [NotifyReminder] ([PersonQuestionnaireScheduleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyReminder_QuestionnaireReminderRuleID] ON [NotifyReminder] ([QuestionnaireReminderRuleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRisk_AssessmentID] ON [NotifyRisk] ([AssessmentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRisk_PersonID] ON [NotifyRisk] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRisk_QuestionnaireNotifyRiskRuleID] ON [NotifyRisk] ([QuestionnaireNotifyRiskRuleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRisk_UpdateUserID] ON [NotifyRisk] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRiskRule_NotificationLevelID] ON [NotifyRiskRule] ([NotificationLevelID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRiskRule_QuestionnaireItemID] ON [NotifyRiskRule] ([QuestionnaireItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRiskRule_UpdateUserID] ON [NotifyRiskRule] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRiskValue_AssessmentResponseID] ON [NotifyRiskValue] ([AssessmentResponseID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRiskValue_NotifyRiskID] ON [NotifyRiskValue] ([NotifyRiskID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotifyRiskValue_QuestionnaireNotifyRiskRuleConditionID] ON [NotifyRiskValue] ([QuestionnaireNotifyRiskRuleConditionID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Person_AgencyID] ON [Person] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Person_BiologicalSexID] ON [Person] ([BiologicalSexID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Person_GenderID] ON [Person] ([GenderID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Person_PersonIndex] ON [Person] ([PersonIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Person_PersonScreeningStatusID] ON [Person] ([PersonScreeningStatusID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Person_PreferredLanguageID] ON [Person] ([PreferredLanguageID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Person_PrimaryLanguageID] ON [Person] ([PrimaryLanguageID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Person_SexualityID] ON [Person] ([SexualityID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Person_UpdateUserID] ON [Person] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonAddress_AddressID] ON [PersonAddress] ([AddressID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonAddress_PersonID] ON [PersonAddress] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonCollaboration_CollaborationID] ON [PersonCollaboration] ([CollaborationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonCollaboration_PersonID] ON [PersonCollaboration] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonCollaboration_UpdateUserID] ON [PersonCollaboration] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonContact_ContactID] ON [PersonContact] ([ContactID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonContact_PersonID] ON [PersonContact] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonHelper_HelperID] ON [PersonHelper] ([HelperID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonHelper_PersonID] ON [PersonHelper] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonHelper_UpdateUserID] ON [PersonHelper] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonIdentification_IdentificationTypeID] ON [PersonIdentification] ([IdentificationTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonIdentification_PersonID] ON [PersonIdentification] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonIdentification_UpdateUserID] ON [PersonIdentification] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonLanguage_LanguageID] ON [PersonLanguage] ([LanguageID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonNote_NoteID] ON [PersonNote] ([NoteID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonNote_PersonID] ON [PersonNote] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonQuestionnaire_CollaborationID] ON [PersonQuestionnaire] ([CollaborationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonQuestionnaire_PersonID] ON [PersonQuestionnaire] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonQuestionnaire_QuestionnaireID] ON [PersonQuestionnaire] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonQuestionnaire_UpdateUserID] ON [PersonQuestionnaire] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonQuestionnaireSchedule_PersonQuestionnaireID] ON [PersonQuestionnaireSchedule] ([PersonQuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonQuestionnaireSchedule_QuestionnaireWindowID] ON [PersonQuestionnaireSchedule] ([QuestionnaireWindowID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonRaceEthnicity_PersonID] ON [PersonRaceEthnicity] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonRaceEthnicity_RaceEthnicityID] ON [PersonRaceEthnicity] ([RaceEthnicityID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonSupport_PersonID] ON [PersonSupport] ([PersonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonSupport_SupportTypeID] ON [PersonSupport] ([SupportTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonSupport_UpdateUserID] ON [PersonSupport] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Questionnaire_AgencyID] ON [Questionnaire] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Questionnaire_InstrumentID] ON [Questionnaire] ([InstrumentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Questionnaire_ParentQuestionnaireID] ON [Questionnaire] ([ParentQuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Questionnaire_UpdateUserID] ON [Questionnaire] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireItem_CategoryID] ON [QuestionnaireItem] ([CategoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireItem_ItemID] ON [QuestionnaireItem] ([ItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireItem_LowerItemResponseBehaviorID] ON [QuestionnaireItem] ([LowerItemResponseBehaviorID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireItem_MedianItemResponseBehaviorID] ON [QuestionnaireItem] ([MedianItemResponseBehaviorID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireItem_QuestionnaireID] ON [QuestionnaireItem] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_QuestionnaireItem_QuestionnaireItemIndex] ON [QuestionnaireItem] ([QuestionnaireItemIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireItem_UpdateUserID] ON [QuestionnaireItem] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireItem_UpperItemResponseBehaviorID] ON [QuestionnaireItem] ([UpperItemResponseBehaviorID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireItemHistory_QuestionnaireItemID] ON [QuestionnaireItemHistory] ([QuestionnaireItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireNotifyRiskRule_NotificationLevelID] ON [QuestionnaireNotifyRiskRule] ([NotificationLevelID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireNotifyRiskRule_QuestionnaireNotifyRiskScheduleID] ON [QuestionnaireNotifyRiskRule] ([QuestionnaireNotifyRiskScheduleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireNotifyRiskRule_UpdateUserID] ON [QuestionnaireNotifyRiskRule] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireNotifyRiskRuleCondition_QuestionnaireItemID] ON [QuestionnaireNotifyRiskRuleCondition] ([QuestionnaireItemID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireNotifyRiskRuleCondition_QuestionnaireNotifyRiskRuleID] ON [QuestionnaireNotifyRiskRuleCondition] ([QuestionnaireNotifyRiskRuleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireNotifyRiskRuleCondition_UpdateUserID] ON [QuestionnaireNotifyRiskRuleCondition] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireNotifyRiskSchedule_QuestionnaireID] ON [QuestionnaireNotifyRiskSchedule] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireNotifyRiskSchedule_UpdateUserID] ON [QuestionnaireNotifyRiskSchedule] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireReminderRule_QuestionnaireID] ON [QuestionnaireReminderRule] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireReminderRule_QuestionnaireReminderTypeID] ON [QuestionnaireReminderRule] ([QuestionnaireReminderTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireWindow_AssessmentReasonID] ON [QuestionnaireWindow] ([AssessmentReasonID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireWindow_QuestionnaireID] ON [QuestionnaireWindow] ([QuestionnaireID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ReportingUnit_ParentAgencyID] ON [ReportingUnit] ([ParentAgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_ReportingUnit_ReportingUnitIndex] ON [ReportingUnit] ([ReportingUnitIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ReportingUnit_UpdateUserID] ON [ReportingUnit] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Response_ResponseIndex] ON [Response] ([ResponseIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Response_UpdateUserID] ON [Response] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ReviewerHistory_StatusFrom] ON [ReviewerHistory] ([StatusFrom]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ReviewerHistory_StatusTo] ON [ReviewerHistory] ([StatusTo]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ReviewerHistory_UpdateUserID] ON [ReviewerHistory] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SupportAddress_AddressID] ON [SupportAddress] ([AddressID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SupportAddress_SupportID] ON [SupportAddress] ([SupportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SupportContact_ContactID] ON [SupportContact] ([ContactID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SupportContact_SupportID] ON [SupportContact] ([SupportID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_User_UserIndex] ON [User] ([UserIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_UserRole_SystemRoleID] ON [UserRole] ([SystemRoleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_UserRole_UserID] ON [UserRole] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_UserRole_UserRoleIndex] ON [UserRole] ([UserRoleIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_WeatherForecasts_WeatherForecastIndex] ON [WeatherForecasts] ([WeatherForecastIndex]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentReason_UpdateUserID] ON [info].[AssessmentReason] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_AssessmentStatus_UpdateUserID] ON [info].[AssessmentStatus] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Category_UpdateUserID] ON [info].[Category] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CategoryFocus_UpdateUserID] ON [info].[CategoryFocus] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationLevel_AgencyID] ON [info].[CollaborationLevel] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationLevel_UpdateUserID] ON [info].[CollaborationLevel] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationTagType_AgencyID] ON [info].[CollaborationTagType] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CollaborationTagType_UpdateUserID] ON [info].[CollaborationTagType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ColorPalette_UpdateUserID] ON [info].[ColorPalette] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Configuration_ConfigurationParameterContextID] ON [info].[Configuration] ([ConfigurationParameterContextID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ConfigurationAttachment_AttachmentID] ON [info].[ConfigurationAttachment] ([AttachmentID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ConfigurationAttachment_ConfigurationID] ON [info].[ConfigurationAttachment] ([ConfigurationID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ConfigurationContext_ParentContextID] ON [info].[ConfigurationContext] ([ParentContextID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ConfigurationParameter_ConfigurationValueTypeID] ON [info].[ConfigurationParameter] ([ConfigurationValueTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ConfigurationParameterContext_ConfigurationContextID] ON [info].[ConfigurationParameterContext] ([ConfigurationContextID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ConfigurationParameterContext_ConfigurationParameterID] ON [info].[ConfigurationParameterContext] ([ConfigurationParameterID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Contact_ContactTypeID] ON [info].[Contact] ([ContactTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ContactType_UpdateUserID] ON [info].[ContactType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Country_UpdateUserID] ON [info].[Country] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CountryState_CountryID] ON [info].[CountryState] ([CountryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_CountryState_UpdateUserID] ON [info].[CountryState] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Gender_AgencyID] ON [info].[Gender] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Gender_UpdateUserID] ON [info].[Gender] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_HelperTitle_AgencyID] ON [info].[HelperTitle] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_HelperTitle_UpdateUserID] ON [info].[HelperTitle] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_IdentificationType_AgencyID] ON [info].[IdentificationType] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_IdentificationType_UpdateUserID] ON [info].[IdentificationType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Instrument_UpdateUserID] ON [info].[Instrument] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ItemResponseBehavior_ItemResponseTypeID] ON [info].[ItemResponseBehavior] ([ItemResponseTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ItemResponseBehavior_UpdateUserID] ON [info].[ItemResponseBehavior] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ItemResponseType_UpdateUserID] ON [info].[ItemResponseType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Language_UpdateUserID] ON [info].[Language] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationLevel_AgencyID] ON [info].[NotificationLevel] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationLevel_NotificationTypeID] ON [info].[NotificationLevel] ([NotificationTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationLevel_UpdateUserID] ON [info].[NotificationLevel] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationResolutionStatus_UpdateUserID] ON [info].[NotificationResolutionStatus] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_NotificationType_UpdateUserID] ON [info].[NotificationType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_OperationType_UpdateUserID] ON [info].[OperationType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Permission_ApplicationObjectID] ON [info].[Permission] ([ApplicationObjectID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Permission_OperationTypeID] ON [info].[Permission] ([OperationTypeID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Permission_UpdateUserID] ON [info].[Permission] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_PersonScreeningStatus_UpdateUserId] ON [info].[PersonScreeningStatus] ([UpdateUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireReminderType_NotificationLevelID] ON [info].[QuestionnaireReminderType] ([NotificationLevelID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_QuestionnaireReminderType_UpdateUserID] ON [info].[QuestionnaireReminderType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_RaceEthnicity_AgencyID] ON [info].[RaceEthnicity] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_RaceEthnicity_UpdateUserID] ON [info].[RaceEthnicity] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_ResponseValueType_UpdateUserID] ON [info].[ResponseValueType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_RolePermission_PermissionID] ON [info].[RolePermission] ([PermissionID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_RolePermission_UserRoleID] ON [info].[RolePermission] ([UserRoleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Sexuality_AgencyID] ON [info].[Sexuality] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_Sexuality_UpdateUserID] ON [info].[Sexuality] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SupportType_AgencyID] ON [info].[SupportType] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SupportType_UpdateUserID] ON [info].[SupportType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SystemRole_UpdateUserID] ON [info].[SystemRole] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SystemRolePermission_PermissionID] ON [info].[SystemRolePermission] ([PermissionID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_SystemRolePermission_SystemRoleID] ON [info].[SystemRolePermission] ([SystemRoleID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_TherapyType_AgencyID] ON [info].[TherapyType] ([AgencyID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_TherapyType_UpdateUserID] ON [info].[TherapyType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    CREATE INDEX [IX_VoiceType_UpdateUserID] ON [info].[VoiceType] ([UpdateUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200810082553_InitialMigrations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200810082553_InitialMigrations', N'3.1.4');
END;

GO

