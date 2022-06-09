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
                    RepeatScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_Rules_RepeatSchedules_RepeatScheduleId",
                        column: x => x.RepeatScheduleId,
                        principalTable: "RepeatSchedules",
                        principalColumn: "RepeatScheduleId",
                        onDelete: ReferentialAction.Restrict);
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
                    CodeCount = table.Column<int>(type: "int", nullable: false),
                    CodeLength = table.Column<int>(type: "int", nullable: false),
                    Prefix = table.Column<string>(type: "varchar(10)", nullable: false),
                    Postfix = table.Column<string>(type: "varchar(10)", nullable: true),
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
                    GiftId = table.Column<int>(type: "int", nullable: false),
                    RuleId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_CampaignGifts_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "RuleId",
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
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(20)", nullable: false),
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
                name: "ScannedOrSpins",
                columns: table => new
                {
                    ScannedOrSpinId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScannedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SpinDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CodeCampaignId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScannedOrSpins", x => x.ScannedOrSpinId);
                    table.ForeignKey(
                        name: "FK_ScannedOrSpins_CodeCampaigns_CodeCampaignId",
                        column: x => x.CodeCampaignId,
                        principalTable: "CodeCampaigns",
                        principalColumn: "CodeCampaignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScannedOrSpins_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
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
                    CustomerId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_Winners_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
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
                columns: new[] { "CampaignId", "ApplyAllCampaign", "AutoUpdate", "CharsetId", "CodeCount", "CodeLength", "CodeUsageLimit", "CustomerJoinOnlyOne", "Description", "EndDate", "EndTime", "Name", "Postfix", "Prefix", "SizeProgramId", "StartDate", "StartTime", "Unlimited" },
                values: new object[] { 1, false, true, 1, 50, 10, 1, false, "Description Lucky Draw 1", new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 15, 0, 0, 0), "Lucky Draw 1", "", "ALTA", 1, new DateTime(2020, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 13, 0, 0, 0), false });

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
                table: "Rules",
                columns: new[] { "RuleId", "AllDay", "EndTime", "GiftAmount", "Probability", "RepeatScheduleId", "RuleName", "ScheduleValue", "StartTime" },
                values: new object[,]
                {
                    { 1, false, new TimeSpan(0, 17, 0, 0, 0), 10, 20, 1, "Monthly", "1, 15", new TimeSpan(0, 15, 0, 0, 0) },
                    { 2, false, null, 10, 10, 2, "Weekly", "Mon, Thu", new TimeSpan(0, 15, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "CampaignGifts",
                columns: new[] { "CampaignGiftId", "CampaignId", "GiftId", "RuleId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "CodeCampaigns",
                columns: new[] { "CodeCampaignId", "CampaignId", "Code", "CreatedDate" },
                values: new object[,]
                {
                    { 1, 1, "ALTAMNA5H5", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "ALTAOAOF4K", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "ALTADI2LRB", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, "ALTA5I66GY", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, "ALTAFKNPYR", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, "ALTAVNEOTA", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, "ALTAYJMQGK", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, "ALTAC3JOZJ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, "ALTAL1IQWJ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 1, "ALTA67D0CV", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, "ALTAP42S38", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 1, "ALTAJVDE6B", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 1, "ALTA8HFTFW", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 1, "ALTAFSOGKJ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 1, "ALTA2ZDHRZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 1, "ALTAF71766", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 1, "ALTA6M00GS", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 1, "ALTAQ84G8H", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 1, "ALTALK31DJ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 1, "ALTANRJWYO", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 1, "ALTAP2NBAR", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 1, "ALTASJRE2A", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 1, "ALTA0QNBCQ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 1, "ALTA0YR1IL", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 1, "ALTAMFD8VN", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, 1, "ALTAQP7EGZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, 1, "ALTA4ASWOJ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 1, "ALTA2IQQSS", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, 1, "ALTALMHPM3", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, 1, "ALTARTDPI7", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, 1, "ALTAA0J56L", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, 1, "ALTAKLH0NP", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, 1, "ALTAZHVWGY", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, 1, "ALTA4MB1WF", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, 1, "ALTAFKQSVS", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, 1, "ALTAB0ZHIH", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, 1, "ALTAM3YVJQ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, 1, "ALTALQK6QM", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, 1, "ALTASJQ6B1", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, 1, "ALTA0Q37JL", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "CodeCampaigns",
                columns: new[] { "CodeCampaignId", "CampaignId", "Code", "CreatedDate" },
                values: new object[,]
                {
                    { 41, 1, "ALTAA3N6FD", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, 1, "ALTA1Y2CAH", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, 1, "ALTAW7YTQA", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, 1, "ALTAHAZCW0", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, 1, "ALTAF261PP", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, 1, "ALTA4MS1LZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, 1, "ALTARCP77V", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, 1, "ALTA54RCIM", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, 1, "ALTAB6DRW2", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, 1, "ALTA6LH7OT", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Block", "CustomerAddress", "CustomerName", "CustomerPhone", "DateOfBirth", "PositionId" },
                values: new object[,]
                {
                    { 1, false, "Quận 1, TP.HCM", "Nguyễn Văn A", "0987654321", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, false, "Quận 2, TP.HCM", "Nguyễn Văn B", "0987654322", new DateTime(2000, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, false, "Quận 11, TP.HCM", "Nguyễn Văn C", "0987654311", new DateTime(2000, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 }
                });

            migrationBuilder.InsertData(
                table: "CodeGiftCampaigns",
                columns: new[] { "CodeGiftCampaignId", "CampaignGiftId", "Code", "CreatedDate" },
                values: new object[,]
                {
                    { 1, 1, "GIF1ZWTD3I0PY1", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "GIF2VMKNP455FH", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "GIF3EZJN64VOVI", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, "GIF4VLIYO1JNK1", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, "GIF55HACOEN4QW", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, "GIF6RPZSPGN7RT", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, "GIF7PA5FEP2Q0S", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, "GIF85EWAZL6O06", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, "GIF9QQBWMF1Z42", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 1, "GIF10ASYABN25Q8", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, "GIF1168QRWLH43E", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 1, "GIF12YV3ST5B5TB", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 1, "GIF13L80ZQ6SW2Y", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 1, "GIF14YQKMM23VZ3", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 1, "GIF15HME42GZF53", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 2, "GIF16MA814N8TBD", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 2, "GIF17CRCDKOJYTV", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 2, "GIF18KV7Y3Q64Q7", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 2, "GIF19BGPSJ336TI", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 2, "GIF20MV2WB5G2OF", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 2, "GIF217203H6TPYH", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 2, "GIF22ZT4FDZMC8L", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 2, "GIF2372LO3M0818", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 2, "GIF24MRFFMAJLV6", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 2, "GIF25AYOPRWMAAZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ScannedOrSpins",
                columns: new[] { "ScannedOrSpinId", "CodeCampaignId", "CustomerId", "ScannedDate", "SpinDate" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2020, 3, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 1, 16, 1, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 1, new DateTime(2020, 3, 1, 16, 2, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 1, 16, 3, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, 1, new DateTime(2020, 3, 1, 16, 4, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 1, 16, 5, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 2, new DateTime(2020, 3, 1, 16, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 1, 16, 6, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 2, new DateTime(2020, 3, 1, 16, 8, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 1, 16, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, 3, new DateTime(2020, 3, 1, 16, 1, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 3, 1, 16, 3, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Winners",
                columns: new[] { "WinnerId", "AddressReceivedGift", "CodeGiftCampaignId", "CustomerId", "SentGift", "WinDate" },
                values: new object[] { 1, "Quận 1, TP.HCM", 1, 1, true, new DateTime(2020, 3, 1, 16, 5, 30, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Winners",
                columns: new[] { "WinnerId", "AddressReceivedGift", "CodeGiftCampaignId", "CustomerId", "SentGift", "WinDate" },
                values: new object[] { 2, "Quận 2, TP.HCM", 2, 2, true, new DateTime(2020, 3, 1, 16, 6, 30, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Winners",
                columns: new[] { "WinnerId", "AddressReceivedGift", "CodeGiftCampaignId", "CustomerId", "SentGift", "WinDate" },
                values: new object[] { 3, "Quận 2, TP.HCM", 16, 2, true, new DateTime(2020, 3, 1, 16, 10, 30, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignGifts_CampaignId",
                table: "CampaignGifts",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignGifts_GiftId",
                table: "CampaignGifts",
                column: "GiftId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignGifts_RuleId",
                table: "CampaignGifts",
                column: "RuleId");

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
                name: "IX_Rules_RepeatScheduleId",
                table: "Rules",
                column: "RepeatScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScannedOrSpins_CodeCampaignId",
                table: "ScannedOrSpins",
                column: "CodeCampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_ScannedOrSpins_CustomerId",
                table: "ScannedOrSpins",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SizePrograms_Name",
                table: "SizePrograms",
                column: "Name",
                unique: true);

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
                name: "IX_Winners_CustomerId",
                table: "Winners",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScannedOrSpins");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Winners");

            migrationBuilder.DropTable(
                name: "CodeCampaigns");

            migrationBuilder.DropTable(
                name: "CodeGiftCampaigns");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CampaignGifts");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Gifts");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "TypeOfBussiness");

            migrationBuilder.DropTable(
                name: "Charsets");

            migrationBuilder.DropTable(
                name: "SizePrograms");

            migrationBuilder.DropTable(
                name: "RepeatSchedules");
        }
    }
}
