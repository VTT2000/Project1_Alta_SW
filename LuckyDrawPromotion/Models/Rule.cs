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
        public int CampainGiftId { get; set; }
        public RepeatSchedule RepeatSchedule { get; set; } = null!;
        public CampaignGift CampaignGift { get; set; } = null!;
    }
}
