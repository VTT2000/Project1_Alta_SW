namespace LuckyDrawPromotion.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string CustomerEmail { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public bool Block { get; set; } = false;

        

        
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;

        public int TypeOfBussinessId { get; set; }
        public TypeOfBussiness TypeOfBussiness { get; set; } = null!;
        public ICollection<CodeCampaign> CodeCampaigns { get; set; } = new HashSet<CodeCampaign>();
    }
    public class CustomerDTO_ResponseFilter
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public bool Block { get; set; } = false;
        public string PositionName { get; set; } = null!;
        public string TypeOfBussinessName { get; set; } = null!;
    }
}
