
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckyDrawPromotion.Models
{
    public class CodeCampaign
    {
        public int CodeCampaignId { get; set; }
        public string Code { get; set; } = null!;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ActivatedDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public DateTime? ScannedDate { get; set; }

        public bool Scanned { get; set; } = false;
        public bool Actived { get; set; } = false;

        public int CodeRedemptionLimit { get; set; } = 1;
        public bool Unlimited { get; set; } = false;

        public string? Note { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; } = null!;

        public ICollection<Spin> Spins { get; set; } = new HashSet<Spin>();

    }

    public class CodeBarDTO_ResponseFilter
    {
        public int CodeCampaignId { get; set; }
        public string Code { get; set; } = null!;
        public string BarCode { get; set; } = null!;
        public string QRCode { get; set; } = null!;
        public string CreatedDate { get; set; } = null!;
        public string ExpiredDate { get; set; } = null!;
        public string? ScannedDate { get; set; }
        public bool Scanned { get; set; } = false;
        public bool Actived { get; set; } = false;
    }

    public class CodeBarDTO_ResponseDetail
    {
        public int CodeCampaignId { get; set; }
        public string Code { get; set; } = null!;
        public string NameCampaign { get; set; } = null!;
        public int CodeRedemptionLimit { get; set; } = 1;
        public bool Unlimited { get; set; } = false;
        public string CreatedDate { get; set; } = null!;
        public string ExpiredDate { get; set; } = null!;
        public string? Owner { get; set; }
        public string? Note { get; set; }
    }

    public class CodeCampaignDTO_RequestGenerate
    {
        [Required]
        public int CampaignId { get; set; }
        [Required]
        public int CodeRedemLimit { get; set; }
        [Required]
        public bool Unlimited { get; set; }
        [Required]
        public double CodeCount { get; set; }
        [Required]
        public int CharsetId { get; set; }
        [Required]
        public int CodeLength { get; set; }
        public string? Prefix { get; set; }
        public string? Postfix { get; set; }
    }
    public class CodeBarDTO_ResponseHistoryFilter
    {
        public int CodeCampaignId { get; set; }
        public string Code { get; set; } = null!;
        public string CreatedDate { get; set; } = null!;
        public string ExpiredDate { get; set; } = null!;
        public string? ScannedDate { get; set; }
        public string? SpinDate { get; set; }
        public string? Owner { get; set; } = null!;
        public bool Scanned { get; set; } = false;
        public bool UsedForSpin { get; set; } = false;
        
    }
}
