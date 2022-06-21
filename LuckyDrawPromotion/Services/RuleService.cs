using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;

namespace LuckyDrawPromotion.Services
{
    public interface IRuleService
    {
        IEnumerable<Rule> GetAll();
        Rule GetById(int id);
        void Save(Rule temp);
        void Remove(Rule temp);
        bool IsExistsCampaignId(int campaignId);
        bool IsExistsRuleId(int campaignId);
        List<RuleForGiftDTO> GetRuleForGifts(int campaignId);
        string UpOrDownPriority(int RuleId, int numberUpOrDown);
        List<CampaignGiftDTO_ResponseEditForRule> GetCampaignGifts(int campaignId);
        string EditUpdateRule(RuleForGiftDTO rule);
        bool ActivatedOrDeactivatedRule(int RuleId);
        string AddRuleForCampaign(int campaignId, List<CampaignGiftDTO_RequestRuleCampaign> listCampaignGifts);
        string DeletedRule(int RuleId);
    }
    public class RuleService : IRuleService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public RuleService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public IEnumerable<Rule> GetAll()
        {
            return _context.Rules.ToList();
        }

        public Rule GetById(int id)
        {
            return _context.Rules.ToList().FirstOrDefault(x => x.RuleId == id)!;
        }

        public void Save(Rule temp)
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

        public void Remove(Rule temp)
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
        public bool IsExistsRuleId(int RuleId)
        {
            return _context.Rules.ToList().Exists(p => p.RuleId == RuleId);
        }
        public bool IsExistsCampaignId(int campaignId)
        {
            return _context.Campaigns.ToList().Exists(p => p.CampaignId == campaignId);
        }
        
        public List<RuleForGiftDTO> GetRuleForGifts(int campaignId)
        {
            List<RuleForGiftDTO> rules = new List<RuleForGiftDTO>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Rule, RuleForGiftDTO>();
            });
            var mapper = config.CreateMapper();
            var list = (from a in _context.Rules
                       from b in _context.CampaignGifts
                       where a.CampaignGiftId == b.CampaignGiftId
                       where b.CampaignId == campaignId
                       select a).ToList();
            for(int i = 0; i < list.Count; i++)
            {
                RuleForGiftDTO temp = mapper.Map<Rule, RuleForGiftDTO>(list[i]);
                var repeatName = _context.RepeatSchedules.FirstOrDefault(p => p.RepeatScheduleId == temp.RepeatScheduleId);
                temp.RepeatScheduleName = repeatName!= null ? repeatName.Name : null;
                temp.GiftName = _context.Gifts.First(p => p.GiftId == _context.CampaignGifts.First(z=>z.CampaignGiftId == list[i].CampaignGiftId).GiftId).Name;
                rules.Add(temp);
            }
            return rules.OrderBy(p=>p.Priority).ToList();
        }
        public List<CampaignGiftDTO_ResponseEditForRule> GetCampaignGifts(int campaignId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CampaignGift, CampaignGiftDTO_ResponseEditForRule>();
            });
            var mapper = config.CreateMapper();
            return _context.CampaignGifts.Where(p=>p.CampaignId == campaignId).Select(e=>mapper.Map<CampaignGift, CampaignGiftDTO_ResponseEditForRule>(e)).ToList();
        }
        public string EditUpdateRule(RuleForGiftDTO rule)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RuleForGiftDTO, Rule>();
            });
            var mapper = config.CreateMapper();
            try
            {
                Rule temp = mapper.Map<RuleForGiftDTO, Rule>(rule);
                _context.Rules.Update(temp);
                _context.SaveChanges();
            }catch (Exception ex)
            {
                return ex.ToString();
            }
            return "true";
        }
        public string UpOrDownPriority(int RuleId, int numberUpOrDown)
        {
            try
            {
                int campaignId = (from a in _context.CampaignGifts
                                  from b in _context.Rules
                                  from c in _context.Campaigns
                                  where b.RuleId == RuleId
                                  where a.CampaignGiftId == b.CampaignGiftId
                                  where a.CampaignId == c.CampaignId
                                  select c).ToList().First().CampaignId;
                var list = (from a in _context.CampaignGifts
                            from b in _context.Rules
                            where a.CampaignId == campaignId
                            where a.CampaignGiftId == b.CampaignGiftId
                            select b).OrderBy(p=>p.Priority).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].RuleId == RuleId)
                    {
                        if (numberUpOrDown == 1)
                        {
                            int temp = list[i - 1].Priority;
                            list[i - 1].Priority = list[i].Priority;
                            list[i].Priority = temp;
                        }
                        if (numberUpOrDown == -1)
                        {
                            int temp = list[i + 1].Priority;
                            list[i + 1].Priority = list[i].Priority;
                            list[i].Priority = temp;
                        }
                    }
                }
                _context.Rules.UpdateRange(list);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "true";
        }
        public bool ActivatedOrDeactivatedRule(int RuleId)
        {
            Rule rule = _context.Rules.First(p=>p.RuleId == RuleId);
            if (rule.Active)
            {
                rule.Active = false;
            }
            else
            {
                rule.Active = true;
            }
            _context.Rules.Update(rule);
            _context.SaveChanges();
            return rule.Active;
        }
        public string AddRuleForCampaign(int campaignId, List<CampaignGiftDTO_RequestRuleCampaign> listCampaignGifts)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RuleDTO, Rule>();
            });
            var mapper = config.CreateMapper();
            try
            {
                for (int i = 0; i < listCampaignGifts.Count; i++)
                {
                    int giftId = listCampaignGifts[i].GiftId;
                    var campaignGift = _context.CampaignGifts.ToList().FirstOrDefault(p => p.CampaignId == campaignId && p.GiftId == giftId);
                    if (campaignGift == null)
                    {
                        campaignGift = new CampaignGift();
                        campaignGift.CampaignId = campaignId;
                        campaignGift.GiftId = giftId;
                        _context.CampaignGifts.Add(campaignGift);
                        _context.SaveChanges();
                    }
                    List<RuleDTO> listRules = listCampaignGifts[i].ListRules.ToList();
                    if (listRules.Count > 0)
                    {
                        for (int j = 0; j < listRules.Count; j++)
                        {
                            RuleDTO tempRule = listRules.ToList()[j];
                            Rule rule = mapper.Map<RuleDTO, Rule>(tempRule);
                            rule.CampaignGiftId = campaignGift.CampaignGiftId;
                            rule.Active = true;
                            _context.Rules.Add(rule);
                            _context.SaveChanges();
                            
                            rule.Priority = rule.RuleId;

                            _context.Rules.Update(rule);
                            _context.SaveChanges();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "true";
        }

        public string DeletedRule(int RuleId)
        {
            try
            {
                Rule rule = _context.Rules.First(p => p.RuleId == RuleId);
                _context.Rules.Remove(rule);
                _context.SaveChanges();
            }
            catch (Exception ex){ return ex.ToString(); }
            return "true";
        }
    }
}
