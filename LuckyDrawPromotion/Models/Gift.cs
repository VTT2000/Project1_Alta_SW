namespace LuckyDrawPromotion.Models
{
    public class Gift
    {
        public int GiftId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; }


        public ICollection<CampaignGift> CampaignGifts { get; set; } = new HashSet<CampaignGift>();
    }
}
