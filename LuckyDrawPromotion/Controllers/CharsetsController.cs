using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CharsetsController : ControllerBase
    {
        private readonly ICharsetService _charsetService;
        public CharsetsController(ICharsetService charsetService) { _charsetService = charsetService; }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_charsetService.GetCharsets());
        }
    }
}
