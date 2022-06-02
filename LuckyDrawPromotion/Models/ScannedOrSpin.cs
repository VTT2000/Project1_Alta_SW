namespace LuckyDrawPromotion.Models
{
    public class ScannedOrSpin
    {
        public int ScannedOrSpinId { get; set; }
        public DateTime? ScannedDate { get; set; }
        public DateTime? SpinDate { get; set; }



        public int CodeCampaignId { get; set; }
        public int CustomerId { get; set; }
        public CodeCampaign? CodeCampaign { get; set; }
        public Customer? Customer { get; set; }
    }
}
