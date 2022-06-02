namespace LuckyDrawPromotion.Models
{
    public class CampaignGift
    {
        public int CampaignGiftId { get; set; }
        public int GiftCodeCount { get; set; }



        public int CampaignId { get; set; }
        public int GiftId { get; set; }
        public Campaign Campaign { get; set; } = null!;
        public Gift Gift { get; set; } = null!;


        public ICollection<CodeGiftCampaign> CodeGiftCampaigns { get; set; } = new HashSet<CodeGiftCampaign>();
        public ICollection<Rule> Rules { get; set; } = new HashSet<Rule>();
    }
}
