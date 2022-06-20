namespace LuckyDrawPromotion.Models
{
    public class Spin
    {
        public int SpinId { get; set; }
        public DateTime SpinDate { get; set; }

        public int CodeCampaignId { get; set; } = 0!;
        public CodeCampaign CodeCampaign { get; set; } = null!;
        public ICollection<Winner> Winners { get; set; } = new HashSet<Winner>();
    }
}
