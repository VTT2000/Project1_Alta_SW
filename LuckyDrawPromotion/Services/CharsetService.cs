using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;

namespace LuckyDrawPromotion.Services
{
    public interface ICharsetService
    {
        IEnumerable<Charset> GetAll();
        Charset GetById(int id);
        object Save(Charset temp);
        object Remove(Charset temp);

        public IEnumerable<CharsetDTO_Response> GetCharsets();
    }
    public class CharsetService : ICharsetService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public CharsetService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public IEnumerable<Charset> GetAll()
        {
            return _context.Charsets.ToList();
        }

        public Charset GetById(int id)
        {
            return _context.Charsets.ToList().FirstOrDefault(x => x.CharsetId == id)!;
        }


        public object Save(Charset temp)
        {
            try
            {
                _context.Update(temp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "/" + ex.Source);
                return ex;
            }
            return temp;
        }

        public object Remove(Charset temp)
        {
            try
            {
                _context.Remove(temp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "/" + ex.Source);
                return ex;
            }
            return temp;
        }

        public IEnumerable<CharsetDTO_Response> GetCharsets()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Charset, CharsetDTO_Response>();
            });
            var mapper = config.CreateMapper();
            var list = GetAll().Select
                        (
                          emp => mapper.Map<Charset, CharsetDTO_Response>(emp)
                        ).ToList();
            return list;
        }
    }
}
