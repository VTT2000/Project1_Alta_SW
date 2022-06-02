namespace LuckyDrawPromotion.Models
{
    public class Winner
    {
        public int WinnerId { get; set; }
        public DateTime WinDate { get; set; }
        public bool SentGift { get; set; } = false;
        public string AddressReceivedGift { get; set; } = null!;



        public int CodeGiftCampaignId { get; set; }
        public int CustomerId { get; set; }
        public CodeGiftCampaign CodeGiftCampaign { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}
