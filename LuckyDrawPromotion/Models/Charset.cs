namespace LuckyDrawPromotion.Models
{
    public class Charset
    {
        public int CharsetId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;


        public ICollection<Campaign> Campaigns { get; set; } = new HashSet<Campaign>();
    }
    public class CharsetDTO_Response
    {
        public int CharsetId { get; set; }
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }

}
