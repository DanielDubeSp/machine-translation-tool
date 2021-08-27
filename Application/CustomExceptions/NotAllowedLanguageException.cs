namespace Application.CustomExceptions
{
    public class NotAllowedLanguageException : TranslateException
    {
        public NotAllowedLanguageException(string language) : base($"Language {language} is not allowed")
        {

        }
    }
}
