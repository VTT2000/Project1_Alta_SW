namespace LuckyDrawPromotion.Models
{
    public class Rule
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; } = null!;
        public int GiftAmount { get; set; } = 0;
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool AllDay { get; set; } = false;
        public int Probability { get; set; } = 0;
        public string ScheduleValue { get; set; } = null!;



        public int RepeatScheduleId { get; set; }
        public RepeatSchedule RepeatSchedule { get; set; } = null!;

        public ICollection<CampaignGift> CampaignGifts { get; set; } = new HashSet<CampaignGift>();
    }
    public class RuleDTO
    {
        public string RuleName { get; set; } = null!;
        public int GiftAmount { get; set; } = 0;
        public string StartTime { get; set; } = null!;
        public string? EndTime { get; set; }
        public bool AllDay { get; set; } = false;
        public int Probability { get; set; } = 0;
        public string ScheduleValue { get; set; } = null!;
        public int RepeatScheduleId { get; set; }
    }
}
