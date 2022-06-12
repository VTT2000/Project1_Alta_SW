
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckyDrawPromotion.Models
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string Name { get; set; } = null!;
        public bool AutoUpdate { get; set; } = false;
        public bool CustomerJoinOnlyOne { get; set; } = false;
        public bool ApplyAllCampaign { get; set; } = false;
        public string Description { get; set; } = null!;
        public int CodeUsageLimit { get; set; } = 1;
        public bool Unlimited { get; set; } = false;

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }



        public int SizeProgramId { get; set; }
        public int CharsetId { get; set; }
        public SizeProgram SizeProgram { get; set; } = null!;
        public Charset Charset { get; set; } = null!;



        public ICollection<CampaignGift> CampaignGifts { get; set; } = new HashSet<CampaignGift>();
        
        
        public ICollection<CodeCampaign> CodeCampaigns { get; set; } = new HashSet<CodeCampaign>();
    }

    //--------------------------------------------------------------------------------------------------
    // class dung cho create campaign
    public class CampaignDTO_Request
    {
        public string Name { get; set; } = null!;
        public bool AutoUpdate { get; set; } = false;
        public bool CustomerJoinOnlyOne { get; set; } = false;
        public bool ApplyAllCampaign { get; set; } = false;
        public string Description { get; set; } = null!;
        public int CodeUsageLimit { get; set; } = 1;
        public bool Unlimited { get; set; } = false;
        public int CodeCount { get; set; } = 1;
        public int CodeLength { get; set; } = 10;
        public string Prefix { get; set; } = "ALTA";
        public string Postfix { get; set; } = "";
        public string StartDate { get; set; } = null!;
        public string? EndDate { get; set; }
        public string StartTime { get; set; } = null!;
        public string? EndTime { get; set; }
        public int SizeProgramId { get; set; }
        public int CharsetId { get; set; }
        public ICollection<CampaignGiftDTO_Request> ListCampaignGifts { get; set; } = new HashSet<CampaignGiftDTO_Request>();
    }

    public class CampaignGiftDTO_Request
    {
        public int GiftId { get; set; }
        public ICollection<CodeGiftCampaignDTO> ListCodeGiftCampaigns { get; set; } = new HashSet<CodeGiftCampaignDTO>();
        public RuleDTO? ARule { get; set; }
    }
    
    public class CampaignDTO_ResponseSearch
    {
        public int CampaignId { get; set; }
        public string Name { get; set; } = null!;
        public double ActivatedCode { get; set; } = 0;
        public double GiftQuantity { get; set; } = 0;
        public double Scanned { get; set; } = 0;
        public double UsedForSpin { get; set; } = 0;
        public double Win { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
    //-----------------------------------------------------------------------------------
    // class dung cho filter condition campaign
    public class CampaignDTO_FilterMethod
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public CampaignDTO_FilterMethod(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class CampaignDTO_SearchCriteria
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<CampaignDTO_Condition> Conditions { get; set; } = new HashSet<CampaignDTO_Condition>();
        public CampaignDTO_SearchCriteria(int id, string name, List<CampaignDTO_Condition> list)
        {
            Id = id;
            Name = name;
            Conditions = list;
        }
    }
    public class CampaignDTO_Condition
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public CampaignDTO_Condition(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class CampaignDTO_Request_ConditionSearch
    {
        [Required]
        public int SearchCriteria { get; set; } = 0;
        [Required]
        public int Condition { get; set; } = 0;
        [Required]
        public string Value { get; set; } = null!;
    }


    //-----------------------------------------------------------------------------------
    // class dung cho cua dashboard controller
    public class DashBoardDTO_FilterCondition
    {
        public int Id { get; set; }
        public string Condition { get; set; } = null!;

        public DashBoardDTO_FilterCondition(int id, string condition)
        {
            Id = id;
            Condition = condition;
        }
    }
    public class DashBoardsDTO_ListCampaignFilter
    {
        public double CodeActivated { get; set; } = 0;
        public double Scanned { get; set; } = 0;
        public double UsedForSpin { get; set; } = 0;
        public double Winners { get; set; } = 0;
        public ICollection<CampaignDTO_ResponseSearch> ListCampaigns { get; set; } = new HashSet<CampaignDTO_ResponseSearch>();

    }
    //------------------------------------------------------------------------------------
    public class CampaignDashBoardDTO_OptionFilter
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CampaignDashBoardDTO_OptionFilter(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class CampaignDashBoardDTO_ResponseDetailCampaign
    {
        public int CampaignId { get; set; }
        public string Name { get; set; } = null!;
        public bool AutoUpdate { get; set; } = false;
        public bool CustomerJoinOnlyOne { get; set; } = false;
        public bool ApplyAllCampaign { get; set; } = false;
        public string Description { get; set; } = null!;
        public int CodeUsageLimit { get; set; } = 1;
        public bool Unlimited { get; set; } = false;
        public int CodeCount { get; set; } = 1;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int SizeProgramId { get; set; }
        public int CharsetId { get; set; }
    }
    public class CampaignDashBoardDTO_ResponseUsageSummary
    {
        public DateTime DateUsageSummary { get; set; }
        public double Summary { get; set; } = 0;
        public CampaignDashBoardDTO_ResponseUsageSummary(DateTime dateUsageSummary, double summary)
        {
            DateUsageSummary = dateUsageSummary;
            Summary = summary;
        }
    }

}
