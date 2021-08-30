using Application.CustomExceptions;
using Domain.Shared.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.MachineTranslationTool.API.Services
{
    public class TranslateService : ITranslateService
    {
        private readonly ITranslator translator;
        private readonly IAllowedLanguagesValidator allowedLanguagesValidator;

        public TranslateService(ITranslator translator, IAllowedLanguagesValidator allowedLanguagesValidator)
        {
            this.translator = translator;
            this.allowedLanguagesValidator = allowedLanguagesValidator;
        }
        public async Task<string> Translate(string sourceText, string sourceLang, string targetLang)
        {
            // TODO: Move to MiddelWare?
            allowedLanguagesValidator.Validate(sourceText, sourceLang, targetLang);

            try
            {
                return await translator.Translate(sourceText, sourceLang, targetLang);
            }
            catch (Exception ex)
            {
                // TODO: Log Exception ex.Message
                throw new TranslateException("Translation error");
            }
        }
    }
}
