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

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly LuckyDrawPromotionContext _context;

        public CampaignsController(LuckyDrawPromotionContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SizeProgramDTO_Response>>> GetSizeProgram()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SizeProgram,SizeProgramDTO_Response>();
            });
            var mapper = config.CreateMapper();
            var list = await _context.SizePrograms.Select
                            (
                              emp => mapper.Map<SizeProgram, SizeProgramDTO_Response>(emp)
                            ).ToListAsync();
            return list;
        }

        
        [HttpGet("{name}")]
        public async Task<IActionResult> ExistNameCampaignOrProgram(string name)
        {
            Campaign temp = await _context.Campaigns.FirstOrDefaultAsync(p => p.Name.Equals(name));
            if (temp != null)
            {
                return BadRequest(new { message = "NameCampaignOrProgram must be unique" });
            }
            else
            {
                return Ok(new { message = "NameCampaignOrProgram is valid" });
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharsetDTO_Response>>> GetCharset()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Charset, CharsetDTO_Response>();
            });
            var mapper = config.CreateMapper();
            var list = await _context.Charsets.Select
                            (
                              emp => mapper.Map<Charset, CharsetDTO_Response>(emp)
                            ).ToListAsync();
            return list;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDTO_Response>>> GetGift()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Gift, GiftDTO_Response>();
            });
            var mapper = config.CreateMapper();
            var list = await _context.Gifts.Select
                            (
                              emp => mapper.Map<Gift, GiftDTO_Response>(emp)
                            ).ToListAsync();
            return list;
        }
        
        
        [HttpGet("{GiftId}/{GiftCodeCount}")]
        public IActionResult GetCreateTempGiftCode(int GiftId, int GiftCodeCount)
        {
            var codes = new List<CodeGiftCampaignDTO_Response>();
            string[] MangKyTu = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "V", "W", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            //tạo một chuỗi ngẫu nhiên
            Random fr = new Random();
            for (int i = 0; i < GiftCodeCount; i++)
            {
                CodeGiftCampaignDTO_Response code = new CodeGiftCampaignDTO_Response();
                do
                {
                    string chuoi = "";
                    for (int j = 0; j < 10; j++)
                    {
                        int t = fr.Next(0, MangKyTu.Length-1);
                        chuoi = chuoi + MangKyTu[t];
                    }
                    code.Code = "GIF" + GiftId + chuoi;
                }
                while (codes.Exists(p => p.Code.Equals(code.Code)));
                codes.Add(code);
            }
            return Ok(codes);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepeatScheduleDTO_Response>>> GetRepeatSchedule()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RepeatSchedule, RepeatScheduleDTO_Response>();
            });
            var mapper = config.CreateMapper();
            var list = await _context.RepeatSchedules.Select
                            (
                              emp => mapper.Map<RepeatSchedule, RepeatScheduleDTO_Response>(emp)
                            ).ToListAsync();
            return list;
        }

        [HttpPost]
        public async Task<ActionResult<Campaign>> CreateCampaign(CampaignDTO_Request campaignRequest)
        {
            // start save class campaign
            Campaign campaignNew = new Campaign();
            try
            {
                campaignNew.Name = campaignRequest.Name;
                if (campaignRequest.SizeProgramId == 1)
                {
                    campaignNew.AutoUpdate = campaignRequest.AutoUpdate;
                    campaignNew.CustomerJoinOnlyOne = campaignRequest.CustomerJoinOnlyOne;
                }
                if (campaignRequest.SizeProgramId == 2)
                {
                    campaignNew.ApplyAllCampaign = campaignRequest.ApplyAllCampaign;
                }
                campaignNew.Description = campaignRequest.Description;
                campaignNew.CodeUsageLimit = campaignRequest.CodeUsageLimit;
                campaignNew.Unlimited = campaignRequest.Unlimited;
                campaignNew.CodeCount = campaignRequest.CodeCount;
                campaignNew.CodeLength = campaignRequest.CodeLength;
                campaignNew.Prefix = campaignRequest.Prefix;
                campaignNew.Postfix = campaignRequest.Postfix;

                campaignNew.StartDate = DateTime.Parse(campaignRequest.StartDate);
                campaignNew.EndDate = DateTime.Parse(campaignRequest.EndDate);
                campaignNew.StartTime = TimeSpan.Parse(campaignRequest.StartTime);
                campaignNew.EndTime = TimeSpan.Parse(campaignRequest.EndTime);

                campaignNew.SizeProgramId = campaignRequest.SizeProgramId;
                campaignNew.CharsetId = campaignRequest.CharsetId;

                _context.Campaigns.Add(campaignNew);
                await _context.SaveChangesAsync();
                // end save class campaign

                // start save code campaign
                Charset charset = await _context.Charsets.FirstOrDefaultAsync(p => p.CharsetId == campaignRequest.CharsetId);
                char[] MangKyTu = charset.Value.ToCharArray();
                Random fr = new Random();
                // Do dai tao khi chiem postfix va prefix
                int DoDaiTao = campaignRequest.CodeLength - (campaignRequest.Prefix.Length + campaignRequest.Postfix.Length);
                // khi codecount > so code duoc tao gioi han boi postfix va prefix
                int gioiHanTao = campaignRequest.CodeCount;
                List<CodeCampaign> list0 = new List<CodeCampaign>();
                for (int i = 0; i < gioiHanTao; i++)
                {
                    CodeCampaign code = new CodeCampaign();
                    do
                    {
                        string chuoi0 = "";
                        for (int j = 0; j < DoDaiTao; j++)
                        {
                            int t = fr.Next(0, MangKyTu.Length);
                            chuoi0 += MangKyTu[t];
                        }
                        code.Code = campaignRequest.Prefix + chuoi0 + campaignRequest.Postfix;
                    }
                    while (list0.Exists(p => p.Code.Equals(code.Code)));

                    code.CampaignId = campaignNew.CampaignId;
                    list0.Add(code);
                }
                _context.CodeCampaigns.AddRange(list0);
                await _context.SaveChangesAsync();

                // end save code campaign

                // start save Campaign gift and Rule and CodeGiftCampaign
                List<CampaignGiftDTO_Request> tempList1 = (List<CampaignGiftDTO_Request>)campaignRequest.CampaignGifts;
                for (int i = 0; i < tempList1.Count; i++)
                {
                    CampaignGift campaignGift = new CampaignGift();
                    campaignGift.CampaignId = campaignNew.CampaignId;
                    campaignGift.GiftId = tempList1[i].GiftId;

                    RuleDTO_Request tempRule = (RuleDTO_Request)tempList1[i].Rule;
                    if (tempRule != null)
                    {
                        Rule rule = new Rule();
                        rule.RuleName = tempRule.RuleName;
                        rule.GiftAmount = tempRule.GiftAmount;
                        rule.StartTime = TimeSpan.Parse(tempRule.StartTime);
                        rule.EndTime = TimeSpan.Parse(tempRule.EndTime);
                        rule.AllDay = tempRule.AllDay;
                        rule.Probability = tempRule.Probability;
                        rule.ScheduleValue = tempRule.ScheduleValue;
                        rule.RepeatScheduleId = tempRule.RepeatScheduleId;

                        _context.Rules.Add(rule);
                        await _context.SaveChangesAsync();
                        
                        campaignGift.RuleId = rule.RuleId;
                    }
                    _context.CampaignGifts.Add(campaignGift);
                    await _context.SaveChangesAsync();
                    
                    List<CodeGiftCampaign> list2 = new List<CodeGiftCampaign>();
                    List<CodeGiftCampaignDTO_Request> tempList2 = (List<CodeGiftCampaignDTO_Request>)tempList1[i].CodeGiftCampaigns;
                    for (int j = 0; j < tempList2.Count; j++)
                    {
                        CodeGiftCampaign codeGiftCampaign = new CodeGiftCampaign();
                        codeGiftCampaign.CampaignGiftId = campaignGift.CampaignGiftId;
                        codeGiftCampaign.CreatedDate = DateTime.Parse(tempList2[j].CreatedDate);
                        codeGiftCampaign.Code = tempList2[j].Code;
                        list2.Add(codeGiftCampaign);
                    }
                    _context.CodeGiftCampaigns.AddRange(list2);
                    await _context.SaveChangesAsync();
                }
                // end save Campaign gift and Rule and CodeGiftCampaign
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { message = "NameCampaignOrProgram is created" });
        }
        
    }
}
