using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Summary;

namespace Summary.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/summary")]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        // Obter o resumo individual por pessoa
        [HttpGet("person")]
        public async Task<IActionResult> GetPersonSummaries()
        {
            var summaries = await _summaryService.GetPersonSummariesAsync();
            return Ok(summaries);
        }

        // Obter o resumo geral de todas as pessoas
        [HttpGet("overall")]
        public async Task<IActionResult> GetOverallSummary()
        {
            var overallSummary = await _summaryService.GetOverallSummaryAsync();
            return Ok(overallSummary);
        }
    }
}
