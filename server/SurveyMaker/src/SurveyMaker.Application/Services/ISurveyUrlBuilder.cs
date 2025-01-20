using static SurveyMaker.Domain.Entities.Survey;

namespace SurveyMaker.Application.Services
{
    public interface ISurveyUrlBuilder
    {
        SurveyLink Build();
    }
}
