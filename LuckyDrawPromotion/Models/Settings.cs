using System.ComponentModel.DataAnnotations;

namespace LuckyDrawPromotion.Models
{
    public class Settings
    {
        [Key]
        public int SettingId { get; set; }
        public int CampaignId { get; set; }
        public string QRcodeURL { get; set; } = "https://www.the-qrcode-generator.com/";
        public string LandingPage { get; set; } = "https://www.campaign-landing-url.com/";
        public string SMStext { get; set; } = "";
        public bool SendReportAuto { get; set; } = true;
        public TimeSpan? TimeSend { get; set; }
        public string? SendToEmail { get; set; }

        public Campaign Campaign { get; set; } = null!;
    }
}
