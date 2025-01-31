using SurveyMaker.Domain.Entities;

namespace SurveyMaker.Application.Models.Dtos
{
    public class SurveyLinkDto
    {
        public string? Url { get; set; }

        public static SurveyLinkDto Create(Survey survey)
        {
            var surveyLink = survey?.Url;
            return new SurveyLinkDto
            {
                Url = surveyLink?.Url
            };
        }
    }
}
