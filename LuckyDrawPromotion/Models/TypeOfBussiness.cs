namespace LuckyDrawPromotion.Models
{
    public class TypeOfBussiness
    {
        public int TypeOfBussinessId { get; set; }
        public string Name { get; set; } = null!;


        public ICollection<Position> Positions { get; set; } = new HashSet<Position>();
    }
}
