
using System.Threading.Tasks;

namespace Services.MachineTranslationTool.API.Services
{
    public interface ITranslator
    {
        Task<string> Translate(string sourceText, string sourceLang, string targetLang);
    }
}
