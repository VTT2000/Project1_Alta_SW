using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;
        public SettingsController(ISettingService settingService) { _settingService = settingService; }
        
        [HttpGet]
        public IActionResult GetSettingByCampaignId(int campaignId)
        {
            if (!_settingService.IsExistCampaignId(campaignId))
            {
                return BadRequest(new { message = "CampaignId not exist" });
            }
            return Ok(_settingService.GetSettingByCampaignId(campaignId));
        }

        [HttpPut]
        public IActionResult UpdateSettingCampaign(Settings settings)
        {
            if (!_settingService.IsExistCampaignId(settings.CampaignId))
            {
                return BadRequest(new { message = "CampaignId not exist" });
            }
            return Ok(_settingService.UpdateSetting(settings));
        }
    }
}
