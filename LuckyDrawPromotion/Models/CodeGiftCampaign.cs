namespace LuckyDrawPromotion.Models
{
    public class CodeGiftCampaign
    {
        public int CodeGiftCampaignId { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; } = false;



        public int CampaignGiftId { get; set; }
        public CampaignGift CampaignGift { get; set; } = null!;


        public ICollection<Winner> Winners { get; set; } = new HashSet<Winner>();

    }
}
