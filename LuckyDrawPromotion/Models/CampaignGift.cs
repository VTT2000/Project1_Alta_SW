namespace LuckyDrawPromotion.Models
{
    public class CampaignGift
    {
        public int CampaignGiftId { get; set; }


        public int CampaignId { get; set; }
        public int GiftId { get; set; }
        public int RuleId { get; set; }

        public Campaign? Campaign { get; set; }
        public Gift? Gift { get; set; }
        public Rule? Rule { get; set; }

        public ICollection<CodeGiftCampaign> CodeGiftCampaigns { get; set; } = new HashSet<CodeGiftCampaign>();
    }


}
