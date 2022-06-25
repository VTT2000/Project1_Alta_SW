using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly IGiftService _giftService;
        public GiftsController(IGiftService giftService) { _giftService = giftService; }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_giftService.GetGifts());
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
        public IActionResult GetSearchCriteriaGeneratedGift()
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
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(2, "Campaign", campaignDTO_Conditions));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(3, "Created Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(4, "Expired Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(5, "Activation status", campaignDTO_Conditions1));

            return Ok(campaignDTO_SearchCriterias);
        }

        [HttpPost]
        public IActionResult GetAllForFilterGeneratedGift(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            if (filterMethod <= 0 || filterMethod >= 3)
            {
                return BadRequest(new { message = "FilterMethod not empty" });
            }
            return Ok(_giftService.GetAllForSort(filterMethod, listConditionSearches));
        }

        // xuat file excels 
        [HttpPost]
        public IActionResult ExportToExcelGeneratedGift(List<GiftDTO_ResponseGiftCode> list)
        {
            if (list.Count == 0 || list == null)
            {
                return BadRequest("No data export");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = _giftService.ExportToExcel(list);
            stream.Position = 0;
            string excelName = "Gift-Code_list.xlsx";
            return File(stream, "application/vnd.openxmlformat-officedocument.spredsheetml.sheet", excelName);
        }

        [HttpPut]
        public IActionResult ActivatedOrDeactivatedGeneratedGift(int id)
        {
            if (!_giftService.IsExistsCodeGiftCampaignId(id))
            {
                return BadRequest("CodeGiftCampaignId not exists");
            }
            var kq = _giftService.ActivatedOrDeactivatedGeneratedGift(id);
            if (kq == true)
            {
                return Ok("The GeneratedGift (GeneratedGift) is Activated");
            }
            else
            {
                return Ok("The GeneratedGift (name of the GeneratedGift) is De-activated");
            }
        }

        [HttpPut]
        public IActionResult EditUpdateGeneratedGift(GiftDTO_ResponseGiftCode temp)
        {
            if (!_giftService.IsExistsCodeGiftCampaignId(temp.CodeGiftCampaignId))
            {
                return BadRequest("CodeGiftCampaignId not exists");
            }
            var kq = _giftService.EditUpdateGeneratedGift(temp);
            if (kq.Equals("true"))
            {
                return Ok("GeneratedGift has been updated.");
            }
            else
            {
                return BadRequest(kq);
            }
        }
        

        [HttpDelete]
        public IActionResult DeletedGeneratedGiftt(int CodeGiftCampaignId)
        {
            if (!_giftService.IsExistsCodeGiftCampaignId(CodeGiftCampaignId))
            {
                return BadRequest("CodeGiftCampaignId not exists");
            }
            var kq = _giftService.DeletedGeneratedGift(CodeGiftCampaignId);
            if (kq.Equals("true"))
            {
                return Ok("The GeneratedGift (name of the GeneratedGift) is deleted");
            }
            else
            {
                Console.WriteLine(kq);
                return BadRequest("GeneratedGift is used");
            }
        }
    }
}
