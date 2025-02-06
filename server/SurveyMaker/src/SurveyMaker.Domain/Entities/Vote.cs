using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SurveyMaker.Domain.Entities
{
    public class Vote
    {
        public Vote()
        {
            Answers = new List<VoteAnswer>();
        }

        public int Id { get; set; }
        public int SurveyId { get; set; }
        public virtual Survey? Survey { get; set; }
        public string VoterId { get; set; }
        public DateTime VotedAt { get; set; }

        // Esta es la propiedad que se mapea a la base de datos como un JSON
        public string AnswersJson { get; set; }  // Aquí almacenaremos la lista de respuestas como un JSON

        // Propiedad que representa las respuestas como una lista en el código
        [NotMapped]
        public virtual ICollection<VoteAnswer> Answers
        {
            get => DeserializeAnswers();
            set => AnswersJson = SerializeAnswers(value); // ✅ Ahora asigna el valor serializado
        }


        // Método para serializar la lista de respuestas a JSON
        private string SerializeAnswers(ICollection<VoteAnswer> answers)
        {
            return JsonSerializer.Serialize(answers);
        }


        // Método para deserializar el JSON de respuestas
        private ICollection<VoteAnswer> DeserializeAnswers()
        {
            return string.IsNullOrEmpty(AnswersJson)
                ? new List<VoteAnswer>()
                : JsonSerializer.Deserialize<List<VoteAnswer>>(AnswersJson);
        }

        public static Vote Create(int surveyId, DateTime votedAt, string userId, ICollection<VoteAnswer> answers)
        {
            return new Vote
            {
                SurveyId = surveyId,
                VotedAt = votedAt,
                VoterId = userId,
                Answers = answers
            };
        }
    }


    public class VoteAnswer
    {
        public VoteAnswer()
        {
            SelectedOptionsIds = new List<int>();
        }

        public int QuestionId { get; set; }
        public ICollection<int> SelectedOptionsIds { get; set; }

        public static VoteAnswer Create(int questionId, ICollection<int> selectedOptionsIds)
        {
            return new VoteAnswer
            {
                QuestionId = questionId,
                SelectedOptionsIds = selectedOptionsIds
            };
        }
    }


    public class VoteCounter
    {
        public int Id { get; set; }
        public int OptionId { get; set; } // Relacionado con la opción seleccionada en la pregunta
        public int Counter { get; set; } // Número de votos para esta opción

        public virtual Option Option { get; set; } // Relación con la opción
    }
}