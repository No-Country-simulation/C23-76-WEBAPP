using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Domain.Repositories
{
    public interface ISurveyRepository
    {
        Task<int> SaveAsync(Survey survey, CancellationToken cancellationToken);
        Task<Survey?> GetByIdAsync(int surveyId);
        Task<int> UpdateAsync(Survey survey, CancellationToken cancellationToken);
        Task<List<Survey>> GetAllByUserAsync(Guid userId);
        Task<List<Survey>> GetAllAsync();
    }
}
