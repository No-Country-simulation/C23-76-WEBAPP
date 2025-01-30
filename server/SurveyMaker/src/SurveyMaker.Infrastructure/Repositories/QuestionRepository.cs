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

        public async Task<int> SaveAsync(Question question, CancellationToken cancellationToken)
        {
            await _context.Questions.AddAsync(question, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
