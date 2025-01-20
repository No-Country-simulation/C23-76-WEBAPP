using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Domain.Repositories
{
    public interface ISurveyRepository
    {
        Task<int> SaveAsync(Survey survey, CancellationToken cancellationToken);
    }
}
