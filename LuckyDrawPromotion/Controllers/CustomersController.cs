using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService) { _customerService = customerService; }
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
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(1, "Name", campaignDTO_Conditions));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(2, "Phone Number", campaignDTO_Conditions));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(3, "Date of Birth(DoB)", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(4, "Position", campaignDTO_Conditions1));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(5, "Type of Business", campaignDTO_Conditions1));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(6, "Status", campaignDTO_Conditions1));

            return Ok(campaignDTO_SearchCriterias);
        }
        [HttpGet]
        public IActionResult GetPositions()
        {
            return Ok(_customerService.GetPositions());
        }
        [HttpGet]
        public IActionResult GetTypeOfBussinesss()
        {
            return Ok(_customerService.GetTypeOfBussiness().OrderBy(p=>p.TypeOfBussinessId));
        }
        [HttpPost]
        public IActionResult GetAllForFilter(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            if (filterMethod <= 0 || filterMethod >= 3)
            {
                return BadRequest(new { message = "FilterMethod not empty" });
            }
            try
            {
                return Ok(_customerService.GetAllForSort(filterMethod, listConditionSearches));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        // xuat file exels 
        [HttpPost]
        public IActionResult ExportToExcel(List<CustomerDTO_ResponseFilter> list)
        {
            if (list.Count == 0 || list == null)
            {
                return BadRequest("No data export");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = _customerService.ExportToExcel(list);
            stream.Position = 0;
            string excelName = "Customer_list.xlsx";
            return File(stream, "application/vnd.openxmlformat-officedocument.spredsheetml.sheet", excelName);
        }
        [HttpPut]
        public IActionResult BlockOrUnblockCustomer(int id)
        {
            if (!_customerService.IsExistCustomerId(id))
            {
                return BadRequest("CustomerId not exist");
            }
            try
            {
                bool kq = _customerService.BlockOrUnblockCustomer(id);
                if (kq)
                {
                    return Ok("The customer (name of customer) is blocked");
                }
                else
                {
                    return Ok("The customer (name of customer) is unblocked");
                }
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
