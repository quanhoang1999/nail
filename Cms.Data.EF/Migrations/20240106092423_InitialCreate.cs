using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cms.Data.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BrowserInfo = table.Column<string>(nullable: true),
                    ClientIpAddress = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    CustomData = table.Column<string>(nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    ExecutionDuration = table.Column<int>(nullable: false),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    ImpersonatorTenantId = table.Column<Guid>(nullable: true),
                    ImpersonatorUserId = table.Column<Guid>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    Parameters = table.Column<string>(nullable: true),
                    ServiceName = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Code = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Website = table.Column<string>(maxLength: 250, nullable: true),
                    Address = table.Column<string>(maxLength: 250, nullable: true),
                    Other = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: true),
                    Lng = table.Column<double>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(nullable: true),
                    CommonName = table.Column<string>(nullable: true),
                    FormalName = table.Column<string>(nullable: true),
                    CountryType = table.Column<string>(nullable: true),
                    CountrySubType = table.Column<string>(nullable: true),
                    Sovereignty = table.Column<string>(nullable: true),
                    Capital = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    CurrencyName = table.Column<string>(nullable: true),
                    TelephoneCode = table.Column<string>(nullable: true),
                    CountryCode3 = table.Column<string>(nullable: true),
                    CountryNumber = table.Column<string>(nullable: true),
                    InternetCountryCode = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    Flags = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerTypeId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(maxLength: 256, nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    CustomerFax = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerWebsite = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerBirthDay = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerEmail = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerPhone = table.Column<string>(maxLength: 50, nullable: true),
                    ProvinceID = table.Column<int>(nullable: false),
                    DistrictID = table.Column<int>(nullable: false),
                    WardID = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    FullAddress = table.Column<string>(maxLength: 250, nullable: true),
                    Taxcode = table.Column<string>(maxLength: 50, nullable: true),
                    BrandID = table.Column<int>(nullable: false),
                    Facebook = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserDeleted = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    LatiLongTude = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Message = table.Column<string>(maxLength: 500, nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    URL = table.Column<string>(maxLength: 250, nullable: false),
                    ParentId = table.Column<string>(maxLength: 128, nullable: true),
                    IconCss = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MimeType = table.Column<string>(nullable: true),
                    SeoFilename = table.Column<string>(nullable: true),
                    AltAttribute = table.Column<string>(nullable: true),
                    TitleAttribute = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    VirtualPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Resources = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NailEGifts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    RecipientName = table.Column<string>(nullable: true),
                    RecipientPhoneNumber = table.Column<string>(nullable: true),
                    RecipientEmail = table.Column<string>(nullable: true),
                    RecipientNote = table.Column<string>(nullable: true),
                    EGiftID = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    SalePrice = table.Column<decimal>(nullable: true),
                    TaxAmount = table.Column<decimal>(nullable: true),
                    TaxPercent = table.Column<decimal>(nullable: true),
                    DisountPrice = table.Column<decimal>(nullable: true),
                    DisountPercent = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    ExpriredDate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCompevbs = table.Column<bool>(nullable: false),
                    IsPayPal = table.Column<bool>(nullable: false),
                    PayPayError = table.Column<string>(nullable: true),
                    PayPalID = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailEGifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NailStores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailStores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NailTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OAInfomations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AppId = table.Column<string>(nullable: true),
                    AccessToken = table.Column<string>(nullable: true),
                    OATypeId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OAInfomations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Alias = table.Column<string>(maxLength: 256, nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    SeoFilename = table.Column<string>(nullable: true),
                    AltAttribute = table.Column<string>(nullable: true),
                    TitleAttribute = table.Column<string>(nullable: true),
                    IsNew = table.Column<bool>(nullable: false),
                    VirtualPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Image = table.Column<string>(maxLength: 256, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    HomeFlag = table.Column<bool>(nullable: true),
                    HotFlag = table.Column<bool>(nullable: true),
                    ViewCount = table.Column<int>(nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    SeoPageTitle = table.Column<string>(maxLength: 256, nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 256, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 256, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 256, nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Categories = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    HomeOrder = table.Column<int>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    HomeFlag = table.Column<bool>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SeoPageTitle = table.Column<string>(nullable: true),
                    SeoAlias = table.Column<string>(nullable: true),
                    SeoKeywords = table.Column<string>(nullable: true),
                    SeoDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: true),
                    HomeOrder = table.Column<int>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    HomeFlag = table.Column<bool>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    SeoPageTitle = table.Column<string>(nullable: true),
                    SeoAlias = table.Column<string>(nullable: true),
                    SeoKeywords = table.Column<string>(nullable: true),
                    SeoDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Type = table.Column<string>(maxLength: 20, nullable: true),
                    TelephoneCode = table.Column<int>(nullable: false),
                    ZipCode = table.Column<string>(maxLength: 20, nullable: true),
                    CountryId = table.Column<int>(nullable: false),
                    CountryCode = table.Column<string>(maxLength: 2, nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Vote = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Social = table.Column<int>(nullable: false),
                    Avartar = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeteted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TextValue = table.Column<string>(nullable: true),
                    IntegerValue = table.Column<int>(nullable: true),
                    BooleanValue = table.Column<bool>(nullable: true),
                    DateValue = table.Column<DateTime>(nullable: true),
                    DecimalValue = table.Column<decimal>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UniqueCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Image = table.Column<string>(maxLength: 250, nullable: false),
                    Url = table.Column<string>(maxLength: 250, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    GroupAlias = table.Column<int>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TagCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UIElement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Caption = table.Column<string>(nullable: true),
                    TargetURL = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: true),
                    IconPath = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UIElement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ward",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    LatiLongTude = table.Column<string>(nullable: true),
                    DistrictID = table.Column<int>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    IsPublished = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ward", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AppId = table.Column<string>(nullable: true),
                    AccessToken = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    BusinessTypeId = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Business_BusinessType_BusinessTypeId",
                        column: x => x.BusinessTypeId,
                        principalTable: "BusinessType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    ShortName = table.Column<string>(type: "varchar(3)", nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeteted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NailStoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_NailStores_NailStoreId",
                        column: x => x.NailStoreId,
                        principalTable: "NailStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FileUrl = table.Column<string>(nullable: true),
                    FileType = table.Column<int>(nullable: false),
                    NailStoreId = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Galleries_NailStores_NailStoreId",
                        column: x => x.NailStoreId,
                        principalTable: "NailStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NailCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    OrderIndex = table.Column<int>(nullable: true),
                    IsShowMenu = table.Column<bool>(nullable: false),
                    IsShowHomePage = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Images = table.Column<string>(nullable: true),
                    NailStoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailCategories_NailStores_NailStoreId",
                        column: x => x.NailStoreId,
                        principalTable: "NailStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NailCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NailStoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailCustomers_NailStores_NailStoreId",
                        column: x => x.NailStoreId,
                        principalTable: "NailStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NailOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    DatePick = table.Column<DateTime>(nullable: false),
                    TimePick = table.Column<DateTime>(nullable: false),
                    IsFinish = table.Column<bool>(nullable: false),
                    IsSendSMS = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true),
                    NailStoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailOrders_NailStores_NailStoreId",
                        column: x => x.NailStoreId,
                        principalTable: "NailStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NailPromotions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    Image = table.Column<string>(nullable: true),
                    SalePrice = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    ExpriredDate = table.Column<DateTime>(nullable: true),
                    IsShowHomePage = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true),
                    NailStoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailPromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailPromotions_NailStores_NailStoreId",
                        column: x => x.NailStoreId,
                        principalTable: "NailStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NailTypeId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    SalePrice = table.Column<decimal>(nullable: false),
                    Desription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    ExpriredDate = table.Column<DateTime>(nullable: true),
                    TaxAmount = table.Column<decimal>(nullable: true),
                    TaxPercent = table.Column<decimal>(nullable: true),
                    IsUsingTaxPercent = table.Column<bool>(nullable: false),
                    DisountPrice = table.Column<decimal>(nullable: true),
                    IsUsingDisountPercent = table.Column<bool>(nullable: false),
                    DisountPercent = table.Column<decimal>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeteted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true),
                    Time = table.Column<int>(nullable: true),
                    NotePrice = table.Column<string>(nullable: true),
                    OrderIndex = table.Column<int>(nullable: true),
                    IsShowMenu = table.Column<bool>(nullable: false),
                    IsShowHomePage = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nails_NailTypes_NailTypeId",
                        column: x => x.NailTypeId,
                        principalTable: "NailTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
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
                    FullName = table.Column<string>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    Gender = table.Column<bool>(nullable: false),
                    UserType = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    SelectedOaId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_OAInfomations_SelectedOaId",
                        column: x => x.SelectedOaId,
                        principalTable: "OAInfomations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostCates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<Guid>(nullable: false),
                    PostCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCates_PostCategories_PostCategoryId",
                        column: x => x.PostCategoryId,
                        principalTable: "PostCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCates_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Image = table.Column<string>(maxLength: 255, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    PromotionPrice = table.Column<decimal>(nullable: true),
                    OriginalPrice = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    HomeFlag = table.Column<bool>(nullable: true),
                    HotFlag = table.Column<bool>(nullable: true),
                    ViewCount = table.Column<int>(nullable: true),
                    Tags = table.Column<string>(maxLength: 255, nullable: true),
                    Unit = table.Column<string>(maxLength: 255, nullable: true),
                    SeoPageTitle = table.Column<string>(nullable: true),
                    SeoAlias = table.Column<string>(maxLength: 255, nullable: true),
                    SeoKeywords = table.Column<string>(maxLength: 255, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 255, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    TagId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTag_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BussinessId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppGroups_Business_BussinessId",
                        column: x => x.BussinessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BussinessId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoles_Business_BussinessId",
                        column: x => x.BussinessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NailCategoryPictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NailCategoryId = table.Column<int>(nullable: false),
                    PictureId = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailCategoryPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailCategoryPictures_NailCategories_NailCategoryId",
                        column: x => x.NailCategoryId,
                        principalTable: "NailCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NailCategoryPictures_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NailServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Time = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    ExpriredDate = table.Column<DateTime>(nullable: true),
                    SalePrice = table.Column<decimal>(nullable: false),
                    NotePrice = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    OrderIndex = table.Column<int>(nullable: true),
                    IsShowMenu = table.Column<bool>(nullable: false),
                    IsShowHomePage = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: true),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Images = table.Column<string>(nullable: true),
                    NailCategoryId = table.Column<int>(nullable: true),
                    NailStoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailServices_NailCategories_NailCategoryId",
                        column: x => x.NailCategoryId,
                        principalTable: "NailCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NailServices_NailStores_NailStoreId",
                        column: x => x.NailStoreId,
                        principalTable: "NailStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCoupons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateUsed = table.Column<DateTime>(nullable: true),
                    NailCustomerId = table.Column<int>(nullable: true),
                    NailStoreId = table.Column<int>(nullable: true),
                    DateExpired = table.Column<DateTime>(nullable: false),
                    DateStarted = table.Column<DateTime>(nullable: false),
                    DiscountType = table.Column<int>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    MinDiscount = table.Column<decimal>(nullable: false),
                    MaxDiscount = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCoupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCoupons_NailCustomers_NailCustomerId",
                        column: x => x.NailCustomerId,
                        principalTable: "NailCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerCoupons_NailStores_NailStoreId",
                        column: x => x.NailStoreId,
                        principalTable: "NailStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(maxLength: 256, nullable: false),
                    CustomerAddress = table.Column<string>(maxLength: 256, nullable: false),
                    CustomerMobile = table.Column<string>(maxLength: 50, nullable: false),
                    CustomerMessage = table.Column<string>(maxLength: 256, nullable: false),
                    PaymentMethod = table.Column<int>(nullable: false),
                    BillStatus = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_AppUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserBussiness",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    BussinessId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBussiness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBussiness_Business_BussinessId",
                        column: x => x.BussinessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBussiness_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ImageId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(maxLength: 250, nullable: true),
                    Caption = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    SizeId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    TagId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholePrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    FromQuantity = table.Column<int>(nullable: false),
                    ToQuantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholePrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholePrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoleGroup_AppGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AppGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppRoleGroup_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    FunctionId = table.Column<string>(nullable: false),
                    CanCreate = table.Column<bool>(nullable: false),
                    CanRead = table.Column<bool>(nullable: false),
                    CanUpdate = table.Column<bool>(nullable: false),
                    CanDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Functions_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "Functions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUIElement",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    ElementId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUIElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUIElement_UIElement_ElementId",
                        column: x => x.ElementId,
                        principalTable: "UIElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUIElement_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NailEmployeeServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NailServiceId = table.Column<int>(nullable: true),
                    NailEmployeeId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailEmployeeServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailEmployeeServices_Employees_NailEmployeeId",
                        column: x => x.NailEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NailEmployeeServices_NailServices_NailServiceId",
                        column: x => x.NailServiceId,
                        principalTable: "NailServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NailOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NailServiceId = table.Column<int>(nullable: false),
                    NailOrderId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    NailServiceName = table.Column<string>(nullable: true),
                    NailEmployeeId = table.Column<Guid>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    DateDeleted = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserCreated = table.Column<Guid>(nullable: false),
                    UserModified = table.Column<Guid>(nullable: true),
                    UserDeleted = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailOrderDetails_Employees_NailEmployeeId",
                        column: x => x.NailEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NailOrderDetails_NailOrders_NailOrderId",
                        column: x => x.NailOrderId,
                        principalTable: "NailOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NailOrderDetails_NailServices_NailServiceId",
                        column: x => x.NailServiceId,
                        principalTable: "NailServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NailServicePictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NailServiceId = table.Column<int>(nullable: false),
                    PictureId = table.Column<int>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NailServicePictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NailServicePictures_NailServices_NailServiceId",
                        column: x => x.NailServiceId,
                        principalTable: "NailServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NailServicePictures_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    SizeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppGroups_BussinessId",
                table: "AppGroups",
                column: "BussinessId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleGroup_GroupId",
                table: "AppRoleGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleGroup_RoleId",
                table: "AppRoleGroup",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppRoles_BussinessId",
                table: "AppRoles",
                column: "BussinessId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_SelectedOaId",
                table: "AppUsers",
                column: "SelectedOaId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillId",
                table: "BillDetails",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ColorId",
                table: "BillDetails",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ProductId",
                table: "BillDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_SizeId",
                table: "BillDetails",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Business_BusinessTypeId",
                table: "Business",
                column: "BusinessTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCoupons_NailCustomerId",
                table: "CustomerCoupons",
                column: "NailCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCoupons_NailStoreId",
                table: "CustomerCoupons",
                column: "NailStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NailStoreId",
                table: "Employees",
                column: "NailStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_NailStoreId",
                table: "Galleries",
                column: "NailStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_NailCategories_NailStoreId",
                table: "NailCategories",
                column: "NailStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_NailCategoryPictures_NailCategoryId",
                table: "NailCategoryPictures",
                column: "NailCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NailCategoryPictures_PictureId",
                table: "NailCategoryPictures",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_NailCustomers_NailStoreId",
                table: "NailCustomers",
                column: "NailStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_NailEmployeeServices_NailEmployeeId",
                table: "NailEmployeeServices",
                column: "NailEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_NailEmployeeServices_NailServiceId",
                table: "NailEmployeeServices",
                column: "NailServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_NailOrderDetails_NailEmployeeId",
                table: "NailOrderDetails",
                column: "NailEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_NailOrderDetails_NailOrderId",
                table: "NailOrderDetails",
                column: "NailOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_NailOrderDetails_NailServiceId",
                table: "NailOrderDetails",
                column: "NailServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_NailOrders_NailStoreId",
                table: "NailOrders",
                column: "NailStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_NailPromotions_NailStoreId",
                table: "NailPromotions",
                column: "NailStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Nails_NailTypeId",
                table: "Nails",
                column: "NailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NailServicePictures_NailServiceId",
                table: "NailServicePictures",
                column: "NailServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_NailServicePictures_PictureId",
                table: "NailServicePictures",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_NailServices_NailCategoryId",
                table: "NailServices",
                column: "NailCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NailServices_NailStoreId",
                table: "NailServices",
                column: "NailStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_FunctionId",
                table: "Permissions",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCates_PostCategoryId",
                table: "PostCates",
                column: "PostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCates_PostId",
                table: "PostCates",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_PostId",
                table: "PostTag",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ImageId",
                table: "ProductImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_ColorId",
                table: "ProductQuantities",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_ProductId",
                table: "ProductQuantities",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_SizeId",
                table: "ProductQuantities",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductId",
                table: "ProductTags",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUIElement_ElementId",
                table: "RoleUIElement",
                column: "ElementId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUIElement_RoleId",
                table: "RoleUIElement",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBussiness_BussinessId",
                table: "UserBussiness",
                column: "BussinessId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBussiness_UserId",
                table: "UserBussiness",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WholePrices_ProductId",
                table: "WholePrices",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoleGroup");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserGroup");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "CustomerCoupons");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "NailCategoryPictures");

            migrationBuilder.DropTable(
                name: "NailEGifts");

            migrationBuilder.DropTable(
                name: "NailEmployeeServices");

            migrationBuilder.DropTable(
                name: "NailOrderDetails");

            migrationBuilder.DropTable(
                name: "NailPromotions");

            migrationBuilder.DropTable(
                name: "Nails");

            migrationBuilder.DropTable(
                name: "NailServicePictures");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PostCates");

            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductQuantities");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "RoleUIElement");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "Slides");

            migrationBuilder.DropTable(
                name: "UserBussiness");

            migrationBuilder.DropTable(
                name: "Ward");

            migrationBuilder.DropTable(
                name: "WholePrices");

            migrationBuilder.DropTable(
                name: "AppGroups");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "NailCustomers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "NailOrders");

            migrationBuilder.DropTable(
                name: "NailTypes");

            migrationBuilder.DropTable(
                name: "NailServices");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropTable(
                name: "PostCategories");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "UIElement");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "NailCategories");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "OAInfomations");

            migrationBuilder.DropTable(
                name: "NailStores");

            migrationBuilder.DropTable(
                name: "BusinessType");
        }
    }
}
