using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GiftsController : ControllerBase
    {
        private readonly IGiftService _giftService;
        public GiftsController(IGiftService giftService) { _giftService = giftService; }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_giftService.GetGifts());
        }

        
    }
}
