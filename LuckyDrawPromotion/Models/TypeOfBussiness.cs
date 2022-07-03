namespace LuckyDrawPromotion.Models
{
    public class TypeOfBussiness
    {
        public int TypeOfBussinessId { get; set; }
        public string Name { get; set; } = null!;


        public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    }
    public class TypeOfBussinessDTO
    {
        public int TypeOfBussinessId { get; set; }
        public string Name { get; set; } = null!;
    }
}
