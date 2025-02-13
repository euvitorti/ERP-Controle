using Summary.DTO;

namespace Services.Summary
{
    public interface ISummaryService
    {
        Task<IEnumerable<SummaryDto>> GetPersonSummariesAsync();
        Task<IEnumerable<GeneralSummaryDto>> GetOverallSummaryAsync();
    }
}