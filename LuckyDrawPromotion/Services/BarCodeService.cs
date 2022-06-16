using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Data;
using Microsoft.Extensions.Options;
using LuckyDrawPromotion.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IronBarCode;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Reflection;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;

namespace LuckyDrawPromotion.Services
{
    public interface IBarCodeService
    {
        IEnumerable<CodeCampaign> GetAll();
        CodeCampaign GetById(int id);
        void Save(CodeCampaign temp);
        void Remove(CodeCampaign temp);

        IEnumerable<CodeBarDTO_ResponseFilter> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> conditionSearches);
        string generatedBarCode(CodeCampaignDTO_RequestGenerate temp);
        CodeBarDTO_ResponseDetail GetBarCodeDetail(int CodeCampaignId);
        bool IsCodeCampaign(int CodeCampaignId);
        bool BarCodeIsActived(int CodeCampaignId);
        int GetIdAstCustomerEmail(string Email);
        bool BarCodeScanned(int CodeCampaignId, int CustomerId);
        MemoryStream ExportToExcel(List<CodeBarDTO_ResponseFilter> list);
    }
    public class BarCodeService : IBarCodeService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public BarCodeService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public IEnumerable<CodeCampaign> GetAll()
        {
            return _context.CodeCampaigns.ToList();
        }

        public CodeCampaign GetById(int id)
        {
            return _context.CodeCampaigns.ToList().FirstOrDefault(x => x.CodeCampaignId == id)!;
        }

        public void Save(CodeCampaign temp)
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

        public void Remove(CodeCampaign temp)
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
        private string SqlCmdWhereBarCode(int SearchCriteria, int Condition, string Value)
        {
            string sqlWhere = "";
            if (SearchCriteria == 1)
            {
                sqlWhere = sqlWhere + "Code ";
                if (Condition == 1)
                {
                    return sqlWhere + "Like '%" + Value + "%' ";
                }
                if (Condition == 2)
                {
                    return "Not" + sqlWhere + "Like '%" + Value + "%' ";
                }
            }
            if (SearchCriteria == 2)
            {
                sqlWhere = sqlWhere + "CreatedDate ";
            }
            if (SearchCriteria == 3)
            {
                sqlWhere = sqlWhere + "ExpiredDate ";
            }
            if (SearchCriteria == 4)
            {
                sqlWhere = sqlWhere + "ScannedDate ";
            }
            if (SearchCriteria == 2 || SearchCriteria == 3 || SearchCriteria == 4)
            {
                if (Condition == 1)
                {
                    return sqlWhere + ">= '" + DateTime.ParseExact(Value, "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + "' ";
                }
                if (Condition == 2)
                {
                    return sqlWhere + "<= '" + DateTime.ParseExact(Value, "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + "' ";
                }
                if (Condition == 3)
                {
                    return sqlWhere + "= '" + DateTime.ParseExact(Value, "dd/MM/yyyy", null).ToString("yyyy-MM-dd") + "' ";
                }
            }
            if (SearchCriteria == 5)
            {
                sqlWhere = sqlWhere + "Scanned ";
                // “is”, “is not” is Condition
                if (Condition == 1)
                {
                    // “scanned”, “not scan” is Value.
                    if (Value.Equals("scanned"))
                    {
                        return sqlWhere + "= 1";
                    }
                    if (Value.Equals("not scan"))
                    {
                        return sqlWhere + "= 0";
                    }
                }
                if (Condition == 2)
                {
                    // “scanned”, “not scan” is Value.
                    if (Value.Equals("scanned"))
                    {
                        return sqlWhere + "= 0";
                    }
                    if (Value.Equals("not scan"))
                    {
                        return sqlWhere + "= 1";
                    }
                }
            }
            if (SearchCriteria == 6)
            {
                sqlWhere = sqlWhere + "Actived ";
                // “is”, “is not” is Condition
                // "Activate”, “De-activate is Value
                if (Condition == 1)
                {
                    // “scanned”, “not scan” is Value.
                    if (Value.Equals("Activate"))
                    {
                        return sqlWhere + "= 1";
                    }
                    if (Value.Equals("not scan"))
                    {
                        return sqlWhere + "= 0";
                    }
                }
                if (Condition == 2)
                {
                    // “scanned”, “not scan” is Value.
                    if (Value.Equals("De-activate"))
                    {
                        return sqlWhere + "= 0";
                    }
                    if (Value.Equals("not scan"))
                    {
                        return sqlWhere + "= 1";
                    }
                }
            }
            return sqlWhere;
        }
        public IEnumerable<CodeBarDTO_ResponseFilter> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> conditionSearches)
        {
            string sqlWhere = "";
            for (int i = 0; i < conditionSearches.Count; i++)
            {
                if (i > 0)
                {
                    if (filterMethod == 1)
                    {
                        sqlWhere = sqlWhere + "AND ";
                    }
                    if (filterMethod == 2)
                    {
                        sqlWhere = sqlWhere + "OR ";
                    }
                }

                CampaignDTO_Request_ConditionSearch temp = conditionSearches[i];
                sqlWhere = sqlWhere + SqlCmdWhereBarCode(temp.SearchCriteria, temp.Condition, temp.Value);
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CodeCampaign, CodeBarDTO_ResponseFilter>();
            });
            var mapper = config.CreateMapper();

            List<CodeCampaign> list0 = new List<CodeCampaign>();
            if (sqlWhere.Length == 0)
            {
                // tra full
                list0 = _context.CodeCampaigns.ToList();
            }
            else
            {
                list0 = _context.CodeCampaigns.FromSqlRaw("Select * from dbo.CodeCampaigns Where " + sqlWhere).ToList();
            }

            List<CodeBarDTO_ResponseFilter> result = new List<CodeBarDTO_ResponseFilter>();
            for (int i = 0; i < list0.Count; i++)
            {
                CodeBarDTO_ResponseFilter temp = mapper.Map<CodeCampaign, CodeBarDTO_ResponseFilter>(list0[i]);
                string codeValue = temp.Code;
                // Tao hinh anh base64 bar code
                GeneratedBarcode MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode(codeValue, BarcodeWriterEncoding.Code128);
                temp.BarCode = "data:image/png;base64," + Convert.ToBase64String(MyBarCode.ToPngBinaryData());
                // Tao hinh anh base64 QR code
                GeneratedBarcode MyBarCode0 = IronBarCode.QRCodeWriter.CreateQrCode(codeValue);
                temp.QRCode = "data:image/png;base64," + Convert.ToBase64String(MyBarCode0.ToPngBinaryData());
                temp.CreatedDate = list0[i].CreatedDate.ToString("dd/MM/yyyy HH:mm:ss");
                temp.ExpiredDate = list0[i].ExpiredDate!.Value.ToString("dd/MM/yyyy HH:mm:ss");
                temp.ScannedDate = list0[i].ScannedDate.HasValue ? list0[i].ScannedDate!.Value.ToString("dd/MM/yyyy HH:mm:ss") : null;
                result.Add(temp);
            }
            return result;
        }

        public string generatedBarCode(CodeCampaignDTO_RequestGenerate tempX)
        {
            var x = _context.Campaigns.FirstOrDefault(p => p.CampaignId == tempX.CampaignId);
            if (x == null)
            {
                return "CampaignId not existed";
            }
            try
            {
                char[] MangKyTu = _context.Charsets.First(c => c.CharsetId == tempX.CharsetId).Value.ToCharArray();
                int DoDaiTao = tempX.CodeLength - (String.IsNullOrEmpty(tempX.Prefix) ? 0 : tempX.Prefix.Length) - (String.IsNullOrEmpty(tempX.Postfix) ? 0 : tempX.Postfix.Length);
                Random fr = new Random();
                for (int i = 0; i < tempX.CodeCount; i++)
                {
                    CodeCampaign temp = new CodeCampaign();
                    temp.CampaignId = tempX.CampaignId;
                    temp.CodeRedemptionLimit = tempX.CodeRedemLimit;
                    temp.Unlimited = tempX.Unlimited;
                    temp.Actived = true;
                    temp.ActivatedDate = DateTime.Now;
                    temp.ExpiredDate = x.EndDate.HasValue ? x.EndDate.Value.Add(x.EndTime.HasValue ? x.EndTime.Value : TimeSpan.Zero) : null;
                    do
                    {
                        string chuoi = "";
                        for (int j = 0; j < DoDaiTao; j++)
                        {
                            int t = fr.Next(0, (MangKyTu.Length - 1));
                            chuoi = chuoi + MangKyTu[t];
                        }
                        temp.Code = tempX.Prefix + chuoi + tempX.Postfix;
                    }
                    while (_context.CodeCampaigns.FirstOrDefault(p => p.CampaignId == x.CampaignId && p.Code == temp.Code) != null);
                    _context.CodeCampaigns.Add(temp);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "true";
        }

        public CodeBarDTO_ResponseDetail GetBarCodeDetail(int CodeCampaignId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CodeCampaign, CodeBarDTO_ResponseDetail>();
                cfg.CreateMap<DateTime, string>().ConvertUsing(dt => dt.ToString("dd/MM/yyyy HH:mm:ss"));
            });
            var mapper = config.CreateMapper();

            var temp = _context.CodeCampaigns.FirstOrDefault(p => p.CodeCampaignId == CodeCampaignId);
            if (temp == null)
            {
                return new CodeBarDTO_ResponseDetail();
            }
            var result = mapper.Map<CodeCampaign, CodeBarDTO_ResponseDetail>(temp);
            var CampaignID = _context.CodeCampaigns.First(p => p.CodeCampaignId == CodeCampaignId).CampaignId;
            result.NameCampaign = _context.Campaigns.First(p=>p.CampaignId == CampaignID).Name;
            result.Owner = temp.CustomerId.HasValue ? _context.Customers.First(p=>p.CustomerId == temp.CustomerId.Value).CustomerEmail : "";
            return result;
        }
        public bool IsCodeCampaign(int CodeCampaignId)
        {
            return _context.CodeCampaigns.ToList().Exists(p => p.CodeCampaignId == CodeCampaignId);
        }
        public bool BarCodeIsActived(int CodeCampaignId)
        {
            var x = _context.CodeCampaigns.First(p => p.CodeCampaignId == CodeCampaignId);
            if (x.Actived)
            {
                x.Actived = false;
            }
            else
            {
                x.Actived = true;
                x.ActivatedDate = DateTime.Now;
            }
            Save(x);
            return x.Actived;
        }

        public int GetIdAstCustomerEmail(string Email)
        {
            var x = _context.Customers.FirstOrDefault(p => p.CustomerEmail.Equals(Email));
            if (x != null)
            {
                return x.CustomerId;
            }
            else
            {
                return 0;
            }
        }
        public bool BarCodeScanned(int CodeCampaignId, int CustomerId)
        {
            var x = _context.CodeCampaigns.First(p => p.CodeCampaignId == CodeCampaignId);
            if (x.Scanned)
            {
                /*
                x.Scanned = false;
                x.CustomerId = null;
                */
            }
            else
            {
                x.Scanned = true;
                x.CustomerId = CustomerId;
                x.ScannedDate = DateTime.Now;
            }
            Save(x);
            return x.Scanned;
        }

        public MemoryStream ExportToExcel(List<CodeBarDTO_ResponseFilter> list)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // for ten thuoc tinh
                for (int q = 0; q < list[0].GetType().GetProperties().Count(); q++)
                {
                    worksheet.Cells[1, q + 1].Value = list[0].GetType().GetProperties()[q].Name;
                }
                worksheet.Cells[1, 1].AutoFitColumns();

                for (int i = 0; i < list.Count; i++)
                {
                    worksheet.Row(i + 2).Height = 90;
                    int j = 0;
                    worksheet.Cells[i + 2, ++j].Value = list[i].CodeCampaignId;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Code;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    string codeValue = list[i].Code;
                    // Tao hinh anh base64 bar code
                    GeneratedBarcode MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode(codeValue, BarcodeWriterEncoding.Code128);
                    ExcelPicture picture = null!;
                    picture = worksheet.Drawings.AddPicture("pic " + i + j, MyBarCode.ToStream(), ePictureType.Png);
                    picture.From.Column = j;
                    picture.From.Row = i + 1;
                    picture.SetSize(250, 50);
                    worksheet.Column(++j).Width = 35;

                    // Tao hinh anh base64 QR code
                    GeneratedBarcode MyBarCode0 = IronBarCode.QRCodeWriter.CreateQrCode(codeValue);
                    ExcelPicture picture0 = null!;
                    picture0 = worksheet.Drawings.AddPicture("pic " + i + j, MyBarCode0.ToStream(), ePictureType.Png);
                    picture0.From.Column = j;
                    picture0.From.Row = i + 1;
                    picture0.SetSize(100, 100);
                    worksheet.Column(++j).Width = 35;

                    worksheet.Cells[i + 2, ++j].Value = list[i].CreatedDate; //5
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].ExpiredDate;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].ScannedDate;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Scanned;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Actived;
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
