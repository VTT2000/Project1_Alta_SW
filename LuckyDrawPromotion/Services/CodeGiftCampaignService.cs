using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace LuckyDrawPromotion.Services
{
    public interface ICodeGiftCampaignService
    {
        IEnumerable<CodeGiftCampaign> GetAll();
        CodeGiftCampaign GetById(int id);
        void Save(CodeGiftCampaign temp);
        void Remove(CodeGiftCampaign temp);

        IEnumerable<CodeGiftCampaignDTO_ResponseFilter> GetAllForSort(int campaignId, int filterMethod, List<CampaignDTO_Request_ConditionSearch> conditionSearches);
        double GetCodeCount(int campaignId);
        double GetCodeGiftCount(int campaignId);
        bool ExistCampaignId(int CampaignId);
        IEnumerable<CodeGiftCampaignDTO> GetCreateTempGiftCode(int CampaignId, int GiftId, int GiftCodeCount);
        string generatedGiftCodeCampaign(int CampaignId, List<CampaignGiftDTO_Request0> ListCampaignGifts);
        MemoryStream ExportToExcel(List<CodeGiftCampaignDTO_ResponseFilter> list);
    }
    public class CodeGiftCampaignService : ICodeGiftCampaignService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public CodeGiftCampaignService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public IEnumerable<CodeGiftCampaign> GetAll()
        {
            return _context.CodeGiftCampaigns.ToList();
        }

        public CodeGiftCampaign GetById(int id)
        {
            return _context.CodeGiftCampaigns.ToList().FirstOrDefault(x => x.CodeGiftCampaignId== id)!;
        }


        public void Save(CodeGiftCampaign temp)
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

        public void Remove(CodeGiftCampaign temp)
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

        public IEnumerable<CodeGiftCampaignDTO_ResponseFilter> GetAllForSort(int campaignId, int filterMethod, List<CampaignDTO_Request_ConditionSearch> conditionSearches)
        {
            List<CodeGiftCampaign> listTemp = new List<CodeGiftCampaign>();
            if (filterMethod == 1)
            {
                var list = (from codegiftcampaign in _context.CodeGiftCampaigns
                            from giftcampaign in _context.CampaignGifts
                            where codegiftcampaign.CampaignGiftId == giftcampaign.CampaignGiftId
                            where giftcampaign.CampaignId == campaignId
                            select codegiftcampaign).ToList();
                for (int i = 0; i < conditionSearches.Count; i++)
                {
                    if (conditionSearches[i].SearchCriteria == 1)
                    {
                        if (conditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.Code.Contains(conditionSearches[i].Value)).ToList();
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !p.Code.Contains(conditionSearches[i].Value)).ToList();
                        }
                    }
                    if (conditionSearches[i].SearchCriteria == 2)
                    {
                        // gift name
                        string giftName = conditionSearches[i].Value;
                        if (conditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => _context.Gifts.First(z => z.GiftId == _context.CampaignGifts.First(q => q.CampaignGiftId == p.CampaignGiftId).GiftId).Name.Contains(giftName)).ToList();
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !_context.Gifts.First(z => z.GiftId == _context.CampaignGifts.First(q => q.CampaignGiftId == p.CampaignGiftId).GiftId).Name.Contains(giftName)).ToList();
                        }
                    }
                    if (conditionSearches[i].SearchCriteria == 3)
                    {
                        // created date
                        DateTime ngaySS = DateTime.ParseExact(conditionSearches[i].Value, "dd/MM/yyyy", null);
                        if (conditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.CreatedDate.Date >= ngaySS).ToList();
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => p.CreatedDate.Date <= ngaySS).ToList();
                        }
                        if (conditionSearches[i].Condition == 3)
                        {
                            list = list.Where(p => p.CreatedDate.Date == ngaySS).ToList();
                        }
                    }
                    if (conditionSearches[i].SearchCriteria == 4)
                    {
                        // Code usage limit
                        int codeUsageLimit = Int32.Parse(conditionSearches[i].Value);

                        if (conditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => _context.Campaigns.First(q => _context.CampaignGifts.First(z => z.CampaignGiftId == p.CampaignGiftId).CampaignId == q.CampaignId).CodeUsageLimit >= codeUsageLimit).ToList();
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => _context.Campaigns.First(q => _context.CampaignGifts.First(z => z.CampaignGiftId == p.CampaignGiftId).CampaignId == q.CampaignId).CodeUsageLimit <= codeUsageLimit).ToList();
                        }
                        if (conditionSearches[i].Condition == 3)
                        {
                            list = list.Where(p => _context.Campaigns.First(q => _context.CampaignGifts.First(z => z.CampaignGiftId == p.CampaignGiftId).CampaignId == q.CampaignId).CodeUsageLimit == codeUsageLimit).ToList();
                        }
                    }
                    if (conditionSearches[i].SearchCriteria == 5)
                    {
                        // activation status
                        bool value = conditionSearches[i].Value.Equals("Activate") ? true : false;
                        if (conditionSearches[i].Condition == 1)
                        {
                            if (value)
                            {
                                list = list.Where(p => p.ActivatedDate.HasValue).ToList();
                            }
                            else
                            {
                                list = list.Where(p => !p.ActivatedDate.HasValue).ToList();
                            }
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            if (!value)
                            {
                                list = list.Where(p => p.ActivatedDate.HasValue).ToList();
                            }
                            else
                            {
                                list = list.Where(p => !p.ActivatedDate.HasValue).ToList();
                            }
                        }
                    }
                }
                listTemp = list;
            }
            if (filterMethod == 2)
            {
                for (int i = 0; i < conditionSearches.Count; i++)
                {
                    var list = (from codegiftcampaign in _context.CodeGiftCampaigns
                                from giftcampaign in _context.CampaignGifts
                                where codegiftcampaign.CampaignGiftId == giftcampaign.CampaignGiftId
                                where giftcampaign.CampaignId == campaignId
                                select codegiftcampaign).ToList();
                    if (conditionSearches[i].SearchCriteria == 1)
                    {
                        if (conditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.Code.Contains(conditionSearches[i].Value)).ToList();
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !p.Code.Contains(conditionSearches[i].Value)).ToList();
                        }
                    }
                    if (conditionSearches[i].SearchCriteria == 2)
                    {
                        // gift name
                        string giftName = conditionSearches[i].Value;
                        if (conditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => _context.Gifts.First(z => z.GiftId == _context.CampaignGifts.First(q => q.CampaignGiftId == p.CampaignGiftId).GiftId).Name.Contains(giftName)).ToList();
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !_context.Gifts.First(z => z.GiftId == _context.CampaignGifts.First(q => q.CampaignGiftId == p.CampaignGiftId).GiftId).Name.Contains(giftName)).ToList();
                        }
                    }
                    if (conditionSearches[i].SearchCriteria == 3)
                    {
                        // created date
                        DateTime ngaySS = DateTime.ParseExact(conditionSearches[i].Value, "dd/MM/yyyy", null);
                        if (conditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.CreatedDate.Date >= ngaySS).ToList();
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => p.CreatedDate.Date <= ngaySS).ToList();
                        }
                        if (conditionSearches[i].Condition == 3)
                        {
                            list = list.Where(p => p.CreatedDate.Date == ngaySS).ToList();
                        }
                    }
                    if (conditionSearches[i].SearchCriteria == 4)
                    {
                        // Code usage limit
                        int codeUsageLimit = Int32.Parse(conditionSearches[i].Value);

                        if (conditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => _context.Campaigns.First(q => _context.CampaignGifts.First(z => z.CampaignGiftId == p.CampaignGiftId).CampaignId == q.CampaignId).CodeUsageLimit >= codeUsageLimit).ToList();
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => _context.Campaigns.First(q => _context.CampaignGifts.First(z => z.CampaignGiftId == p.CampaignGiftId).CampaignId == q.CampaignId).CodeUsageLimit <= codeUsageLimit).ToList();
                        }
                        if (conditionSearches[i].Condition == 3)
                        {
                            list = list.Where(p => _context.Campaigns.First(q => _context.CampaignGifts.First(z => z.CampaignGiftId == p.CampaignGiftId).CampaignId == q.CampaignId).CodeUsageLimit == codeUsageLimit).ToList();
                        }
                    }
                    if (conditionSearches[i].SearchCriteria == 5)
                    {
                        // activation status
                        bool value = conditionSearches[i].Value.Equals("Activate") ? true : false;
                        if (conditionSearches[i].Condition == 1)
                        {
                            if (value)
                            {
                                list = list.Where(p => p.ActivatedDate.HasValue).ToList();
                            }
                            else
                            {
                                list = list.Where(p => !p.ActivatedDate.HasValue).ToList();
                            }
                        }
                        if (conditionSearches[i].Condition == 2)
                        {
                            if (!value)
                            {
                                list = list.Where(p => p.ActivatedDate.HasValue).ToList();
                            }
                            else
                            {
                                list = list.Where(p => !p.ActivatedDate.HasValue).ToList();
                            }
                        }
                    }
                    listTemp.Union(list);
                }
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CodeGiftCampaign, CodeGiftCampaignDTO_ResponseFilter>();
                cfg.CreateMap<DateTime, string>().ConvertUsing(p => p.ToString("dd/MM/yyyy HH:mm:ss"));
            });
            var mapper = config.CreateMapper();

            List<CodeGiftCampaignDTO_ResponseFilter> kq = new List<CodeGiftCampaignDTO_ResponseFilter>();
            for (int i = 0; i < listTemp.Count; i++)
            {
                CodeGiftCampaignDTO_ResponseFilter temp = mapper.Map<CodeGiftCampaign, CodeGiftCampaignDTO_ResponseFilter>(listTemp[i]);
                temp.GiftName = _context.Gifts.First(z => z.GiftId == _context.CampaignGifts.First(q => q.CampaignGiftId == listTemp[i].CampaignGiftId).GiftId).Name;
                temp.CodeUsageLimit = _context.Campaigns.First(q => _context.CampaignGifts.First(z => z.CampaignGiftId == listTemp[i].CampaignGiftId).CampaignId == q.CampaignId).CodeUsageLimit;
                temp.Active = listTemp[i].ActivatedDate.HasValue;
                temp.Used = _context.Winners.ToList().Exists(p => p.CodeGiftCampaignId == listTemp[i].CodeGiftCampaignId);
                kq.Add(temp);
            }
            return kq;
        }
        public double GetCodeCount(int campaignId)
        {
            return _context.CodeCampaigns.Where(p => p.CampaignId == campaignId).Count();
        }
        public bool ExistCampaignId(int CampaignId)
        {
            return _context.Campaigns.ToList().Exists(p => p.CampaignId == CampaignId);
        }

        public double GetCodeGiftCount(int campaignId)
        {
            return (from codeGiftCampaign in _context.CodeGiftCampaigns
                    from giftCamapign in _context.CampaignGifts
                    where codeGiftCampaign.CampaignGiftId == giftCamapign.CampaignGiftId
                    where giftCamapign.CampaignId == campaignId
                    select codeGiftCampaign).Count();
        }
        public IEnumerable<CodeGiftCampaignDTO> GetCreateTempGiftCode(int CampaignId, int GiftId, int GiftCodeCount)
        {
            var list = (from codeGiftCampaign in _context.CodeGiftCampaigns
                        from giftCamapign in _context.CampaignGifts
                        where codeGiftCampaign.CampaignGiftId == giftCamapign.CampaignGiftId
                        where giftCamapign.CampaignId == CampaignId
                        where giftCamapign.GiftId == GiftId
                        select codeGiftCampaign).ToList();
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
                    code.CreatedDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    code.Active = true;
                    code.Code = "GIF" + GiftId + chuoi;
                }
                while (list.Exists(p => p.Code.Equals(code.Code)) || codes.Exists(p => p.Code.Equals(code.Code)));
                codes.Add(code);
            }
            return codes;
        }
        public string generatedGiftCodeCampaign(int CampaignId, List<CampaignGiftDTO_Request0> ListCampaignGifts)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CodeGiftCampaignDTO, CodeGiftCampaign>();
                cfg.CreateMap<string, DateTime>().ConvertUsing(p => DateTime.ParseExact(p, "dd/MM/yyyy HH:mm:ss", null));
            });
            var mapper = config.CreateMapper();
            try
            {
                for (int i = 0; i < ListCampaignGifts.Count; i++)
                {
                    CampaignGift campaigngift = new CampaignGift();
                    var dk = _context.CampaignGifts.FirstOrDefault(c => c.CampaignId == CampaignId && c.GiftId == ListCampaignGifts[i].GiftId);
                    if (dk != null)
                    {
                        campaigngift = dk;
                    }
                    else
                    {
                        campaigngift.CampaignId = CampaignId;
                        campaigngift.GiftId = ListCampaignGifts[i].GiftId;
                        _context.CampaignGifts.Add(campaigngift);
                        _context.SaveChanges();
                    }
                    List<CodeGiftCampaignDTO> listgiftcode = (List<CodeGiftCampaignDTO>)ListCampaignGifts[i].ListCodeGiftCampaigns;
                    for (int j = 0; j < listgiftcode.Count; j++)
                    {
                        CodeGiftCampaign codegift = (CodeGiftCampaign)mapper.Map<CodeGiftCampaignDTO, CodeGiftCampaign>(listgiftcode[j]);
                        codegift.CampaignGiftId = campaigngift.CampaignGiftId;
                        if (listgiftcode[j].Active)
                        {
                            codegift.ActivatedDate = DateTime.Now;
                        }
                        else
                        {
                            codegift.ActivatedDate = null;
                        }
                        _context.CodeGiftCampaigns.Add(codegift);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "true";
        }

        public MemoryStream ExportToExcel(List<CodeGiftCampaignDTO_ResponseFilter> list)
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

                    worksheet.Cells[i + 2, ++j].Value = list[i].CreatedDate;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].GiftName;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].CodeUsageLimit;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Used;
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
    }
}
