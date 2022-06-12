namespace LuckyDrawPromotion.Models
{
    public class Winner
    {
        public int WinnerId { get; set; }
        public DateTime WinDate { get; set; }
        public bool SentGift { get; set; } = false;
        public string AddressReceivedGift { get; set; } = null!;



        public int CodeGiftCampaignId { get; set; }
        public int SpinId { get; set; }
        public Spin Spin { get; set; } = null!;
        public CodeGiftCampaign CodeGiftCampaign { get; set; } = null!;
    }
}
