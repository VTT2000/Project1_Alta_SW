using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashBoardsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        public DashBoardsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public IActionResult GetFilterConditions()
        {
            List<DashBoardDTO_FilterCondition> dashBoard_FilterConditions = new List<DashBoardDTO_FilterCondition>();
            dashBoard_FilterConditions.Add(new DashBoardDTO_FilterCondition(1, "Today"));
            dashBoard_FilterConditions.Add(new DashBoardDTO_FilterCondition(2, "This week"));
            dashBoard_FilterConditions.Add(new DashBoardDTO_FilterCondition(3, "This month"));
            return Ok(dashBoard_FilterConditions);
        }
        
        [HttpPost]
        public IActionResult GetCampaignFilterConditions(int id)
        {
            return Ok(_campaignService.GetCampaignFilterConditions(id));
        }

    }
}
