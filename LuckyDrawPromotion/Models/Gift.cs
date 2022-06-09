namespace LuckyDrawPromotion.Models
{
    public class Gift
    {
        public int GiftId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;


        public ICollection<CampaignGift> CampaignGifts { get; set; } = new HashSet<CampaignGift>();
    }
    public class GiftDTO_Response
    {
        public int GiftId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
