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

        public async Task<Survey?> GetByIdAsync(int surveyId, bool withQuestions = false , bool withOptions = false)
        {
            var query = _context.Surveys.AsQueryable();

            if (withQuestions)
            {
                query = query.Include(x => x.Questions);
            }

            if (withOptions)
            {
                query = query.Include(x => x.Questions).ThenInclude(x => x.Options);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == surveyId);
        }

        public async Task<List<Survey>> GetAllByUserAsync(Guid userId, bool withQuestions = false, bool withOptions = false)
        {
            var query = _context.Surveys
                .Where(x => x.CreatedBy == userId.ToString());

            if (withQuestions)
            {
                query = query.Include(x => x.Questions);
            }

            if (withOptions)
            {
                query = query.Include(x => x.Questions).ThenInclude(x => x.Options);
            }

            return await query.ToListAsync();
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

        public async Task<Survey?> GetSurveyLinkAsync(int surveyId, CancellationToken cancellationToken)
        {
            return await _context.Surveys
                .AsNoTracking()
                .Where(x => x.Id == surveyId)
                .Select(x => new Survey
                {
                    Url = x.Url,
                    AllowAnonymousVotes = x.AllowAnonymousVotes
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Survey>> GetAllAsync(bool withQuestions = false, bool withOptions = false)
        {
            var query = _context.Surveys.AsQueryable();

            if (withQuestions)
            {
                query = query.Include(x => x.Questions);
            }

            if (withOptions)
            {
                query = query.Include(x => x.Questions).ThenInclude(x => x.Options);
            }

            return await query.ToListAsync();
        }
    }
}
