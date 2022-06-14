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

namespace LuckyDrawPromotion.Services
{
    public interface IBarCodeService
    {
        IEnumerable<CodeCampaign> GetAll();
        CodeCampaign GetById(int id);
        void Save(CodeCampaign temp);
        void Remove(CodeCampaign temp);

        IEnumerable<CodeBarDTO_ResponseFilter> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> conditionSearches);
        string generatedBarCode(int campaignId, int codeRedemLimit, bool unLimited, double codeCount, int charsetId, int codeLength, string prefix, string postfix);
        CodeBarDTO_ResponseDetail GetBarCodeDetail(int CodeCampaignId);
        bool IsCodeCampaign(int CodeCampaignId);
        bool BarCodeIsActived(int CodeCampaignId);
        int GetIdAstCustomerEmail(string Email);
        bool BarCodeScanned(int CodeCampaignId, int CustomerId);
        
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

        public string generatedBarCode(int campaignId, int codeRedemLimit, bool unLimited, double codeCount, int charsetId, int codeLength, string prefix, string postfix)
        {
            var x = _context.Campaigns.FirstOrDefault(p => p.CampaignId == campaignId);
            if (x == null)
            {
                return "CampaignId not existed";
            }
            try
            {
                char[] MangKyTu = _context.Charsets.First(c => c.CharsetId == charsetId).Value.ToCharArray();
                int DoDaiTao = codeLength - prefix.Length - postfix.Length;
                Random fr = new Random();
                for (int i = 0; i < codeCount; i++)
                {
                    CodeCampaign temp = new CodeCampaign();
                    temp.CodeRedemptionLimit = codeRedemLimit;
                    temp.Unlimited = unLimited;
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
                        temp.Code = prefix + chuoi + postfix;
                    }
                    while (_context.CodeCampaigns.FirstOrDefault(p => p.CampaignId == x.CampaignId && p.Code == temp.Code) != null);
                    Save(temp);
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
            result.NameCampaign = _context.CodeCampaigns.First(p => p.CodeCampaignId == CodeCampaignId).Campaign.Name;
            result.Owner = temp.CustomerId.HasValue ? temp.Customer!.CustomerEmail : null;
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

        
    }
}
