using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Opeeka.PICS.Infrastructure.Data.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "info");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "AgencySharing",
                columns: table => new
                {
                    AgencySharingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencySharingIndex = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    ReportingUnitID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false),
                    AgencySharingPolicyID = table.Column<int>(nullable: true),
                    HistoricalView = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencySharing", x => x.AgencySharingID);
                });

            migrationBuilder.CreateTable(
                name: "AgencySharingPolicy",
                columns: table => new
                {
                    AgencySharingPolicyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencySharingID = table.Column<int>(nullable: false),
                    SharingPolicyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencySharingPolicy", x => x.AgencySharingPolicyID);
                });

            migrationBuilder.CreateTable(
                name: "AuditDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    KeyValues = table.Column<string>(nullable: true),
                    OldValues = table.Column<string>(nullable: true),
                    NewValues = table.Column<string>(nullable: true),
                    AuditUser = table.Column<string>(nullable: true),
                    ReferenceKeyValues = table.Column<string>(nullable: true),
                    EntityState = table.Column<string>(nullable: true),
                    Tenant = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditFieldName",
                columns: table => new
                {
                    FieldName = table.Column<string>(nullable: false),
                    TableName = table.Column<string>(nullable: false),
                    Label = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditFieldName", x => x.FieldName);
                });

            migrationBuilder.CreateTable(
                name: "AuditTableName",
                columns: table => new
                {
                    TableName = table.Column<string>(nullable: false),
                    Label = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTableName", x => x.TableName);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserIndex = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID ( )"),
                    UserName = table.Column<string>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: true, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    AgencyID = table.Column<long>(nullable: true),
                    AspNetUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeatherForecastIndex = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Date = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    TemperatureC = table.Column<int>(nullable: false),
                    TemperatureF = table.Column<int>(nullable: false),
                    Summary = table.Column<string>(nullable: false),
                    Summary1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonQuestionnaireMetrics",
                schema: "dbo",
                columns: table => new
                {
                    PersonQuestionnaireMetricsID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    InstrumentID = table.Column<int>(nullable: false),
                    PersonQuestionnaireID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    NeedsEver = table.Column<int>(nullable: false),
                    NeedsIdentified = table.Column<int>(nullable: false),
                    NeedsAddressed = table.Column<int>(nullable: false),
                    NeedsAddressing = table.Column<int>(nullable: false),
                    NeedsImproved = table.Column<int>(nullable: false),
                    StrengthsEver = table.Column<int>(nullable: false),
                    StrengthsIdentified = table.Column<int>(nullable: false),
                    StrengthsBuilt = table.Column<int>(nullable: false),
                    StrengthsBuilding = table.Column<int>(nullable: false),
                    StrengthsImproved = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonQuestionnaireMetrics", x => x.PersonQuestionnaireMetricsID);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                schema: "info",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Attachments = table.Column<string>(nullable: true),
                    ContextType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationContext",
                schema: "info",
                columns: table => new
                {
                    ConfigurationContextID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentContextID = table.Column<int>(nullable: true),
                    EntityName = table.Column<string>(nullable: true),
                    FKValueRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationContext", x => x.ConfigurationContextID);
                    table.ForeignKey(
                        name: "FK_ConfigurationContext_ConfigurationContext_ParentContextID",
                        column: x => x.ParentContextID,
                        principalSchema: "info",
                        principalTable: "ConfigurationContext",
                        principalColumn: "ConfigurationContextID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationValueType",
                schema: "info",
                columns: table => new
                {
                    ConfigurationValueTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationValueType", x => x.ConfigurationValueTypeID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationMode",
                schema: "info",
                columns: table => new
                {
                    NotificationModeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMode", x => x.NotificationModeID);
                });

            migrationBuilder.CreateTable(
                name: "SharingPolicy",
                schema: "info",
                columns: table => new
                {
                    SharingPolicyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharingPolicy", x => x.SharingPolicyID);
                });

            migrationBuilder.CreateTable(
                name: "IdentityRoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityRoleClaim_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressIndex = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CountryStateId = table.Column<int>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    Zip4 = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Address_User_CountryStateId",
                        column: x => x.CountryStateId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    AgencyID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    AgencyIndex = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    IsCustomer = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Note = table.Column<string>(unicode: false, nullable: true),
                    Abbrev = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    ContactLastName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ContactFirstName = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.AgencyID);
                    table.ForeignKey(
                        name: "FK_Agency_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationObjectType",
                columns: table => new
                {
                    ApplicationObjectTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationObjectType", x => x.ApplicationObjectTypeID);
                    table.ForeignKey(
                        name: "FK_ApplicationObjectType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteText = table.Column<string>(nullable: true),
                    IsConfidential = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_Note_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    ResponseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponseIndex = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID ( )"),
                    ItemID = table.Column<int>(nullable: false),
                    BackgroundColorPaletteID = table.Column<int>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    KeyCodes = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    MaxRangeValue = table.Column<int>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    DefaultItemResponseBehaviorID = table.Column<int>(nullable: true),
                    IsItemResponseBehaviorDisabled = table.Column<bool>(nullable: false, defaultValue: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.ResponseID);
                    table.ForeignKey(
                        name: "FK_Response_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentReason",
                schema: "info",
                columns: table => new
                {
                    AssessmentReasonID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentReason", x => x.AssessmentReasonID);
                    table.ForeignKey(
                        name: "FK_AssessmentReason_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentStatus",
                schema: "info",
                columns: table => new
                {
                    AssessmentStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentStatus", x => x.AssessmentStatusID);
                    table.ForeignKey(
                        name: "FK_AssessmentStatus_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "info",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryFocusID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                    table.ForeignKey(
                        name: "FK_Category_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryFocus",
                schema: "info",
                columns: table => new
                {
                    CategoryFocusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryFocus", x => x.CategoryFocusID);
                    table.ForeignKey(
                        name: "FK_CategoryFocus_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ColorPalette",
                schema: "info",
                columns: table => new
                {
                    ColorPaletteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    RGB = table.Column<string>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorPalette", x => x.ColorPaletteID);
                    table.ForeignKey(
                        name: "FK_ColorPalette_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactType",
                schema: "info",
                columns: table => new
                {
                    ContactTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactType", x => x.ContactTypeID);
                    table.ForeignKey(
                        name: "FK_ContactType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "info",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    CountryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                    table.ForeignKey(
                        name: "FK_Country_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                schema: "info",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Instructions = table.Column<string>(nullable: true),
                    Authors = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    InstrumentUrl = table.Column<string>(nullable: true),
                    IconUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.InstrumentID);
                    table.ForeignKey(
                        name: "FK_Instrument_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemResponseType",
                schema: "info",
                columns: table => new
                {
                    ItemResponseTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemResponseType", x => x.ItemResponseTypeID);
                    table.ForeignKey(
                        name: "FK_ItemResponseType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                schema: "info",
                columns: table => new
                {
                    LanguageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageID);
                    table.ForeignKey(
                        name: "FK_Language_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationResolutionStatus",
                schema: "info",
                columns: table => new
                {
                    NotificationResolutionStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationResolutionStatus", x => x.NotificationResolutionStatusID);
                    table.ForeignKey(
                        name: "FK_NotificationResolutionStatus_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationType",
                schema: "info",
                columns: table => new
                {
                    NotificationTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.NotificationTypeID);
                    table.ForeignKey(
                        name: "FK_NotificationType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OperationType",
                schema: "info",
                columns: table => new
                {
                    OperationTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationType", x => x.OperationTypeID);
                    table.ForeignKey(
                        name: "FK_OperationType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonScreeningStatus",
                schema: "info",
                columns: table => new
                {
                    PersonScreeningStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonScreeningStatus", x => x.PersonScreeningStatusId);
                    table.ForeignKey(
                        name: "FK_PersonScreeningStatus_User_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResponseValueType",
                schema: "info",
                columns: table => new
                {
                    ResponseValueTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseValueType", x => x.ResponseValueTypeID);
                    table.ForeignKey(
                        name: "FK_ResponseValueType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemRole",
                schema: "info",
                columns: table => new
                {
                    SystemRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    IsExternal = table.Column<bool>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: true),
                    Weight = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRole", x => x.SystemRoleID);
                    table.ForeignKey(
                        name: "FK_SystemRole_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoiceType",
                schema: "info",
                columns: table => new
                {
                    VoiceTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceType", x => x.VoiceTypeID);
                    table.ForeignKey(
                        name: "FK_VoiceType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationParameter",
                schema: "info",
                columns: table => new
                {
                    ConfigurationParameterID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigurationValueTypeID = table.Column<int>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Deprecated = table.Column<bool>(nullable: false),
                    CanModify = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationParameter", x => x.ConfigurationParameterID);
                    table.ForeignKey(
                        name: "FK_ConfigurationParameter_ConfigurationValueType_ConfigurationValueTypeID",
                        column: x => x.ConfigurationValueTypeID,
                        principalSchema: "info",
                        principalTable: "ConfigurationValueType",
                        principalColumn: "ConfigurationValueTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyAddress",
                columns: table => new
                {
                    AgencyAddressID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyID = table.Column<long>(nullable: false),
                    AddressID = table.Column<long>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyAddress", x => x.AgencyAddressID);
                    table.ForeignKey(
                        name: "FK_AgencyAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyAddress_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyAddress_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgencySharingHistory",
                columns: table => new
                {
                    AgencySharingHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportingUnitAgencyID = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    HistoricalView = table.Column<bool>(nullable: false),
                    RemovedUserID = table.Column<int>(nullable: true),
                    RemovedNoteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencySharingHistory", x => x.AgencySharingHistoryID);
                    table.ForeignKey(
                        name: "FK_AgencySharingHistory_Agency_ReportingUnitAgencyID",
                        column: x => x.ReportingUnitAgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    UserIndex = table.Column<Guid>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    AgencyId = table.Column<long>(nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUsers_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportingUnit",
                columns: table => new
                {
                    ReportingUnitID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportingUnitIndex = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(nullable: false),
                    Abbrev = table.Column<string>(nullable: true),
                    ParentAgencyID = table.Column<long>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportingUnit", x => x.ReportingUnitID);
                    table.ForeignKey(
                        name: "FK_ReportingUnit_Agency_ParentAgencyID",
                        column: x => x.ParentAgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportingUnit_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationLevel",
                schema: "info",
                columns: table => new
                {
                    CollaborationLevelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationLevel", x => x.CollaborationLevelID);
                    table.ForeignKey(
                        name: "FK_CollaborationLevel_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborationLevel_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationTagType",
                schema: "info",
                columns: table => new
                {
                    CollaborationTagTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationTagType", x => x.CollaborationTagTypeID);
                    table.ForeignKey(
                        name: "FK_CollaborationTagType_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborationTagType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "info",
                columns: table => new
                {
                    GenderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderID);
                    table.ForeignKey(
                        name: "FK_Gender_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gender_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HelperTitle",
                schema: "info",
                columns: table => new
                {
                    HelperTitleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelperTitle", x => x.HelperTitleID);
                    table.ForeignKey(
                        name: "FK_HelperTitle_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HelperTitle_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentificationType",
                schema: "info",
                columns: table => new
                {
                    IdentificationTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationType", x => x.IdentificationTypeID);
                    table.ForeignKey(
                        name: "FK_IdentificationType_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IdentificationType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaceEthnicity",
                schema: "info",
                columns: table => new
                {
                    RaceEthnicityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceEthnicity", x => x.RaceEthnicityID);
                    table.ForeignKey(
                        name: "FK_RaceEthnicity_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaceEthnicity_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sexuality",
                schema: "info",
                columns: table => new
                {
                    SexualityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexuality", x => x.SexualityID);
                    table.ForeignKey(
                        name: "FK_Sexuality_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sexuality_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportType",
                schema: "info",
                columns: table => new
                {
                    SupportTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportType", x => x.SupportTypeID);
                    table.ForeignKey(
                        name: "FK_SupportType_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TherapyType",
                schema: "info",
                columns: table => new
                {
                    TherapyTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsResidential = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapyType", x => x.TherapyTypeID);
                    table.ForeignKey(
                        name: "FK_TherapyType_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TherapyType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationObject",
                columns: table => new
                {
                    ApplicationObjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationObjectTypeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationObject", x => x.ApplicationObjectID);
                    table.ForeignKey(
                        name: "FK_ApplicationObject_ApplicationObjectType_ApplicationObjectTypeID",
                        column: x => x.ApplicationObjectTypeID,
                        principalTable: "ApplicationObjectType",
                        principalColumn: "ApplicationObjectTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationObject_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewerHistory",
                columns: table => new
                {
                    AssessmentReviewHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordedDate = table.Column<DateTime>(nullable: true),
                    StatusFrom = table.Column<int>(nullable: false),
                    StatusTo = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AssessmentReviewHistoryID", x => x.AssessmentReviewHistoryID);
                    table.ForeignKey(
                        name: "FK_ReviewerHistory_AssessmentStatus_StatusFrom",
                        column: x => x.StatusFrom,
                        principalSchema: "info",
                        principalTable: "AssessmentStatus",
                        principalColumn: "AssessmentStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewerHistory_AssessmentStatus_StatusTo",
                        column: x => x.StatusTo,
                        principalSchema: "info",
                        principalTable: "AssessmentStatus",
                        principalColumn: "AssessmentStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewerHistory_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "info",
                columns: table => new
                {
                    ContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactTypeID = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contact_ContactType_ContactTypeID",
                        column: x => x.ContactTypeID,
                        principalSchema: "info",
                        principalTable: "ContactType",
                        principalColumn: "ContactTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryState",
                schema: "info",
                columns: table => new
                {
                    CountryStateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryState", x => x.CountryStateID);
                    table.ForeignKey(
                        name: "FK_CountryState_Country_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "info",
                        principalTable: "Country",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountryState_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentAgency",
                columns: table => new
                {
                    InstrumentAgencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentAgency", x => x.InstrumentAgencyID);
                    table.ForeignKey(
                        name: "FK_InstrumentAgency_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstrumentAgency_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "info",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questionnaire",
                columns: table => new
                {
                    QuestionnaireID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstrumentID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    ReminderScheduleName = table.Column<string>(nullable: true),
                    RequiredConfidentialityLanguage = table.Column<string>(nullable: false, defaultValue: "Confidential"),
                    PersonRequestedConfidentialityLanguage = table.Column<string>(nullable: true),
                    OtherConfidentialityLanguage = table.Column<string>(nullable: true),
                    IsPubllished = table.Column<bool>(nullable: false),
                    ParentQuestionnaireID = table.Column<int>(nullable: true),
                    IsBaseQuestionnaire = table.Column<bool>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    OwnerUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.QuestionnaireID);
                    table.ForeignKey(
                        name: "FK_Questionnaire_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questionnaire_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalSchema: "info",
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questionnaire_Questionnaire_ParentQuestionnaireID",
                        column: x => x.ParentQuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questionnaire_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemResponseBehavior",
                schema: "info",
                columns: table => new
                {
                    ItemResponseBehaviorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemResponseTypeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemResponseBehavior", x => x.ItemResponseBehaviorID);
                    table.ForeignKey(
                        name: "FK_ItemResponseBehavior_ItemResponseType_ItemResponseTypeID",
                        column: x => x.ItemResponseTypeID,
                        principalSchema: "info",
                        principalTable: "ItemResponseType",
                        principalColumn: "ItemResponseTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemResponseBehavior_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLevel",
                schema: "info",
                columns: table => new
                {
                    NotificationLevelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationTypeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    RequireResolution = table.Column<bool>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLevel", x => x.NotificationLevelID);
                    table.ForeignKey(
                        name: "FK_NotificationLevel_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationLevel_NotificationType_NotificationTypeID",
                        column: x => x.NotificationTypeID,
                        principalSchema: "info",
                        principalTable: "NotificationType",
                        principalColumn: "NotificationTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationLevel_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemIndex = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID ( )"),
                    Abbreviation = table.Column<string>(nullable: true),
                    ItemResponseTypeID = table.Column<int>(nullable: false),
                    Label = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Considerations = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SupplementalDescription = table.Column<string>(nullable: true),
                    ResponseValueTypeID = table.Column<int>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    UseRequiredConfidentiality = table.Column<bool>(nullable: false, defaultValue: false),
                    UsePersonRequestedConfidentiality = table.Column<bool>(nullable: false, defaultValue: false),
                    UseOtherConfidentiality = table.Column<bool>(nullable: false, defaultValue: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Item_ItemResponseType_ItemResponseTypeID",
                        column: x => x.ItemResponseTypeID,
                        principalSchema: "info",
                        principalTable: "ItemResponseType",
                        principalColumn: "ItemResponseTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_ResponseValueType_ResponseValueTypeID",
                        column: x => x.ResponseValueTypeID,
                        principalSchema: "info",
                        principalTable: "ResponseValueType",
                        principalColumn: "ResponseValueTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleIndex = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID ( )"),
                    UserID = table.Column<int>(nullable: false),
                    SystemRoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleID);
                    table.ForeignKey(
                        name: "FK_UserRole_SystemRole_SystemRoleID",
                        column: x => x.SystemRoleID,
                        principalSchema: "info",
                        principalTable: "SystemRole",
                        principalColumn: "SystemRoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationParameterContext",
                schema: "info",
                columns: table => new
                {
                    ConfigurationParameterContextID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigurationParameterID = table.Column<int>(nullable: false),
                    ConfigurationContextID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationParameterContext", x => x.ConfigurationParameterContextID);
                    table.ForeignKey(
                        name: "FK_ConfigurationParameterContext_ConfigurationContext_ConfigurationContextID",
                        column: x => x.ConfigurationContextID,
                        principalSchema: "info",
                        principalTable: "ConfigurationContext",
                        principalColumn: "ConfigurationContextID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationParameterContext_ConfigurationParameter_ConfigurationParameterID",
                        column: x => x.ConfigurationParameterID,
                        principalSchema: "info",
                        principalTable: "ConfigurationParameter",
                        principalColumn: "ConfigurationParameterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUserClaim_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IdentityUserLogin_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IdentityUserRole_IdentityRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentityUserRole_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_IdentityUserToken_IdentityUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Helper",
                columns: table => new
                {
                    HelperID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HelperIndex = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID ( )"),
                    UserID = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false),
                    HelperTitleID = table.Column<int>(nullable: true),
                    Phone2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    SupervisorHelperID = table.Column<int>(nullable: true),
                    ReviewerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helper", x => x.HelperID);
                    table.ForeignKey(
                        name: "FK_Helper_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Helper_HelperTitle_HelperTitleID",
                        column: x => x.HelperTitleID,
                        principalSchema: "info",
                        principalTable: "HelperTitle",
                        principalColumn: "HelperTitleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Helper_Helper_ReviewerID",
                        column: x => x.ReviewerID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Helper_Helper_SupervisorHelperID",
                        column: x => x.SupervisorHelperID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Helper_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Helper_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonIndex = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID ( )"),
                    FirstName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Suffix = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PrimaryLanguageID = table.Column<int>(nullable: true),
                    PreferredLanguageID = table.Column<int>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    GenderID = table.Column<int>(nullable: true),
                    SexualityID = table.Column<int>(nullable: true),
                    BiologicalSexID = table.Column<int>(nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Phone1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    PersonScreeningStatusID = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    AgencyID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_Person_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Gender_BiologicalSexID",
                        column: x => x.BiologicalSexID,
                        principalSchema: "info",
                        principalTable: "Gender",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Gender_GenderID",
                        column: x => x.GenderID,
                        principalSchema: "info",
                        principalTable: "Gender",
                        principalColumn: "GenderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_PersonScreeningStatus_PersonScreeningStatusID",
                        column: x => x.PersonScreeningStatusID,
                        principalSchema: "info",
                        principalTable: "PersonScreeningStatus",
                        principalColumn: "PersonScreeningStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Language_PreferredLanguageID",
                        column: x => x.PreferredLanguageID,
                        principalSchema: "info",
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Language_PrimaryLanguageID",
                        column: x => x.PrimaryLanguageID,
                        principalSchema: "info",
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_Sexuality_SexualityID",
                        column: x => x.SexualityID,
                        principalSchema: "info",
                        principalTable: "Sexuality",
                        principalColumn: "SexualityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Person_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Collaboration",
                columns: table => new
                {
                    CollaborationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborationIndex = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    ReportingUnitID = table.Column<int>(nullable: false),
                    TherapyTypeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IntervalDays = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Abbreviation = table.Column<string>(nullable: true),
                    AgencyID = table.Column<long>(nullable: false),
                    CollaborationLeadUserID = table.Column<int>(nullable: true),
                    CollaborationLevelID = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaboration", x => x.CollaborationID);
                    table.ForeignKey(
                        name: "FK_Collaboration_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collaboration_CollaborationLevel_CollaborationLevelID",
                        column: x => x.CollaborationLevelID,
                        principalSchema: "info",
                        principalTable: "CollaborationLevel",
                        principalColumn: "CollaborationLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collaboration_TherapyType_TherapyTypeID",
                        column: x => x.TherapyTypeID,
                        principalSchema: "info",
                        principalTable: "TherapyType",
                        principalColumn: "TherapyTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collaboration_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "info",
                columns: table => new
                {
                    PermissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationObjectID = table.Column<int>(nullable: false),
                    OperationTypeID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionID);
                    table.ForeignKey(
                        name: "FK_Permission_ApplicationObject_ApplicationObjectID",
                        column: x => x.ApplicationObjectID,
                        principalTable: "ApplicationObject",
                        principalColumn: "ApplicationObjectID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_OperationType_OperationTypeID",
                        column: x => x.OperationTypeID,
                        principalSchema: "info",
                        principalTable: "OperationType",
                        principalColumn: "OperationTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgencyContact",
                columns: table => new
                {
                    AgencyContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false),
                    ListOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContact", x => x.AgencyContactID);
                    table.ForeignKey(
                        name: "FK_AgencyContact_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgencyContact_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "info",
                        principalTable: "Contact",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireNotifyRiskSchedule",
                columns: table => new
                {
                    QuestionnaireNotifyRiskScheduleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireNotifyRiskSchedule", x => x.QuestionnaireNotifyRiskScheduleID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireNotifyRiskSchedule_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireNotifyRiskSchedule_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireWindow",
                columns: table => new
                {
                    QuestionnaireWindowID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    AssessmentReasonID = table.Column<int>(nullable: false),
                    DueDateOffsetDays = table.Column<int>(nullable: true),
                    WindowOpenOffsetDays = table.Column<int>(nullable: true),
                    WindowCloseOffsetDays = table.Column<int>(nullable: true),
                    CanRepeat = table.Column<bool>(nullable: false),
                    RepeatIntervalDays = table.Column<int>(nullable: true),
                    IsSelected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireWindow", x => x.QuestionnaireWindowID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireWindow_AssessmentReason_AssessmentReasonID",
                        column: x => x.AssessmentReasonID,
                        principalSchema: "info",
                        principalTable: "AssessmentReason",
                        principalColumn: "AssessmentReasonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireWindow_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplate",
                columns: table => new
                {
                    NotificationTemplateID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationLevelID = table.Column<int>(nullable: false),
                    NotificationModeID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TemplateText = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplate", x => x.NotificationTemplateID);
                    table.ForeignKey(
                        name: "FK_NotificationTemplate_NotificationLevel_NotificationLevelID",
                        column: x => x.NotificationLevelID,
                        principalSchema: "info",
                        principalTable: "NotificationLevel",
                        principalColumn: "NotificationLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationTemplate_NotificationMode_NotificationModeID",
                        column: x => x.NotificationModeID,
                        principalSchema: "info",
                        principalTable: "NotificationMode",
                        principalColumn: "NotificationModeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireReminderType",
                schema: "info",
                columns: table => new
                {
                    QuestionnaireReminderTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Abbrev = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ListOrder = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    NotificationLevelID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireReminderType", x => x.QuestionnaireReminderTypeID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireReminderType_NotificationLevel_NotificationLevelID",
                        column: x => x.NotificationLevelID,
                        principalSchema: "info",
                        principalTable: "NotificationLevel",
                        principalColumn: "NotificationLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireReminderType_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireItem",
                columns: table => new
                {
                    QuestionnaireItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireItemIndex = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID ( )"),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    IsOptional = table.Column<bool>(nullable: false, defaultValue: false),
                    CanOverrideLowerResponseBehavior = table.Column<bool>(nullable: false, defaultValue: true),
                    CanOverrideMedianResponseBehavior = table.Column<bool>(nullable: false, defaultValue: true),
                    CanOverrideUpperResponseBehavior = table.Column<bool>(nullable: false, defaultValue: true),
                    LowerItemResponseBehaviorID = table.Column<int>(nullable: true),
                    MedianItemResponseBehaviorID = table.Column<int>(nullable: true),
                    UpperItemResponseBehaviorID = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    LowerResponseValue = table.Column<int>(nullable: true),
                    UpperResponseValue = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireItem", x => x.QuestionnaireItemID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItem_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "info",
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItem_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItem_ItemResponseBehavior_LowerItemResponseBehaviorID",
                        column: x => x.LowerItemResponseBehaviorID,
                        principalSchema: "info",
                        principalTable: "ItemResponseBehavior",
                        principalColumn: "ItemResponseBehaviorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItem_ItemResponseBehavior_MedianItemResponseBehaviorID",
                        column: x => x.MedianItemResponseBehaviorID,
                        principalSchema: "info",
                        principalTable: "ItemResponseBehavior",
                        principalColumn: "ItemResponseBehaviorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItem_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItem_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItem_ItemResponseBehavior_UpperItemResponseBehaviorID",
                        column: x => x.UpperItemResponseBehaviorID,
                        principalSchema: "info",
                        principalTable: "ItemResponseBehavior",
                        principalColumn: "ItemResponseBehaviorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                schema: "info",
                columns: table => new
                {
                    ConfigurationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    ContextFKValue = table.Column<int>(nullable: false),
                    ConfigurationParameterContextID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => x.ConfigurationID);
                    table.ForeignKey(
                        name: "FK_Configuration_ConfigurationParameterContext_ConfigurationParameterContextID",
                        column: x => x.ConfigurationParameterContextID,
                        principalSchema: "info",
                        principalTable: "ConfigurationParameterContext",
                        principalColumn: "ConfigurationParameterContextID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelperAddress",
                columns: table => new
                {
                    HelperAddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HelperAddressIndex = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID ( )"),
                    HelperID = table.Column<int>(nullable: false),
                    AddressID = table.Column<long>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelperAddress", x => x.HelperAddressID);
                    table.ForeignKey(
                        name: "FK_HelperAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HelperAddress_Helper_HelperID",
                        column: x => x.HelperID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HelperContact",
                columns: table => new
                {
                    HelperContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<int>(nullable: false),
                    HelperID = table.Column<int>(nullable: false),
                    ListOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelperContact", x => x.HelperContactID);
                    table.ForeignKey(
                        name: "FK_HelperContact_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "info",
                        principalTable: "Contact",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HelperContact_Helper_HelperID",
                        column: x => x.HelperID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationLog",
                columns: table => new
                {
                    NotificationLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationDate = table.Column<DateTime>(nullable: false),
                    PersonID = table.Column<long>(nullable: false),
                    NotificationTypeID = table.Column<int>(nullable: false),
                    FKeyValue = table.Column<int>(nullable: true),
                    NotificationData = table.Column<string>(nullable: true),
                    NotificationResolutionStatusID = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLog", x => x.NotificationLogID);
                    table.ForeignKey(
                        name: "FK_NotificationLog_NotificationResolutionStatus_NotificationResolutionStatusID",
                        column: x => x.NotificationResolutionStatusID,
                        principalSchema: "info",
                        principalTable: "NotificationResolutionStatus",
                        principalColumn: "NotificationResolutionStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationLog_NotificationType_NotificationTypeID",
                        column: x => x.NotificationTypeID,
                        principalSchema: "info",
                        principalTable: "NotificationType",
                        principalColumn: "NotificationTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationLog_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationLog_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddress",
                columns: table => new
                {
                    PersonAddressID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    AddressID = table.Column<long>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddress", x => x.PersonAddressID);
                    table.ForeignKey(
                        name: "FK_PersonAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonAddress_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonContact",
                columns: table => new
                {
                    PersonContactID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<int>(nullable: false),
                    PersonID = table.Column<long>(nullable: false),
                    ListOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContact", x => x.PersonContactID);
                    table.ForeignKey(
                        name: "FK_PersonContact_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "info",
                        principalTable: "Contact",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonContact_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonHelper",
                columns: table => new
                {
                    PersonHelperID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    HelperID = table.Column<int>(nullable: false),
                    IsLead = table.Column<bool>(nullable: false),
                    IsCurrent = table.Column<bool>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonHelper", x => x.PersonHelperID);
                    table.ForeignKey(
                        name: "FK_PersonHelper_Helper_HelperID",
                        column: x => x.HelperID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonHelper_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonHelper_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonIdentification",
                columns: table => new
                {
                    PersonIdentificationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    IdentificationTypeID = table.Column<int>(nullable: false),
                    IdentificationNumber = table.Column<string>(unicode: false, nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonIdentification", x => x.PersonIdentificationID);
                    table.ForeignKey(
                        name: "FK_PersonIdentification_IdentificationType_IdentificationTypeID",
                        column: x => x.IdentificationTypeID,
                        principalSchema: "info",
                        principalTable: "IdentificationType",
                        principalColumn: "IdentificationTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonIdentification_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonIdentification_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonLanguage",
                columns: table => new
                {
                    PersonID = table.Column<long>(nullable: false),
                    LanguageID = table.Column<int>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false),
                    IsPreferred = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLanguage", x => new { x.PersonID, x.LanguageID });
                    table.ForeignKey(
                        name: "FK_PersonLanguage_Language_LanguageID",
                        column: x => x.LanguageID,
                        principalSchema: "info",
                        principalTable: "Language",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonLanguage_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonNote",
                columns: table => new
                {
                    PersonNoteID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    NoteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNote", x => x.PersonNoteID);
                    table.ForeignKey(
                        name: "FK_PersonNote_Note_NoteID",
                        column: x => x.NoteID,
                        principalTable: "Note",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonNote_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonRaceEthnicity",
                columns: table => new
                {
                    PersonRaceEthnicityID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    RaceEthnicityID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRaceEthnicity", x => x.PersonRaceEthnicityID);
                    table.ForeignKey(
                        name: "FK_PersonRaceEthnicity_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonRaceEthnicity_RaceEthnicity_RaceEthnicityID",
                        column: x => x.RaceEthnicityID,
                        principalSchema: "info",
                        principalTable: "RaceEthnicity",
                        principalColumn: "RaceEthnicityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonSupport",
                columns: table => new
                {
                    PersonSupportID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    SupportTypeID = table.Column<int>(nullable: false),
                    IsCurrent = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    LastName = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Suffix = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Note = table.Column<string>(unicode: false, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSupport", x => x.PersonSupportID);
                    table.ForeignKey(
                        name: "FK_PersonSupport_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonSupport_SupportType_SupportTypeID",
                        column: x => x.SupportTypeID,
                        principalSchema: "info",
                        principalTable: "SupportType",
                        principalColumn: "SupportTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonSupport_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationAgencyAddress",
                columns: table => new
                {
                    CollaborationID = table.Column<int>(nullable: false),
                    AddressID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationAgencyAddress", x => new { x.CollaborationID, x.AddressID });
                    table.ForeignKey(
                        name: "FK_CollaborationAgencyAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborationAgencyAddress_Collaboration_CollaborationID",
                        column: x => x.CollaborationID,
                        principalTable: "Collaboration",
                        principalColumn: "CollaborationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationLeadHistory",
                columns: table => new
                {
                    CollaborationLeadHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborationID = table.Column<int>(nullable: false),
                    LeadUserID = table.Column<int>(nullable: true),
                    RemovedUserID = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationLeadHistory", x => x.CollaborationLeadHistoryID);
                    table.ForeignKey(
                        name: "FK_CollaborationLeadHistory_Collaboration_CollaborationID",
                        column: x => x.CollaborationID,
                        principalTable: "Collaboration",
                        principalColumn: "CollaborationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationQuestionnaire",
                columns: table => new
                {
                    CollaborationQuestionnaireID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborationID = table.Column<int>(nullable: false),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    IsMandatory = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationQuestionnaire", x => x.CollaborationQuestionnaireID);
                    table.ForeignKey(
                        name: "FK_CollaborationQuestionnaire_Collaboration_CollaborationID",
                        column: x => x.CollaborationID,
                        principalTable: "Collaboration",
                        principalColumn: "CollaborationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborationQuestionnaire_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationSharing",
                columns: table => new
                {
                    CollaborationSharingID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborationSharingIndex = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    ReportingUnitID = table.Column<int>(nullable: false),
                    AgencyID = table.Column<long>(nullable: false),
                    CollaborationID = table.Column<int>(nullable: false),
                    CollaborationSharingPolicyID = table.Column<int>(nullable: true),
                    HistoricalView = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationSharing", x => x.CollaborationSharingID);
                    table.ForeignKey(
                        name: "FK_CollaborationSharing_Agency_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agency",
                        principalColumn: "AgencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborationSharing_Collaboration_CollaborationID",
                        column: x => x.CollaborationID,
                        principalTable: "Collaboration",
                        principalColumn: "CollaborationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborationSharing_ReportingUnit_ReportingUnitID",
                        column: x => x.ReportingUnitID,
                        principalTable: "ReportingUnit",
                        principalColumn: "ReportingUnitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationTag",
                columns: table => new
                {
                    CollaborationTagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborationID = table.Column<int>(nullable: false),
                    CollaborationTagTypeID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationTag", x => x.CollaborationTagID);
                    table.ForeignKey(
                        name: "FK_CollaborationTag_Collaboration_CollaborationID",
                        column: x => x.CollaborationID,
                        principalTable: "Collaboration",
                        principalColumn: "CollaborationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborationTag_CollaborationTagType_CollaborationTagTypeID",
                        column: x => x.CollaborationTagTypeID,
                        principalSchema: "info",
                        principalTable: "CollaborationTagType",
                        principalColumn: "CollaborationTagTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonCollaboration",
                columns: table => new
                {
                    PersonCollaborationID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    CollaborationID = table.Column<int>(nullable: false),
                    EnrollDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsPrimary = table.Column<bool>(nullable: true),
                    IsCurrent = table.Column<bool>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCollaboration", x => x.PersonCollaborationID);
                    table.ForeignKey(
                        name: "FK_PersonCollaboration_Collaboration_CollaborationID",
                        column: x => x.CollaborationID,
                        principalTable: "Collaboration",
                        principalColumn: "CollaborationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonCollaboration_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonCollaboration_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonQuestionnaire",
                columns: table => new
                {
                    PersonQuestionnaireID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<long>(nullable: false),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    CollaborationID = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDueDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonQuestionnaire", x => x.PersonQuestionnaireID);
                    table.ForeignKey(
                        name: "FK_PersonQuestionnaire_Collaboration_CollaborationID",
                        column: x => x.CollaborationID,
                        principalTable: "Collaboration",
                        principalColumn: "CollaborationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonQuestionnaire_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonQuestionnaire_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonQuestionnaire_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "info",
                columns: table => new
                {
                    RolePermissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleID = table.Column<int>(nullable: false),
                    PermissionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.RolePermissionID);
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalSchema: "info",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermission_UserRole_UserRoleID",
                        column: x => x.UserRoleID,
                        principalTable: "UserRole",
                        principalColumn: "UserRoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SystemRolePermission",
                schema: "info",
                columns: table => new
                {
                    SystemRolePermissionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemRoleID = table.Column<int>(nullable: false),
                    PermissionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRolePermission", x => x.SystemRolePermissionID);
                    table.ForeignKey(
                        name: "FK_SystemRolePermission_Permission_PermissionID",
                        column: x => x.PermissionID,
                        principalSchema: "info",
                        principalTable: "Permission",
                        principalColumn: "PermissionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemRolePermission_SystemRole_SystemRoleID",
                        column: x => x.SystemRoleID,
                        principalSchema: "info",
                        principalTable: "SystemRole",
                        principalColumn: "SystemRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireNotifyRiskRule",
                columns: table => new
                {
                    QuestionnaireNotifyRiskRuleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    QuestionnaireNotifyRiskScheduleID = table.Column<int>(nullable: false),
                    NotificationLevelID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireNotifyRiskRule", x => x.QuestionnaireNotifyRiskRuleID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireNotifyRiskRule_NotificationLevel_NotificationLevelID",
                        column: x => x.NotificationLevelID,
                        principalSchema: "info",
                        principalTable: "NotificationLevel",
                        principalColumn: "NotificationLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireNotifyRiskRule_QuestionnaireNotifyRiskSchedule_QuestionnaireNotifyRiskScheduleID",
                        column: x => x.QuestionnaireNotifyRiskScheduleID,
                        principalTable: "QuestionnaireNotifyRiskSchedule",
                        principalColumn: "QuestionnaireNotifyRiskScheduleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireNotifyRiskRule_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireReminderRule",
                columns: table => new
                {
                    QuestionnaireReminderRuleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    QuestionnaireReminderTypeID = table.Column<int>(nullable: false),
                    ReminderOffsetDays = table.Column<int>(nullable: true),
                    CanRepeat = table.Column<bool>(nullable: false),
                    RepeatInterval = table.Column<int>(nullable: true),
                    IsSelected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireReminderRule", x => x.QuestionnaireReminderRuleID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireReminderRule_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireReminderRule_QuestionnaireReminderType_QuestionnaireReminderTypeID",
                        column: x => x.QuestionnaireReminderTypeID,
                        principalSchema: "info",
                        principalTable: "QuestionnaireReminderType",
                        principalColumn: "QuestionnaireReminderTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotifyRiskRule",
                columns: table => new
                {
                    NotifyRiskRuleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireItemID = table.Column<int>(nullable: false),
                    NotifyThresholdMinimumListOrder = table.Column<int>(nullable: false),
                    NotificationLevelID = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyRiskRule", x => x.NotifyRiskRuleID);
                    table.ForeignKey(
                        name: "FK_NotifyRiskRule_NotificationLevel_NotificationLevelID",
                        column: x => x.NotificationLevelID,
                        principalSchema: "info",
                        principalTable: "NotificationLevel",
                        principalColumn: "NotificationLevelID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotifyRiskRule_QuestionnaireItem_QuestionnaireItemID",
                        column: x => x.QuestionnaireItemID,
                        principalTable: "QuestionnaireItem",
                        principalColumn: "QuestionnaireItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotifyRiskRule_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireItemHistory",
                columns: table => new
                {
                    QuestionnaireItemHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireItemID = table.Column<int>(nullable: false),
                    InactiveStartDate = table.Column<DateTime>(nullable: false),
                    InactiveEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireItemHistory", x => x.QuestionnaireItemHistoryID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireItemHistory_QuestionnaireItem_QuestionnaireItemID",
                        column: x => x.QuestionnaireItemID,
                        principalTable: "QuestionnaireItem",
                        principalColumn: "QuestionnaireItemID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationAttachment",
                schema: "info",
                columns: table => new
                {
                    ConfigurationAttachmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigurationID = table.Column<int>(nullable: false),
                    AttachmentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationAttachment", x => x.ConfigurationAttachmentID);
                    table.ForeignKey(
                        name: "FK_ConfigurationAttachment_Attachment_AttachmentID",
                        column: x => x.AttachmentID,
                        principalSchema: "info",
                        principalTable: "Attachment",
                        principalColumn: "AttachmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigurationAttachment_Configuration_ConfigurationID",
                        column: x => x.ConfigurationID,
                        principalSchema: "info",
                        principalTable: "Configuration",
                        principalColumn: "ConfigurationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationDelivery",
                columns: table => new
                {
                    NotificationDeliveryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationLogID = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    NotificationTemplateID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationDelivery", x => x.NotificationDeliveryID);
                    table.ForeignKey(
                        name: "FK_NotificationDelivery_NotificationLog_NotificationLogID",
                        column: x => x.NotificationLogID,
                        principalTable: "NotificationLog",
                        principalColumn: "NotificationLogID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationDelivery_NotificationTemplate_NotificationTemplateID",
                        column: x => x.NotificationTemplateID,
                        principalTable: "NotificationTemplate",
                        principalColumn: "NotificationTemplateID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationResolutionHistory",
                columns: table => new
                {
                    NotificationResolutionHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationLogID = table.Column<int>(nullable: false),
                    RecordedDate = table.Column<DateTime>(nullable: true),
                    StatusFrom = table.Column<int>(nullable: false),
                    StatusTo = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationResolutionHistory", x => x.NotificationResolutionHistoryID);
                    table.ForeignKey(
                        name: "FK_NotificationResolutionHistory_NotificationLog_NotificationLogID",
                        column: x => x.NotificationLogID,
                        principalTable: "NotificationLog",
                        principalColumn: "NotificationLogID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationResolutionHistory_NotificationResolutionStatus_StatusFrom",
                        column: x => x.StatusFrom,
                        principalSchema: "info",
                        principalTable: "NotificationResolutionStatus",
                        principalColumn: "NotificationResolutionStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationResolutionHistory_NotificationResolutionStatus_StatusTo",
                        column: x => x.StatusTo,
                        principalSchema: "info",
                        principalTable: "NotificationResolutionStatus",
                        principalColumn: "NotificationResolutionStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationResolutionHistory_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportAddress",
                columns: table => new
                {
                    SupportAddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupportID = table.Column<int>(nullable: false),
                    AddressID = table.Column<long>(nullable: false),
                    IsPrimary = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportAddress", x => x.SupportAddressID);
                    table.ForeignKey(
                        name: "FK_SupportAddress_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportAddress_PersonSupport_SupportID",
                        column: x => x.SupportID,
                        principalTable: "PersonSupport",
                        principalColumn: "PersonSupportID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportContact",
                columns: table => new
                {
                    SupportContactID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactID = table.Column<int>(nullable: false),
                    SupportID = table.Column<int>(nullable: false),
                    ListOrder = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportContact", x => x.SupportContactID);
                    table.ForeignKey(
                        name: "FK_SupportContact_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "info",
                        principalTable: "Contact",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportContact_PersonSupport_SupportID",
                        column: x => x.SupportID,
                        principalTable: "PersonSupport",
                        principalColumn: "PersonSupportID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationSharingHistory",
                columns: table => new
                {
                    CollaborationSharingHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportingUnitCollaborationID = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    HistoricalView = table.Column<bool>(nullable: false),
                    RemovedUserID = table.Column<int>(nullable: true),
                    RemovedNoteID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationSharingHistory", x => x.CollaborationSharingHistoryID);
                    table.ForeignKey(
                        name: "FK_CollaborationSharingHistory_CollaborationSharing_ReportingUnitCollaborationID",
                        column: x => x.ReportingUnitCollaborationID,
                        principalTable: "CollaborationSharing",
                        principalColumn: "CollaborationSharingID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CollaborationSharingPolicy",
                columns: table => new
                {
                    CollaborationSharingPolicyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaborationSharingID = table.Column<int>(nullable: false),
                    SharingPolicyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaborationSharingPolicy", x => x.CollaborationSharingPolicyID);
                    table.ForeignKey(
                        name: "FK_CollaborationSharingPolicy_CollaborationSharing_CollaborationSharingID",
                        column: x => x.CollaborationSharingID,
                        principalTable: "CollaborationSharing",
                        principalColumn: "CollaborationSharingID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollaborationSharingPolicy_SharingPolicy_SharingPolicyID",
                        column: x => x.SharingPolicyID,
                        principalSchema: "info",
                        principalTable: "SharingPolicy",
                        principalColumn: "SharingPolicyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonQuestionnaireSchedule",
                columns: table => new
                {
                    PersonQuestionnaireScheduleID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonQuestionnaireID = table.Column<long>(nullable: false),
                    WindowDueDate = table.Column<DateTime>(nullable: false),
                    QuestionnaireWindowID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonQuestionnaireSchedule", x => x.PersonQuestionnaireScheduleID);
                    table.ForeignKey(
                        name: "FK_PersonQuestionnaireSchedule_PersonQuestionnaire_PersonQuestionnaireID",
                        column: x => x.PersonQuestionnaireID,
                        principalTable: "PersonQuestionnaire",
                        principalColumn: "PersonQuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonQuestionnaireSchedule_QuestionnaireWindow_QuestionnaireWindowID",
                        column: x => x.QuestionnaireWindowID,
                        principalTable: "QuestionnaireWindow",
                        principalColumn: "QuestionnaireWindowID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionnaireNotifyRiskRuleCondition",
                columns: table => new
                {
                    QuestionnaireNotifyRiskRuleConditionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireItemID = table.Column<int>(nullable: false),
                    ComparisonOperator = table.Column<string>(nullable: true),
                    ComparisonValue = table.Column<int>(nullable: false),
                    QuestionnaireNotifyRiskRuleID = table.Column<int>(nullable: false),
                    ListOrder = table.Column<int>(nullable: false),
                    JoiningOperator = table.Column<string>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireNotifyRiskRuleCondition", x => x.QuestionnaireNotifyRiskRuleConditionID);
                    table.ForeignKey(
                        name: "FK_QuestionnaireNotifyRiskRuleCondition_QuestionnaireItem_QuestionnaireItemID",
                        column: x => x.QuestionnaireItemID,
                        principalTable: "QuestionnaireItem",
                        principalColumn: "QuestionnaireItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireNotifyRiskRuleCondition_QuestionnaireNotifyRiskRule_QuestionnaireNotifyRiskRuleID",
                        column: x => x.QuestionnaireNotifyRiskRuleID,
                        principalTable: "QuestionnaireNotifyRiskRule",
                        principalColumn: "QuestionnaireNotifyRiskRuleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireNotifyRiskRuleCondition_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationResolutionNote",
                columns: table => new
                {
                    NotificationResolutionNoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationLogID = table.Column<int>(nullable: false),
                    Note_NoteID = table.Column<int>(nullable: false),
                    NotificationResolutionHistoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationResolutionNote", x => x.NotificationResolutionNoteID);
                    table.ForeignKey(
                        name: "FK_NotificationResolutionNote_Note_Note_NoteID",
                        column: x => x.Note_NoteID,
                        principalTable: "Note",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationResolutionNote_NotificationLog_NotificationLogID",
                        column: x => x.NotificationLogID,
                        principalTable: "NotificationLog",
                        principalColumn: "NotificationLogID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationResolutionNote_NotificationResolutionHistory_NotificationResolutionHistoryID",
                        column: x => x.NotificationResolutionHistoryID,
                        principalTable: "NotificationResolutionHistory",
                        principalColumn: "NotificationResolutionHistoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    AssessmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonQuestionnaireID = table.Column<long>(nullable: false),
                    VoiceTypeID = table.Column<int>(nullable: false),
                    DateTaken = table.Column<DateTime>(nullable: false),
                    ReasoningText = table.Column<string>(nullable: true),
                    AssessmentReasonID = table.Column<int>(nullable: false),
                    AssessmentStatusID = table.Column<int>(nullable: false),
                    PersonQuestionnaireScheduleID = table.Column<long>(nullable: true),
                    IsUpdate = table.Column<bool>(nullable: false),
                    Approved = table.Column<int>(nullable: true),
                    CloseDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.AssessmentID);
                    table.ForeignKey(
                        name: "FK_Assessment_AssessmentReason_AssessmentReasonID",
                        column: x => x.AssessmentReasonID,
                        principalSchema: "info",
                        principalTable: "AssessmentReason",
                        principalColumn: "AssessmentReasonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assessment_AssessmentStatus_AssessmentStatusID",
                        column: x => x.AssessmentStatusID,
                        principalSchema: "info",
                        principalTable: "AssessmentStatus",
                        principalColumn: "AssessmentStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assessment_PersonQuestionnaire_PersonQuestionnaireID",
                        column: x => x.PersonQuestionnaireID,
                        principalTable: "PersonQuestionnaire",
                        principalColumn: "PersonQuestionnaireID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assessment_PersonQuestionnaireSchedule_PersonQuestionnaireScheduleID",
                        column: x => x.PersonQuestionnaireScheduleID,
                        principalTable: "PersonQuestionnaireSchedule",
                        principalColumn: "PersonQuestionnaireScheduleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assessment_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assessment_VoiceType_VoiceTypeID",
                        column: x => x.VoiceTypeID,
                        principalSchema: "info",
                        principalTable: "VoiceType",
                        principalColumn: "VoiceTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotifyReminder",
                columns: table => new
                {
                    NotifyReminderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonQuestionnaireScheduleID = table.Column<long>(nullable: true),
                    QuestionnaireReminderRuleID = table.Column<int>(nullable: false),
                    NotifyDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyReminder", x => x.NotifyReminderID);
                    table.ForeignKey(
                        name: "FK_NotifyReminder_PersonQuestionnaireSchedule_PersonQuestionnaireScheduleID",
                        column: x => x.PersonQuestionnaireScheduleID,
                        principalTable: "PersonQuestionnaireSchedule",
                        principalColumn: "PersonQuestionnaireScheduleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotifyReminder_QuestionnaireReminderRule_QuestionnaireReminderRuleID",
                        column: x => x.QuestionnaireReminderRuleID,
                        principalTable: "QuestionnaireReminderRule",
                        principalColumn: "QuestionnaireReminderRuleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentEmailLinkDetails",
                columns: table => new
                {
                    AssessmentEmailLinkDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailLinkDetailsIndex = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    PersonIndex = table.Column<Guid>(nullable: false),
                    AssessmentID = table.Column<int>(nullable: false),
                    QuestionnaireID = table.Column<int>(nullable: false),
                    PersonSupportID = table.Column<int>(nullable: false),
                    HelperID = table.Column<int>(nullable: false),
                    PersonSupportEmail = table.Column<string>(nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AssessmentEmailLinkDetailsID", x => x.AssessmentEmailLinkDetailsID);
                    table.ForeignKey(
                        name: "FK_AssessmentEmailLinkDetails_Assessment_AssessmentID",
                        column: x => x.AssessmentID,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentEmailLinkDetails_Helper_HelperID",
                        column: x => x.HelperID,
                        principalTable: "Helper",
                        principalColumn: "HelperID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentEmailLinkDetails_PersonSupport_PersonSupportID",
                        column: x => x.PersonSupportID,
                        principalTable: "PersonSupport",
                        principalColumn: "PersonSupportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentEmailLinkDetails_Questionnaire_QuestionnaireID",
                        column: x => x.QuestionnaireID,
                        principalTable: "Questionnaire",
                        principalColumn: "QuestionnaireID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentNote",
                columns: table => new
                {
                    AssessmentNoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentID = table.Column<int>(nullable: false),
                    NoteID = table.Column<int>(nullable: false),
                    AssessmentReviewHistoryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentNote", x => x.AssessmentNoteID);
                    table.ForeignKey(
                        name: "FK_AssessmentNote_Assessment_AssessmentID",
                        column: x => x.AssessmentID,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentNote_ReviewerHistory_AssessmentReviewHistoryID",
                        column: x => x.AssessmentReviewHistoryID,
                        principalTable: "ReviewerHistory",
                        principalColumn: "AssessmentReviewHistoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentNote_Note_NoteID",
                        column: x => x.NoteID,
                        principalTable: "Note",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentResponse",
                columns: table => new
                {
                    AssessmentResponseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentID = table.Column<int>(nullable: false),
                    PersonSupportID = table.Column<int>(nullable: true),
                    ResponseID = table.Column<int>(nullable: false),
                    ItemResponseBehaviorID = table.Column<int>(nullable: true),
                    IsRequiredConfidential = table.Column<bool>(nullable: false),
                    IsPersonRequestedConfidential = table.Column<bool>(nullable: true),
                    IsOtherConfidential = table.Column<bool>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false),
                    QuestionnaireItemID = table.Column<int>(nullable: false),
                    IsCloned = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentResponse", x => x.AssessmentResponseID);
                    table.ForeignKey(
                        name: "FK_AssessmentResponse_Assessment_AssessmentID",
                        column: x => x.AssessmentID,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResponse_ItemResponseBehavior_ItemResponseBehaviorID",
                        column: x => x.ItemResponseBehaviorID,
                        principalSchema: "info",
                        principalTable: "ItemResponseBehavior",
                        principalColumn: "ItemResponseBehaviorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResponse_PersonSupport_PersonSupportID",
                        column: x => x.PersonSupportID,
                        principalTable: "PersonSupport",
                        principalColumn: "PersonSupportID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResponse_QuestionnaireItem_QuestionnaireItemID",
                        column: x => x.QuestionnaireItemID,
                        principalTable: "QuestionnaireItem",
                        principalColumn: "QuestionnaireItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResponse_Response_ResponseID",
                        column: x => x.ResponseID,
                        principalTable: "Response",
                        principalColumn: "ResponseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResponse_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotifyRisk",
                columns: table => new
                {
                    NotifyRiskID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnaireNotifyRiskRuleID = table.Column<int>(nullable: false),
                    PersonID = table.Column<long>(nullable: false),
                    AssessmentID = table.Column<int>(nullable: false),
                    NotifyDate = table.Column<DateTime>(nullable: true),
                    CloseDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    UpdateUserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyRisk", x => x.NotifyRiskID);
                    table.ForeignKey(
                        name: "FK_NotifyRisk_Assessment_AssessmentID",
                        column: x => x.AssessmentID,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotifyRisk_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotifyRisk_QuestionnaireNotifyRiskRule_QuestionnaireNotifyRiskRuleID",
                        column: x => x.QuestionnaireNotifyRiskRuleID,
                        principalTable: "QuestionnaireNotifyRiskRule",
                        principalColumn: "QuestionnaireNotifyRiskRuleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotifyRisk_User_UpdateUserID",
                        column: x => x.UpdateUserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentResponseNote",
                columns: table => new
                {
                    AssessmentResponseNoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssessmentResponseID = table.Column<int>(nullable: false),
                    NoteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentResponseNote", x => x.AssessmentResponseNoteID);
                    table.ForeignKey(
                        name: "FK_AssessmentResponseNote_AssessmentResponse_AssessmentResponseID",
                        column: x => x.AssessmentResponseID,
                        principalTable: "AssessmentResponse",
                        principalColumn: "AssessmentResponseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssessmentResponseNote_Note_NoteID",
                        column: x => x.NoteID,
                        principalTable: "Note",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentResponseText",
                columns: table => new
                {
                    AssessmentResponseTextID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponseText = table.Column<string>(unicode: false, nullable: true),
                    AssessmentResponseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentResponseText", x => x.AssessmentResponseTextID);
                    table.ForeignKey(
                        name: "FK_AssessmentResponseText_AssessmentResponse_AssessmentResponseID",
                        column: x => x.AssessmentResponseID,
                        principalTable: "AssessmentResponse",
                        principalColumn: "AssessmentResponseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotifyRiskValue",
                columns: table => new
                {
                    NotifyRiskValueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotifyRiskID = table.Column<int>(nullable: false),
                    QuestionnaireNotifyRiskRuleConditionID = table.Column<int>(nullable: false),
                    AssessmentResponseID = table.Column<int>(nullable: false),
                    AssessmentResponseValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotifyRiskValue", x => x.NotifyRiskValueID);
                    table.ForeignKey(
                        name: "FK_NotifyRiskValue_AssessmentResponse_AssessmentResponseID",
                        column: x => x.AssessmentResponseID,
                        principalTable: "AssessmentResponse",
                        principalColumn: "AssessmentResponseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotifyRiskValue_NotifyRisk_NotifyRiskID",
                        column: x => x.NotifyRiskID,
                        principalTable: "NotifyRisk",
                        principalColumn: "NotifyRiskID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotifyRiskValue_QuestionnaireNotifyRiskRuleCondition_QuestionnaireNotifyRiskRuleConditionID",
                        column: x => x.QuestionnaireNotifyRiskRuleConditionID,
                        principalTable: "QuestionnaireNotifyRiskRuleCondition",
                        principalColumn: "QuestionnaireNotifyRiskRuleConditionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressIndex",
                table: "Address",
                column: "AddressIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Address_CountryStateId",
                table: "Address",
                column: "CountryStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UpdateUserID",
                table: "Address",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Agency_AgencyIndex",
                table: "Agency",
                column: "AgencyIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Agency_UpdateUserID",
                table: "Agency",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyAddress_AddressID",
                table: "AgencyAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyAddress_AgencyID",
                table: "AgencyAddress",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyAddress_UpdateUserID",
                table: "AgencyAddress",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContact_AgencyID",
                table: "AgencyContact",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyContact_ContactID",
                table: "AgencyContact",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencySharing_AgencySharingIndex",
                table: "AgencySharing",
                column: "AgencySharingIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_AgencySharingHistory_ReportingUnitAgencyID",
                table: "AgencySharingHistory",
                column: "ReportingUnitAgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationObject_ApplicationObjectTypeID",
                table: "ApplicationObject",
                column: "ApplicationObjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationObject_UpdateUserID",
                table: "ApplicationObject",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationObjectType_UpdateUserID",
                table: "ApplicationObjectType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_AssessmentReasonID",
                table: "Assessment",
                column: "AssessmentReasonID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_AssessmentStatusID",
                table: "Assessment",
                column: "AssessmentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_PersonQuestionnaireID",
                table: "Assessment",
                column: "PersonQuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_PersonQuestionnaireScheduleID",
                table: "Assessment",
                column: "PersonQuestionnaireScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_UpdateUserID",
                table: "Assessment",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_VoiceTypeID",
                table: "Assessment",
                column: "VoiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentEmailLinkDetails_AssessmentID",
                table: "AssessmentEmailLinkDetails",
                column: "AssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentEmailLinkDetails_HelperID",
                table: "AssessmentEmailLinkDetails",
                column: "HelperID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentEmailLinkDetails_PersonSupportID",
                table: "AssessmentEmailLinkDetails",
                column: "PersonSupportID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentEmailLinkDetails_QuestionnaireID",
                table: "AssessmentEmailLinkDetails",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentNote_AssessmentID",
                table: "AssessmentNote",
                column: "AssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentNote_AssessmentReviewHistoryID",
                table: "AssessmentNote",
                column: "AssessmentReviewHistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentNote_NoteID",
                table: "AssessmentNote",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponse_AssessmentID",
                table: "AssessmentResponse",
                column: "AssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponse_ItemResponseBehaviorID",
                table: "AssessmentResponse",
                column: "ItemResponseBehaviorID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponse_PersonSupportID",
                table: "AssessmentResponse",
                column: "PersonSupportID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponse_QuestionnaireItemID",
                table: "AssessmentResponse",
                column: "QuestionnaireItemID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponse_ResponseID",
                table: "AssessmentResponse",
                column: "ResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponse_UpdateUserID",
                table: "AssessmentResponse",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponseNote_AssessmentResponseID",
                table: "AssessmentResponseNote",
                column: "AssessmentResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponseNote_NoteID",
                table: "AssessmentResponseNote",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentResponseText_AssessmentResponseID",
                table: "AssessmentResponseText",
                column: "AssessmentResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_Collaboration_AgencyID",
                table: "Collaboration",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Collaboration_CollaborationIndex",
                table: "Collaboration",
                column: "CollaborationIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Collaboration_CollaborationLevelID",
                table: "Collaboration",
                column: "CollaborationLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Collaboration_TherapyTypeID",
                table: "Collaboration",
                column: "TherapyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Collaboration_UpdateUserID",
                table: "Collaboration",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationAgencyAddress_AddressID",
                table: "CollaborationAgencyAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationLeadHistory_CollaborationID",
                table: "CollaborationLeadHistory",
                column: "CollaborationID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationQuestionnaire_CollaborationID",
                table: "CollaborationQuestionnaire",
                column: "CollaborationID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationQuestionnaire_QuestionnaireID",
                table: "CollaborationQuestionnaire",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharing_AgencyID",
                table: "CollaborationSharing",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharing_CollaborationID",
                table: "CollaborationSharing",
                column: "CollaborationID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharing_CollaborationSharingIndex",
                table: "CollaborationSharing",
                column: "CollaborationSharingIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharing_ReportingUnitID",
                table: "CollaborationSharing",
                column: "ReportingUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharingHistory_ReportingUnitCollaborationID",
                table: "CollaborationSharingHistory",
                column: "ReportingUnitCollaborationID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharingPolicy_CollaborationSharingID",
                table: "CollaborationSharingPolicy",
                column: "CollaborationSharingID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationSharingPolicy_SharingPolicyID",
                table: "CollaborationSharingPolicy",
                column: "SharingPolicyID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationTag_CollaborationID",
                table: "CollaborationTag",
                column: "CollaborationID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationTag_CollaborationTagTypeID",
                table: "CollaborationTag",
                column: "CollaborationTagTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Helper_AgencyID",
                table: "Helper",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Helper_HelperIndex",
                table: "Helper",
                column: "HelperIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Helper_HelperTitleID",
                table: "Helper",
                column: "HelperTitleID");

            migrationBuilder.CreateIndex(
                name: "IX_Helper_ReviewerID",
                table: "Helper",
                column: "ReviewerID");

            migrationBuilder.CreateIndex(
                name: "IX_Helper_SupervisorHelperID",
                table: "Helper",
                column: "SupervisorHelperID");

            migrationBuilder.CreateIndex(
                name: "IX_Helper_UpdateUserID",
                table: "Helper",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Helper_UserID",
                table: "Helper",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HelperAddress_AddressID",
                table: "HelperAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_HelperAddress_HelperAddressIndex",
                table: "HelperAddress",
                column: "HelperAddressIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_HelperAddress_HelperID",
                table: "HelperAddress",
                column: "HelperID");

            migrationBuilder.CreateIndex(
                name: "IX_HelperContact_ContactID",
                table: "HelperContact",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_HelperContact_HelperID",
                table: "HelperContact",
                column: "HelperID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "IdentityRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityRoleClaim_RoleId",
                table: "IdentityRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserClaim_UserId",
                table: "IdentityUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserLogin_UserId",
                table: "IdentityUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUserRole_RoleId",
                table: "IdentityUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUsers_AgencyId",
                table: "IdentityUsers",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "IdentityUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "IdentityUsers",
                columns: new[] { "NormalizedUserName", "AgencyId" },
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL AND [AgencyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAgency_AgencyID",
                table: "InstrumentAgency",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentAgency_InstrumentID",
                table: "InstrumentAgency",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemIndex",
                table: "Item",
                column: "ItemIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemResponseTypeID",
                table: "Item",
                column: "ItemResponseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ResponseValueTypeID",
                table: "Item",
                column: "ResponseValueTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UpdateUserID",
                table: "Item",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_UpdateUserID",
                table: "Note",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDelivery_NotificationLogID",
                table: "NotificationDelivery",
                column: "NotificationLogID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDelivery_NotificationTemplateID",
                table: "NotificationDelivery",
                column: "NotificationTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLog_NotificationResolutionStatusID",
                table: "NotificationLog",
                column: "NotificationResolutionStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLog_NotificationTypeID",
                table: "NotificationLog",
                column: "NotificationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLog_PersonID",
                table: "NotificationLog",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLog_UpdateUserID",
                table: "NotificationLog",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationResolutionHistory_NotificationLogID",
                table: "NotificationResolutionHistory",
                column: "NotificationLogID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationResolutionHistory_StatusFrom",
                table: "NotificationResolutionHistory",
                column: "StatusFrom");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationResolutionHistory_StatusTo",
                table: "NotificationResolutionHistory",
                column: "StatusTo");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationResolutionHistory_UpdateUserID",
                table: "NotificationResolutionHistory",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationResolutionNote_Note_NoteID",
                table: "NotificationResolutionNote",
                column: "Note_NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationResolutionNote_NotificationLogID",
                table: "NotificationResolutionNote",
                column: "NotificationLogID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationResolutionNote_NotificationResolutionHistoryID",
                table: "NotificationResolutionNote",
                column: "NotificationResolutionHistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplate_NotificationLevelID",
                table: "NotificationTemplate",
                column: "NotificationLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplate_NotificationModeID",
                table: "NotificationTemplate",
                column: "NotificationModeID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyReminder_PersonQuestionnaireScheduleID",
                table: "NotifyReminder",
                column: "PersonQuestionnaireScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyReminder_QuestionnaireReminderRuleID",
                table: "NotifyReminder",
                column: "QuestionnaireReminderRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRisk_AssessmentID",
                table: "NotifyRisk",
                column: "AssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRisk_PersonID",
                table: "NotifyRisk",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRisk_QuestionnaireNotifyRiskRuleID",
                table: "NotifyRisk",
                column: "QuestionnaireNotifyRiskRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRisk_UpdateUserID",
                table: "NotifyRisk",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRiskRule_NotificationLevelID",
                table: "NotifyRiskRule",
                column: "NotificationLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRiskRule_QuestionnaireItemID",
                table: "NotifyRiskRule",
                column: "QuestionnaireItemID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRiskRule_UpdateUserID",
                table: "NotifyRiskRule",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRiskValue_AssessmentResponseID",
                table: "NotifyRiskValue",
                column: "AssessmentResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRiskValue_NotifyRiskID",
                table: "NotifyRiskValue",
                column: "NotifyRiskID");

            migrationBuilder.CreateIndex(
                name: "IX_NotifyRiskValue_QuestionnaireNotifyRiskRuleConditionID",
                table: "NotifyRiskValue",
                column: "QuestionnaireNotifyRiskRuleConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AgencyID",
                table: "Person",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_BiologicalSexID",
                table: "Person",
                column: "BiologicalSexID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderID",
                table: "Person",
                column: "GenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonIndex",
                table: "Person",
                column: "PersonIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonScreeningStatusID",
                table: "Person",
                column: "PersonScreeningStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PreferredLanguageID",
                table: "Person",
                column: "PreferredLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PrimaryLanguageID",
                table: "Person",
                column: "PrimaryLanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_SexualityID",
                table: "Person",
                column: "SexualityID");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UpdateUserID",
                table: "Person",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddress_AddressID",
                table: "PersonAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddress_PersonID",
                table: "PersonAddress",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCollaboration_CollaborationID",
                table: "PersonCollaboration",
                column: "CollaborationID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCollaboration_PersonID",
                table: "PersonCollaboration",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCollaboration_UpdateUserID",
                table: "PersonCollaboration",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContact_ContactID",
                table: "PersonContact",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContact_PersonID",
                table: "PersonContact",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonHelper_HelperID",
                table: "PersonHelper",
                column: "HelperID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonHelper_PersonID",
                table: "PersonHelper",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonHelper_UpdateUserID",
                table: "PersonHelper",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentification_IdentificationTypeID",
                table: "PersonIdentification",
                column: "IdentificationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentification_PersonID",
                table: "PersonIdentification",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentification_UpdateUserID",
                table: "PersonIdentification",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonLanguage_LanguageID",
                table: "PersonLanguage",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNote_NoteID",
                table: "PersonNote",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNote_PersonID",
                table: "PersonNote",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonQuestionnaire_CollaborationID",
                table: "PersonQuestionnaire",
                column: "CollaborationID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonQuestionnaire_PersonID",
                table: "PersonQuestionnaire",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonQuestionnaire_QuestionnaireID",
                table: "PersonQuestionnaire",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonQuestionnaire_UpdateUserID",
                table: "PersonQuestionnaire",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonQuestionnaireSchedule_PersonQuestionnaireID",
                table: "PersonQuestionnaireSchedule",
                column: "PersonQuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonQuestionnaireSchedule_QuestionnaireWindowID",
                table: "PersonQuestionnaireSchedule",
                column: "QuestionnaireWindowID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRaceEthnicity_PersonID",
                table: "PersonRaceEthnicity",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRaceEthnicity_RaceEthnicityID",
                table: "PersonRaceEthnicity",
                column: "RaceEthnicityID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSupport_PersonID",
                table: "PersonSupport",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSupport_SupportTypeID",
                table: "PersonSupport",
                column: "SupportTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSupport_UpdateUserID",
                table: "PersonSupport",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_AgencyID",
                table: "Questionnaire",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_InstrumentID",
                table: "Questionnaire",
                column: "InstrumentID");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_ParentQuestionnaireID",
                table: "Questionnaire",
                column: "ParentQuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_UpdateUserID",
                table: "Questionnaire",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItem_CategoryID",
                table: "QuestionnaireItem",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItem_ItemID",
                table: "QuestionnaireItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItem_LowerItemResponseBehaviorID",
                table: "QuestionnaireItem",
                column: "LowerItemResponseBehaviorID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItem_MedianItemResponseBehaviorID",
                table: "QuestionnaireItem",
                column: "MedianItemResponseBehaviorID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItem_QuestionnaireID",
                table: "QuestionnaireItem",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItem_QuestionnaireItemIndex",
                table: "QuestionnaireItem",
                column: "QuestionnaireItemIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItem_UpdateUserID",
                table: "QuestionnaireItem",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItem_UpperItemResponseBehaviorID",
                table: "QuestionnaireItem",
                column: "UpperItemResponseBehaviorID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireItemHistory_QuestionnaireItemID",
                table: "QuestionnaireItemHistory",
                column: "QuestionnaireItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireNotifyRiskRule_NotificationLevelID",
                table: "QuestionnaireNotifyRiskRule",
                column: "NotificationLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireNotifyRiskRule_QuestionnaireNotifyRiskScheduleID",
                table: "QuestionnaireNotifyRiskRule",
                column: "QuestionnaireNotifyRiskScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireNotifyRiskRule_UpdateUserID",
                table: "QuestionnaireNotifyRiskRule",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireNotifyRiskRuleCondition_QuestionnaireItemID",
                table: "QuestionnaireNotifyRiskRuleCondition",
                column: "QuestionnaireItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireNotifyRiskRuleCondition_QuestionnaireNotifyRiskRuleID",
                table: "QuestionnaireNotifyRiskRuleCondition",
                column: "QuestionnaireNotifyRiskRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireNotifyRiskRuleCondition_UpdateUserID",
                table: "QuestionnaireNotifyRiskRuleCondition",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireNotifyRiskSchedule_QuestionnaireID",
                table: "QuestionnaireNotifyRiskSchedule",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireNotifyRiskSchedule_UpdateUserID",
                table: "QuestionnaireNotifyRiskSchedule",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireReminderRule_QuestionnaireID",
                table: "QuestionnaireReminderRule",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireReminderRule_QuestionnaireReminderTypeID",
                table: "QuestionnaireReminderRule",
                column: "QuestionnaireReminderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireWindow_AssessmentReasonID",
                table: "QuestionnaireWindow",
                column: "AssessmentReasonID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireWindow_QuestionnaireID",
                table: "QuestionnaireWindow",
                column: "QuestionnaireID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportingUnit_ParentAgencyID",
                table: "ReportingUnit",
                column: "ParentAgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportingUnit_ReportingUnitIndex",
                table: "ReportingUnit",
                column: "ReportingUnitIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_ReportingUnit_UpdateUserID",
                table: "ReportingUnit",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Response_ResponseIndex",
                table: "Response",
                column: "ResponseIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Response_UpdateUserID",
                table: "Response",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerHistory_StatusFrom",
                table: "ReviewerHistory",
                column: "StatusFrom");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerHistory_StatusTo",
                table: "ReviewerHistory",
                column: "StatusTo");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerHistory_UpdateUserID",
                table: "ReviewerHistory",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportAddress_AddressID",
                table: "SupportAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportAddress_SupportID",
                table: "SupportAddress",
                column: "SupportID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportContact_ContactID",
                table: "SupportContact",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportContact_SupportID",
                table: "SupportContact",
                column: "SupportID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserIndex",
                table: "User",
                column: "UserIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_SystemRoleID",
                table: "UserRole",
                column: "SystemRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                table: "UserRole",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserRoleIndex",
                table: "UserRole",
                column: "UserRoleIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherForecasts_WeatherForecastIndex",
                table: "WeatherForecasts",
                column: "WeatherForecastIndex")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentReason_UpdateUserID",
                schema: "info",
                table: "AssessmentReason",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentStatus_UpdateUserID",
                schema: "info",
                table: "AssessmentStatus",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Category_UpdateUserID",
                schema: "info",
                table: "Category",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryFocus_UpdateUserID",
                schema: "info",
                table: "CategoryFocus",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationLevel_AgencyID",
                schema: "info",
                table: "CollaborationLevel",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationLevel_UpdateUserID",
                schema: "info",
                table: "CollaborationLevel",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationTagType_AgencyID",
                schema: "info",
                table: "CollaborationTagType",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_CollaborationTagType_UpdateUserID",
                schema: "info",
                table: "CollaborationTagType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ColorPalette_UpdateUserID",
                schema: "info",
                table: "ColorPalette",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_ConfigurationParameterContextID",
                schema: "info",
                table: "Configuration",
                column: "ConfigurationParameterContextID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationAttachment_AttachmentID",
                schema: "info",
                table: "ConfigurationAttachment",
                column: "AttachmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationAttachment_ConfigurationID",
                schema: "info",
                table: "ConfigurationAttachment",
                column: "ConfigurationID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationContext_ParentContextID",
                schema: "info",
                table: "ConfigurationContext",
                column: "ParentContextID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationParameter_ConfigurationValueTypeID",
                schema: "info",
                table: "ConfigurationParameter",
                column: "ConfigurationValueTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationParameterContext_ConfigurationContextID",
                schema: "info",
                table: "ConfigurationParameterContext",
                column: "ConfigurationContextID");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationParameterContext_ConfigurationParameterID",
                schema: "info",
                table: "ConfigurationParameterContext",
                column: "ConfigurationParameterID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactTypeID",
                schema: "info",
                table: "Contact",
                column: "ContactTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactType_UpdateUserID",
                schema: "info",
                table: "ContactType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Country_UpdateUserID",
                schema: "info",
                table: "Country",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CountryState_CountryID",
                schema: "info",
                table: "CountryState",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_CountryState_UpdateUserID",
                schema: "info",
                table: "CountryState",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_AgencyID",
                schema: "info",
                table: "Gender",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_UpdateUserID",
                schema: "info",
                table: "Gender",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_HelperTitle_AgencyID",
                schema: "info",
                table: "HelperTitle",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_HelperTitle_UpdateUserID",
                schema: "info",
                table: "HelperTitle",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationType_AgencyID",
                schema: "info",
                table: "IdentificationType",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationType_UpdateUserID",
                schema: "info",
                table: "IdentificationType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_UpdateUserID",
                schema: "info",
                table: "Instrument",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemResponseBehavior_ItemResponseTypeID",
                schema: "info",
                table: "ItemResponseBehavior",
                column: "ItemResponseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemResponseBehavior_UpdateUserID",
                schema: "info",
                table: "ItemResponseBehavior",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemResponseType_UpdateUserID",
                schema: "info",
                table: "ItemResponseType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Language_UpdateUserID",
                schema: "info",
                table: "Language",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLevel_AgencyID",
                schema: "info",
                table: "NotificationLevel",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLevel_NotificationTypeID",
                schema: "info",
                table: "NotificationLevel",
                column: "NotificationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationLevel_UpdateUserID",
                schema: "info",
                table: "NotificationLevel",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationResolutionStatus_UpdateUserID",
                schema: "info",
                table: "NotificationResolutionStatus",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationType_UpdateUserID",
                schema: "info",
                table: "NotificationType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_OperationType_UpdateUserID",
                schema: "info",
                table: "OperationType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ApplicationObjectID",
                schema: "info",
                table: "Permission",
                column: "ApplicationObjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_OperationTypeID",
                schema: "info",
                table: "Permission",
                column: "OperationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_UpdateUserID",
                schema: "info",
                table: "Permission",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonScreeningStatus_UpdateUserId",
                schema: "info",
                table: "PersonScreeningStatus",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireReminderType_NotificationLevelID",
                schema: "info",
                table: "QuestionnaireReminderType",
                column: "NotificationLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireReminderType_UpdateUserID",
                schema: "info",
                table: "QuestionnaireReminderType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RaceEthnicity_AgencyID",
                schema: "info",
                table: "RaceEthnicity",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_RaceEthnicity_UpdateUserID",
                schema: "info",
                table: "RaceEthnicity",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseValueType_UpdateUserID",
                schema: "info",
                table: "ResponseValueType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionID",
                schema: "info",
                table: "RolePermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_UserRoleID",
                schema: "info",
                table: "RolePermission",
                column: "UserRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Sexuality_AgencyID",
                schema: "info",
                table: "Sexuality",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Sexuality_UpdateUserID",
                schema: "info",
                table: "Sexuality",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportType_AgencyID",
                schema: "info",
                table: "SupportType",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportType_UpdateUserID",
                schema: "info",
                table: "SupportType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRole_UpdateUserID",
                schema: "info",
                table: "SystemRole",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRolePermission_PermissionID",
                schema: "info",
                table: "SystemRolePermission",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_SystemRolePermission_SystemRoleID",
                schema: "info",
                table: "SystemRolePermission",
                column: "SystemRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_TherapyType_AgencyID",
                schema: "info",
                table: "TherapyType",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_TherapyType_UpdateUserID",
                schema: "info",
                table: "TherapyType",
                column: "UpdateUserID");

            migrationBuilder.CreateIndex(
                name: "IX_VoiceType_UpdateUserID",
                schema: "info",
                table: "VoiceType",
                column: "UpdateUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyAddress");

            migrationBuilder.DropTable(
                name: "AgencyContact");

            migrationBuilder.DropTable(
                name: "AgencySharing");

            migrationBuilder.DropTable(
                name: "AgencySharingHistory");

            migrationBuilder.DropTable(
                name: "AgencySharingPolicy");

            migrationBuilder.DropTable(
                name: "AssessmentEmailLinkDetails");

            migrationBuilder.DropTable(
                name: "AssessmentNote");

            migrationBuilder.DropTable(
                name: "AssessmentResponseNote");

            migrationBuilder.DropTable(
                name: "AssessmentResponseText");

            migrationBuilder.DropTable(
                name: "AuditDetails");

            migrationBuilder.DropTable(
                name: "AuditFieldName");

            migrationBuilder.DropTable(
                name: "AuditTableName");

            migrationBuilder.DropTable(
                name: "CollaborationAgencyAddress");

            migrationBuilder.DropTable(
                name: "CollaborationLeadHistory");

            migrationBuilder.DropTable(
                name: "CollaborationQuestionnaire");

            migrationBuilder.DropTable(
                name: "CollaborationSharingHistory");

            migrationBuilder.DropTable(
                name: "CollaborationSharingPolicy");

            migrationBuilder.DropTable(
                name: "CollaborationTag");

            migrationBuilder.DropTable(
                name: "HelperAddress");

            migrationBuilder.DropTable(
                name: "HelperContact");

            migrationBuilder.DropTable(
                name: "IdentityRoleClaim");

            migrationBuilder.DropTable(
                name: "IdentityUserClaim");

            migrationBuilder.DropTable(
                name: "IdentityUserLogin");

            migrationBuilder.DropTable(
                name: "IdentityUserRole");

            migrationBuilder.DropTable(
                name: "IdentityUserToken");

            migrationBuilder.DropTable(
                name: "InstrumentAgency");

            migrationBuilder.DropTable(
                name: "NotificationDelivery");

            migrationBuilder.DropTable(
                name: "NotificationResolutionNote");

            migrationBuilder.DropTable(
                name: "NotifyReminder");

            migrationBuilder.DropTable(
                name: "NotifyRiskRule");

            migrationBuilder.DropTable(
                name: "NotifyRiskValue");

            migrationBuilder.DropTable(
                name: "PersonAddress");

            migrationBuilder.DropTable(
                name: "PersonCollaboration");

            migrationBuilder.DropTable(
                name: "PersonContact");

            migrationBuilder.DropTable(
                name: "PersonHelper");

            migrationBuilder.DropTable(
                name: "PersonIdentification");

            migrationBuilder.DropTable(
                name: "PersonLanguage");

            migrationBuilder.DropTable(
                name: "PersonNote");

            migrationBuilder.DropTable(
                name: "PersonRaceEthnicity");

            migrationBuilder.DropTable(
                name: "QuestionnaireItemHistory");

            migrationBuilder.DropTable(
                name: "SupportAddress");

            migrationBuilder.DropTable(
                name: "SupportContact");

            migrationBuilder.DropTable(
                name: "WeatherForecasts");

            migrationBuilder.DropTable(
                name: "PersonQuestionnaireMetrics",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CategoryFocus",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ColorPalette",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ConfigurationAttachment",
                schema: "info");

            migrationBuilder.DropTable(
                name: "CountryState",
                schema: "info");

            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "info");

            migrationBuilder.DropTable(
                name: "SystemRolePermission",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ReviewerHistory");

            migrationBuilder.DropTable(
                name: "CollaborationSharing");

            migrationBuilder.DropTable(
                name: "SharingPolicy",
                schema: "info");

            migrationBuilder.DropTable(
                name: "CollaborationTagType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "IdentityUsers");

            migrationBuilder.DropTable(
                name: "NotificationTemplate");

            migrationBuilder.DropTable(
                name: "NotificationResolutionHistory");

            migrationBuilder.DropTable(
                name: "QuestionnaireReminderRule");

            migrationBuilder.DropTable(
                name: "AssessmentResponse");

            migrationBuilder.DropTable(
                name: "NotifyRisk");

            migrationBuilder.DropTable(
                name: "QuestionnaireNotifyRiskRuleCondition");

            migrationBuilder.DropTable(
                name: "Helper");

            migrationBuilder.DropTable(
                name: "IdentificationType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "RaceEthnicity",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Attachment",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Configuration",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "info");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ReportingUnit");

            migrationBuilder.DropTable(
                name: "NotificationMode",
                schema: "info");

            migrationBuilder.DropTable(
                name: "NotificationLog");

            migrationBuilder.DropTable(
                name: "QuestionnaireReminderType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "PersonSupport");

            migrationBuilder.DropTable(
                name: "Response");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "QuestionnaireItem");

            migrationBuilder.DropTable(
                name: "QuestionnaireNotifyRiskRule");

            migrationBuilder.DropTable(
                name: "HelperTitle",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ContactType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ConfigurationParameterContext",
                schema: "info");

            migrationBuilder.DropTable(
                name: "SystemRole",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ApplicationObject");

            migrationBuilder.DropTable(
                name: "OperationType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "NotificationResolutionStatus",
                schema: "info");

            migrationBuilder.DropTable(
                name: "SupportType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "AssessmentStatus",
                schema: "info");

            migrationBuilder.DropTable(
                name: "PersonQuestionnaireSchedule");

            migrationBuilder.DropTable(
                name: "VoiceType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "ItemResponseBehavior",
                schema: "info");

            migrationBuilder.DropTable(
                name: "NotificationLevel",
                schema: "info");

            migrationBuilder.DropTable(
                name: "QuestionnaireNotifyRiskSchedule");

            migrationBuilder.DropTable(
                name: "ConfigurationContext",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ConfigurationParameter",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ApplicationObjectType");

            migrationBuilder.DropTable(
                name: "PersonQuestionnaire");

            migrationBuilder.DropTable(
                name: "QuestionnaireWindow");

            migrationBuilder.DropTable(
                name: "ResponseValueType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ItemResponseType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "NotificationType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "ConfigurationValueType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Collaboration");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "AssessmentReason",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Questionnaire");

            migrationBuilder.DropTable(
                name: "CollaborationLevel",
                schema: "info");

            migrationBuilder.DropTable(
                name: "TherapyType",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "info");

            migrationBuilder.DropTable(
                name: "PersonScreeningStatus",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Language",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Sexuality",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Instrument",
                schema: "info");

            migrationBuilder.DropTable(
                name: "Agency");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
