﻿using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task<int> SaveAsync(Question question, CancellationToken cancellationToken);
        Task<int> UpdateAsync(Question question, CancellationToken cancellationToken);
        
    }
}
