using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;

namespace LuckyDrawPromotion.Services
{
    public interface ISettingService
    {
        IEnumerable<Settings> GetAll();
        Settings GetById(int id);
        void Save(Settings temp);
        void Remove(Settings temp);

        bool IsExistCampaignId(int CampaignId);
        Settings GetSettingByCampaignId(int CampaignId);
        string UpdateSetting(Settings settings);
    }
    public class SettingService : ISettingService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public SettingService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public IEnumerable<Settings> GetAll()
        {
            return _context.Settings.ToList();
        }

        public Settings GetById(int id)
        {
            return _context.Settings.ToList().FirstOrDefault(x => x.SettingId == id)!;
        }

        public void Save(Settings temp)
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

        public void Remove(Settings temp)
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

        public bool IsExistCampaignId(int CampaignId)
        {
            return _context.Campaigns.ToList().Exists(p => p.CampaignId == CampaignId);
        }

        public Settings GetSettingByCampaignId(int CampaignId)
        {
            return _context.Settings.First(p=>p.CampaignId == CampaignId);
        }

        public string UpdateSetting(Settings settings)
        {
            try
            {
                _context.Settings.Update(settings);
                _context.SaveChanges();
            }catch (Exception ex)
            { return ex.ToString(); }
            return "true";
        }
    }
}
