namespace SurveyMaker.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException()
        {
            
        }

        public DomainException(string message) : base(message)
        {

        }
    }
}
