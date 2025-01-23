using MediatR;
using Microsoft.Extensions.Logging;


namespace SurveyMaker.Application.Features.ExceptionHandlingBehavior
{
    public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : notnull
    {
        private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;
        private readonly IExceptionHandler _exceptionHandler;

        public ExceptionHandlingBehavior(
            ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger,
            IExceptionHandler exceptionHandler)
        {
            _logger = logger;
            _exceptionHandler = exceptionHandler;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                await _exceptionHandler.HandleAsync(ex);
                throw;

            }
        }
    }
}
