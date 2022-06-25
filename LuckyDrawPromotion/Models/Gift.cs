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

    public class GiftDTO_ResponseGiftCode
    {
        public int CodeGiftCampaignId { get; set; }
        public string Code { get; set; } = null!;
        public string? Campaign { get; set; }
        public string? CreatedDate { get; set; }
        public string? ExpiredDate { get; set; }
        public string? Usage { get; set; }
        public bool Active{ get; set; } = false;
        public int CampaignGiftId { get; set; }
    }
}
