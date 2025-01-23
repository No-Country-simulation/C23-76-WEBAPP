using Microsoft.Extensions.Logging;

namespace SurveyMaker.Application.Features.ExceptionHandlingBehavior
{
    public interface IExceptionHandler
    {
        Task HandleAsync(Exception exception);
    }

    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(Exception exception)
        {
            // Acá se puede registrar el error o realizar alguna otra acción
            await Task.Run(() => {
                _logger.LogError(exception, exception.Message);
            });
        }
    }
}
