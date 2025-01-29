using SurveyMaker.Domain.Entities;
using static SurveyMaker.Domain.Entities.Survey;

namespace SurveyMaker.Domain.Repositories
{
    public interface ISurveyRepository
    {
        Task<int> SaveAsync(Survey survey, CancellationToken cancellationToken);
        Task<Survey?>? GetSurveyLinkAsync(int surveyId, CancellationToken cancellationToken);
    }
}
