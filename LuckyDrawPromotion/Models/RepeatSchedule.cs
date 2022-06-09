namespace LuckyDrawPromotion.Models
{
    public class RepeatSchedule
    {
        public int RepeatScheduleId { get; set; }
        public string Name { get; set; } = null!;



        public ICollection<Rule> Rules { get; set; } = new HashSet<Rule>();
    }

    public class RepeatScheduleDTO_Response
    {
        public int RepeatScheduleId { get; set; }
        public string Name { get; set; } = null!;

    }
}
