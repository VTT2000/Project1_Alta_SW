#nullable disable
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
                return Ok(new { message = "NameCampaignOrProgram is created" });
            }
            else
            {
                return BadRequest(new { message = kq });
            }
            
        }

    }
}
