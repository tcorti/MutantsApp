using Microsoft.AspNetCore.Mvc;
using Mutants.Models.DTOs;
using Mutants.Services;
using System.Threading.Tasks;

namespace Mutants.Controllers
{
    [ApiController]
    [Route("mutant")]
    public class MutantController : ControllerBase
    {
        private readonly MutantService _mutantService;

        public MutantController(MutantService mutantService)
        {
            _mutantService = mutantService;
        }

        [HttpPost]
        public async Task<IActionResult> IsMutantAsync(IsMutantRequest rq)
        {
            if (rq != null && rq.dna != null)
            {
                if (await _mutantService.IsMutantAsync(rq))
                    return Ok();
                else
                    return StatusCode(403);
            }

            return BadRequest();
        }
    }
}