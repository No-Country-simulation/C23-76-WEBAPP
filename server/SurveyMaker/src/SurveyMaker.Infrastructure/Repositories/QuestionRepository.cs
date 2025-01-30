using Microsoft.EntityFrameworkCore;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;
using SurveyMaker.Infrastructure.EF;

namespace SurveyMaker.Infrastructure.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly SurveyMakerDbContext _context;

        public QuestionRepository(SurveyMakerDbContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteAsync(Question question, CancellationToken cancellationToken)
        {
            _context.Questions.Remove(question);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Questions
                .Include(x => x.Options)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> SaveAsync(Question question, CancellationToken cancellationToken)
        {
            await _context.Questions.AddAsync(question, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(Question question, CancellationToken cancellationToken)
        {
            _context.Questions.Update(question);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
