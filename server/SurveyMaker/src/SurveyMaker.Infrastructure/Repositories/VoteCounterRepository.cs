using Microsoft.EntityFrameworkCore;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;
using SurveyMaker.Infrastructure.EF;

namespace SurveyMaker.Infrastructure.Repositories
{
    public class VoteCounterRepository : IVoteCounterRepository
    {
        private readonly SurveyMakerDbContext _context;

        public VoteCounterRepository(SurveyMakerDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(VoteCounter voteCounter, CancellationToken cancellationToken)
        {
            await _context.VoteCounters.AddAsync(voteCounter, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<VoteCounter?> GetByOptionIdAsync(int optionId)
        {
            return await _context.VoteCounters
                .FirstOrDefaultAsync(vc => vc.OptionId == optionId); // esto retorna null si no encuentra nada
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
