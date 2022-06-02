namespace LuckyDrawPromotion.Models
{
    public class SizeProgram
    {
        public int SizeProgramId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        
        public ICollection<Campaign> Campaigns { get; set; } = new HashSet<Campaign>();
    }
}
