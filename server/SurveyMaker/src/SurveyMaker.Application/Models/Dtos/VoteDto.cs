using SurveyMaker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyMaker.Application.Models.Dtos
{
    public  class VoteDto
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public DateTime VotedAt { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }

        public static VoteDto Create(Vote vote)
        {
            return new VoteDto
            {
                Id = vote.Id,
                SurveyId = vote.SurveyId,
                VotedAt = vote.VotedAt,
                Answers = vote.Answers.Select(x => new AnswerDto
                {
                    QuestionId = x.QuestionId,
                    SelectedOptionsIds = x.SelectedOptionsIds,
                })
                .ToList()
            };
        }
    }

    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public ICollection<int> SelectedOptionsIds { get; set; }
    }
}
