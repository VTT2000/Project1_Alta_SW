namespace LuckyDrawPromotion.Models
{
    public class CodeGiftCampaign
    {
        public int CodeGiftCampaignId { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? ActivatedDate { get; set; }

        public int CampaignGiftId { get; set; }
        public CampaignGift CampaignGift { get; set; } = null!;


        public ICollection<Winner> Winners { get; set; } = new HashSet<Winner>();

    }

    public class CodeGiftCampaignDTO
    {
        public string Code { get; set; } = null!;
        public string CreatedDate { get; set; } = null!;
        public bool Active { get; set; } = false;

    }
    public class CodeGiftCampaignDTO_ResponseFilter
    {
        public int CodeGiftCampaignId { get; set; }
        public string Code { get; set; } = null!;
        public string? CreatedDate { get; set; }


        public string GiftName { get; set; } = null!;
        public int CodeUsageLimit { get; set; }
        public bool Used { get; set; } = false;
        public bool Active { get; set; } = false;
    }
}
