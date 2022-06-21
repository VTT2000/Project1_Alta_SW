using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace LuckyDrawPromotion.Services
{
    public interface IWinnerService
    {
        IEnumerable<Winner> GetAll();
        Winner GetById(int id);
        void Save(Winner temp);
        void Remove(Winner temp);

        bool IsExistsCampaignId(int id);
        IEnumerable<WinnerDTO_ResponseFilter> GetAllForSort(int campaignId, int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches);
        MemoryStream ExportToExcel(List<WinnerDTO_ResponseFilter> list);
    }
    public class WinnerService : IWinnerService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public WinnerService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public IEnumerable<Winner> GetAll()
        {
            return _context.Winners.ToList();
        }

        public Winner GetById(int id)
        {
            return _context.Winners.ToList().FirstOrDefault(x => x.WinnerId == id)!;
        }

        public void Save(Winner temp)
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

        public void Remove(Winner temp)
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

        public bool IsExistsCampaignId(int id)
        {
            return _context.Campaigns.ToList().Exists(p=>p.CampaignId == id);
        }

        public IEnumerable<WinnerDTO_ResponseFilter> GetAllForSort(int campaignId, int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            var list = (from a in _context.Winners
                       from b in _context.Spins
                       from c in _context.CodeCampaigns
                       from d in _context.Customers
                       from e in _context.CodeGiftCampaigns
                       from f in _context.CampaignGifts
                       from g in _context.Gifts
                       where a.SpinId == b.SpinId
                       where b.CodeCampaignId == c.CodeCampaignId
                       where c.CampaignId == campaignId
                       where c.CustomerId == d.CustomerId
                       where a.CodeGiftCampaignId == e.CodeGiftCampaignId
                       where e.CampaignGiftId == f.CampaignGiftId
                       where f.GiftId == g.GiftId
                       select new
                       {
                           WinnerId = a.WinnerId,
                           WinnerName = a.Spin.CodeCampaign.Customer!.CustomerName,
                           WinDate = a.WinDate,
                           GiftCode = a.CodeGiftCampaign.Code,
                           GiftName = a.CodeGiftCampaign.CampaignGift.Gift!.Name,
                           SentGift = a.SentGift,
                           AddressReceivedGift = a.AddressReceivedGift,
                           CodeGiftCampaignId = a.CodeGiftCampaignId,
                           SpinId = a.SpinId
                        }).ToList();
            List<WinnerDTO_ResponseFilter> kq = new List<WinnerDTO_ResponseFilter>();
            if (filterMethod == 1)
            {
                for(int i = 0; i < listConditionSearches.Count; i++)
                {
                    if(listConditionSearches[i].SearchCriteria == 1)
                    {
                        string value = listConditionSearches[i].Value;
                        if(listConditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.WinnerName.Contains(value)).ToList();
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !p.WinnerName.Contains(value)).ToList();
                        }
                    }
                    if (listConditionSearches[i].SearchCriteria == 2)
                    {
                        DateTime winDate = DateTime.ParseExact(listConditionSearches[i].Value, "dd/MM/yyyy", null);
                        if (listConditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p=>p.WinDate.Date >= winDate).ToList();
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => p.WinDate.Date <= winDate).ToList();
                        }
                        if (listConditionSearches[i].Condition == 3)
                        {
                            list = list.Where(p => p.WinDate.Date == winDate).ToList();
                        }
                    }
                    if (listConditionSearches[i].SearchCriteria == 3)
                    {
                        string value = listConditionSearches[i].Value;
                        if (listConditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.GiftCode.Contains(value)).ToList();
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !p.GiftCode.Contains(value)).ToList();
                        }
                    }
                    if (listConditionSearches[i].SearchCriteria == 4)
                    {
                        string value = listConditionSearches[i].Value;
                        if (listConditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.GiftName.Contains(value)).ToList();
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !p.GiftName.Contains(value)).ToList();
                        }
                    }
                    if (listConditionSearches[i].SearchCriteria == 5)
                    {
                        string value = listConditionSearches[i].Value;
                        if (listConditionSearches[i].Condition == 1) 
                        {
                            if (value.Equals("Sent"))
                            {
                                list = list.Where(p => p.SentGift == true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => p.SentGift == false).ToList();
                            }
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            if (value.Equals("Sent"))
                            {
                                list = list.Where(p => p.SentGift != true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => p.SentGift != false).ToList();
                            }
                        }
                    }
                }
                for (int i = 0; i < list.Count; i++)
                {
                    WinnerDTO_ResponseFilter temp = new WinnerDTO_ResponseFilter
                    {
                        WinnerId = list[i].WinnerId,
                        WinnerName = list[i].WinnerName,
                        WinDate = list[i].WinDate.ToString("dd/MM/yyyy"),
                        GiftCode = list[i].GiftCode,
                        GiftName = list[i].GiftName,
                        SentGift = list[i].SentGift
                    };
                    kq.Add(temp);
                }
                return kq;
            }
            if(filterMethod == 2)
            {
                for (int i = 0; i < listConditionSearches.Count; i++)
                {
                    if (listConditionSearches[i].SearchCriteria == 1)
                    {
                        string value = listConditionSearches[i].Value;
                        if (listConditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.WinnerName.Contains(value)).ToList();
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !p.WinnerName.Contains(value)).ToList();
                        }
                    }
                    if (listConditionSearches[i].SearchCriteria == 2)
                    {
                        DateTime winDate = DateTime.ParseExact(listConditionSearches[i].Value, "dd/MM/yyyy", null);
                        if (listConditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.WinDate.Date >= winDate).ToList();
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => p.WinDate.Date <= winDate).ToList();
                        }
                        if (listConditionSearches[i].Condition == 3)
                        {
                            list = list.Where(p => p.WinDate.Date == winDate).ToList();
                        }
                    }
                    if (listConditionSearches[i].SearchCriteria == 3)
                    {
                        string value = listConditionSearches[i].Value;
                        if (listConditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.GiftCode.Contains(value)).ToList();
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !p.GiftCode.Contains(value)).ToList();
                        }
                    }
                    if (listConditionSearches[i].SearchCriteria == 4)
                    {
                        string value = listConditionSearches[i].Value;
                        if (listConditionSearches[i].Condition == 1)
                        {
                            list = list.Where(p => p.GiftName.Contains(value)).ToList();
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            list = list.Where(p => !p.GiftName.Contains(value)).ToList();
                        }
                    }
                    if (listConditionSearches[i].SearchCriteria == 5)
                    {
                        string value = listConditionSearches[i].Value;
                        if (listConditionSearches[i].Condition == 1)
                        {
                            if (value.Equals("Sent"))
                            {
                                list = list.Where(p => p.SentGift == true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => p.SentGift == false).ToList();
                            }
                        }
                        if (listConditionSearches[i].Condition == 2)
                        {
                            if (value.Equals("Sent"))
                            {
                                list = list.Where(p => p.SentGift != true).ToList();
                            }
                            else
                            {
                                list = list.Where(p => p.SentGift != false).ToList();
                            }
                        }
                    }
                    List<WinnerDTO_ResponseFilter> tempList = new List<WinnerDTO_ResponseFilter>();
                    for (int z = 0; z < list.Count; z++)
                    {
                        WinnerDTO_ResponseFilter temp = new WinnerDTO_ResponseFilter
                        {
                            WinnerId = list[z].WinnerId,
                            WinnerName = list[z].WinnerName,
                            WinDate = list[z].WinDate.ToString("dd/MM/yyyy"),
                            GiftCode = list[z].GiftCode,
                            GiftName = list[z].GiftName,
                            SentGift = list[z].SentGift
                        };
                        tempList.Add(temp);
                    }
                    kq = kq.Union(tempList).ToList();
                }
                return kq;
            }
            return new List<WinnerDTO_ResponseFilter>();
        }

        public MemoryStream ExportToExcel(List<WinnerDTO_ResponseFilter> list)
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
                    worksheet.Cells[i + 2, ++j].Value = list[i].WinnerId;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].WinnerName;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].WinDate;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].GiftCode;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].GiftName;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].SentGift;
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
