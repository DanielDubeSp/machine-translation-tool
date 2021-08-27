using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.MachineTranslationTool.API.Services;
using System.Threading.Tasks;

namespace Services.MachineTranslationTool.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class TranslateController : ControllerBase
    {
        private readonly ITranslator translator;
        private readonly ILogger<TranslateController> _logger;

        public TranslateController(ITranslator translator, ILogger<TranslateController> logger)
        {
            this.translator = translator;
            _logger = logger;
        }

        /// <summary>
        ///     Gets translation from source to target language
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sourceLang">en,es,de</param>
        /// <param name="targetLang">en,es,de</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetByParameters(string text, string sourceLang, string targetLang)
        {
            var result = await translator.Translate(text, sourceLang, targetLang);
            return new OkObjectResult(result);

        }
        /// <summary>
        ///     Gets translation from source to target language
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sourceLang">en,es,de</param>
        /// <param name="targetLang">en,es,de</param>
        /// <returns></returns>
        [HttpGet("{sourceLang}/{targetLang}/{text}")]
        public async Task<IActionResult> GetByRoute(string text, string sourceLang, string targetLang)
        {
            var result = await translator.Translate(text, sourceLang, targetLang);
            return new OkObjectResult(result);

        }
    }
}
