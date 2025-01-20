using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SurveyMaker.Application.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        private Guid? _userId { get; set; }

        public Guid UserId
        {
            get => _userId ?? Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            set => _userId = value;
        }
    }
}
