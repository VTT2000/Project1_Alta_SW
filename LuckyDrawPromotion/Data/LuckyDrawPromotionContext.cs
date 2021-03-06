#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LuckyDrawPromotion.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckyDrawPromotion.Data
{
    public class LuckyDrawPromotionContext : DbContext
    {
        public LuckyDrawPromotionContext (DbContextOptions<LuckyDrawPromotionContext> options)
            : base(options)
        {

        }

        public DbSet<LuckyDrawPromotion.Models.User> Users { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignGift> CampaignGifts { get; set; }
        public DbSet<Charset> Charsets { get; set; }
        public DbSet<CodeCampaign> CodeCampaigns { get; set; }
        public DbSet<CodeGiftCampaign> CodeGiftCampaigns { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<RepeatSchedule> RepeatSchedules { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Spin> Spins { get; set; }
        public DbSet<SizeProgram> SizePrograms { get; set; }
        public DbSet<TypeOfBussiness> TypeOfBussiness { get; set; }
        public DbSet<Winner> Winners { get; set; }

        public DbSet<Settings> Settings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cấu hình API fluent khóa ngoại và ngăn xóa theo tầng
            modelBuilder.Entity<CampaignGift>().HasOne<Gift>(s => s.Gift).WithMany(g => g.CampaignGifts).HasForeignKey(s => s.GiftId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CampaignGift>().HasOne<Campaign>(s => s.Campaign).WithMany(g => g.CampaignGifts).HasForeignKey(s => s.CampaignId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CodeGiftCampaign>().HasOne<CampaignGift>(s => s.CampaignGift).WithMany(g => g.CodeGiftCampaigns).HasForeignKey(s => s.CampaignGiftId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Campaign>().HasOne<Charset>(s=>s.Charset).WithMany(g=>g.Campaigns).HasForeignKey(s => s.CharsetId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Campaign>().HasOne<SizeProgram>(s => s.SizeProgram).WithMany(g => g.Campaigns).HasForeignKey(s => s.SizeProgramId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Rule>().HasOne<CampaignGift>(s => s.CampaignGift).WithMany(g => g.Rules).HasForeignKey(s => s.CampaignGiftId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Rule>().HasOne<RepeatSchedule>(s => s.RepeatSchedule).WithMany(g => g.Rules).HasForeignKey(s => s.RepeatScheduleId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Winner>().HasOne<CodeGiftCampaign>(s => s.CodeGiftCampaign).WithMany(g => g.Winners).HasForeignKey(s => s.CodeGiftCampaignId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Winner>().HasOne<Spin>(s=>s.Spin).WithMany(g=>g.Winners).HasForeignKey(s => s.SpinId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CodeCampaign>().HasOne<Campaign>(s => s.Campaign).WithMany(g => g.CodeCampaigns).HasForeignKey(s => s.CampaignId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CodeCampaign>().HasOne<Customer>(s => s.Customer).WithMany(g => g.CodeCampaigns).HasForeignKey(s => s.CustomerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Spin>().HasOne<CodeCampaign>(s => s.CodeCampaign).WithMany(g => g.Spins).HasForeignKey(s => s.CodeCampaignId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Customer>().HasOne<Position>(s => s.Position).WithMany(g => g.Customers).HasForeignKey(s => s.PositionId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Customer>().HasOne<TypeOfBussiness>(s => s.TypeOfBussiness).WithMany(g => g.Customers).HasForeignKey(s => s.TypeOfBussinessId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Campaign>().HasOne<Settings>(s => s.Setting).WithOne(g => g.Campaign).HasForeignKey<Settings>(s => s.CampaignId).OnDelete(DeleteBehavior.Restrict);
            //Cấu hình API fluent các thuộc tính của table
            
            modelBuilder.Entity<User>().HasIndex(s=>s.Email).IsUnique();
            modelBuilder.Entity<User>().Property(s => s.Email).HasColumnType("varchar(50)");
            modelBuilder.Entity<User>().Property(s => s.Password).HasColumnType("varchar(50)");
            modelBuilder.Entity<Campaign>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Campaign>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Campaign>().Property(s => s.Description).HasColumnType("ntext");
            modelBuilder.Entity<Campaign>().Property(s => s.StartDate).HasColumnType("date");
            modelBuilder.Entity<Campaign>().Property(s => s.EndDate).HasColumnType("date");
            modelBuilder.Entity<Campaign>().Property(s => s.StartTime).HasColumnType("time(0)");
            modelBuilder.Entity<Campaign>().Property(s => s.EndTime).HasColumnType("time(0)");
            modelBuilder.Entity<Charset>().HasIndex(s=>s.Name).IsUnique();
            modelBuilder.Entity<Charset>().Property(s => s.Name).HasColumnType("varchar(50)");
            modelBuilder.Entity<Charset>().Property(s => s.Value).HasColumnType("varchar(256)");
            modelBuilder.Entity<CodeCampaign>().Property(s => s.Code).HasColumnType("varchar(50)");

            modelBuilder.Entity<CodeCampaign>().Property(s => s.CreatedDate).HasColumnType("datetime");
            modelBuilder.Entity<CodeCampaign>().Property(s => s.ActivatedDate).HasColumnType("datetime");
            modelBuilder.Entity<CodeCampaign>().Property(s => s.ExpiredDate).HasColumnType("datetime");
            modelBuilder.Entity<CodeCampaign>().Property(s => s.ScannedDate).HasColumnType("datetime");
            modelBuilder.Entity<CodeGiftCampaign>().Property(s => s.Code).HasColumnType("varchar(50)");
            modelBuilder.Entity<CodeGiftCampaign>().Property(s => s.CreatedDate).HasColumnType("datetime");
            
            modelBuilder.Entity<Customer>().Property(s => s.CustomerName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Customer>().HasIndex(s => s.CustomerPhone).IsUnique();
            modelBuilder.Entity<Customer>().Property(s => s.CustomerPhone).HasColumnType("nvarchar(20)");
            modelBuilder.Entity<Customer>().Property(s => s.CustomerAddress).HasColumnType("nvarchar(200)");
            modelBuilder.Entity<Customer>().Property(s => s.DateOfBirth).HasColumnType("date");
            modelBuilder.Entity<Gift>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Gift>().Property(s => s.Name).HasColumnType("nvarchar(300)");
            modelBuilder.Entity<Gift>().Property(s => s.Description).HasColumnType("ntext");
            modelBuilder.Entity<Gift>().Property(s => s.CreatedDate).HasColumnType("datetime");
            modelBuilder.Entity<Position>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<RepeatSchedule>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<RepeatSchedule>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Rule>().Property(s => s.RuleName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Rule>().Property(s => s.StartTime).HasColumnType("time(0)");
            modelBuilder.Entity<Rule>().Property(s => s.EndTime).HasColumnType("time(0)");

            modelBuilder.Entity<Spin>().Property(s => s.SpinDate).HasColumnType("datetime");
            modelBuilder.Entity<SizeProgram>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<SizeProgram>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<SizeProgram>().Property(s => s.Description).HasColumnType("ntext");
            modelBuilder.Entity<TypeOfBussiness>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<TypeOfBussiness>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Winner>().Property(s => s.WinDate).HasColumnType("datetime");
            modelBuilder.Entity<Winner>().Property(s => s.AddressReceivedGift).HasColumnType("nvarchar(200)");
            

            //modelBuilder.Entity<User>().HasCheckConstraint

            //Cấu hình API fluent Đổ dữ liệu vào database khi migration tạo database
            modelBuilder.Entity<User>(entity =>
            {
                List<User> users = new List<User>();
                for (int i = 0; i < 5; i++)
                {
                    User user = new User();
                    user.UserId = i+1;
                    user.Email = "admin" + i + "@gmail.com";
                    user.Password = "Aa12345";
                    users.Add(user);
                }
                entity.HasData(users);
            });
            modelBuilder.Entity<Charset>(entity =>
            {
                entity.HasData(
                    new
                    {
                        CharsetId = 1,
                        Name = "Numbers",
                        Value = "123456789"
                    }
                    );
            });
            modelBuilder.Entity<SizeProgram>(entity =>
            {
                entity.HasData(
                    new
                    {
                        SizeProgramId = 1,
                        Name = "Bulk codes",
                        Description = "up to thounsands of random discount codes designed for single use by a limited group customers (e.g., \"ACME-5P13R\" gives $25 off for the dirst 3 purches, new customers from Warsaw only)."
                    },
                    new
                    {
                        SizeProgramId = 2,
                        Name = "Standalone code",
                        Description = "A fixed-code discount designed for mutiple uses (e.g., 10% of with \"blackfriday\" code)"
                    }
                    );
            });
            modelBuilder.Entity<TypeOfBussiness>(entity =>
            {
                entity.HasData(
                    new
                    {
                        TypeOfBussinessId = 1,
                        Name = "Khách sạn",
                    },
                    new
                    {
                        TypeOfBussinessId = 2,
                        Name = "Nhà hàng",
                    },
                    new
                    {
                        TypeOfBussinessId = 3,
                        Name = "Quán ăn",
                    },
                    new
                    {
                        TypeOfBussinessId = 4,
                        Name = "Bán sỉ",
                    },
                    new
                    {
                        TypeOfBussinessId = 5,
                        Name = "Resort",
                    }
                    );
            });
            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasData(
                    new
                    {
                        PositionId = 1,
                        Name = "Chủ",
                    },
                    new
                    {
                        PositionId = 2,
                        Name = "Quản lý",
                    },
                    new
                    {
                        PositionId = 3,
                        Name = "Bếp",
                    }
                    );
            });
            modelBuilder.Entity<Gift>(entity =>
            {
                entity.HasData(
                    new
                    {
                        GiftId = 1,
                        Name = "Hạt nêm Knnor Chay Nấm Hương 400g",
                        Description = "Hạt nêm Knnor Chay Nấm Hương 400g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00"),
                        Active = true
                    },
                    new
                    {
                        GiftId = 2,
                        Name = "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 600g",
                        Description = "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 600g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00"),
                        Active = true
                    },
                    new
                    {
                        GiftId = 3,
                        Name = "Gia vị Hoàn Chỉnh Knnor Canh Chua 30g",
                        Description = "Gia vị Hoàn Chỉnh Knnor Canh Chua 30g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00"),
                        Active = true
                    },
                    new
                    {
                        GiftId = 4,
                        Name = "Xốt nêm Đậm Đặc Knnor Từ Vị Thịt Heo 240g",
                        Description = "Xốt nêm Đậm Đặc Knnor Từ Vị Thịt Heo 240g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00"),
                        Active = true
                    },
                    new
                    {
                        GiftId = 5,
                        Name = "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 900g",
                        Description = "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 900g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00"),
                        Active = true
                    },
                    new
                    {
                        GiftId = 6,
                        Name = "Knnor Natural Bột Nêm Tự Nhiên Vị Rau Cũ 150g",
                        Description = "Knnor Natural Bột Nêm Tự Nhiên Vị Rau Cũ 150g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00"),
                        Active = true
                    }
                    );
            });
            modelBuilder.Entity<RepeatSchedule>(entity =>
            {
                entity.HasData(
                    new { RepeatScheduleId = 1, Name = "Monthly on day" },
                    new { RepeatScheduleId = 2, Name = "Weekly on day" },
                    new { RepeatScheduleId = 3, Name = "Repeat daily" }
                    );
            });
            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.HasData(
                    new {
                        CampaignId = 1,
                        Name = "Lucky Draw 1",
                        AutoUpdate = true,
                        CustomerJoinOnlyOne = false,
                        ApplyAllCampaign = false,
                        Description = "Description Lucky Draw 1",
                        CodeUsageLimit = 1,
                        Unlimited = false,
                        StartDate = DateTime.Parse("2020-02-24"),
                        EndDate = DateTime.Parse("2020-03-15"),
                        StartTime = TimeSpan.Parse("13:00:00"),
                        EndTime = TimeSpan.Parse("15:00:00"),
                        SizeProgramId = 1,
                        CharsetId = 1
                    }
                    );
            });
            modelBuilder.Entity<Settings>(entity =>
            {
                entity.HasData(new
                Settings
                {
                    SettingId = 1,
                    CampaignId = 1,
                    QRcodeURL = "https://www.the-qrcode-generator.com/",
                    LandingPage = "https://www.campaign-landing-url.com/",
                    SMStext = "",
                    SendReportAuto = true,
                    TimeSend = null,
                    SendToEmail = null
                });
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasData(new
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn A",
                    CustomerPhone = "0987654321",
                    CustomerEmail =  "NguyenVanA@gmail.com",
                    CustomerAddress = "Quận 1, TP.HCM",
                    DateOfBirth = DateTime.Parse("2000-01-01"),
                    Block = false,
                    PositionId = 1,
                    TypeOfBussinessId = 1
                },
                new
                {
                    CustomerId = 2,
                    CustomerName = "Nguyễn Văn B",
                    CustomerEmail = "NguyenVanB@gmail.com",
                    CustomerPhone = "0987654322",
                    CustomerAddress = "Quận 2, TP.HCM",
                    DateOfBirth = DateTime.Parse("2000-02-01"),
                    Block = false,
                    PositionId = 2,
                    TypeOfBussinessId = 2
                },
                new
                {
                    CustomerId = 3,
                    CustomerName = "Nguyễn Văn C",
                    CustomerEmail = "NguyenVanC@gmail.com",
                    CustomerPhone = "0987654311",
                    CustomerAddress = "Quận 11, TP.HCM",
                    DateOfBirth = DateTime.Parse("2000-11-01"),
                    Block = false,
                    PositionId = 3,
                    TypeOfBussinessId = 3
                }
                );
            });
            modelBuilder.Entity<CodeCampaign>(entity =>
            {
                string[] MangKyTu = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "V", "W", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                Random fr = new Random();
                List<CodeCampaign> codes = new List<CodeCampaign>();
                for(int i = 0; i < 50; i++)
                {
                    CodeCampaign code = new CodeCampaign();
                    code.CodeCampaignId = i+1;
                    code.CampaignId = 1;
                    code.CreatedDate = DateTime.Parse("2020-02-24 13:00:00");
                    code.ActivatedDate = DateTime.Parse("2020-02-24 13:00:00");
                    code.ExpiredDate = DateTime.Parse("2020-03-15 15:00:00");
                    code.Actived = true;
                    if (i < 5)
                    {
                        code.CustomerId = 1;
                        code.Scanned = true;
                        code.ScannedDate = DateTime.Parse("2020-03-01 16:00:00");
                    }
                    else if(i < 8)
                    {
                        code.CustomerId = 2;
                        code.Scanned = true;
                        code.ScannedDate = DateTime.Parse("2020-03-01 16:00:00");
                    }
                    else if(i < 10)
                    {
                        code.CustomerId = 3;
                        code.Scanned = true;
                        code.ScannedDate = DateTime.Parse("2020-03-01 16:00:00");
                    }
                    code.CodeRedemptionLimit = 1;
                    code.Unlimited = false;
                    do
                    {
                        string chuoi = "";
                        for (int j = 0; j < 6; j++)
                        {
                            int t = fr.Next(0, (MangKyTu.Length-1));
                            chuoi = chuoi + MangKyTu[t];
                        }
                        code.Code = "ALTA" + chuoi;
                    }
                    while (codes.Exists(p => p.Code == code.Code));
                    codes.Add(code);
                }
                entity.HasData(codes);
            });
            modelBuilder.Entity<CampaignGift>(entity =>
            {
                entity.HasData(
                    new
                    {
                        CampaignGiftId = 1,
                        CampaignId = 1,
                        GiftId = 1
                    },
                    new
                    {
                        CampaignGiftId = 2,
                        CampaignId = 1,
                        GiftId = 2
                    }
                    );
            });
            modelBuilder.Entity<CodeGiftCampaign>(entity => {
                string[] MangKyTu = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "V", "W", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                Random fr = new Random();
                List<CodeGiftCampaign> codes = new List<CodeGiftCampaign>();
                for (int i = 0; i < 25; i++)
                {
                    CodeGiftCampaign code = new CodeGiftCampaign();
                    code.CodeGiftCampaignId = i + 1;
                    if(i + 1 > 15)
                    {
                        code.CampaignGiftId = 2;
                    }
                    else
                    {
                        code.CampaignGiftId = 1;
                    }
                    code.CreatedDate = DateTime.Parse("2020-02-24 13:00:00");
                    code.ActivatedDate = DateTime.Parse("2020-02-24 13:00:00");
                    do
                    {
                        string chuoi = "";
                        for (int j = 0; j < 10; j++)
                        {
                            int t = fr.Next(0, (MangKyTu.Length-1));
                            chuoi = chuoi + MangKyTu[t];
                        }
                        code.Code = "GIF" + code.CodeGiftCampaignId + chuoi;
                    }
                    while (codes.Exists(p => p.Code == code.Code));
                    codes.Add(code);
                }
                entity.HasData(codes);
            });
            modelBuilder.Entity<Rule>(entity =>
            {
                entity.HasData(new
                {
                    RuleId = 1,
                    RuleName = "Monthly",
                    GiftAmount = 10,
                    StartTime = TimeSpan.Parse("15:00:00"),
                    EndTime = TimeSpan.Parse("17:00:00"),
                    AllDay = false,
                    Probability = 20,
                    ScheduleValue = "1, 15",
                    RepeatScheduleId = 1,
                    CampaignGiftId = 1,
                    Active = true,
                    Priority = 1
                }, new
                {
                    RuleId = 2,
                    RuleName = "Weekly",
                    GiftAmount = 10,
                    StartTime = TimeSpan.Parse("15:00:00"),
                    AllDay = false,
                    Probability = 10,
                    ScheduleValue = "Mon, Thu",
                    RepeatScheduleId = 2,
                    CampaignGiftId = 2,
                    Active = true,
                    Priority = 2
                }
                );
            });
            
            modelBuilder.Entity<Spin>(entity =>
            {
                entity.HasData(new
                {
                    SpinId = 1,
                    SpinDate = DateTime.Parse("2020-03-01 16:01:00"),
                    CodeCampaignId = 1
                }, new
                {
                    SpinId = 2,
                    SpinDate = DateTime.Parse("2020-03-01 16:03:00"),
                    CodeCampaignId = 2
                }, new
                {
                    SpinId = 3,
                    SpinDate = DateTime.Parse("2020-03-01 16:05:00"),
                    CodeCampaignId = 3
                }, new
                {
                    SpinId = 4,
                    SpinDate = DateTime.Parse("2020-03-01 16:07:00"),
                    CodeCampaignId = 4
                }, new
                {
                    SpinId = 5,
                    SpinDate = DateTime.Parse("2020-03-01 16:09:00"),
                    CodeCampaignId = 5
                }, new
                {
                    SpinId = 6,
                    SpinDate = DateTime.Parse("2020-03-01 16:06:00"),
                    CodeCampaignId = 6
                }, new
                {
                    SpinId = 7,
                    SpinDate = DateTime.Parse("2020-03-01 16:10:00"),
                    CodeCampaignId = 7
                }, new
                {
                    SpinId = 8,
                    SpinDate = DateTime.Parse("2020-03-01 16:13:00"),
                    CodeCampaignId = 8
                }, new
                {
                    SpinId = 9,
                    SpinDate = DateTime.Parse("2020-04-01 16:01:00"),
                    CodeCampaignId = 9
                }, new
                {
                    SpinId = 10,
                    SpinDate = DateTime.Parse("2020-04-01 16:03:00"),
                    CodeCampaignId = 10
                }
                );
            });
            modelBuilder.Entity<Winner>(entity =>
            {
                entity.HasData(new
                {
                    WinnerId = 1,
                    WinDate = DateTime.Parse("2020-03-01 16:05:30"),
                    SentGift = true,
                    AddressReceivedGift = "Quận 1, TP.HCM",
                    CodeGiftCampaignId = 1,
                    SpinId = 3
                }, new
                {
                    WinnerId = 2,
                    WinDate = DateTime.Parse("2020-03-01 16:06:30"),
                    SentGift = true,
                    AddressReceivedGift = "Quận 2, TP.HCM",
                    CodeGiftCampaignId = 2,
                    SpinId = 6
                }, new
                {
                    WinnerId = 3,
                    WinDate = DateTime.Parse("2020-03-01 16:10:30"),
                    SentGift = true,
                    AddressReceivedGift = "Quận 2, TP.HCM",
                    CodeGiftCampaignId = 16,
                    SpinId = 7
                }
                );
            });
        }

    }
}
