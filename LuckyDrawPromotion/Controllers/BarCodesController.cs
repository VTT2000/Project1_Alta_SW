using IronBarCode;
using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Reflection;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BarCodesController : ControllerBase
    {
        private readonly IBarCodeService _barCodeService;
        public BarCodesController(IBarCodeService barCodeService)
        {
            _barCodeService = barCodeService;
        }

        [HttpGet]
        public IActionResult GetFilterMethod()
        {
            List<CampaignDTO_FilterMethod> campaignDTO_FilterMethods = new List<CampaignDTO_FilterMethod>();
            campaignDTO_FilterMethods.Add(new CampaignDTO_FilterMethod(1, "Match all filters"));
            campaignDTO_FilterMethods.Add(new CampaignDTO_FilterMethod(2, "Match any filters"));
            return Ok(campaignDTO_FilterMethods);
        }
        [HttpGet]
        public IActionResult GetSearchCriteriaBarCode()
        {
            List<CampaignDTO_Condition> campaignDTO_Conditions = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions.Add(new CampaignDTO_Condition(1, "includes"));
            campaignDTO_Conditions.Add(new CampaignDTO_Condition(2, "is not include"));

            List<CampaignDTO_Condition> campaignDTO_Conditions0 = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(1, "more than"));
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(2, "less than"));
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(3, "exactly"));

            List<CampaignDTO_Condition> campaignDTO_Conditions1 = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions1.Add(new CampaignDTO_Condition(1, "is"));
            campaignDTO_Conditions1.Add(new CampaignDTO_Condition(2, "is not"));

            List<CampaignDTO_SearchCriteria> campaignDTO_SearchCriterias = new List<CampaignDTO_SearchCriteria>();
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(1, "Code", campaignDTO_Conditions));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(2, "Created Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(3, "Expired Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(4, "Scanned Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(5, "Scanned status", campaignDTO_Conditions1));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(6, "Activation status", campaignDTO_Conditions1));

            return Ok(campaignDTO_SearchCriterias);
        }

        [HttpPost]
        public IActionResult GetAllForFilter(int filterMethod, List<CampaignDTO_Request_ConditionSearch> listConditionSearches)
        {
            if (filterMethod <= 0 || filterMethod >= 3)
            {
                return BadRequest(new { message = "FilterMethod not empty" });
            }
            return Ok(_barCodeService.GetAllForSort(filterMethod, listConditionSearches));
        }

        [HttpPost]
        public IActionResult GeneratedBarCode(int campaignId, int codeRedemLimit, bool Unlimited, double codeCount,int charsetId, int codeLength, string prefix, string postfix)
        {
            string result = _barCodeService.generatedBarCode(campaignId, codeRedemLimit, Unlimited, codeCount, charsetId, codeLength, prefix, postfix);
            if (result.Equals("true"))
            {
                return Ok("Generated (amount of barcodes) Barcodes successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public IActionResult GetBarCodeDetail(int CodeCampaignId)
        {
            return Ok(_barCodeService.GetBarCodeDetail(CodeCampaignId));
        }

        [HttpPut]
        public IActionResult IsActivetedBarCode(int CodeCampaignId)
        {
            if (_barCodeService.IsCodeCampaign(CodeCampaignId))
            {
                return BadRequest("CodeCampaignId is not exist");
            }
            if (_barCodeService.BarCodeIsActived(CodeCampaignId))
            {
                return Ok("The Barcode (code) is Activated");
            }
            else
            {
                return Ok("The Barcode (code) is De-activated");
            }
        }

        [HttpPut]
        public IActionResult ScannedBarCode(int CodeCampaignId, string Email)
        {
            int existCustomer = _barCodeService.GetIdAstCustomerEmail(Email);
            if (existCustomer == 0)
            {
                return BadRequest("The Email Customer is not Exist");
            }
            if(_barCodeService.BarCodeScanned(CodeCampaignId, existCustomer))
            {
                return Ok("The Barcode (code) is scanned for customer (customer email) by (the session owner)");
            }
            return BadRequest("False barcode scanned");
        }

        // xuat file exels 
        [HttpPost]
        public IActionResult ExportToExcel(List<CodeBarDTO_ResponseFilter> list)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
               
                // for ten thuoc tinh
                for(int q =0; q < list[0].GetType().GetProperties().Count();q++)
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
                    picture.From.Row = i+1;
                    picture.SetSize(250, 50);
                    worksheet.Column(++j).Width = 35;

                    // Tao hinh anh base64 QR code
                    GeneratedBarcode MyBarCode0 = IronBarCode.QRCodeWriter.CreateQrCode(codeValue);
                    ExcelPicture picture0 = null!;
                    picture0 = worksheet.Drawings.AddPicture("pic " + i + j, MyBarCode0.ToStream(), ePictureType.Png);
                    picture0.From.Column = j;
                    picture0.From.Row = i+1;
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
            stream.Position = 0;
            string excelName = "Barcode_list.xlsx";
            return File(stream, "application/vnd.openxmlformat-officedocument.spredsheetml.sheet", excelName);
        }


        [HttpGet]
        public IActionResult GetSearchCriteriaBarCodeHistory()
        {
            List<CampaignDTO_Condition> campaignDTO_Conditions = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions.Add(new CampaignDTO_Condition(1, "includes"));
            campaignDTO_Conditions.Add(new CampaignDTO_Condition(2, "is not include"));

            List<CampaignDTO_Condition> campaignDTO_Conditions0 = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(1, "more than"));
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(2, "less than"));
            campaignDTO_Conditions0.Add(new CampaignDTO_Condition(3, "exactly"));

            List<CampaignDTO_Condition> campaignDTO_Conditions1 = new List<CampaignDTO_Condition>();
            campaignDTO_Conditions1.Add(new CampaignDTO_Condition(1, "is"));
            campaignDTO_Conditions1.Add(new CampaignDTO_Condition(2, "is not"));

            List<CampaignDTO_SearchCriteria> campaignDTO_SearchCriterias = new List<CampaignDTO_SearchCriteria>();
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(1, "Code", campaignDTO_Conditions));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(2, "Created Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(3, "Scanned Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(4, "Spin Date", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(5, "Owner", campaignDTO_Conditions0));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(6, "Scanned status", campaignDTO_Conditions1));
            campaignDTO_SearchCriterias.Add(new CampaignDTO_SearchCriteria(6, "Used for spin", campaignDTO_Conditions1));



            return Ok(campaignDTO_SearchCriterias);
        }


    }
}
