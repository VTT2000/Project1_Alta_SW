using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HistoryBarCodesController : ControllerBase
    {
        private readonly IBarCodeService _barCodeService;
        public HistoryBarCodesController(IBarCodeService barCodeService) { _barCodeService = barCodeService; }
        [HttpGet]
        public IActionResult GetFilterMethod()
        {
            List<CampaignDTO_FilterMethod> campaignDTO_FilterMethods = new List<CampaignDTO_FilterMethod>();
            campaignDTO_FilterMethods.Add(new CampaignDTO_FilterMethod(1, "Match all filters"));
            campaignDTO_FilterMethods.Add(new CampaignDTO_FilterMethod(2, "Match any filters"));
            return Ok(campaignDTO_FilterMethods);
        }
        [HttpGet]
        public IActionResult GetSearchCriteriaBarCodeHistory()
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
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(3, "Scanned Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(4, "Spin Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(5, "Owner", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(6, "Scanned status", campaignDTO_Conditions1));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(7, "Used for spin", campaignDTO_Conditions1));

            return Ok(campaignDTO_SearchCriterias);
        }
        

        [HttpPost]
        public IActionResult GetAllForFilter(int campaignId, int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            if (!_barCodeService.ExistCampaignId(campaignId))
            {
                return BadRequest("CampaignId not exist");
            }
            if (filterMethod <= 0 || filterMethod >= 3)
            {
                return BadRequest(new { message = "FilterMethod not empty" });
            }
            return Ok(_barCodeService.GetAllHistoryForSort(campaignId, filterMethod, listConditionSearches));
        }

        // xuat file exels 
        [HttpPost]
        public IActionResult ExportHistoryToExcel(List<CodeBarDTO_ResponseHistoryFilter> list)
        {
            if(list.Count == 0 || list == null)
            {
                return BadRequest("No data export");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = _barCodeService.ExportHistoryToExcel(list);
            stream.Position = 0;
            string excelName = "BarcodeHistory_list.xlsx";
            return File(stream, "application/vnd.openxmlformat-officedocument.spredsheetml.sheet", excelName);
        }
    }
}
