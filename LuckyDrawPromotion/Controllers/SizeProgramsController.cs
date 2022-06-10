using AutoMapper;
using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SizeProgramsController : ControllerBase
    {
        private readonly ISizeProgramService _sizeProgramService;
        
        public SizeProgramsController(ISizeProgramService sizeProgramService) { _sizeProgramService = sizeProgramService; }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_sizeProgramService.GetSizePrograms());
        }

    }
}
