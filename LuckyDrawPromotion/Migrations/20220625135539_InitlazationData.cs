using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyDrawPromotion.Migrations
{
    public partial class InitlazationData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charsets",
                columns: table => new
                {
                    CharsetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Value = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charsets", x => x.CharsetId);
                });

            migrationBuilder.CreateTable(
                name: "Gifts",
                columns: table => new
                {
                    GiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.GiftId);
                });

            migrationBuilder.CreateTable(
                name: "RepeatSchedules",
                columns: table => new
                {
                    RepeatScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepeatSchedules", x => x.RepeatScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "SizePrograms",
                columns: table => new
                {
                    SizeProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizePrograms", x => x.SizeProgramId);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfBussiness",
                columns: table => new
                {
                    TypeOfBussinessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfBussiness", x => x.TypeOfBussinessId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    CampaignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    AutoUpdate = table.Column<bool>(type: "bit", nullable: false),
                    CustomerJoinOnlyOne = table.Column<bool>(type: "bit", nullable: false),
                    ApplyAllCampaign = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: false),
                    CodeUsageLimit = table.Column<int>(type: "int", nullable: false),
                    Unlimited = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    SizeProgramId = table.Column<int>(type: "int", nullable: false),
                    CharsetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.CampaignId);
                    table.ForeignKey(
                        name: "FK_Campaigns_Charsets_CharsetId",
                        column: x => x.CharsetId,
                        principalTable: "Charsets",
                        principalColumn: "CharsetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Campaigns_SizePrograms_SizeProgramId",
                        column: x => x.SizeProgramId,
                        principalTable: "SizePrograms",
                        principalColumn: "SizeProgramId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    TypeOfBussinessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                    table.ForeignKey(
                        name: "FK_Positions_TypeOfBussiness_TypeOfBussinessId",
                        column: x => x.TypeOfBussinessId,
                        principalTable: "TypeOfBussiness",
                        principalColumn: "TypeOfBussinessId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CampaignGifts",
                columns: table => new
                {
                    CampaignGiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    GiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignGifts", x => x.CampaignGiftId);
                    table.ForeignKey(
                        name: "FK_CampaignGifts_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CampaignGifts_Gifts_GiftId",
                        column: x => x.GiftId,
                        principalTable: "Gifts",
                        principalColumn: "GiftId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    QRcodeURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandingPage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMStext = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendReportAuto = table.Column<bool>(type: "bit", nullable: false),
                    TimeSend = table.Column<TimeSpan>(type: "time", nullable: true),
                    SendToEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                    table.ForeignKey(
                        name: "FK_Settings_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Block = table.Column<bool>(type: "bit", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CodeGiftCampaigns",
                columns: table => new
                {
                    CodeGiftCampaignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActivatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CampaignGiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeGiftCampaigns", x => x.CodeGiftCampaignId);
                    table.ForeignKey(
                        name: "FK_CodeGiftCampaigns_CampaignGifts_CampaignGiftId",
                        column: x => x.CampaignGiftId,
                        principalTable: "CampaignGifts",
                        principalColumn: "CampaignGiftId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    RuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    GiftAmount = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    AllDay = table.Column<bool>(type: "bit", nullable: false),
                    Probability = table.Column<int>(type: "int", nullable: false),
                    ScheduleValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RepeatScheduleId = table.Column<int>(type: "int", nullable: false),
                    CampaignGiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_Rules_CampaignGifts_CampaignGiftId",
                        column: x => x.CampaignGiftId,
                        principalTable: "CampaignGifts",
                        principalColumn: "CampaignGiftId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rules_RepeatSchedules_RepeatScheduleId",
                        column: x => x.RepeatScheduleId,
                        principalTable: "RepeatSchedules",
                        principalColumn: "RepeatScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CodeCampaigns",
                columns: table => new
                {
                    CodeCampaignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ActivatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ScannedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Scanned = table.Column<bool>(type: "bit", nullable: false),
                    Actived = table.Column<bool>(type: "bit", nullable: false),
                    CodeRedemptionLimit = table.Column<int>(type: "int", nullable: false),
                    Unlimited = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CampaignId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeCampaigns", x => x.CodeCampaignId);
                    table.ForeignKey(
                        name: "FK_CodeCampaigns_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CodeCampaigns_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Spins",
                columns: table => new
                {
                    SpinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpinDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CodeCampaignId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spins", x => x.SpinId);
                    table.ForeignKey(
                        name: "FK_Spins_CodeCampaigns_CodeCampaignId",
                        column: x => x.CodeCampaignId,
                        principalTable: "CodeCampaigns",
                        principalColumn: "CodeCampaignId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Winners",
                columns: table => new
                {
                    WinnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WinDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SentGift = table.Column<bool>(type: "bit", nullable: false),
                    AddressReceivedGift = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CodeGiftCampaignId = table.Column<int>(type: "int", nullable: false),
                    SpinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Winners", x => x.WinnerId);
                    table.ForeignKey(
                        name: "FK_Winners_CodeGiftCampaigns_CodeGiftCampaignId",
                        column: x => x.CodeGiftCampaignId,
                        principalTable: "CodeGiftCampaigns",
                        principalColumn: "CodeGiftCampaignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Winners_Spins_SpinId",
                        column: x => x.SpinId,
                        principalTable: "Spins",
                        principalColumn: "SpinId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Charsets",
                columns: new[] { "CharsetId", "Name", "Value" },
                values: new object[] { 1, "Numbers", "123456789" });

            migrationBuilder.InsertData(
                table: "Gifts",
                columns: new[] { "GiftId", "CreatedDate", "Description", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 5, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Hạt nêm Knnor Chay Nấm Hương 400g", "Hạt nêm Knnor Chay Nấm Hương 400g" },
                    { 2, new DateTime(2022, 5, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 600g", "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 600g" },
                    { 3, new DateTime(2022, 5, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Gia vị Hoàn Chỉnh Knnor Canh Chua 30g", "Gia vị Hoàn Chỉnh Knnor Canh Chua 30g" },
                    { 4, new DateTime(2022, 5, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Xốt nêm Đậm Đặc Knnor Từ Vị Thịt Heo 240g", "Xốt nêm Đậm Đặc Knnor Từ Vị Thịt Heo 240g" },
                    { 5, new DateTime(2022, 5, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 900g", "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 900g" },
                    { 6, new DateTime(2022, 5, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), "Knnor Natural Bột Nêm Tự Nhiên Vị Rau Cũ 150g", "Knnor Natural Bột Nêm Tự Nhiên Vị Rau Cũ 150g" }
                });

            migrationBuilder.InsertData(
                table: "RepeatSchedules",
                columns: new[] { "RepeatScheduleId", "Name" },
                values: new object[,]
                {
                    { 1, "Monthly on day" },
                    { 2, "Weekly on day" },
                    { 3, "Repeat daily" }
                });

            migrationBuilder.InsertData(
                table: "SizePrograms",
                columns: new[] { "SizeProgramId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "up to thounsands of random discount codes designed for single use by a limited group customers (e.g., \"ACME-5P13R\" gives $25 off for the dirst 3 purches, new customers from Warsaw only).", "Bulk codes" },
                    { 2, "A fixed-code discount designed for mutiple uses (e.g., 10% of with \"blackfriday\" code)", "Standalone code" }
                });

            migrationBuilder.InsertData(
                table: "TypeOfBussiness",
                columns: new[] { "TypeOfBussinessId", "Name" },
                values: new object[,]
                {
                    { 1, "Khách sạn" },
                    { 2, "Nhà hàng" },
                    { 3, "Quán ăn" },
                    { 4, "Bán sỉ" },
                    { 5, "Resort" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password" },
                values: new object[,]
                {
                    { 1, "admin0@gmail.com", "Aa12345" },
                    { 2, "admin1@gmail.com", "Aa12345" },
                    { 3, "admin2@gmail.com", "Aa12345" },
                    { 4, "admin3@gmail.com", "Aa12345" },
                    { 5, "admin4@gmail.com", "Aa12345" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "CampaignId", "ApplyAllCampaign", "AutoUpdate", "CharsetId", "CodeUsageLimit", "CustomerJoinOnlyOne", "Description", "EndDate", "EndTime", "Name", "SizeProgramId", "StartDate", "StartTime", "Unlimited" },
                values: new object[] { 1, false, true, 1, 1, false, "Description Lucky Draw 1", new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 0, 0, 0), "Lucky Draw 1", 1, new DateTime(2020, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), false });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "Name", "TypeOfBussinessId" },
                values: new object[,]
                {
                    { 1, "Chủ", 1 },
                    { 2, "Quản lý", 1 },
                    { 3, "Chủ", 2 },
                    { 4, "Quản lý", 2 },
                    { 5, "Chủ", 3 },
                    { 6, "Quản lý", 3 },
                    { 7, "Chủ", 4 },
                    { 8, "Quản lý", 4 },
                    { 9, "Chủ", 5 },
                    { 10, "Quản lý", 5 },
                    { 11, "Bếp", 3 }
                });

            migrationBuilder.InsertData(
                table: "CampaignGifts",
                columns: new[] { "CampaignGiftId", "CampaignId", "GiftId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "CodeCampaigns",
                columns: new[] { "CodeCampaignId", "ActivatedDate", "Actived", "CampaignId", "Code", "CodeRedemptionLimit", "CreatedDate", "CustomerId", "ExpiredDate", "Note", "Scanned", "ScannedDate", "Unlimited" },
                values: new object[,]
                {
                    { 11, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTATFG1DR", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 12, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA0PQP52", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 13, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA7DZ1DN", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 14, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTARWPP5Q", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 15, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA7FGCZR", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 16, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA4BLR20", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 17, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAOA6RT5", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 18, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAQ7KTIW", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 19, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAILEKGO", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 20, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTABTC0ZH", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 21, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAQCKIR5", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 22, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA68K6EG", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 23, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAE4ZVQJ", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 24, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAV2GP5Q", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 25, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAKBS6Z8", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 26, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTABLRW0B", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 27, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAQR48L0", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 28, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAK2SNML", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 29, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAQ6IIIE", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 30, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTARJYJ1Z", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 31, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTASMOR0E", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 32, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA55FO8R", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 33, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAFGKFNE", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 34, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA0S8GEV", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 35, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAF72L1T", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 36, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAQWCG34", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 37, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTARH0128", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 38, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAV764ZE", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 39, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTABMK8ES", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 40, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAHJP38J", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 41, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAKO5OTN", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 42, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAVQL1K3", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 43, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA4P0YGT", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 44, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAY23Y28", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 45, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAZW1W7Z", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 46, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAA4OQWQ", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 47, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAHLISNM", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 48, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAS7JI32", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 49, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAWG16EW", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 50, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAFW3MPB", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Block", "CustomerAddress", "CustomerEmail", "CustomerName", "CustomerPhone", "DateOfBirth", "PositionId" },
                values: new object[,]
                {
                    { 1, false, "Quận 1, TP.HCM", "NguyenVanA@gmail.com", "Nguyễn Văn A", "0987654321", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, false, "Quận 2, TP.HCM", "NguyenVanB@gmail.com", "Nguyễn Văn B", "0987654322", new DateTime(2000, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, false, "Quận 11, TP.HCM", "NguyenVanC@gmail.com", "Nguyễn Văn C", "0987654311", new DateTime(2000, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "SettingId", "CampaignId", "LandingPage", "QRcodeURL", "SMStext", "SendReportAuto", "SendToEmail", "TimeSend" },
                values: new object[] { 1, 1, "https://www.campaign-landing-url.com/", "https://www.the-qrcode-generator.com/", "", true, null, null });

            migrationBuilder.InsertData(
                table: "CodeCampaigns",
                columns: new[] { "CodeCampaignId", "ActivatedDate", "Actived", "CampaignId", "Code", "CodeRedemptionLimit", "CreatedDate", "CustomerId", "ExpiredDate", "Note", "Scanned", "ScannedDate", "Unlimited" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTADQGYAH", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 2, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTACTJ0YA", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 3, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTARMVNIA", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 4, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTACQANRA", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 5, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAWVA6K8", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 6, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAQCZQBH", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 7, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAWS3ECP", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 8, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAYWJHYL", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 9, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA3SM07C", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 10, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAKZKAT0", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.InsertData(
                table: "CodeGiftCampaigns",
                columns: new[] { "CodeGiftCampaignId", "ActivatedDate", "CampaignGiftId", "Code", "CreatedDate", "IsActive" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF10KBPC0W5Y0", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 2, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF2BWCS6DFRWL", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 3, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF3AAHYQRHFQZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 4, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF4RJYMG41SDV", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 5, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF5FLAN1RFR8J", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 6, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF62Z3CM3W5VN", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 7, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF7KVQ8HZRDZR", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 8, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF8R8TEGNEL1P", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 9, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF96LOI7KRWDJ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 10, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF10TTDDD3YKLP", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 11, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF11VACSWB3P6T", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 12, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF125REWI701LV", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 13, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF138HZN22RJYI", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 14, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF14AYEMC063LI", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 15, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF15ABLAOSFFNE", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 16, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF16JWHDCFGECF", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 17, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF17BQQMYQAK5R", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 18, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF18WD7N0JKWFQ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 19, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF19FGRD5V0FO8", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 20, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF20N4S3GN2KHD", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 21, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF213SJ00E2LGM", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 22, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF22PM5SDL6BG1", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 23, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF233MMIVB18OC", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 24, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF24F2636MIPYO", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true },
                    { 25, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF25224HZ466YE", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true }
                });

            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "RuleId", "Active", "AllDay", "CampaignGiftId", "EndTime", "GiftAmount", "Priority", "Probability", "RepeatScheduleId", "RuleName", "ScheduleValue", "StartTime" },
                values: new object[,]
                {
                    { 1, true, false, 1, new TimeSpan(0, 17, 0, 0, 0), 10, 1, 20, 1, "Monthly", "1, 15", new TimeSpan(0, 15, 0, 0, 0) },
                    { 2, true, false, 2, null, 10, 2, 10, 2, "Weekly", "Mon, Thu", new TimeSpan(0, 15, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Spins",
                columns: new[] { "SpinId", "CodeCampaignId", "SpinDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2020, 3, 1, 16, 1, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, new DateTime(2020, 3, 1, 16, 3, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, new DateTime(2020, 3, 1, 16, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, new DateTime(2020, 3, 1, 16, 7, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, new DateTime(2020, 3, 1, 16, 9, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 6, new DateTime(2020, 3, 1, 16, 6, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 7, new DateTime(2020, 3, 1, 16, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 8, new DateTime(2020, 3, 1, 16, 13, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 9, new DateTime(2020, 4, 1, 16, 1, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 10, new DateTime(2020, 4, 1, 16, 3, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Winners",
                columns: new[] { "WinnerId", "AddressReceivedGift", "CodeGiftCampaignId", "SentGift", "SpinId", "WinDate" },
                values: new object[] { 1, "Quận 1, TP.HCM", 1, true, 3, new DateTime(2020, 3, 1, 16, 5, 30, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Winners",
                columns: new[] { "WinnerId", "AddressReceivedGift", "CodeGiftCampaignId", "SentGift", "SpinId", "WinDate" },
                values: new object[] { 2, "Quận 2, TP.HCM", 2, true, 6, new DateTime(2020, 3, 1, 16, 6, 30, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Winners",
                columns: new[] { "WinnerId", "AddressReceivedGift", "CodeGiftCampaignId", "SentGift", "SpinId", "WinDate" },
                values: new object[] { 3, "Quận 2, TP.HCM", 16, true, 7, new DateTime(2020, 3, 1, 16, 10, 30, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignGifts_CampaignId",
                table: "CampaignGifts",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignGifts_GiftId",
                table: "CampaignGifts",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CharsetId",
                table: "Campaigns",
                column: "CharsetId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_Name",
                table: "Campaigns",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_SizeProgramId",
                table: "Campaigns",
                column: "SizeProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Charsets_Name",
                table: "Charsets",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeCampaigns_CampaignId",
                table: "CodeCampaigns",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeCampaigns_CustomerId",
                table: "CodeCampaigns",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeGiftCampaigns_CampaignGiftId",
                table: "CodeGiftCampaigns",
                column: "CampaignGiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerPhone",
                table: "Customers",
                column: "CustomerPhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PositionId",
                table: "Customers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_Name",
                table: "Gifts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_TypeOfBussinessId",
                table: "Positions",
                column: "TypeOfBussinessId");

            migrationBuilder.CreateIndex(
                name: "IX_RepeatSchedules_Name",
                table: "RepeatSchedules",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rules_CampaignGiftId",
                table: "Rules",
                column: "CampaignGiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_RepeatScheduleId",
                table: "Rules",
                column: "RepeatScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_CampaignId",
                table: "Settings",
                column: "CampaignId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SizePrograms_Name",
                table: "SizePrograms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spins_CodeCampaignId",
                table: "Spins",
                column: "CodeCampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfBussiness_Name",
                table: "TypeOfBussiness",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Winners_CodeGiftCampaignId",
                table: "Winners",
                column: "CodeGiftCampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Winners_SpinId",
                table: "Winners",
                column: "SpinId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Winners");

            migrationBuilder.DropTable(
                name: "RepeatSchedules");

            migrationBuilder.DropTable(
                name: "CodeGiftCampaigns");

            migrationBuilder.DropTable(
                name: "Spins");

            migrationBuilder.DropTable(
                name: "CampaignGifts");

            migrationBuilder.DropTable(
                name: "CodeCampaigns");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Charsets");

            migrationBuilder.DropTable(
                name: "SizePrograms");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "TypeOfBussiness");
        }
    }
}
