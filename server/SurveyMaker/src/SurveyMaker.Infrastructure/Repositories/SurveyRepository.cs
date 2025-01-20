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

        public async Task<int> SaveAsync(Survey survey, CancellationToken cancellationToken)
        {
            await _context.Surveys.AddAsync(survey, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
