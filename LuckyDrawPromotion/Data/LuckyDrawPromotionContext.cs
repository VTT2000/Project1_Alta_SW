#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LuckyDrawPromotion.Models;

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
        public DbSet<ScannedOrSpin> ScannedOrSpins { get; set; }
        public DbSet<SizeProgram> SizePrograms { get; set; }
        public DbSet<TypeOfBussiness> TypeOfBussiness { get; set; }
        public DbSet<Winner> Winners { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cấu hình API fluent khóa ngoại và ngăn xóa theo tầng
            modelBuilder.Entity<CampaignGift>().HasOne<Gift>(s => s.Gift).WithMany(g => g.CampaignGifts).HasForeignKey(s => s.GiftId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CampaignGift>().HasOne<Campaign>(s => s.Campaign).WithMany(g => g.CampaignGifts).HasForeignKey(s => s.CampaignId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CodeGiftCampaign>().HasOne<CampaignGift>(s => s.CampaignGift).WithMany(g => g.CodeGiftCampaigns).HasForeignKey(s => s.CampaignGiftId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Campaign>().HasOne<Charset>(s=>s.Charset).WithMany(g=>g.Campaigns).HasForeignKey(s => s.CharsetId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Campaign>().HasOne<SizeProgram>(s => s.SizeProgram).WithMany(g => g.Campaigns).HasForeignKey(s => s.SizeProgramId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CampaignGift>().HasOne<Rule>(s=>s.Rule).WithMany(g=>g.CampaignGifts).HasForeignKey(s => s.RuleId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Rule>().HasOne<RepeatSchedule>(s => s.RepeatSchedule).WithMany(g => g.Rules).HasForeignKey(s => s.RepeatScheduleId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Winner>().HasOne<CodeGiftCampaign>(s => s.CodeGiftCampaign).WithMany(g => g.Winners).HasForeignKey(s => s.CodeGiftCampaignId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Winner>().HasOne<Customer>(s=>s.Customer).WithMany(g=>g.Winners).HasForeignKey(s => s.CustomerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CodeCampaign>().HasOne<Campaign>(s => s.Campaign).WithMany(g => g.CodeCampaigns).HasForeignKey(s => s.CampaignId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ScannedOrSpin>().HasOne<CodeCampaign>(s=>s.CodeCampaign).WithMany(g=>g.ScannedOrSpins).HasForeignKey(s => s.CodeCampaignId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ScannedOrSpin>().HasOne<Customer>(s => s.Customer).WithMany(g => g.ScannedOrSpins).HasForeignKey(s => s.CustomerId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Position>().HasOne<TypeOfBussiness>(s => s.TypeOfBussiness).WithMany(g => g.Positions).HasForeignKey(s => s.TypeOfBussinessId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Customer>().HasOne<Position>(s => s.Position).WithMany(g => g.Customers).HasForeignKey(s => s.PositionId).OnDelete(DeleteBehavior.Restrict);
            
            //Cấu hình API fluent các thuộc tính của table
            modelBuilder.Entity<User>().HasIndex(s=>s.Email).IsUnique();
            modelBuilder.Entity<User>().Property(s => s.Email).HasColumnType("varchar(50)");
            modelBuilder.Entity<User>().Property(s => s.Password).HasColumnType("varchar(50)");
            modelBuilder.Entity<Campaign>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Campaign>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Campaign>().Property(s => s.Description).HasColumnType("ntext");
            modelBuilder.Entity<Campaign>().Property(s => s.Prefix).HasColumnType("varchar(10)");
            modelBuilder.Entity<Campaign>().Property(s => s.Postfix).HasColumnType("varchar(10)");
            modelBuilder.Entity<Campaign>().Property(s => s.StartDate).HasColumnType("date");
            modelBuilder.Entity<Campaign>().Property(s => s.EndDate).HasColumnType("date");
            modelBuilder.Entity<Campaign>().Property(s => s.StartTime).HasColumnType("time(0)");
            modelBuilder.Entity<Campaign>().Property(s => s.EndTime).HasColumnType("time(0)");
            modelBuilder.Entity<Charset>().HasIndex(s=>s.Name).IsUnique();
            modelBuilder.Entity<Charset>().Property(s => s.Name).HasColumnType("varchar(50)");
            modelBuilder.Entity<Charset>().Property(s => s.Value).HasColumnType("varchar(256)");
            modelBuilder.Entity<CodeCampaign>().Property(s => s.Code).HasColumnType("varchar(50)");
            modelBuilder.Entity<CodeCampaign>().Property(s => s.CreatedDate).HasColumnType("datetime");
            modelBuilder.Entity<CodeGiftCampaign>().Property(s => s.Code).HasColumnType("varchar(50)");
            modelBuilder.Entity<CodeGiftCampaign>().Property(s => s.CreatedDate).HasColumnType("datetime");
            modelBuilder.Entity<Customer>().Property(s => s.CustomerName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Customer>().HasIndex(s => s.CustomerPhone).IsUnique();
            modelBuilder.Entity<Customer>().Property(s => s.CustomerPhone).HasColumnType("nvarchar(20)");
            modelBuilder.Entity<Customer>().Property(s => s.CustomerAddress).HasColumnType("nvarchar(200)");
            modelBuilder.Entity<Customer>().Property(s => s.DateOfBirth).HasColumnType("date");
            modelBuilder.Entity<Gift>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Gift>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Gift>().Property(s => s.Description).HasColumnType("ntext");
            modelBuilder.Entity<Gift>().Property(s => s.CreatedDate).HasColumnType("datetime");
            modelBuilder.Entity<Position>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<RepeatSchedule>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<RepeatSchedule>().Property(s => s.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Rule>().Property(s => s.RuleName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Rule>().Property(s => s.StartTime).HasColumnType("time(0)");
            modelBuilder.Entity<Rule>().Property(s => s.EndTime).HasColumnType("time(0)");
            modelBuilder.Entity<ScannedOrSpin>().Property(s => s.ScannedDate).HasColumnType("datetime");
            modelBuilder.Entity<ScannedOrSpin>().Property(s => s.SpinDate).HasColumnType("datetime");
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
                List<Position> positions = new List<Position>();
                for(int i = 0; i < 5; i++)
                {
                    Position position = new Position();
                    position.PositionId = (i*2)+1;
                    position.Name = "Chủ";
                    position.TypeOfBussinessId = i+1;
                    positions.Add(position);
                    position = new Position();
                    position.PositionId = (i * 2) + 2;
                    position.Name = "Quản lý";
                    position.TypeOfBussinessId = i + 1;
                    positions.Add(position);
                }
                Position position1 = new Position();
                position1.PositionId = 11;
                position1.Name = "Bếp";
                position1.TypeOfBussinessId = 3;
                positions.Add(position1);
                entity.HasData(positions);
            });
            modelBuilder.Entity<Gift>(entity =>
            {
                entity.HasData(
                    new
                    {
                        GiftId = 1,
                        Name = "Hạt nêm Knnor Chay Nấm Hương 400g",
                        Description = "Hạt nêm Knnor Chay Nấm Hương 400g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00")
                    },
                    new
                    {
                        GiftId = 2,
                        Name = "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 600g",
                        Description = "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 600g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00")
                    },
                    new
                    {
                        GiftId = 3,
                        Name = "Gia vị Hoàn Chỉnh Knnor Canh Chua 30g",
                        Description = "Gia vị Hoàn Chỉnh Knnor Canh Chua 30g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00")
                    },
                    new
                    {
                        GiftId = 4,
                        Name = "Xốt nêm Đậm Đặc Knnor Từ Vị Thịt Heo 240g",
                        Description = "Xốt nêm Đậm Đặc Knnor Từ Vị Thịt Heo 240g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00")
                    },
                    new
                    {
                        GiftId = 5,
                        Name = "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 900g",
                        Description = "Hạt nêm Knnor Từ Thịt Thăn, Xương Ống & Tủy 900g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00")
                    },
                    new
                    {
                        GiftId = 6,
                        Name = "Knnor Natural Bột Nêm Tự Nhiên Vị Rau Cũ 150g",
                        Description = "Knnor Natural Bột Nêm Tự Nhiên Vị Rau Cũ 150g",
                        CreatedDate = DateTime.Parse("2022-05-05 18:00:00")
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
                        CodeCount = 50,
                        CodeLength = 10,
                        Prefix = "ALTA",
                        Postfix = "",
                        StartDate = DateTime.Parse("2020-02-24"),
                        EndDate = DateTime.Parse("2020-03-15"),
                        StartTime = TimeSpan.Parse("13:00:00"),
                        EndTime = TimeSpan.Parse("15:00:00"),
                        SizeProgramId = 1,
                        CharsetId = 1
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
                        GiftId = 1,
                        RuleId = 1
                    },
                    new
                    {
                        CampaignGiftId = 2,
                        CampaignId = 1,
                        GiftId = 2,
                        RuleId = 2
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
                    RepeatScheduleId = 1
                }, new
                {
                    RuleId = 2,
                    RuleName = "Weekly",
                    GiftAmount = 10,
                    StartTime = TimeSpan.Parse("15:00:00"),
                    AllDay = false,
                    Probability = 10,
                    ScheduleValue = "Mon, Thu",
                    RepeatScheduleId = 2
                }
                );
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasData(new
                {
                    CustomerId = 1,
                    CustomerName = "Nguyễn Văn A",
                    CustomerPhone = "0987654321",
                    CustomerAddress = "Quận 1, TP.HCM",
                    DateOfBirth = DateTime.Parse("2000-01-01"),
                    Block = false,
                    PositionId = 1
                },
                new
                {
                    CustomerId = 2,
                    CustomerName = "Nguyễn Văn B",
                    CustomerPhone = "0987654322",
                    CustomerAddress = "Quận 2, TP.HCM",
                    DateOfBirth = DateTime.Parse("2000-02-01"),
                    Block = false,
                    PositionId = 2
                },
                new
                {
                    CustomerId = 3,
                    CustomerName = "Nguyễn Văn C",
                    CustomerPhone = "0987654311",
                    CustomerAddress = "Quận 11, TP.HCM",
                    DateOfBirth = DateTime.Parse("2000-11-01"),
                    Block = false,
                    PositionId = 11
                }
                );
            });
            modelBuilder.Entity<ScannedOrSpin>(entity =>
            {
                entity.HasData(new
                {
                    ScannedOrSpinId = 1,
                    ScannedDate = DateTime.Parse("2020-03-01 16:00:00"),
                    SpinDate = DateTime.Parse("2020-03-01 16:01:00"),
                    CodeCampaignId = 1,
                    CustomerId = 1
                }, new
                {
                    ScannedOrSpinId = 2,
                    ScannedDate = DateTime.Parse("2020-03-01 16:02:00"),
                    SpinDate = DateTime.Parse("2020-03-01 16:03:00"),
                    CodeCampaignId = 1,
                    CustomerId = 1
                }, new
                {
                    ScannedOrSpinId = 3,
                    ScannedDate = DateTime.Parse("2020-03-01 16:04:00"),
                    SpinDate = DateTime.Parse("2020-03-01 16:05:00"),
                    CodeCampaignId = 1,
                    CustomerId = 1
                }, new
                {
                    ScannedOrSpinId = 4,
                    ScannedDate = DateTime.Parse("2020-03-01 16:05:00"),
                    SpinDate = DateTime.Parse("2020-03-01 16:06:00"),
                    CodeCampaignId = 1,
                    CustomerId = 2
                }, new
                {
                    ScannedOrSpinId = 5,
                    ScannedDate = DateTime.Parse("2020-03-01 16:08:00"),
                    SpinDate = DateTime.Parse("2020-03-01 16:10:00"),
                    CodeCampaignId = 1,
                    CustomerId = 2
                }, new
                {
                    ScannedOrSpinId = 6,
                    ScannedDate = DateTime.Parse("2020-03-01 16:01:00"),
                    SpinDate = DateTime.Parse("2020-03-01 16:03:00"),
                    CodeCampaignId = 1,
                    CustomerId = 3
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
                    CustomerId = 1
                }, new
                {
                    WinnerId = 2,
                    WinDate = DateTime.Parse("2020-03-01 16:06:30"),
                    SentGift = true,
                    AddressReceivedGift = "Quận 2, TP.HCM",
                    CodeGiftCampaignId = 2,
                    CustomerId = 2
                }, new
                {
                    WinnerId = 3,
                    WinDate = DateTime.Parse("2020-03-01 16:10:30"),
                    SentGift = true,
                    AddressReceivedGift = "Quận 2, TP.HCM",
                    CodeGiftCampaignId = 16,
                    CustomerId = 2
                }
                );
            });
        }

    }
}
