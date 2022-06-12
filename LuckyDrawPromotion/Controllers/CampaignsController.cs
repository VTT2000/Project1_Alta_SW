#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Models;
using AutoMapper;
using LuckyDrawPromotion.Services;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        public CampaignsController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet("{name}")]
        public IActionResult ExistNameCampaignOrProgram(string name)
        {
            Campaign temp = _campaignService.GetByName(name);
            if (temp != null)
            {
                return BadRequest(new { message = "NameCampaignOrProgram must be unique" });
            }
            else
            {
                return Ok(new { message = "NameCampaignOrProgram is valid" });
            }
        }
        
        [HttpGet("{GiftId}/{GiftCodeCount}")]
        public IActionResult GetCreateTempGiftCode(int GiftId, int GiftCodeCount)
        {
            return Ok(_campaignService.GetCreateTempGiftCode(GiftId, GiftCodeCount));
        }
        
        [HttpPost]
        public IActionResult CreateCampaign(CampaignDTO_Request campaignRequest)
        {
            if (_campaignService.GetByName(campaignRequest.Name) != null)
            {
                return BadRequest(new { message = "NameCampaignOrProgram must be unique" });
            }
            string kq = _campaignService.IsCampaignCreatedSucess(campaignRequest);

            if (kq.Equals("true"))
            {
                return Ok(new { message = "The Campaign (campaign name) is created successfully." });
            }
            else
            {
                return BadRequest(new { message = kq });
            }
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

            List<CampaignDTO_SearchCriteria> campaignDTO_SearchCriterias = new List<CampaignDTO_SearchCriteria>();
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(1, "Campaign Name", campaignDTO_Conditions));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(2, "Created Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(2, "Expired Date", campaignDTO_Conditions0));

            return Ok(campaignDTO_SearchCriterias);
        }

        [HttpPost]
        public IActionResult GetAllForFilter(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        { 
            if (filterMethod <= 0 || filterMethod >= 3)
            {
                return BadRequest(new { message = "FilterMethod not empty" });
            }
            return Ok(_campaignService.GetAllForSort(filterMethod, listConditionSearches));
        }

        [HttpGet]
        public IActionResult GetOptionFilterForDashBoardCampaign()
        {
            List<CampaignDashBoardDTO_OptionFilter> list = new List<CampaignDashBoardDTO_OptionFilter>();
            list.Add(new CampaignDashBoardDTO_OptionFilter(1, "Today"));
            list.Add(new CampaignDashBoardDTO_OptionFilter(2, "This week"));
            list.Add(new CampaignDashBoardDTO_OptionFilter(3, "This month"));
            list.Add(new CampaignDashBoardDTO_OptionFilter(4, "This quarter"));
            return Ok(list);
        }

        [HttpGet("{CampaignId}")]
        public IActionResult GetCampaignDashBoardDetail(int CampaignId)
        {
            return Ok(_campaignService.GetCampaignDashBoardDTO_ResponseDetailCampaign(CampaignId));
        }
        [HttpGet("{CampaignId}/{OptionFilterId}")]
        public IActionResult GetCampaignDashBoardUsageSummary(int CampaignId, int OptionFilterId)
        {
            return Ok(_campaignService.GetCampaignDashBoardDTO_ResponseUsageSummary(CampaignId, OptionFilterId));
        }


        
    }
}
