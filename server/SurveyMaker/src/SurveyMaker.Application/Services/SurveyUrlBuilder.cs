using Microsoft.AspNetCore.Http;
using static SurveyMaker.Domain.Entities.Survey;

namespace SurveyMaker.Application.Services
{
    public class SurveyUrlBuilder : ISurveyUrlBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SurveyUrlBuilder(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public SurveyLink Build()
        {
            var token = Guid.NewGuid();
            var request = _httpContextAccessor.HttpContext.Request;
            return new SurveyLink
            {
                Token = token,
                Url = $"{request.Scheme}://{request.Host}{request.PathBase}/{token}"
            };
        }
    }
}
