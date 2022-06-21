using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RulesController : ControllerBase
    {
        private readonly IRuleService _ruleService;
        public RulesController(IRuleService ruleService) { _ruleService = ruleService; }
        [HttpGet]
        public IActionResult GetRuleForGifts(int campaignId)
        {
            if (!_ruleService.IsExistsCampaignId(campaignId))
            {
                return BadRequest("CampaignId not exists");
            }
            return Ok(_ruleService.GetRuleForGifts(campaignId));
        }
        [HttpPut]
        public IActionResult UpOrDownPriority(int RuleId, int numberUpOrDown)
        {
            if (!_ruleService.IsExistsRuleId(RuleId))
            {
                return BadRequest("RuleId not exists");
            }
            if(numberUpOrDown != 1)
            {
                if(numberUpOrDown != -1)
                {
                    return BadRequest("NumberUpOrDown is:  1.(up) and -1.(down)");
                }
            }
            var kq = _ruleService.UpOrDownPriority(RuleId, numberUpOrDown);
            if (kq.Equals("true"))
            {
                if(numberUpOrDown == 1)
                {
                    return Ok("The priority of the rule (name of the rule) is raised.");
                }
                if(numberUpOrDown == -1)
                {
                    return Ok("The priority of the rule (name of the rule) is reduced.");
                }
            }
            return BadRequest(kq);
        }
        [HttpGet]
        public IActionResult GetCampaignGifts(int campaignId)
        {
            if (!_ruleService.IsExistsCampaignId(campaignId))
            {
                return BadRequest("CampaignId not exists");
            }
            return Ok(_ruleService.GetCampaignGifts(campaignId));
        }
        [HttpPut]
        public IActionResult EditUpdateRule(RuleForGiftDTO rule)
        {
            if (!_ruleService.IsExistsRuleId(rule.RuleId))
            {
                return BadRequest("RuleId not exists");
            }
            var kq = _ruleService.EditUpdateRule(rule);
            if (kq.Equals("true"))
            {
                return Ok("Rule for Gifts has been updated.");
            }
            else
            {
                return BadRequest(kq);
            }
        }
        [HttpPut]
        public IActionResult ActivatedOrDeactivatedRule(int RuleId)
        {
            if (!_ruleService.IsExistsRuleId(RuleId))
            {
                return BadRequest("RuleId not exists");
            }
            var kq = _ruleService.ActivatedOrDeactivatedRule(RuleId);
            if (kq == true)
            {
                return Ok("The rule (name of the rule) is Activated");
            }
            else
            {
                return Ok("The rule (name of the rule) is De-activated");
            }
        }
        

        [HttpPost]
        public IActionResult CreateRuleForCampaign(int campaignId, List<CampaignGiftDTO_RequestRuleCampaign> listCampaignGifts)
        {
            if (!_ruleService.IsExistsCampaignId(campaignId))
            {
                return BadRequest("CampaignId not exists");
            }
            if(listCampaignGifts != null)
            {
                var kq = _ruleService.AddRuleForCampaign(campaignId, listCampaignGifts);
                if (kq.Equals("true"))
                {
                    return Ok("Create Rule for Gifts successful.");
                }
                else
                {
                    return BadRequest(kq);
                }
            }
            else
            {
                return BadRequest("listCampaignGifts <> 0");
            }
        }

        [HttpDelete]
        public IActionResult DeletedRuleForGift(int RuleId)
        {
            if (!_ruleService.IsExistsRuleId(RuleId))
            {
                return BadRequest("RuleId not exists");
            }
            var kq = _ruleService.DeletedRule(RuleId);
            if (kq.Equals("true"))
            {
                return Ok("The rule (name of the rule) is deleted");
            }
            else
            {
                return Ok(kq);
            }
        }
    }
}
