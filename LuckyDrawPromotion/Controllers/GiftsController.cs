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

        [HttpGet]
        public IActionResult GetSearchCriteriaGift()
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
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(1, "Gift Name", campaignDTO_Conditions));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(2, "Created Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(3, "Activation status", campaignDTO_Conditions1));

            return Ok(campaignDTO_SearchCriterias);
        }

        [HttpPost]
        public IActionResult GetAllForFilterGift(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            if (filterMethod <= 0 || filterMethod >= 3)
            {
                return BadRequest(new { message = "FilterMethod not empty" });
            }
            return Ok(_giftService.GetAllForSort1(filterMethod, listConditionSearches));
        }
        // xuat file excels 
        [HttpPost]
        public IActionResult ExportToExcelGift(List<GiftDTO_ResponseGift> list)
        {
            if (list.Count == 0 || list == null)
            {
                return BadRequest("No data export");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = _giftService.ExportToExcel1(list);
            stream.Position = 0;
            string excelName = "Gift_list.xlsx";
            return File(stream, "application/vnd.openxmlformat-officedocument.spredsheetml.sheet", excelName);
        }
        [HttpPut]
        public IActionResult ActivatedOrDeactivatedGift(int id)
        {
            if (!_giftService.IsExistsGiftId(id))
            {
                return BadRequest("GiftId not exists");
            }
            var kq = _giftService.ActivatedOrDeactivatedGift(id);
            if (kq == true)
            {
                return Ok("The Gift (name of the gift) is Activated");
            }
            else
            {
                return Ok("The Gift (name of the gift) is De-activated");
            }
        }
        [HttpPost]
        public IActionResult CreateGift(GiftDTO_RequestGiftCreate temp)
        {
            if (_giftService.IsExistsGiftByName(temp.Name))
            {
                return BadRequest("Name is exist");
            }
            string kq = _giftService.CreateGift(temp);
            if (kq.Equals("true"))
            {
                return Ok("The Gift (product name) is added to Gifts Category");
            }
            else
            {
                return BadRequest("The Gift information is invalid, please check and try again.");
            }
        }
        [HttpPost]
        public IActionResult ImportExcelToCreate(IFormFile formFile)
        {
            if (formFile == null || formFile.Length <= 0)
            {
                return BadRequest("formfile is empty");
            }

            if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Not Support file extension");
            }

            string kq = _giftService.ImportDataGiftByExcel(formFile);
            if (kq.Equals("true"))
            {
                return Ok("(Amount of gifts) are added to Gifts Category.");
            }
            else
            {
                return BadRequest("The Gifts list is invalid, please check and try again");
            }
        }

        [HttpPut]
        public IActionResult EditGift(GiftDTO_ResponseGift temp)
        {
            if (!_giftService.IsExistsGiftId(temp.GiftId))
            {
                return BadRequest("GiftId not exists");
            }
            var kq = _giftService.EditUpdateGift(temp);
            if (kq.Equals("true"))
            {
                return Ok("The Gift (name of the gift) information is updated");
            }
            else
            {
                return BadRequest("The Gift information is invalid, please check and try again.");
            }
        }


        [HttpDelete]
        public IActionResult DeletedGiftt(int GiftId)
        {
            if (!_giftService.IsExistsGiftId(GiftId))
            {
                return BadRequest("GiftId not exists");
            }
            var kq = _giftService.DeletedGift(GiftId);
            if (kq.Equals("true"))
            {
                return Ok("The Gift (name of the gift) is deleted");
            }
            else
            {
                Console.WriteLine(kq);
                return BadRequest("Gift is used");
            }

        }
    }
}
