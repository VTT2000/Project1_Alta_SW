using LuckyDrawPromotion.Models;
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
        [HttpGet]
        public IActionResult GetById(int id)
        {
            return Ok(_charsetService.GetById(id));
        }
        [HttpPost]
        public IActionResult Add(Charset temp)
        {
            return Ok(_charsetService.Save(temp));
        }
        [HttpPut]
        public IActionResult Update(Charset temp)
        {
            return Ok(_charsetService.Save(temp));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var temp = _charsetService.GetById(id);
            return Ok(_charsetService.Remove(temp));
        }
    }
}
