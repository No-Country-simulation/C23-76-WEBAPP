namespace SurveyMaker.Domain
{
    public static class Constants
    {
        public static class Errors
        {
            //Survey
            public static string InvalidSurveyConfiguration = "Cannot set expiration date and votes amount at the same time.";
            public static string InvalidSurveyExpirationTime = "Expiration date cannot be older than now.";

            //Question
            public static string InvalidQuestionYesNoConfiguaration = "If question type is Yes/No could't have more than 2 options.";
            public static string InvalidQuestionOptionsMinAmount = "If question is multiple should have at least 2 questions";
            public static string InvalidQuestionMoreSelectionsThanOptions = "Cannot have more options selections than options.";
        }
    }
}
