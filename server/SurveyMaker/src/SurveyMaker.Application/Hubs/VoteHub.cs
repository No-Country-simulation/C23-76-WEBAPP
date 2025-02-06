using Microsoft.AspNetCore.SignalR;

namespace SurveyMaker.Application.Hubs
{
    public class VoteHub : Hub
    {
        // Un usuario se une a un grupo basado en el surveyId
        public async Task JoinSurveyGroup(string surveyId)
        {
            Console.WriteLine($"The user has logged in to survey id: {surveyId}");

            if (string.IsNullOrWhiteSpace(surveyId))
                throw new ArgumentException("surveyId is required");

            await Groups.AddToGroupAsync(Context.ConnectionId, surveyId);
        }


        // Un usuario se desconecta del grupo cuando se va de la encuesta
        public async Task LeaveSurveyGroup(string surveyId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, surveyId);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var surveyId = httpContext.Request.Query["surveyId"];

            if (!string.IsNullOrEmpty(surveyId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, surveyId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Si el cliente pertenece a múltiples grupos, necesitarás una forma de rastrearlos
            await base.OnDisconnectedAsync(exception);
        }
        
    }
}
