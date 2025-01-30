using MediatR;

namespace SurveyMaker.Application.Features.DeleteQuestion
{
    public class DeleteQuestionCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
