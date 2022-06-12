namespace LuckyDrawPromotion.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public bool Block { get; set; } = false;

        

        
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;


        public ICollection<CodeCampaign> CodeCampaigns { get; set; } = new HashSet<CodeCampaign>();
    }
}
