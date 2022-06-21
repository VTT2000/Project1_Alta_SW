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

        public bool Active { get; set; } = false;

        public int Priority { get; set; } = 0;

        public int RepeatScheduleId { get; set; }
        public RepeatSchedule RepeatSchedule { get; set; } = null!;

        public int CampaignGiftId { get; set; }
        public CampaignGift CampaignGift { get; set; } = null!;
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
        public bool Active { get; set; } = true;
    }
    public class RuleForGiftDTO
    {
        public int RuleId { get; set; }
        public string RuleName { get; set; } = null!;
        public string GiftName { get; set; } = null!;
        public int GiftAmount { get; set; } = 0;
        public string StartTime { get; set; } = null!;
        public string? EndTime { get; set; }
        public bool AllDay { get; set; } = false;
        public int Probability { get; set; } = 0;
        public string ScheduleValue { get; set; } = null!;
        public string? RepeatScheduleName { get; set; }
        public int RepeatScheduleId { get; set; }
        public bool Active { get; set; } = true;
        public int Priority { get; set; } = 0;
        public int CampaignGiftId { get; set; }
    }

   
}
