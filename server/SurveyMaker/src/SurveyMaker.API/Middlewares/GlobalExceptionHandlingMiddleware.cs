using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SurveyMaker.Domain.Exceptions;
using System.Net;

namespace SurveyMaker.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Detail = exception.Message
            };

            switch(exception.GetBaseException())
            {
                case DomainException ex:
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            httpContext.Response.StatusCode = (int)problemDetails.Status;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
            
            return true;
        }
    }
}
