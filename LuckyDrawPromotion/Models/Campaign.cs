namespace LuckyDrawPromotion.Models
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string Name { get; set; } = null!;
        public bool AutoUpdate { get; set; } = false;
        public bool CustomerJoinOnlyOne { get; set; } = false;
        public bool ApplyAllCampaign { get; set; } = false;
        public string Description { get; set; } = null!;
        public int CodeUsageLimit { get; set; } = 1;
        public bool Unlimited { get; set; } = false;
        public int CodeCount { get; set; } = 1;
        public int CodeLength { get; set; } = 10;
        public string Prefix { get; set; } = "ALTA";
        public string? Postfix { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }



        public int SizeProgramId { get; set; }
        public int CharsetId { get; set; }
        public SizeProgram SizeProgram { get; set; } = null!;
        public Charset Charset { get; set; } = null!;



        public ICollection<CampaignGift> CampaignGifts { get; set; } = new HashSet<CampaignGift>();
        
        
        public ICollection<CodeCampaign> CodeCampaigns { get; set; } = new HashSet<CodeCampaign>();
    }
}
