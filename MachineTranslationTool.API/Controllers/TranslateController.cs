using Application.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.MachineTranslationTool.API.Services;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Services.MachineTranslationTool.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class TranslateController : ControllerBase
    {
        private readonly ITranslateService translateService;
        private readonly ILogger<TranslateController> _logger;

        public TranslateController(ITranslateService translateService, ILogger<TranslateController> logger)
        {
            this.translateService = translateService;
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
        [ApiExplorerSettings(GroupName = "translate_services")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetByParameters(string text, string sourceLang, string targetLang)
        {
            try
            {
                var result = await translateService.Translate(text, sourceLang, targetLang);
                return new OkObjectResult(result);
            }
            catch (TranslateException ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (ArgumentNullException ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }

        }
        /// <summary>
        ///     Gets translation from source to target language
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sourceLang">en,es,de</param>
        /// <param name="targetLang">en,es,de</param>
        /// <returns></returns>
        [HttpGet("{sourceLang}/{targetLang}/{text}")]
        [ApiExplorerSettings(GroupName = "translate_services")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByRoute(string text, string sourceLang, string targetLang)
        {
            try
            {
                var result = await translateService.Translate(text, sourceLang, targetLang);
                return new OkObjectResult(result);
            }
            catch (TranslateException ex)
            {
                return Problem(title: "Error", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
