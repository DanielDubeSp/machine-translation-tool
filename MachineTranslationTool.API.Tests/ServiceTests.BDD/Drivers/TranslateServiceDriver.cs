using Application.Validators;
using Instrastructure.TranslationApis;
using Moq;
using Serilog;
using Services.MachineTranslationTool.API.Services;
using System.Threading.Tasks;

namespace ServiceTests.BDD.Drivers
{
    public sealed class TranslateServiceDriver
    {
        private Mock<ILogger> loggerMock;

        public TranslateServiceDriver()
        {
            loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.ForContext<It.IsAnyType>()).Returns(loggerMock.Object);
        }
        public async Task<TranslateResponse> Translate(string text, string sourceLang, string targetLang)
        {
            var zaacTranslator = new ZaacGoogleTranslate();
            var validator = new AllowedLanguagesValidator();
            var translator = new TranslateService(zaacTranslator, validator, loggerMock.Object);
            var result = await translator.Translate(text, sourceLang, targetLang);
            return result;
        }
    }
}
