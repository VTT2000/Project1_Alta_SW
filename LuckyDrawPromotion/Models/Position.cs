namespace LuckyDrawPromotion.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string Name { get; set; } = null!;


        public int TypeOfBussinessId { get; set; }
        public TypeOfBussiness TypeOfBussiness { get; set; } = null!;

        public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    }
}
