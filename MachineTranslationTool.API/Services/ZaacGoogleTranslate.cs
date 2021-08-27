
using System;
using System.Threading.Tasks;
using Zaac.GoogleTranslateApi;

namespace Services.MachineTranslationTool.API.Services
{
    public class ZaacGoogleTranslate : ITranslator
    {
        public ZaacGoogleTranslate()
        {

        }
        public async Task<string> Translate(string sourceText, string sourceLang, string targetLang)
        {
            var translator = new GoogleTranslator();
            var result2 = await translator.TranslateAsync(sourceText, sourceLang, targetLang);

            return result2.TargetText;
        }

    }
}
