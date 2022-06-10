using LuckyDrawPromotion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RepeatSchedulesController : ControllerBase
    {
        private readonly IRepeatScheduleService _repeatScheduleService;
        public RepeatSchedulesController(IRepeatScheduleService repeatScheduleService) { _repeatScheduleService = repeatScheduleService; }

        [HttpGet]
        public IActionResult GetRepeatSchedules()
        {
            return Ok(_repeatScheduleService.GetRepeatSchedules());
        }
    }
}
