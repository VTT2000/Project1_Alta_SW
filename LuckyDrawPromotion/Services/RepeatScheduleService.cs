using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;

namespace LuckyDrawPromotion.Services
{
    public interface IRepeatScheduleService
    {
        IEnumerable<RepeatSchedule> GetAll();
        RepeatSchedule GetById(int id);
        void Save(RepeatSchedule temp);
        void Remove(RepeatSchedule temp);

        public IEnumerable<RepeatScheduleDTO_Response> GetRepeatSchedules();
    }
    public class RepeatScheduleService : IRepeatScheduleService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public RepeatScheduleService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public IEnumerable<RepeatSchedule> GetAll()
        {
            return _context.RepeatSchedules.ToList();
        }

        public RepeatSchedule GetById(int id)
        {
            return _context.RepeatSchedules.ToList().FirstOrDefault(x => x.RepeatScheduleId == id)!;
        }


        public void Save(RepeatSchedule temp)
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

        public void Remove(RepeatSchedule temp)
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

        public IEnumerable<RepeatScheduleDTO_Response> GetRepeatSchedules()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RepeatSchedule, RepeatScheduleDTO_Response>();
            });
            var mapper = config.CreateMapper();
            var list = GetAll().Select
                        (
                          emp => mapper.Map<RepeatSchedule, RepeatScheduleDTO_Response>(emp)
                        ).ToList();
            return list;
        }
    }
}
