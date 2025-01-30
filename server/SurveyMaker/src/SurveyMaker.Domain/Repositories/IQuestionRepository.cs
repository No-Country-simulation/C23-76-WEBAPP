using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<int> SaveAsync(Question question, CancellationToken cancellationToken);
        Task<int> UpdateAsync(Question question, CancellationToken cancellationToken);
        Task<int> DeleteAsync(Question question, CancellationToken cancellationToken);

    }
}
