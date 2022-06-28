using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace LuckyDrawPromotion.Services
{
    public interface IGiftService
    {
        IEnumerable<Gift> GetAll();
        Gift GetById(int id);
        void Save(Gift temp);
        void Remove(Gift temp);

        IEnumerable<GiftDTO_Response> GetGifts();

        List<GiftDTO_ResponseGiftCode> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches);
        MemoryStream ExportToExcel(List<GiftDTO_ResponseGiftCode> list);

        bool IsExistsCodeGiftCampaignId(int id);
        string EditUpdateGeneratedGift(GiftDTO_ResponseGiftCode temp);
        bool ActivatedOrDeactivatedGeneratedGift(int id);
        string DeletedGeneratedGift(int id);
        List<GiftDTO_ResponseGift> GetAllForSort1(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches);
        MemoryStream ExportToExcel1(List<GiftDTO_ResponseGift> list);
        bool IsExistsGiftId(int id);
        bool ActivatedOrDeactivatedGift(int id);
        bool IsExistsGiftByName(string name);
        string CreateGift(GiftDTO_RequestGiftCreate temp);
        string ImportDataGiftByExcel(IFormFile formFile);
        string EditUpdateGift(GiftDTO_ResponseGift temp);
        string DeletedGift(int id);
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

        public List<GiftDTO_ResponseGiftCode> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            if (filterMethod == 1)
            {
                var list = (from a in _context.CodeGiftCampaigns
                            from b in _context.CampaignGifts
                            from c in _context.Campaigns
                            where a.CampaignGiftId == b.CampaignGiftId
                            where b.CampaignId == c.CampaignId
                            select new GiftDTO_ResponseGiftCode
                            {
                                CodeGiftCampaignId = a.CodeGiftCampaignId,
                                Code = a.Code,
                                Campaign = a.CampaignGift.Campaign!.Name,
                                CreatedDate = a.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                ExpiredDate = a.CampaignGift.Campaign!.EndDate.HasValue ?
                                             (a.CampaignGift.Campaign!.EndTime.HasValue ?
                                             a.CampaignGift.Campaign!.EndDate!.Value.ToString("dd/MM/yyyy") + " " +
                                             a.CampaignGift.Campaign!.EndTime!.Value.ToString() :
                                             a.CampaignGift.Campaign!.EndDate!.Value.ToString("dd/MM/yyyy")
                                             ) : null,
                                Usage = _context.Winners.Where(p=>p.CodeGiftCampaignId == a.CodeGiftCampaignId).ToList().Count + "/" + a.CampaignGift.Campaign!.CodeUsageLimit,
                                Active = a.IsActive,
                                CampaignGiftId = a.CampaignGiftId,
                            }).ToList();
                for (int i = 0; i < listConditionSearches.Count; i++)
                {
                    var dieukien = listConditionSearches[i];
                    if (dieukien.SearchCriteria == 1)
                    {
                        if(dieukien.Condition == 1)
                        {
                            list = list.Where(p => p.Code.Contains(dieukien.Value)).ToList();
                        }
                        if(dieukien.Condition == 2)
                        {
                            list = list.Where(p => !p.Code.Contains(dieukien.Value)).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 2)
                    {
                        if (dieukien.Condition == 1)
                        {
                            list = list.Where(p => p.Campaign!.Contains(dieukien.Value)).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list = list.Where(p => !p.Campaign!.Contains(dieukien.Value)).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 3)
                    {
                        DateTime date = DateTime.ParseExact(dieukien.Value, "dd/MM/yyyy", null);
                        if (dieukien.Condition == 1)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date >= date).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date <= date).ToList();
                        }
                        if (dieukien.Condition == 3)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date == date).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 4)
                    {
                        DateTime date = DateTime.ParseExact(dieukien.Value, "dd/MM/yyyy", null);
                        if (dieukien.Condition == 1)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.ExpiredDate!, "dd/MM/yyyy HH:mm:ss", null).Date >= date).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.ExpiredDate!, "dd/MM/yyyy HH:mm:ss", null).Date <= date).ToList();
                        }
                        if (dieukien.Condition == 3)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.ExpiredDate!, "dd/MM/yyyy HH:mm:ss", null).Date == date).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 5)
                    {
                        if (dieukien.Condition == 1)
                        {
                            if (dieukien.Value.Equals("Activate"))
                            {
                                list = list.Where(p=>p.Active == true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => !p.Active == true).ToList();
                            }
                        }
                        if (dieukien.Condition == 2)
                        {
                            if (dieukien.Value.Equals("Activate"))
                            {
                                list = list.Where(p => !p.Active == true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => p.Active == true).ToList();
                            }
                        }
                    }
                }
                return list;
            }
            if(filterMethod == 2)
            {
                var list = (from a in _context.CodeGiftCampaigns
                            from b in _context.CampaignGifts
                            from c in _context.Campaigns
                            where a.CampaignGiftId == b.CampaignGiftId
                            where b.CampaignId == c.CampaignId
                            select new GiftDTO_ResponseGiftCode
                            {
                                CodeGiftCampaignId = a.CodeGiftCampaignId,
                                Code = a.Code,
                                Campaign = a.CampaignGift.Campaign!.Name,
                                CreatedDate = a.CreatedDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                ExpiredDate = a.CampaignGift.Campaign!.EndDate.HasValue ?
                                             (a.CampaignGift.Campaign!.EndTime.HasValue ?
                                             a.CampaignGift.Campaign!.EndDate!.Value.ToString("dd/MM/yyyy") + " " +
                                             a.CampaignGift.Campaign!.EndTime!.Value.ToString() :
                                             a.CampaignGift.Campaign!.EndDate!.Value.ToString("dd/MM/yyyy")
                                             ) : null,
                                Usage = _context.Winners.Where(p=>p.CodeGiftCampaignId == a.CodeGiftCampaignId).ToList().Count + "/" + a.CampaignGift.Campaign!.CodeUsageLimit,
                                Active = a.IsActive,
                                CampaignGiftId = a.CampaignGiftId,
                            }).ToList();
                var kq = new List<GiftDTO_ResponseGiftCode>();
                for (int i = 0; i < listConditionSearches.Count; i++)
                {
                    var list0 = new List<GiftDTO_ResponseGiftCode>();
                    var dieukien = listConditionSearches[i];
                    if (dieukien.SearchCriteria == 1)
                    {
                        if (dieukien.Condition == 1)
                        {
                            list0 = list.Where(p => p.Code.Contains(dieukien.Value)).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list0 = list.Where(p => !p.Code.Contains(dieukien.Value)).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 2)
                    {
                        if (dieukien.Condition == 1)
                        {
                            list0 = list.Where(p => p.Campaign!.Contains(dieukien.Value)).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list0 = list.Where(p => !p.Campaign!.Contains(dieukien.Value)).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 3)
                    {
                        DateTime date = DateTime.ParseExact(dieukien.Value, "dd/MM/yyyy", null);
                        if (dieukien.Condition == 1)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date >= date).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date <= date).ToList();
                        }
                        if (dieukien.Condition == 3)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date == date).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 4)
                    {
                        DateTime date = DateTime.ParseExact(dieukien.Value, "dd/MM/yyyy", null);
                        if (dieukien.Condition == 1)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.ExpiredDate!, "dd/MM/yyyy HH:mm:ss", null).Date >= date).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.ExpiredDate!, "dd/MM/yyyy HH:mm:ss", null).Date <= date).ToList();
                        }
                        if (dieukien.Condition == 3)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.ExpiredDate!, "dd/MM/yyyy HH:mm:ss", null).Date == date).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 5)
                    {
                        if (dieukien.Condition == 1)
                        {
                            if (dieukien.Value.Equals("Activate"))
                            {
                                list0 = list.Where(p => p.Active == true).ToList();
                            }
                            else
                            {
                                list0 = list.Where(p => !p.Active == true).ToList();
                            }
                        }
                        if (dieukien.Condition == 2)
                        {
                            if (dieukien.Value.Equals("Activate"))
                            {
                                list = list.Where(p => !p.Active == true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => p.Active == true).ToList();
                            }
                        }
                    }
                    kq = kq.Union(list0).ToList();
                }
                return kq;
            }
            return new List<GiftDTO_ResponseGiftCode>();
        }

        public MemoryStream ExportToExcel(List<GiftDTO_ResponseGiftCode> list)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // for ten thuoc tinh
                for (int q = 0; q < list[0].GetType().GetProperties().Count(); q++)
                {
                    worksheet.Cells[1, q + 1].Value = list[0].GetType().GetProperties()[q].Name;
                    worksheet.Cells[1, q + 1].AutoFitColumns();
                }

                for (int i = 0; i < list.Count; i++)
                {
                    worksheet.Row(i + 2).Height = 90;
                    int j = 0;
                    worksheet.Cells[i + 2, ++j].Value = list[i].CodeGiftCampaignId;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Code;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Campaign;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].CreatedDate;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].ExpiredDate;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Usage;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Active;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].CampaignGiftId;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                }
                package.Save();
            }
            return stream;
        }

        public bool IsExistsCodeGiftCampaignId(int id)
        {
            return _context.CodeGiftCampaigns.ToList().Exists(p => p.CodeGiftCampaignId == id);
        }

        public string EditUpdateGeneratedGift(GiftDTO_ResponseGiftCode temp)
        {
            try
            {

                CodeGiftCampaign temp0 = _context.CodeGiftCampaigns.First(p => p.CodeGiftCampaignId == temp.CodeGiftCampaignId);
                temp0.Code = temp.Code;
                if (temp.Active)
                {
                    temp0.IsActive = true;
                    temp0.ActivatedDate = DateTime.Now;
                }
                else
                {
                    temp0.IsActive = false;
                }
                temp0.CampaignGiftId = temp.CampaignGiftId;

                _context.CodeGiftCampaigns.Update(temp0);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "true";
        }
        
        public bool ActivatedOrDeactivatedGeneratedGift(int Id)
        {
            CodeGiftCampaign temp = _context.CodeGiftCampaigns.First(p => p.CodeGiftCampaignId == Id);
            if (temp.IsActive)
            {
                temp.IsActive = false;
            }
            else
            {
                temp.IsActive = true;
                temp.ActivatedDate = DateTime.Now;
            }
            _context.CodeGiftCampaigns.Update(temp);
            _context.SaveChanges();
            return temp.IsActive;
        }
        

        public string DeletedGeneratedGift(int Id)
        {
            try
            {
                CodeGiftCampaign temp = _context.CodeGiftCampaigns.First(p => p.CodeGiftCampaignId == Id);
                _context.CodeGiftCampaigns.Remove(temp);
                _context.SaveChanges();
            }
            catch (Exception ex) { return ex.ToString(); }
            return "true";
        }

        public List<GiftDTO_ResponseGift> GetAllForSort1(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Gift, GiftDTO_ResponseGift>();
            });
            var mapper = config.CreateMapper();
            if (filterMethod == 1)
            {
                var list = _context.Gifts.ToList().Select(p=>mapper.Map<Gift, GiftDTO_ResponseGift>(p)).ToList();
                for (int i = 0; i < listConditionSearches.Count; i++)
                {
                    var dieukien = listConditionSearches[i];
                    if (dieukien.SearchCriteria == 1)
                    {
                        if (dieukien.Condition == 1)
                        {
                            list = list.Where(p => p.Name.Contains(dieukien.Value)).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list = list.Where(p => !p.Name.Contains(dieukien.Value)).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 2)
                    {
                        DateTime date = DateTime.ParseExact(dieukien.Value, "dd/MM/yyyy", null);
                        if (dieukien.Condition == 1)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date >= date).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date <= date).ToList();
                        }
                        if (dieukien.Condition == 3)
                        {
                            list = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date == date).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 3)
                    {
                        if (dieukien.Condition == 1)
                        {
                            if (dieukien.Value.Equals("Activate"))
                            {
                                list = list.Where(p => p.Active == true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => !p.Active == true).ToList();
                            }
                        }
                        if (dieukien.Condition == 2)
                        {
                            if (dieukien.Value.Equals("Activate"))
                            {
                                list = list.Where(p => !p.Active == true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => p.Active == true).ToList();
                            }
                        }
                    }
                }
                return list;
            }
            if (filterMethod == 2)
            {
                var list = _context.Gifts.ToList().Select(p=>mapper.Map<Gift, GiftDTO_ResponseGift>(p)).ToList();
                var kq = new List<GiftDTO_ResponseGift>();
                for (int i = 0; i < listConditionSearches.Count; i++)
                {
                    var list0 = new List<GiftDTO_ResponseGift>();
                    var dieukien = listConditionSearches[i];
                    if (dieukien.SearchCriteria == 1)
                    {
                        if (dieukien.Condition == 1)
                        {
                            list0 = list.Where(p => p.Name.Contains(dieukien.Value)).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list0 = list.Where(p => !p.Name.Contains(dieukien.Value)).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 2)
                    {
                        DateTime date = DateTime.ParseExact(dieukien.Value, "dd/MM/yyyy", null);
                        if (dieukien.Condition == 1)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date >= date).ToList();
                        }
                        if (dieukien.Condition == 2)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date <= date).ToList();
                        }
                        if (dieukien.Condition == 3)
                        {
                            list0 = list.Where(p => DateTime.ParseExact(p.CreatedDate!, "dd/MM/yyyy HH:mm:ss", null).Date == date).ToList();
                        }
                    }
                    if (dieukien.SearchCriteria == 3)
                    {
                        if (dieukien.Condition == 1)
                        {
                            if (dieukien.Value.Equals("Activate"))
                            {
                                list0 = list.Where(p => p.Active == true).ToList();
                            }
                            else
                            {
                                list0 = list.Where(p => !p.Active == true).ToList();
                            }
                        }
                        if (dieukien.Condition == 2)
                        {
                            if (dieukien.Value.Equals("Activate"))
                            {
                                list = list.Where(p => !p.Active == true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => p.Active == true).ToList();
                            }
                        }
                    }
                    kq = kq.Union(list0).ToList();
                }
                return kq;
            }
            return new List<GiftDTO_ResponseGift>();
        }

        public MemoryStream ExportToExcel1(List<GiftDTO_ResponseGift> list)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // for ten thuoc tinh
                for (int q = 0; q < list[0].GetType().GetProperties().Count(); q++)
                {
                    worksheet.Cells[1, q + 1].Value = list[0].GetType().GetProperties()[q].Name;
                    worksheet.Cells[1, q + 1].AutoFitColumns();
                }

                for (int i = 0; i < list.Count; i++)
                {
                    worksheet.Row(i + 2).Height = 90;
                    int j = 0;
                    worksheet.Cells[i + 2, ++j].Value = list[i].GiftId;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Name;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Description;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].CreatedDate;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Active;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                }
                package.Save();
            }
            return stream;
        }
        public bool IsExistsGiftId(int id)
        {
            return _context.Gifts.ToList().Exists(p => p.GiftId == id);
        }

        public bool ActivatedOrDeactivatedGift(int Id)
        {
            Gift temp = _context.Gifts.First(p => p.GiftId == Id);
            if (temp.Active)
            {
                temp.Active = false;
            }
            else
            {
                temp.Active = true;
            }
            _context.Gifts.Update(temp);
            _context.SaveChanges();
            return temp.Active;
        }

        public bool IsExistsGiftByName(string name)
        {
            return _context.Gifts.ToList().Exists(p => p.Name.Equals(name));
        }
        public string CreateGift(GiftDTO_RequestGiftCreate temp)
        {
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<GiftDTO_RequestGiftCreate, Gift>();
                });
                var mapper = config.CreateMapper();
                Gift x = mapper.Map<GiftDTO_RequestGiftCreate, Gift>(temp);

                _context.Gifts.Add(x);
                _context.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public string ImportDataGiftByExcel(IFormFile formFile)
        {
            try
            {
                var list = new List<Gift>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var stream = new MemoryStream())
                {
                    formFile.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            list.Add(new Gift
                            {
                                Name = worksheet.Cells[row, 1].Value.ToString()!.Trim(),
                                Description = worksheet.Cells[row, 2].Value.ToString()!.Trim(),
                                Active = Boolean.Parse(worksheet.Cells[row, 3].Value.ToString()!.Trim()),
                            });
                        }
                    }
                }
                _context.Gifts.AddRange(list);
                _context.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        public string EditUpdateGift(GiftDTO_ResponseGift temp)
        {
            try
            {
                Gift temp0 = _context.Gifts.First(p => p.GiftId == temp.GiftId);
                temp0.Name = temp.Name;
                temp0.Description = temp.Description;

                _context.Gifts.Update(temp0);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "true";
        }


        public string DeletedGift(int Id)
        {
            try
            {
                Gift temp = _context.Gifts.First(p => p.GiftId == Id);
                _context.Gifts.Remove(temp);
                _context.SaveChanges();
            }
            catch (Exception ex) { return ex.ToString(); }
            return "true";
        }
    }
}
