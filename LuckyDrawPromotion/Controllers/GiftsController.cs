using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
