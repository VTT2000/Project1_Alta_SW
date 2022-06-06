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
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeCodeDTO_Response>>> GetTypeCode()
        {
            IEnumerable<Models.TypeCode> list0 = await _context.TypeCodes.ToListAsync();
            List<TypeCodeDTO_Response> list = new List<TypeCodeDTO_Response>();
            foreach(Models.TypeCode temp in list0)
            {
                TypeCodeDTO_Response add = new TypeCodeDTO_Response();
                add.Id = temp.Id;
                add.Name = temp.Name;
                add.Description = temp.Description;
                list.Add(add);
            }
            return list;
        }

        [HttpGet("{Name}")]
        public IActionResult ExistNameCampaignOrProgram(string Name)
        {
            Campaign temp = _context.Campaigns.FirstOrDefault(p => p.Name == Name);
            if (temp == null)
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
            IEnumerable<Charset> list0 = await _context.Charsets.ToListAsync();
            List<CharsetDTO_Response> list = new List<CharsetDTO_Response>();
            foreach (Models.Charset temp in list0)
            {
                CharsetDTO_Response add = new CharsetDTO_Response();
                add.Id = temp.Id;
                add.Name = temp.Name;
                add.Content = temp.Content;
                list.Add(add);
            }
            return list;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftDTO_Response>>> GetGift()
        {
            IEnumerable<Gift> list0 = await _context.Gifts.ToListAsync();
            List<GiftDTO_Response> list = new List<GiftDTO_Response>();
            foreach (Gift temp in list0)
            {
                GiftDTO_Response add = new GiftDTO_Response();
                add.Id = temp.Id;
                add.Name = temp.Name;
                add.CreatedDate = temp.CreatedDate;
                add.Description = temp.Description;
                list.Add(add);
            }
            return list;
        }

        [HttpGet("{GiftId}/{CodeCount}")]
        public IActionResult GetCreateTempGiftCode(int GiftId, int CodeCount)
        {
            var codes = new List<GiftCodeDTO_Response>();
            string[] MangKyTu = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M ", "N", "O", "P", "Q", "R", "S", "T", "V", "W", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            //tạo một chuỗi ngẫu nhiên
            Random fr = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                GiftCodeDTO_Response code = new GiftCodeDTO_Response();
                code.GiftId = GiftId;
                do
                {
                    string chuoi = "";
                    for (int j = 0; j < 10; j++)
                    {
                        int t = fr.Next(0, MangKyTu.Length);
                        chuoi = chuoi + MangKyTu[t];
                    }
                    code.Name = "GIF" + GiftId + chuoi;
                }
                while (codes.Exists(p => p.Name == code.Name));
                codes.Add(code);
            }
            return Ok(codes);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepeatScheduleDTO_Response>>> GetRepeatSchedule()
        {
            IEnumerable<RepeatSchedule> list0 = await _context.RepeatSchedules.ToListAsync();
            List<RepeatScheduleDTO_Response> list = new List<RepeatScheduleDTO_Response>();
            foreach (Models.RepeatSchedule temp in list0)
            {
                RepeatScheduleDTO_Response add = new RepeatScheduleDTO_Response();
                add.Id = temp.Id;
                add.Name = temp.Name;
                list.Add(add);
            }
            return list;
        }

        [HttpPost]
        public async Task<ActionResult<Campaign>> SaveCampaign(CampaignDTO_Request campaignRequest)
        {
            // start save class campaign
            Campaign campaignNew = new Campaign();
            campaignNew.TypeCodeId = campaignRequest.TypeCodeId;
            campaignNew.Name = campaignRequest.Name;
            campaignNew.Description = campaignRequest.Description;
            campaignNew.TypeCodeId = campaignRequest.TypeCodeId;
            campaignNew.Unlimited = campaignRequest.Unlimited;
            campaignNew.CodeUsageLimit = campaignRequest.CodeUsageLimit;
            if (campaignRequest.TypeCodeId == 1)
            {
                campaignNew.AutoUpdate = campaignRequest.AutoUpdate;
                campaignNew.CustomerOnlyOne = campaignRequest.CustomerOnlyOne;
            }
            if (campaignRequest.TypeCodeId == 2)
            {
                campaignNew.ApplyAllCampaign = campaignRequest.ApplyAllCampaign;
            }
            campaignNew.StartDate = DateTime.Parse(campaignRequest.StartDate + " " + campaignRequest.StartTime);
            campaignNew.ExpiredDate = DateTime.Parse(campaignRequest.EndDate + " " + campaignRequest.EndTime);
            _context.Campaigns.Add(campaignNew);
            await _context.SaveChangesAsync();
            // end save class campaign

            // start save class code
            Charset charset = await _context.Charsets.FirstOrDefaultAsync(p => p.Id == campaignRequest.CharsetId);
            string[] MangKyTu = charset.Content.Split("");
            Random fr = new Random();
            int DoDaiTao = campaignRequest.CodeLength - (campaignRequest.Prefix.Length + campaignRequest.Postfix.Length);
            int gioiHanTao = (campaignRequest.CodeCount > Math.Pow(MangKyTu.Length, DoDaiTao)) ? (int)Math.Pow(MangKyTu.Length, DoDaiTao) : campaignRequest.CodeCount;
            if (campaignRequest.TypeCodeId == 1)
            {
                List<Code> list0 = new List<Code>();
                for (int i = 0; i < gioiHanTao; i++)
                {
                    Code code = new Code();
                    string chuoi0 = campaignRequest.Prefix;
                    do
                    {
                        for (int j = 0; j < DoDaiTao; j++)
                        {
                            int t = fr.Next(0, MangKyTu.Length);
                            chuoi0 += MangKyTu[t];
                        }
                        code.Name = chuoi0 + campaignRequest.Postfix;
                    }
                    while (list0.Exists(p => p.Name == code.Name));
                    code.CampaignId = campaignNew.Id;
                    code.Active = true;
                    list0.Add(code);
                }
                _context.Codes.AddRange(list0);
                await _context.SaveChangesAsync();
            }
            if (campaignRequest.TypeCodeId == 2)
            {
                Code code = new Code();
                string chuoi0 = campaignRequest.Prefix;

                for (int j = 0; j < DoDaiTao; j++)
                {
                    int t = fr.Next(0, MangKyTu.Length);
                    chuoi0 += MangKyTu[t];
                }
                code.Name = chuoi0 + campaignRequest.Postfix;
                code.CampaignId = campaignNew.Id;
                code.Active = true;
                _context.Codes.AddRange(code);
                await _context.SaveChangesAsync();
            }
            // end save class code

            // start save class giftcode
            List<GiftCode> list = new List<GiftCode>();
            for (int j = 0; j < campaignRequest.GiftCodeList.Count; j++)
            {
                GiftCode temp = new GiftCode();
                temp.Name = campaignRequest.GiftCodeList[j].Name;
                temp.CreatedDate = campaignRequest.GiftCodeList[j].CreatedDate;
                temp.Active = campaignRequest.GiftCodeList[j].Active;
                temp.CampaignId = campaignNew.Id;
                temp.GiftId = campaignRequest.GiftCodeList[j].GiftId;
                list.Add(temp);
            }
            _context.GiftCodes.AddRange(list);
            await _context.SaveChangesAsync();
            // end save class giftcode

            // start save class rule
            List<Rule> list2 = new List<Rule>();
            for (int i = 0; i < campaignRequest.RuleList.Count; i++)
            {
                Rule rule = new Rule();
                rule.Name = campaignRequest.RuleList[i].Name;
                rule.GiftIdSeletedCampaign = campaignRequest.RuleList[i].GiftIdSeletedCampaign;
                rule.GiftCount = campaignRequest.RuleList[i].GiftCount;
                rule.StartDate = campaignRequest.RuleList[i].StartDate;
                rule.EndDate = campaignRequest.RuleList[i].EndDate;
                rule.StartTime = campaignRequest.RuleList[i].StartTime;
                rule.EndTime = campaignRequest.RuleList[i].EndTime;
                rule.AllDay = campaignRequest.RuleList[i].AllDay;
                rule.Probability = campaignRequest.RuleList[i].Probability;
                rule.CampaignId = campaignNew.Id;
                rule.RepeatScheduleId = campaignRequest.RuleList[i].RepeatScheduleId;
                rule.RepeatScheduleValue = campaignRequest.RuleList[i].RepeatScheduleValue;
                list2.Add(rule);
            }
            _context.Rules.AddRange(list2);
            await _context.SaveChangesAsync();
            // end save class rule

            return Ok(new { message = "NameCampaignOrProgram is valid" });
        }


        */
        
    }
}
