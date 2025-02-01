﻿using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Domain.Repositories
{
    public interface ISurveyRepository
    {
        Task<int> SaveAsync(Survey survey, CancellationToken cancellationToken);
        Task<Survey?> GetByIdAsync(int surveyId, bool withQuestions = false, bool withOptions = false);
        Task<int> UpdateAsync(Survey survey, CancellationToken cancellationToken);
    }
}
