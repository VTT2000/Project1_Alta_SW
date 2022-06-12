using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Data;
using Microsoft.Extensions.Options;
using LuckyDrawPromotion.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LuckyDrawPromotion.Services
{
    public interface ICampaignService
    {
        IEnumerable<Campaign> GetAll();
        Campaign GetById(int id);
        void Save(Campaign user);
        void Remove(Campaign user);

        DashBoardsDTO_ListCampaignFilter GetCampaignFilterConditions(int id);
        Campaign GetByName(string name);
        IEnumerable<CodeGiftCampaignDTO> GetCreateTempGiftCode(int GiftId, int GiftCodeCount);
        string IsCampaignCreatedSucess(CampaignDTO_Request campaignRequest);
        IEnumerable<CampaignDTO_ResponseSearch> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> conditionSearches);
        CampaignDashBoardDTO_ResponseDetailCampaign GetCampaignDashBoardDTO_ResponseDetailCampaign(int id);
        List<CampaignDashBoardDTO_ResponseUsageSummary> GetCampaignDashBoardDTO_ResponseUsageSummary(int id, int optionFilterId);
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

        public DashBoardsDTO_ListCampaignFilter GetCampaignFilterConditions(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Campaign, CampaignDTO_ResponseSearch>();
            });
            var mapper = config.CreateMapper();
            List<CampaignDTO_ResponseSearch> list = new List<CampaignDTO_ResponseSearch>();
            if (id == 1) // to day
            {
                list = _context.Campaigns.Where(p=>p.StartDate.Date == DateTime.Now.Date).Select(emp => mapper.Map<Campaign, CampaignDTO_ResponseSearch>(emp)).ToList();
            }
            if(id == 2) // this week
            {
                var first = DateTime.Now.FirstDayOfWeek();
                var last = DateTime.Now.LastDayOfWeek();
                list = _context.Campaigns.Where(p => p.StartDate.Date >= first).Where(p=>p.StartDate.Date <= last).Select(emp => mapper.Map<Campaign, CampaignDTO_ResponseSearch>(emp)).ToList();
            }
            if (id == 3) // this month
            {
                var first = DateTime.Now.FirstDayOfMonth();
                var last = DateTime.Now.LastDayOfMonth();
                list = _context.Campaigns.Where(p => p.StartDate.Date >= first).Where(p=>p.StartDate.Date <= last).Select(emp => mapper.Map<Campaign, CampaignDTO_ResponseSearch>(emp)).ToList();
            }
            DashBoardsDTO_ListCampaignFilter kq = new DashBoardsDTO_ListCampaignFilter();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].ActivatedCode = _context.CodeCampaigns.Where(p => p.CampaignId == list[i].CampaignId && p.ActivatedDate.HasValue).ToList().Count;
                list[i].GiftQuantity = _context.CampaignGifts.Where(p => p.CampaignId == list[i].CampaignId).ToList().Count;
                list[i].Scanned = _context.CodeCampaigns.Where(p => p.CampaignId == list[i].CampaignId && p.Scanned == true).ToList().Count;
                list[i].UsedForSpin = _context.CodeCampaigns.Where(p => p.Spins.Count > 0 && p.CampaignId == list[i].CampaignId).ToList().Count;
                list[i].Win = _context.Winners.Where(p => p.CodeGiftCampaign.CampaignGift.CampaignId == list[i].CampaignId).ToList().Count;
                
                kq.CodeActivated += list[i].ActivatedCode;
                kq.Scanned += list[i].Scanned;
                kq.UsedForSpin += list[i].UsedForSpin;
                kq.Winners += list[i].Win;
            }
            kq.ListCampaigns = list;
            return kq;
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
                // chuyen date time camrequest dung dinh dang
                campaignRequest.StartDate = DateTime.ParseExact(campaignRequest.StartDate, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                if(campaignRequest.EndDate != null)
                {
                    campaignRequest.EndDate = DateTime.ParseExact(campaignRequest.EndDate, "dd/MM/yyyy", null).ToString("yyyy-MM-dd");
                }
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
                    code.CodeRedemptionLimit = campaignNew.CodeUsageLimit;
                    code.Unlimited = campaignNew.Unlimited;
                    code.ActivatedDate = DateTime.Now;
                    code.ExpiredDate = DateTime.Parse(campaignNew.EndDate + " " + campaignNew.EndTime);
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
                            List<CodeGiftCampaignDTO> tempList2 = (List<CodeGiftCampaignDTO>)temp.ListCodeGiftCampaigns;
                            for(int z = 0; z < tempList2.Count; z++)
                            {
                                CodeGiftCampaign temp2 = mapper.Map<CodeGiftCampaignDTO, CodeGiftCampaign>(tempList2[z]);
                                temp2.CampaignGiftId = campaignGift.CampaignGiftId;
                                if (tempList2[z].Active)
                                {
                                    temp2.ActivatedDate = DateTime.Now;
                                }
                                _context.CodeGiftCampaigns.Add(temp2);
                                _context.SaveChanges();
                            }
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
        private string SqlCmdWhereCampaign(int SearchCriteria, int Condition, string Value)
        {
            string sqlWhere = "";
            if (SearchCriteria == 1)
            {
                sqlWhere = sqlWhere + "Name ";
                if (Condition == 1)
                {
                    return sqlWhere + "Like '%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "Not" + sqlWhere + "Like '%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 2)
            {
                sqlWhere = sqlWhere + "StartDate ";
            }
            if (SearchCriteria == 3)
            {
                sqlWhere = sqlWhere + "EndDate ";
            }
            if (Condition == 1)
            {
                return sqlWhere + ">= '" + DateTime.ParseExact(Value, "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + "' ";
            }
            if (Condition == 2)
            {
                return sqlWhere + "<= '" + DateTime.ParseExact(Value, "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + "' ";
            }
            if (Condition == 3)
            {
                return sqlWhere + "= '" + DateTime.ParseExact(Value, "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + "' ";
            }
            return sqlWhere;
        }
        public IEnumerable<CampaignDTO_ResponseSearch> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> conditionSearches)
        {
            string sqlWhere = "";
            for (int i = 0; i < conditionSearches.Count; i++)
            {
                if (i > 0)
                {
                    if(filterMethod == 1)
                    {
                        sqlWhere = sqlWhere + "AND ";
                    }
                    if(filterMethod == 2)
                    {
                        sqlWhere = sqlWhere + "OR ";
                    }
                }

                CampaignDTO_Request_ConditionSearch temp = conditionSearches[i];
                sqlWhere = sqlWhere + SqlCmdWhereCampaign(temp.SearchCriteria, temp.Condition, temp.Value);
            }

            if(sqlWhere.Length == 0)
            {
                return new List<CampaignDTO_ResponseSearch>();
            }
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Campaign, CampaignDTO_ResponseSearch>();
            });
            var mapper = config.CreateMapper();
            
            var list = _context.Campaigns.FromSqlRaw("Select * from dbo.Campaigns Where " + sqlWhere).Select(emp => mapper.Map<Campaign, CampaignDTO_ResponseSearch>(emp)).ToList();
            for(int i = 0; i < list.Count; i++)
            {
                list[i].ActivatedCode = _context.CodeCampaigns.Where(p => p.CampaignId == list[i].CampaignId && p.ActivatedDate.HasValue).ToList().Count;
                list[i].GiftQuantity = _context.CampaignGifts.Where(p => p.CampaignId == list[i].CampaignId).ToList().Count;
                list[i].Scanned = _context.CodeCampaigns.Where(p=>p.Scanned == true && p.CampaignId == list[i].CampaignId).ToList().Count;
                list[i].UsedForSpin = _context.CodeCampaigns.Where(p=>p.Spins.Count > 0 && p.CampaignId == list[i].CampaignId).ToList().Count;
                list[i].Win = _context.Winners.Where(p=>p.CodeGiftCampaign.CampaignGift.CampaignId == list[i].CampaignId).ToList().Count;
            }
            return list;
        }

        public CampaignDashBoardDTO_ResponseDetailCampaign GetCampaignDashBoardDTO_ResponseDetailCampaign(int id)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Campaign, CampaignDashBoardDTO_ResponseDetailCampaign>();
            });
            var mapper = config.CreateMapper();

            var temp = _context.Campaigns.FirstOrDefault(p => p.CampaignId == id);
            if (temp == null)
            {
                return new CampaignDashBoardDTO_ResponseDetailCampaign();
            }
            var response = mapper.Map<Campaign, CampaignDashBoardDTO_ResponseDetailCampaign>(temp);
            response.CodeCount = _context.CodeCampaigns.Where(p => p.CampaignId == id).ToList().Count;
            return response;
        }
        public List<CampaignDashBoardDTO_ResponseUsageSummary> GetCampaignDashBoardDTO_ResponseUsageSummary(int id, int optionFilterId)
        {
            List<CampaignDashBoardDTO_ResponseUsageSummary> list = new List<CampaignDashBoardDTO_ResponseUsageSummary>();
            var query = _context.CodeCampaigns
                    .Where(p => p.CampaignId == id && p.Scanned == true)
                    .GroupBy(p => p.ScannedDate!.Value.Date)
                    .Select(g => new { ScannedDate = g.Key, Total = g.Select(p => p.CodeCampaignId).Count() }).ToList();

            if (optionFilterId == 1)
            {
                var kq = query.FirstOrDefault(p => p.ScannedDate == DateTime.Now.Date);
                if(kq == null)
                {
                    list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(DateTime.Now.Date, 0));
                }
                else
                {
                    list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(kq.ScannedDate, kq.Total));
                }
                return list;
            }
            if (optionFilterId == 2)
            {
                var first = DateTime.Now.FirstDayOfWeek();
                var last = DateTime.Now.LastDayOfWeek();

                query = query.Where(p => p.ScannedDate >= first).Where(p => p.ScannedDate <= last).ToList();
                if (query.FirstOrDefault(p => p.ScannedDate == first) == null)
                {
                    list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(first, 0));
                }
                else
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(query[i].ScannedDate, query[i].Total));
                    }
                    return list;
                    //query.ForEach(p => list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(p.ScannedDate, p.Total)));
                }
                if (query.FirstOrDefault(p => p.ScannedDate == last) == null)
                {
                    list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(last, 0));
                }
                return list;
            }
            if (optionFilterId == 3)
            {
                var first = DateTime.Now.FirstDayOfMonth();
                var last = DateTime.Now.LastDayOfMonth();

                query = query.Where(p => p.ScannedDate >= first).Where(p => p.ScannedDate <= last).ToList();
                if (query.FirstOrDefault(p => p.ScannedDate == first) == null)
                {
                    list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(first, 0));
                }
                else
                {
                    for (int i = 0; i < query.Count(); i++)
                    {
                        list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(query[i].ScannedDate, query[i].Total));
                    }
                    return list;
                    //query.ForEach(p => list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(p.ScannedDate, p.Total)));
                }
                if (query.FirstOrDefault(p => p.ScannedDate == last) == null)
                {
                    list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(last, 0));
                }
                return list;
            }
            if (optionFilterId == 4)
            {
                for (int i = 0; i < query.Count(); i++)
                {
                    list.Add(new CampaignDashBoardDTO_ResponseUsageSummary(query[i].ScannedDate, query[i].Total));
                }
                return list;
            }
            return list;
        }
    }
}
