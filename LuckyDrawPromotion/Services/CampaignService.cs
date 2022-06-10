using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Data;
using Microsoft.Extensions.Options;
using LuckyDrawPromotion.Helpers;
using AutoMapper;

namespace LuckyDrawPromotion.Services
{
    public interface ICampaignService
    {
        IEnumerable<Campaign> GetAll();
        Campaign GetById(int id);
        void Save(Campaign user);
        void Remove(Campaign user);

        Campaign GetByName(string name);
        IEnumerable<CodeGiftCampaignDTO> GetCreateTempGiftCode(int GiftId, int GiftCodeCount);
        string IsCampaignCreatedSucess(CampaignDTO_Request campaignRequest);

    }
    public class CampaignService : ICampaignService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public CampaignService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public IEnumerable<Campaign> GetAll()
        {
            return _context.Campaigns.ToList();
        }

        public Campaign GetById(int id)
        {
            return _context.Campaigns.ToList().FirstOrDefault(x => x.CampaignId == id)!;
        }


        public void Save(Campaign temp)
        {
            try
            {
                _context.Update(temp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "/" + ex.Source);
            }
        }

        public void Remove(Campaign temp)
        {
            try
            {
                _context.Remove(temp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "/" + ex.Source);
            }
        }

        public Campaign GetByName(string name)
        {
            return _context.Campaigns.FirstOrDefault(x => x.Name.Equals(name))!;
        }

        public IEnumerable<CodeGiftCampaignDTO> GetCreateTempGiftCode(int GiftId, int GiftCodeCount)
        {
            var codes = new List<CodeGiftCampaignDTO>();
            string[] MangKyTu = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "V", "W", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            //tạo một chuỗi ngẫu nhiên
            Random fr = new Random();
            for (int i = 0; i < GiftCodeCount; i++)
            {
                CodeGiftCampaignDTO code = new CodeGiftCampaignDTO();
                do
                {
                    string chuoi = "";
                    for (int j = 0; j < 10; j++)
                    {
                        int t = fr.Next(0, MangKyTu.Length);
                        chuoi = chuoi + MangKyTu[t];
                    }
                    code.Code = "GIF" + GiftId + chuoi;
                }
                while (codes.Exists(p => p.Code.Equals(code.Code)));
                codes.Add(code);
            }
            return codes;
        }

        public string IsCampaignCreatedSucess(CampaignDTO_Request campaignRequest)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CampaignDTO_Request, Campaign>();
                cfg.CreateMap<CampaignGiftDTO_Request, CampaignGift>();
                cfg.CreateMap<RuleDTO, Rule>();
                cfg.CreateMap<CodeGiftCampaignDTO, CodeGiftCampaign>();
            });
            var mapper = config.CreateMapper();
            try
            {
                // start save class campaign
                Campaign campaignNew = mapper.Map<CampaignDTO_Request, Campaign>(campaignRequest);
                Save(campaignNew);
                // end save class campaign

                // start save code campaign
                Charset charset = _context.Charsets.First(p => p.CharsetId == campaignRequest.CharsetId);
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
                _context.SaveChanges();
                // end save code campaign

                // start save Campaign gift and Rule and CodeGiftCampaign or CampaignGift
                List<CampaignGiftDTO_Request> tempList = (List<CampaignGiftDTO_Request>)campaignRequest.ListCampaignGifts;
                // co thi save gift campaign
                if (tempList.Count > 0)
                {
                    // save tung record gift campaign
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        CampaignGiftDTO_Request temp = tempList[i];
                        // save gift campaign record
                        CampaignGift campaignGift = mapper.Map<CampaignGiftDTO_Request, CampaignGift>(temp);
                        campaignGift.CampaignId = campaignNew.CampaignId;
                        // co thi save rule
                        if (temp.ARule != null)
                        {
                            RuleDTO tempRule = temp.ARule;
                            Rule rule = mapper.Map<RuleDTO, Rule>(tempRule);
                            _context.Rules.Add(rule);
                            _context.SaveChanges();
                            campaignGift.RuleId = rule.RuleId;
                        }
                        _context.CampaignGifts.Add(campaignGift);
                        _context.SaveChanges();
                        // co thi save list gift code
                        if(temp.ListCodeGiftCampaigns.Count > 0)
                        {
                            List<CodeGiftCampaign> list2 = temp.ListCodeGiftCampaigns.Select
                            (
                                emp => mapper.Map<CodeGiftCampaignDTO, CodeGiftCampaign>(emp)
                            ).ToList();
                            list2.ForEach(c => c.CampaignGiftId = campaignGift.CampaignGiftId);
                            _context.CodeGiftCampaigns.AddRange(list2);
                            _context.SaveChanges();
                        }
                    }
                }
                // end save Campaign gift and Rule and CodeGiftCampaign

            }
            catch (Exception ex)
            {
                return ex.Message + "/" + ex.Source + "/"+ ex;
            }
            return "true";
        }
           
    }
}
