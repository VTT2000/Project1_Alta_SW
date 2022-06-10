using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;

namespace LuckyDrawPromotion.Services
{
    public interface ISizeProgramService
    {
        IEnumerable<SizeProgram> GetAll();
        SizeProgram GetById(int id);
        void Save(SizeProgram temp);
        void Remove(SizeProgram temp);

        IEnumerable<SizeProgramDTO_Response> GetSizePrograms();
    }
    public class SizeProgramService : ISizeProgramService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public SizeProgramService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public IEnumerable<SizeProgram> GetAll()
        {
            return _context.SizePrograms.ToList();
        }

        public SizeProgram GetById(int id)
        {
            return _context.SizePrograms.ToList().FirstOrDefault(x => x.SizeProgramId == id)!;
        }


        public void Save(SizeProgram temp)
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

        public void Remove(SizeProgram temp)
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

        public IEnumerable<SizeProgramDTO_Response> GetSizePrograms()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SizeProgram, SizeProgramDTO_Response>();
            });
            var mapper = config.CreateMapper();
            var list = GetAll().Select
                        (
                          emp => mapper.Map<SizeProgram, SizeProgramDTO_Response>(emp)
                        ).ToList();
            return list;
        }
    }
}
