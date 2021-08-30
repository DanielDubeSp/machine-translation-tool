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
        /// Gets translation from source to target language specifiyng parameters
        /// </summary>
        /// <param name="text">The text to be translated</param>
        /// <param name="sourceLang">en,es,de</param>
        /// <param name="targetLang">en,es,de</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/translate/v1/?text=This is my text&amp;sourceLang=en&amp;targetLang=es
        ///
        /// </remarks>
        /// <returns>A JSON result</returns>
        /// <response code="200">Returns the translated text</response>
        /// <response code="500">Returns a JSON with error details</response>         
        [HttpGet]
        [ApiExplorerSettings(GroupName = "translate_services")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        /// Gets translation from source to target language by route
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/translate/v1/en/es/This is my text
        ///
        /// </remarks>
        /// <param name="text">The text to be translated</param>
        /// <param name="sourceLang">en,es,de</param>
        /// <param name="targetLang">en,es,de</param>
        /// <returns>A JSON result</returns>
        /// <response code="200">Returns the translated text</response>
        /// <response code="500">Returns a JSON with error details</response>        
        [HttpGet("{sourceLang}/{targetLang}/{text}")]
        [ApiExplorerSettings(GroupName = "translate_services")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
