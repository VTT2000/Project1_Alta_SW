using IronBarCode;
using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BarCodesController : ControllerBase
    {
        private readonly IBarCodeService _barCodeService;
        public BarCodesController(IBarCodeService barCodeService)
        {
            _barCodeService = barCodeService;
        }

        [HttpGet]
        public IActionResult GetFilterMethod()
        {
            List<CampaignDTO_FilterMethod> campaignDTO_FilterMethods = new List<CampaignDTO_FilterMethod>();
            campaignDTO_FilterMethods.Add(new CampaignDTO_FilterMethod(1, "Match all filters"));
            campaignDTO_FilterMethods.Add(new CampaignDTO_FilterMethod(2, "Match any filters"));
            return Ok(campaignDTO_FilterMethods);
        }
        [HttpGet]
        public IActionResult GetSearchCriteria()
        {
            List<CampaignDTO_Condition> campaignDTO_Conditions = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions.Add(new CampaignDTO_Condition(1, "includes"));
            campaignDTO_Conditions.Add(new CampaignDTO_Condition(2, "is not include"));

            List<CampaignDTO_Condition> campaignDTO_Conditions0 = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(1, "more than"));
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(2, "less than"));
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(3, "exactly"));

            List<CampaignDTO_Condition> campaignDTO_Conditions1 = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions1.Add(new CampaignDTO_Condition(1, "is"));
            campaignDTO_Conditions1.Add(new CampaignDTO_Condition(2, "is not"));

            List<CampaignDTO_SearchCriteria> campaignDTO_SearchCriterias = new List<CampaignDTO_SearchCriteria>();
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(1, "Code", campaignDTO_Conditions));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(2, "Created Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(3, "Expired Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(4, "Scanned Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(5, "Scanned status", campaignDTO_Conditions1));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(6, "Activation status", campaignDTO_Conditions1));

            return Ok(campaignDTO_SearchCriterias);
        }

        [HttpPost]
        public IActionResult GetAllForFilter(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            if (filterMethod <= 0 || filterMethod >= 3)
            {
                return BadRequest(new { message = "FilterMethod not empty" });
            }
            return Ok(_barCodeService.GetAllForSort(filterMethod, listConditionSearches));
        }

    }
}
