namespace LuckyDrawPromotion.Models
{
    public class Position
    {
        public int PositionId { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    }
    public class PositionDTO
    {
        public int PositionId { get; set; }
        public string Name { get; set; } = null!;
    }
}
