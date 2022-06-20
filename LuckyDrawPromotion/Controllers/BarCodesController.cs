using IronBarCode;
using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Reflection;

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
        public IActionResult GetSearchCriteriaBarCode()
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
        public IActionResult GetAllForFilter(int campaignId, int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            if(!_barCodeService.ExistCampaignId(campaignId))
            {
                return BadRequest(new { message = "CampaignId not exist" });
            }
            if (filterMethod <= 0 || filterMethod >= 3)
            {
                return BadRequest(new { message = "FilterMethod not empty" });
            }
            return Ok(_barCodeService.GetAllForSort(campaignId, filterMethod, listConditionSearches));
        }

        [HttpPost]
        public IActionResult GeneratedBarCode(CodeCampaignDTO_RequestGenerate temp)
        {
            string result = _barCodeService.generatedBarCode(temp);
            if (result.Equals("true"))
            {
                return Ok("Generated (amount of barcodes) Barcodes successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public IActionResult GetBarCodeDetail(int CodeCampaignId)
        {
            return Ok(_barCodeService.GetBarCodeDetail(CodeCampaignId));
        }

        [HttpPut]
        public IActionResult IsActivetedBarCode(int CodeCampaignId)
        {
            if (!_barCodeService.IsCodeCampaign(CodeCampaignId))
            {
                return BadRequest("CodeCampaignId is not exist");
            }
            if (_barCodeService.BarCodeIsActived(CodeCampaignId))
            {
                return Ok("The Barcode (code) is Activated");
            }
            else
            {
                return Ok("The Barcode (code) is De-activated");
            }
        }

        [HttpPut]
        public IActionResult ScannedBarCode(int CodeCampaignId, string Email)
        {
            int existCustomer = _barCodeService.GetIdAstCustomerEmail(Email);
            if (existCustomer == 0)
            {
                return BadRequest("The Email Customer is not Exist");
            }
            if(_barCodeService.BarCodeScanned(CodeCampaignId, existCustomer))
            {
                return Ok("The Barcode (code) is scanned for customer (customer email) by (the session owner)");
            }
            return BadRequest("False barcode scanned");
        }

        // xuat file exels 
        [HttpPost]
        public IActionResult ExportToExcel(List<CodeBarDTO_ResponseFilter> list)
        {
            if (list.Count == 0 || list == null)
            {
                return BadRequest("No data export");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = _barCodeService.ExportToExcel(list);
            stream.Position = 0;
            string excelName = "Barcode_list.xlsx";
            return File(stream, "application/vnd.openxmlformat-officedocument.spredsheetml.sheet", excelName);
        }


        


    }
}
