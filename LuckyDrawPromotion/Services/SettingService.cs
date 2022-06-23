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
        Settings_Response GetSettingByCampaignId(int CampaignId);
        string UpdateSetting(Settings_Response settings);
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

        public Settings_Response GetSettingByCampaignId(int CampaignId)
        {
            var temp = _context.Settings.First(p=>p.CampaignId == CampaignId);
            return new Settings_Response()
            {
                SettingId = temp.SettingId,
                CampaignId = temp.CampaignId,
                QRcodeURL = temp.QRcodeURL,
                LandingPage = temp.LandingPage,
                SMStext = temp.SMStext,
                SendReportAuto = temp.SendReportAuto,
                TimeSend = temp.TimeSend.HasValue ? temp.TimeSend.Value.ToString("HH:mm:ss") : null,
                SendToEmail = temp.SendToEmail
            };
        }

        public string UpdateSetting(Settings_Response settings)
        {
            try
            {
                var temp = _context.Settings.First(p=>p.SettingId == settings.SettingId);
                temp.CampaignId = settings.CampaignId;
                temp.QRcodeURL = settings.QRcodeURL;
                temp.LandingPage = settings.LandingPage;
                temp.SMStext = settings.SMStext;
                temp.SendReportAuto = settings.SendReportAuto;
                temp.TimeSend = String.IsNullOrEmpty(settings.TimeSend) ? null : TimeSpan.ParseExact(settings.TimeSend, "HH:mm:ss", null);
                temp.SendToEmail = settings.SendToEmail;

                _context.Settings.Update(temp);
                _context.SaveChanges();
            }catch (Exception ex)
            { return ex.ToString(); }
            return "true";
        }
    }
}
