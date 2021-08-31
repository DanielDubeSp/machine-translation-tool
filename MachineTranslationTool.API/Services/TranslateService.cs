using Domain.Shared.Interfaces;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Services.MachineTranslationTool.API.Services
{
    public sealed class TranslateService : ITranslateService
    {
        private readonly ITranslator translator;
        private readonly IAllowedLanguagesValidator allowedLanguagesValidator;
        private readonly ILogger logger;

        public TranslateService(ITranslator translator, IAllowedLanguagesValidator allowedLanguagesValidator, ILogger logger)
        {
            this.translator = translator;
            this.allowedLanguagesValidator = allowedLanguagesValidator;
            this.logger = logger.ForContext<TranslateService>();
        }
        public async Task<TranslateResponse> Translate(string sourceText, string sourceLang, string targetLang)
        {
            logger.Debug("Starting TranslateService.Translate");
            // TODO: Move to MiddelWare?

            logger.Verbose($"SerializedData: Translating '{sourceText}' from '{sourceLang}' to '{targetLang}'");
            logger.Debug("Validating parameters");
            
            allowedLanguagesValidator.Validate(sourceText, sourceLang, targetLang);

            try
            {
                var result = new TranslateResponse(await translator.Translate(sourceText, sourceLang, targetLang));

                logger.Information("TranslateService.Translate: Obtained result");
                logger.Verbose($"SerializedData: '{sourceText}' to '{result.TranslatedText}'");

                return result;

            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return TranslateResponse.TranslateResponseError("Translation error");
            }
            finally
            {
                logger.Debug("End TranslateService.Translate");
            }
        }
    }
}
