namespace Domain.Shared.Interfaces
{
    public interface IAllowedLanguagesValidator
    {
        void Validate(string text, string sourceLang, string targetLang);
    }
}
