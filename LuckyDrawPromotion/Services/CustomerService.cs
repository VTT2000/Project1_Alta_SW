using AutoMapper;
using LuckyDrawPromotion.Data;
using LuckyDrawPromotion.Helpers;
using LuckyDrawPromotion.Models;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace LuckyDrawPromotion.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetById(int id);
        void Save(Customer temp);
        void Remove(Customer temp);

        IEnumerable<PositionDTO> GetPositions();
        IEnumerable<TypeOfBussinessDTO> GetTypeOfBussiness();
        IEnumerable<CustomerDTO_ResponseFilter> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches);
        MemoryStream ExportToExcel(List<CustomerDTO_ResponseFilter> list);
        bool IsExistCustomerId(int id);
        bool BlockOrUnblockCustomer(int id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public CustomerService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return _context.Customers.ToList().FirstOrDefault(x => x.CustomerId == id)!;
        }

        public void Save(Customer temp)
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

        public void Remove(Customer temp)
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
        public IEnumerable<PositionDTO> GetPositions()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Position, PositionDTO>();
            });
            var mapper = config.CreateMapper();
            return _context.Positions.Select(p => mapper.Map<Position, PositionDTO>(p)).ToList();
        }
        public IEnumerable<TypeOfBussinessDTO> GetTypeOfBussiness()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TypeOfBussiness, TypeOfBussinessDTO>();
            });
            var mapper = config.CreateMapper();
            return _context.TypeOfBussiness.Select(p => mapper.Map<TypeOfBussiness, TypeOfBussinessDTO>(p)).ToList();
        }
        public IEnumerable<CustomerDTO_ResponseFilter> GetAllForSort(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDTO_ResponseFilter>();
                cfg.CreateMap<DateTime, string>().ConvertUsing(p=>p.ToString("dd/MM/yyyy"));
            });
            var mapper = config.CreateMapper();
            var list = (from a in _context.Customers
                        select a).ToList()
                        .Select(p => {
                            var kq = mapper.Map<Customer, CustomerDTO_ResponseFilter>(p);
                            kq.PositionName = _context.Positions.First(p=>p.PositionId == p.PositionId).Name;
                            kq.TypeOfBussinessName = _context.TypeOfBussiness.First(p=>p.TypeOfBussinessId == p.TypeOfBussinessId).Name;
                            return kq;
                        }).ToList();
            if (filterMethod == 1)
            {
                var list0 = list;
                if(listConditionSearches == null || listConditionSearches.Count == 0)
                {
                    return list0;
                }
                for(int i = 0; i < listConditionSearches.Count; i++)
                {
                    var dk = listConditionSearches[i];
                    if(dk.SearchCriteria == 1)
                    {
                        if(dk.Condition == 1)
                        {
                            list0 = list0.Where(p => p.CustomerName.Contains(dk.Value)).ToList();
                        }
                        if(dk.Condition == 2)
                        {
                            list0 = list0.Where(p => !p.CustomerName.Contains(dk.Value)).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 2)
                    {
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => p.CustomerPhone.Contains(dk.Value)).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => !p.CustomerPhone.Contains(dk.Value)).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 3)
                    {
                        DateTime date = DateTime.ParseExact(dk.Value, "dd/MM/yyyy", null);
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => DateTime.Parse(p.DateOfBirth).Date >= date).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => DateTime.Parse(p.DateOfBirth).Date <= date).ToList();
                        }
                        if (dk.Condition == 3)
                        {
                            list0 = list0.Where(p => DateTime.Parse(p.DateOfBirth).Date == date).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 4)
                    {
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => p.PositionName.Equals(dk.Value)).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => !p.PositionName.Equals(dk.Value)).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 5)
                    {
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => p.TypeOfBussinessName.Equals(dk.Value)).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => !p.TypeOfBussinessName.Equals(dk.Value)).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 6)
                    {
                        if (dk.Condition == 1)
                        {
                            if (dk.Value.Equals("block"))
                            {
                                list0 = list0.Where(p => p.Block).ToList();
                            }
                            else
                            {
                                list0 = list0.Where(p => !p.Block).ToList();
                            }
                        }
                        if (dk.Condition == 2)
                        {
                            if (!dk.Value.Equals("block"))
                            {
                                list0 = list0.Where(p => p.Block).ToList();
                            }
                            else
                            {
                                list0 = list0.Where(p => !p.Block).ToList();
                            }
                        }
                    }
                }
                return list0;
            }
            if (filterMethod == 2)
            {
                var kq = new List<CustomerDTO_ResponseFilter>();
                for (int i = 0; i < listConditionSearches.Count; i++)
                {
                    var list0 = list;
                    var dk = listConditionSearches[i];
                    if (dk.SearchCriteria == 1)
                    {
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => p.CustomerName.Contains(dk.Value)).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => !p.CustomerName.Contains(dk.Value)).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 2)
                    {
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => p.CustomerPhone.Contains(dk.Value)).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => !p.CustomerPhone.Contains(dk.Value)).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 3)
                    {
                        DateTime date = DateTime.ParseExact(dk.Value, "dd/MM/yyyy", null);
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => DateTime.Parse(p.DateOfBirth).Date >= date).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => DateTime.Parse(p.DateOfBirth).Date <= date).ToList();
                        }
                        if (dk.Condition == 3)
                        {
                            list0 = list0.Where(p => DateTime.Parse(p.DateOfBirth).Date == date).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 4)
                    {
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => p.PositionName.Equals(dk.Value)).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => !p.PositionName.Equals(dk.Value)).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 5)
                    {
                        if (dk.Condition == 1)
                        {
                            list0 = list0.Where(p => p.TypeOfBussinessName.Equals(dk.Value)).ToList();
                        }
                        if (dk.Condition == 2)
                        {
                            list0 = list0.Where(p => !p.TypeOfBussinessName.Equals(dk.Value)).ToList();
                        }
                    }
                    if (dk.SearchCriteria == 6)
                    {
                        if (dk.Condition == 1)
                        {
                            if (dk.Value.Equals("block"))
                            {
                                list0 = list0.Where(p => p.Block).ToList();
                            }
                            else
                            {
                                list0 = list0.Where(p => !p.Block).ToList();
                            }
                        }
                        if (dk.Condition == 2)
                        {
                            if (!dk.Value.Equals("block"))
                            {
                                list0 = list0.Where(p => p.Block).ToList();
                            }
                            else
                            {
                                list0 = list0.Where(p => !p.Block).ToList();
                            }
                        }
                    }
                    kq = kq.Union(list0).ToList();
                }
                return kq;
            }
            return new List<CustomerDTO_ResponseFilter>();
        }

        public MemoryStream ExportToExcel(List<CustomerDTO_ResponseFilter> list)
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
                    worksheet.Cells[i + 2, ++j].Value = list[i].CustomerId;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].CustomerName;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].CustomerPhone;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].DateOfBirth;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].Block;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].PositionName;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    worksheet.Cells[i + 2, ++j].Value = list[i].TypeOfBussinessName;
                    worksheet.Cells[i + 2, j].AutoFitColumns();
                    worksheet.Cells[i + 2, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                }
                package.Save();
            }
            return stream;
        }
        public bool IsExistCustomerId(int id)
        {
            return _context.Customers.ToList().Exists(p => p.CustomerId == id);
        }
        public bool BlockOrUnblockCustomer(int id)
        {
            Customer temp = _context.Customers.First(p=>p.CustomerId == id);
            if (temp.Block)
            {
                temp.Block = false;
            }
            else
            {
                temp.Block = true;
            }
            _context.Customers.Update(temp);
            _context.SaveChanges();
            return temp.Block;
        }
    }
}
