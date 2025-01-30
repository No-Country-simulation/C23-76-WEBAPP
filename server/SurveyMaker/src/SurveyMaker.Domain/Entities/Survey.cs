using SurveyMaker.Domain.Enums;
using SurveyMaker.Domain.Exceptions;

namespace SurveyMaker.Domain.Entities
{
    public class Survey
    {
        public Survey()
        {
            Questions = new List<Question>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public SurveyType Type { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? StartsAt { get; set; }
        public bool AllowAnonymousVotes { get; set; }
        public int? VotesAmountRequiredToFinish { get; set; }
        public SurveyLink Url { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

        public class SurveyLink
        {
            public Guid Token { get; set; }
            public string Url { get; set; }
        }

        public static Survey Create(
            string title,
            DateTime? expiresAt,
            DateTime? startsAt,
            bool allowAnonymousVotes,
            int? votesAmountRequiredToFinish,
            string createdBy,
            SurveyLink surveyLink,
            ICollection<Question> questions)
        {
            if (expiresAt != null && votesAmountRequiredToFinish != null)
            {
                throw new InvalidSurveyParametersException(Constants.Errors.InvalidSurveyConfiguration);
            }

            if (expiresAt != null && expiresAt < DateTime.UtcNow)
            {
                throw new InvalidSurveyParametersException(Constants.Errors.InvalidSurveyExpirationTime);
            }

            var survey = new Survey
            {
                Title = title,
                AllowAnonymousVotes = allowAnonymousVotes,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow,
                ExpiresAt = startsAt,
                StartsAt = startsAt,
                Type = GetType(expiresAt, votesAmountRequiredToFinish),
                VotesAmountRequiredToFinish = votesAmountRequiredToFinish,
                Url = surveyLink,
                Questions = questions
            };

            return survey;
        }

        private static SurveyType GetType(DateTime? expiresAt, int? votesAmountRequiredToFinish)
        {
            if (expiresAt != null)
            {
                return SurveyType.TIME;
            }
            if (votesAmountRequiredToFinish != null)
            {
                return SurveyType.VOTES_AMOUNT;
            }

            return SurveyType.MANNUALLY_CLOSED;
        }


        public static Survey Update(
            Survey survey,
            string? title,
            DateTime? expiresAt,
            int? votesAmountRequiredToFinish)
        {
            switch(survey.Type)
            {
                case SurveyType.TIME:
                    if (expiresAt != null && expiresAt < DateTime.UtcNow)
                    {
                        throw new InvalidSurveyParametersException(Constants.Errors.InvalidSurveyExpirationTime);
                    }
                    survey.ExpiresAt = expiresAt ?? survey.ExpiresAt;
                    break;
                case SurveyType.VOTES_AMOUNT:
                    if (votesAmountRequiredToFinish != null && votesAmountRequiredToFinish <= 0)
                    {
                        throw new InvalidSurveyParametersException(Constants.Errors.InvalidVotesAmount);
                    }
                    survey.VotesAmountRequiredToFinish = votesAmountRequiredToFinish ?? survey.VotesAmountRequiredToFinish;
                    break;
            }

            survey.Title = title ?? survey.Title;
            return survey;
        }
    }
}
