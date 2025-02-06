using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;
using SurveyMaker.Infrastructure.EF;

namespace SurveyMaker.Infrastructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly SurveyMakerDbContext _context;

        public VoteRepository(SurveyMakerDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync(Vote vote, CancellationToken cancellationToken)
        {
            Console.WriteLine("JSON antes de guardar: " + vote.AnswersJson);
            await _context.Vote.AddAsync(vote, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
