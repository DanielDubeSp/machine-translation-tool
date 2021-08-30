using Application.Validators;
using Instrastructure.TranslationApis;
using Services.MachineTranslationTool.API.Services;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ServiceTests.BDD.Drivers
{
    public sealed class TranslateServiceDriver
    {
        private readonly ScenarioContext scenarioContext;

        public TranslateServiceDriver(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }
        public async Task<TranslateResponse> Translate(string text, string sourceLang, string targetLang)
        {
            var zaacTranslator = new ZaacGoogleTranslate();
            var validator = new AllowedLanguagesValidator();
            var translator = new TranslateService(zaacTranslator, validator);
            var result = await translator.Translate(text, sourceLang, targetLang);
            return result;
        }
    }
}
