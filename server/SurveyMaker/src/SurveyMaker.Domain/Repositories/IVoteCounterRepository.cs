using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Domain.Repositories
{
    public interface IVoteCounterRepository
    {
        Task<int> AddAsync(VoteCounter voteCounter, CancellationToken cancellationToken);
        Task<VoteCounter?> GetByOptionIdAsync(int optionId);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
