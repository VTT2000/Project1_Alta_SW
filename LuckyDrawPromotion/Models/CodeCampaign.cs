
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
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ExpiredDate { get; set; }
        public DateTime? ScannedDate { get; set; }
        public bool Scanned { get; set; } = false;
        public bool Actived { get; set; } = false;
    }
    
}
