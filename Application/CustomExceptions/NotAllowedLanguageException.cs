namespace Application.CustomExceptions
{
    public sealed class NotAllowedLanguageException : TranslateException
    {
        public NotAllowedLanguageException(string language) : base($"Language '{language}' is not allowed")
        {

        }
    }
}
