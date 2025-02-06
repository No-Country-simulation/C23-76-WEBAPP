using MediatR;
using Microsoft.AspNetCore.SignalR;
using SurveyMaker.Application.Hubs;
using SurveyMaker.Application.Models.Dtos;
using SurveyMaker.Application.Services;
using SurveyMaker.Domain.Entities;
using SurveyMaker.Domain.Repositories;

namespace SurveyMaker.Application.Features.CreateVote
{
    public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, VoteDto>
    {
        private readonly IUserContext _userContext;
        private readonly IVoteRepository _voteRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IVoteCounterRepository _voteCounterRepository;
        private readonly IHubContext<VoteHub> _hubContext;

        public CreateVoteCommandHandler(IUserContext userContext, IVoteRepository voteRepository, ISurveyRepository surveyRepository, IVoteCounterRepository voteCounterRepository, IHubContext<VoteHub> hubContext)
        {
            _userContext = userContext;
            _voteRepository = voteRepository;
            _surveyRepository = surveyRepository;
            _voteCounterRepository = voteCounterRepository;
            _hubContext = hubContext;
        }

        public async Task<VoteDto> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
        {
            var survey = await _surveyRepository.GetByIdAsync(request.SurveyId, withQuestions: true);

            if (survey == null)
            {
                throw new NullReferenceException($"Survey with id {request.SurveyId} not found.");
            }

            if (request.Answers == null || !request.Answers.Any())
            {
                throw new ArgumentException("At least one answer must be provided.");
            }

            foreach (var answer in request.Answers)
            {
                if (!survey.Questions.Any(q => q.Id == answer.QuestionId))
                {
                    throw new ArgumentException($"Question {answer.QuestionId} does not belong to the survey.");
                }
            }

            var userId = string.Empty;
            try
            {
                userId = _userContext.UserId.ToString();
            }
            catch (Exception)
            {
                userId = "anonymous";

                if (survey.AllowAnonymousVotes == false)
                    throw new Exception("Anonymous votes are not allowed.");
            }

            var vote = Vote.Create(
                surveyId: survey.Id,
                votedAt: request.VotedAt,
                userId: userId,
                answers: request.Answers.Select(x => VoteAnswer.Create(
                    questionId: x.QuestionId,
                    selectedOptionsIds: x.selectedOptionsIds))
                .ToList());

            try
            {
                await _voteRepository.SaveAsync(vote, cancellationToken);
                await UpdateVoteCountersAsync(vote.Answers, request.SurveyId, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving vote.", ex);
            }

            return VoteDto.Create(vote);
        }

        private async Task UpdateVoteCountersAsync(ICollection<VoteAnswer> answers, int surveyId, CancellationToken cancellationToken)
        {
            var updatedCounters = new List<VoteCounter>();

            foreach (var answer in answers)
            {
                foreach (var optionId in answer.SelectedOptionsIds)
                {
                    var voteCounter = await _voteCounterRepository.GetByOptionIdAsync(optionId);
                    if (voteCounter != null)
                    {
                        voteCounter.Counter++;
                    }
                    else
                    {
                        voteCounter = new VoteCounter { OptionId = optionId, Counter = 1 };
                        await _voteCounterRepository.AddAsync(voteCounter, cancellationToken);
                    }

                    updatedCounters.Add(voteCounter);
                }
            }

            await _voteCounterRepository.SaveChangesAsync(cancellationToken);

            // Notificar a todos los clientes conectados sobre la actualización
            await _hubContext.Clients.Group(surveyId.ToString()).SendAsync("UpdateVoteCount", updatedCounters.AsReadOnly(), cancellationToken: cancellationToken); // test
            //await _hubContext.Clients.Group(surveyId.ToString()).SendAsync("UpdateVoteCount", updatedCounters); // original


        }
    }
}
