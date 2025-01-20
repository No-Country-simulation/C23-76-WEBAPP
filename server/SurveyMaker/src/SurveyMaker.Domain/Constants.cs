namespace SurveyMaker.Domain
{
    public static class Constants
    {
        public static class Errors
        {
            public static string InvalidSurveyConfiguration = "Cannot set expiration date and votes amount at the same time.";
            public static string InvalidSurveyExpirationTime = "Expiration date cannot be older than now.";
        }
    }
}
