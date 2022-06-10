using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;

namespace LuckyDrawPromotion.Services
{
    public interface IGiftService
    {
        IEnumerable<Gift> GetAll();
        Gift GetById(int id);
        void Save(Gift temp);
        void Remove(Gift temp);

        public IEnumerable<GiftDTO_Response> GetGifts();
    }
    public class GiftService: IGiftService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public GiftService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public IEnumerable<Gift> GetAll()
        {
            return _context.Gifts.ToList();
        }

        public Gift GetById(int id)
        {
            return _context.Gifts.ToList().FirstOrDefault(x => x.GiftId == id)!;
        }


        public void Save(Gift temp)
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

        public void Remove(Gift temp)
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

        public IEnumerable<GiftDTO_Response> GetGifts()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Gift, GiftDTO_Response>();
            });
            var mapper = config.CreateMapper();
            var list = GetAll().Select
                        (
                          emp => mapper.Map<Gift, GiftDTO_Response>(emp)
                        ).ToList();
            return list;
        }
    }
}
