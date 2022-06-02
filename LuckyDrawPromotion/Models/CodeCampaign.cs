namespace LuckyDrawPromotion.Models
{
    public class CodeCampaign
    {
        public int CodeCampaignId { get; set; }

        
        public string Code { get; set; } = null!;
        public DateTime CreatedDate { get; set; }



        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; } = null!;



        public ICollection<ScannedOrSpin> ScannedOrSpins { get; set; } = new HashSet<ScannedOrSpin>();

    }
}
