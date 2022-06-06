using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuckyDrawPromotion.Migrations
{
    public partial class InitialazationData : Migration
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
                    GiftCodeCount = table.Column<int>(type: "int", nullable: false),
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
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                    RepeatScheduleId = table.Column<int>(type: "int", nullable: false),
                    CampainGiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.RuleId);
                    table.ForeignKey(
                        name: "FK_Rules_CampaignGifts_CampainGiftId",
                        column: x => x.CampainGiftId,
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
                table: "CampaignGifts",
                columns: new[] { "CampaignGiftId", "CampaignId", "GiftCodeCount", "GiftId" },
                values: new object[,]
                {
                    { 1, 1, 15, 1 },
                    { 2, 1, 10, 2 }
                });

            migrationBuilder.InsertData(
                table: "CodeCampaigns",
                columns: new[] { "CodeCampaignId", "CampaignId", "Code", "CreatedDate" },
                values: new object[,]
                {
                    { 1, 1, "ALTAS07NAC", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "ALTARKM M O4", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 1, "ALTA80P7JZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, "ALTAR48L5F", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, "ALTAB112Q8", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, "ALTA5K60ZM ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, "ALTAOQ4R3E", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 1, "ALTA8POA2S", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, "ALTAFH7TBL", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 1, "ALTAOGZ1TB", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, "ALTAHEHFYI", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 1, "ALTA2836AD", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 1, "ALTAM 1RIGZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 1, "ALTAZK7QSI", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 1, "ALTAE5SS7D", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 1, "ALTA73AF02", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 1, "ALTA1CZ22D", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 1, "ALTAT1PDAT", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 1, "ALTANNB8GN", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 1, "ALTANF87LV", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 1, "ALTAM AC74O", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 1, "ALTAFWG0T4", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 1, "ALTA20ZDJH", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 1, "ALTA7ASDA3", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 1, "ALTAPINGM 2", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 26, 1, "ALTAZ26VVD", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, 1, "ALTA6S42Z8", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 1, "ALTA81488H", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 29, 1, "ALTA8R030Z", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, 1, "ALTAETZVAI", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, 1, "ALTA58KM PB", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 32, 1, "ALTANPT8BZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 33, 1, "ALTAJM 5KSH", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 34, 1, "ALTA4WRPAO", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 35, 1, "ALTAZRNNWD", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 36, 1, "ALTAWRZARS", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 37, 1, "ALTATNALDS", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, 1, "ALTAZSPY5J", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 39, 1, "ALTARYY0VR", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, 1, "ALTAM 454RG", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "CodeCampaigns",
                columns: new[] { "CodeCampaignId", "CampaignId", "Code", "CreatedDate" },
                values: new object[,]
                {
                    { 41, 1, "ALTA16A5LL", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 42, 1, "ALTANJ5SBL", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, 1, "ALTAB0K8CB", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 44, 1, "ALTAATWG4L", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 45, 1, "ALTAII6K88", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 46, 1, "ALTANYBYWR", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 47, 1, "ALTARPKM WI", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 48, 1, "ALTAEZAOSL", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, 1, "ALTACRL26Q", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 50, 1, "ALTA7T1JGE", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) }
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
                columns: new[] { "CodeGiftCampaignId", "Active", "CampaignGiftId", "Code", "CreatedDate" },
                values: new object[,]
                {
                    { 1, true, 1, "GIF1H66ZJRKBGE", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, true, 1, "GIF2CIN1WTHO4W", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, true, 1, "GIF3OG2GEW0WSR", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, true, 1, "GIF476ZDQ0TR6J", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, true, 1, "GIF5P6D48FM 7DR", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, true, 1, "GIF6NCSVG5RQP3", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, true, 1, "GIF7IZALEHL3QB", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, true, 1, "GIF8E604AI513B", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, true, 1, "GIF9Y4JP8QDL7J", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, true, 1, "GIF10SNA65AZNAC", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, true, 1, "GIF11I7SAPTBLTB", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, true, 1, "GIF12PHLZLM L8QK", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, true, 1, "GIF1307HRQLP5DO", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, true, 1, "GIF146T1PNSRQ4E", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, true, 1, "GIF15D0CCLTKLR8", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, true, 2, "GIF16A70M 8JIFQ6", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, true, 2, "GIF17Y2FO507M I5", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, true, 2, "GIF18LCVHCSACK7", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, true, 2, "GIF19TRDP0IKQCD", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, true, 2, "GIF20442ZND8H15", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, true, 2, "GIF21B742VARNGZ", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, true, 2, "GIF22WCDBT0NZA3", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 23, true, 2, "GIF237ZT6GHZ42I", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, true, 2, "GIF24EKRDM PRR1F", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, true, 2, "GIF25THKCZKIW7J", new DateTime(2020, 2, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "RuleId", "AllDay", "CampainGiftId", "EndTime", "GiftAmount", "Probability", "RepeatScheduleId", "RuleName", "ScheduleValue", "StartTime" },
                values: new object[,]
                {
                    { 1, false, 1, new TimeSpan(0, 17, 0, 0, 0), 10, 20, 1, "Monthly", "1, 15", new TimeSpan(0, 15, 0, 0, 0) },
                    { 2, false, 1, null, 10, 10, 2, "Weekly", "Mon, Thu", new TimeSpan(0, 15, 0, 0, 0) }
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
                name: "IX_Campaigns_CharsetId",
                table: "Campaigns",
                column: "CharsetId");

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
                name: "IX_Rules_CampainGiftId",
                table: "Rules",
                column: "CampainGiftId");

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
                name: "Rules");

            migrationBuilder.DropTable(
                name: "ScannedOrSpins");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Winners");

            migrationBuilder.DropTable(
                name: "RepeatSchedules");

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
                name: "TypeOfBussiness");

            migrationBuilder.DropTable(
                name: "Charsets");

            migrationBuilder.DropTable(
                name: "SizePrograms");
        }
    }
}
