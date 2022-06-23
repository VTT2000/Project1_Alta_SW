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
                    { 11, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAMGPH3N", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 12, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAVR8F0L", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 13, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAQCD1ZG", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 14, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTACBJ3O1", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 15, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTATA1642", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 16, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAVSGL3S", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 17, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAHIW3F2", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 18, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAC7RLS7", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 19, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTADQBMCE", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 20, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAMBGQAI", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 21, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA2SIB0O", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 22, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAO6BM2C", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 23, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA2HM7Q4", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 24, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTATKN8CJ", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 25, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAK0WT0B", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 26, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAW4D1BZ", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 27, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA4L0VAB", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 28, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA7L7KVM", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 29, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTADE3TV7", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 30, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAES1RQD", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 31, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAT6W3MW", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 32, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAM328TD", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 33, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAV2BZAC", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 34, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAOPNTQR", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 35, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAYHEFOI", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 36, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTABAFNQL", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 37, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA86AKPN", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 38, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAF8AA7O", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 39, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAL7PNE3", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 40, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA220HRY", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 41, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAQS3SPQ", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 42, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAIT5NKZ", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 43, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA58G3KW", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 44, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAGL61Z8", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 45, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA6YS7QS", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 46, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAOGTKWN", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 47, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAD4QBJC", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 48, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTA2B2CAP", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 49, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTADJEAZ8", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false },
                    { 50, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAC0YO7N", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, false }
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
                    { 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAW6Q8GC", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 2, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAH6Y5CW", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 3, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAWZ8ZCJ", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 4, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAM5MJJ3", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 5, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAFGHMHG", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 6, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAO2VONT", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 7, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAAPAECS", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 8, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTAW238C3", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 9, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTANJL0LR", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false },
                    { 10, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), true, 1, "ALTALEKF3Z", 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2020, 3, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), null, true, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false }
                });

            migrationBuilder.InsertData(
                table: "CodeGiftCampaigns",
                columns: new[] { "CodeGiftCampaignId", "ActivatedDate", "CampaignGiftId", "Code", "CreatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF1MKAE3P3F71", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF23GNWAFFG6D", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF3SDKW2PTC7R", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF4QFSMDEOKES", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF50AM744PY17", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF61S42B34OT2", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF7CWY3I0ISE4", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF8LE4NKQAWLA", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF9CEY2T1B35K", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF1024BQ7T8C6N", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF11VHFNJAAPLC", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF12WHNSMJCZYQ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF13FQNJL5PHTB", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF14JQD774ZGM5", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 1, "GIF15T0LPD1QRSZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF16A68ZMNGIDQ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF17T6V0EZQDOC", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF181KROKKDGDV", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF19LMCQF33LG5", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF204P04VMDZ05", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF21SJVEFFQE1K", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF22ZBYJ0RI5OY", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF23HFL2SVOB61", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF24ECO26M88SE", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, "GIF25NDD74IJ6MQ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) }
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
