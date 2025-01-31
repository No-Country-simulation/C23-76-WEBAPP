using SurveyMaker.Domain.Entities;
using static SurveyMaker.Domain.Entities.Survey;

namespace SurveyMaker.Domain.Repositories
{
    public interface ISurveyRepository
    {
        Task<int> SaveAsync(Survey survey, CancellationToken cancellationToken);
        Task<Survey?>? GetSurveyLinkAsync(int surveyId, CancellationToken cancellationToken);
        Task<Survey?> GetByIdAsync(int surveyId, bool withQuestions = false, bool withOptions = false);
        Task<int> UpdateAsync(Survey survey, CancellationToken cancellationToken);
        Task<List<Survey>> GetAllByUserAsync(Guid userId, bool withQuestions = false, bool withOptions = false);
        Task<List<Survey>> GetAllAsync(bool withQuestions = false, bool withOptions = false);
    }
}
