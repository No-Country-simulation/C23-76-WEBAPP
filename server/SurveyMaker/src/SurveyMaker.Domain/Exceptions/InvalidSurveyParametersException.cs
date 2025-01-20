namespace SurveyMaker.Domain.Exceptions
{
    public class InvalidSurveyParametersException : DomainException
    {
        public InvalidSurveyParametersException(string message) 
            : base(message)
        {
            
        }
    }
}
