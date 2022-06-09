namespace LuckyDrawPromotion.Models
{
    public class CodeGiftCampaign
    {
        public int CodeGiftCampaignId { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedDate { get; set; }



        public int CampaignGiftId { get; set; }
        public CampaignGift CampaignGift { get; set; } = null!;


        public ICollection<Winner> Winners { get; set; } = new HashSet<Winner>();

    }

    public class CodeGiftCampaignDTO_Response
    {
        public string Code { get; set; } = null!;
        public string CreatedDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public bool Active { get; set; } = false;
    }
}
