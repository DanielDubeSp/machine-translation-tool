using System.Threading.Tasks;

namespace Domain.Shared.Interfaces
{
    public interface ITranslator
    {
        Task<string> Translate(string sourceText, string sourceLang, string targetLang);
    }
}
