using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Data;
using Microsoft.Extensions.Options;
using LuckyDrawPromotion.Helpers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IronBarCode;

namespace LuckyDrawPromotion.Services
{
    public interface IBarCodeService
    {
        IEnumerable<CodeCampaign> GetAll();
        CodeCampaign GetById(int id);
        void Save(CodeCampaign temp);
        void Remove(CodeCampaign temp);

        IEnumerable<CodeBarDTO_ResponseFilter> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> conditionSearches);
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
            if(SearchCriteria == 5)
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

            if (sqlWhere.Length == 0)
            {
                return new List<CodeBarDTO_ResponseFilter>();
            }

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CodeCampaign, CodeBarDTO_ResponseFilter>();
            });
            var mapper = config.CreateMapper();

            var list = _context.CodeCampaigns.FromSqlRaw("Select * from dbo.CodeCampaigns Where " + sqlWhere).Select(emp => mapper.Map<CodeCampaign, CodeBarDTO_ResponseFilter>(emp)).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                string codeValue = list[i].Code;
                // Tao hinh anh base64 bar code
                GeneratedBarcode MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode(codeValue, BarcodeWriterEncoding.Code128);
                list[i].BarCode = "data:image/png;base64," + Convert.ToBase64String(MyBarCode.ToPngBinaryData());
                // Tao hinh anh base64 QR code
                GeneratedBarcode MyBarCode0 = IronBarCode.QRCodeWriter.CreateQrCode(codeValue);
                list[i].QRCode = "data:image/png;base64," + Convert.ToBase64String(MyBarCode0.ToPngBinaryData());
            }
            return list;
        }


    }
}
