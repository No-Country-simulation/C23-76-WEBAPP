using Microsoft.EntityFrameworkCore;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;
using SurveyMaker.Infrastructure.EF;

namespace SurveyMaker.Infrastructure.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly SurveyMakerDbContext _context;

        public SurveyRepository(SurveyMakerDbContext context)
        {
            _context = context;
        }

        public async Task<Survey?> GetByIdAsync(int surveyId)
        {
            return await _context.Surveys
                .Include(x => x.Questions)
                .ThenInclude(x => x.Options)
                .FirstOrDefaultAsync(x => x.Id == surveyId);
        }

        public async Task<int> SaveAsync(Survey survey, CancellationToken cancellationToken)
        {
            await _context.Surveys.AddAsync(survey, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(Survey survey, CancellationToken cancellationToken)
        {
            _context.Surveys.Update(survey);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
