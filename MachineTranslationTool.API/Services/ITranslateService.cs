using System.Threading.Tasks;

namespace Services.MachineTranslationTool.API.Services
{
    public interface ITranslateService
    {
        Task<string> Translate(string sourceText, string sourceLang, string targetLang);
    }
}