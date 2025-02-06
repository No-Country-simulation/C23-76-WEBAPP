using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Domain.Repositories
{
    public interface IVoteRepository
    {
        Task<int> SaveAsync(Vote vote, CancellationToken cancellationToken);
    }
}
