using MediatR;

namespace SurveyMaker.Application.Features.DeleteOption
{
    public class DeleteOptionCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
