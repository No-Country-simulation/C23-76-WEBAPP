namespace SurveyMaker.Domain.Exceptions
{
    public class InvalidQuestionParametersException : DomainException
    {
        public InvalidQuestionParametersException(string message)
            : base(message)
        {
            
        }
    }
}
