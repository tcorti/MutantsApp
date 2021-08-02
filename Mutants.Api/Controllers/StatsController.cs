using Microsoft.AspNetCore.Mvc;
using Mutants.Models.DTOs;
using Mutants.Services;

namespace Mutants.Controllers
{
    [ApiController]
    [Route("stats")]
    public class StatsController : ControllerBase
    {
        private readonly StatsService _statsService;

        public StatsController(StatsService statsService)
        {
            _statsService = statsService;
        }

        [HttpGet]
        public ActionResult<StatsResponse> GetStats()
        {
            return Ok(_statsService.GetStats());
        }
    }
}