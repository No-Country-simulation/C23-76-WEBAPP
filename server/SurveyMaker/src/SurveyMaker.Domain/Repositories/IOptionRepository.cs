using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Domain.Repositories
{
    public interface IOptionRepository
    {
        Task<int> SaveAsync(Option option, CancellationToken cancellationToken);
        Task<Option?> GetByIdAsync(int id);
        Task<int> UpdateAsync(Option option, CancellationToken cancellationToken);
        Task<int> DeleteAsync(Option option);
    }
}
