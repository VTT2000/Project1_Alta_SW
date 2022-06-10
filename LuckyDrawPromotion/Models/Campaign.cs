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
    public class CampaignDTO_Request
    {
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
        public string Postfix { get; set; } = "";


        public string StartDate { get; set; } = null!;
        public string? EndDate { get; set; }
        public string StartTime { get; set; } = null!;
        public string? EndTime { get; set; }



        public int SizeProgramId { get; set; }
        public int CharsetId { get; set; }



        public ICollection<CampaignGiftDTO_Request> ListCampaignGifts { get; set; } = new HashSet<CampaignGiftDTO_Request>();


    }

    public class CampaignGiftDTO_Request
    {
        public int GiftId { get; set; }

        public ICollection<CodeGiftCampaignDTO> ListCodeGiftCampaigns { get; set; } = new HashSet<CodeGiftCampaignDTO>();
        public RuleDTO? ARule { get; set; }
    }
    
    

}
