using Microsoft.EntityFrameworkCore;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;
using SurveyMaker.Infrastructure.EF;

namespace SurveyMaker.Infrastructure.Repositories
{
    public class OptionRepository : IOptionRepository
    {
        private readonly SurveyMakerDbContext _context;

        public OptionRepository(SurveyMakerDbContext context)
        {
            _context = context;
        }

        public async Task<Option?> GetByIdAsync(int id)
        {
            return await _context.Options.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> SaveAsync(Option option, CancellationToken cancellationToken)
        {
            await _context.Options.AddAsync(option, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(Option option, CancellationToken cancellationToken)
        {
            _context.Options.Update(option);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
