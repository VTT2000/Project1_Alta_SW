namespace LuckyDrawPromotion.Models
{
    public class CampaignGift
    {
        public int CampaignGiftId { get; set; }


        public int CampaignId { get; set; }
        public int GiftId { get; set; }

        public Campaign? Campaign { get; set; }
        public Gift? Gift { get; set; }

        public ICollection<CodeGiftCampaign> CodeGiftCampaigns { get; set; } = new HashSet<CodeGiftCampaign>();
        public ICollection<Rule> Rules { get; set; } = new HashSet<Rule>();
    }

    public class CampaignGiftDTO_RequestRuleCampaign
    {
        public int GiftId { get; set; }
        public ICollection<RuleDTO> ListRules { get; set; } = new HashSet<RuleDTO>();
    }

    public class CampaignGiftDTO_ResponseEditForRule
    {
        public int CampaignGiftId { get; set; }
        public int CampaignId { get; set; }
        public int GiftId { get; set; }
        
    }
}
