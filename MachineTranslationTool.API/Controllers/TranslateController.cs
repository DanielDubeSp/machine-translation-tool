﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
        private readonly ILogger logger;

        public TranslateController(ITranslateService translateService, ILogger logger)
        {
            this.translateService = translateService;
            this.logger = logger.ForContext<TranslateController>();
        }

        /// <summary>
        ///     Gets translation from source to target language specifiyng parameters
        /// </summary>
        /// <param name="text">The text to be translated</param>
        /// <param name="sourceLang">en,es,de</param>
        /// <param name="targetLang">en,es,de</param>
        /// <remarks>
        /// Request example:
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
            logger.Debug("Starting GetByParameters");
            logger.Verbose("SerializedData: Parameters -> {parameters}", new { text, sourceLang, targetLang });
            return await GenericResult(text, sourceLang, targetLang);
        }


        /// <summary>
        ///     Gets translation from source to target language by route
        /// </summary>
        /// <remarks>
        /// Request example:
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
            logger.Debug("Starting GetByRoute");
            logger.Verbose("SerializedData: Parameters -> {parameters}", new { text, sourceLang, targetLang });

            return await GenericResult(text, sourceLang, targetLang);
        }


        private async Task<IActionResult> GenericResult(string text, string sourceLang, string targetLang)
        {
            try
            {
                var result = await translateService.Translate(text, sourceLang, targetLang);

                logger.Information("Obtained result: {result}", new { result.IsOk, result.Error });
                logger.Verbose($"SerializedData: Translated from '{sourceLang}' to '{targetLang}'");
                logger.Verbose($"SerializedData: '{text}' to '{result.TranslatedText}'");

                return result.IsOk ? new OkObjectResult(result.TranslatedText) : Problem(result.Error, statusCode: StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                return Problem(title: "Error", detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
            finally
            {

            }
        }
    }
}
