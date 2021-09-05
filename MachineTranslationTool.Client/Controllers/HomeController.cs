using MachineTranslationTool.Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace MachineTranslationTool.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<Settings> settings;

        public HomeController(IOptionsSnapshot<Settings> settings)
        {
            this.settings = settings;
        }
        public ActionResult Index()
        {
            return View();
        }

        [Route("translate")]
        //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Translate(string sourceText, string sourceLang, string targetLang)
        {
            var url = settings.Value.ApiUrl;
            var escapedData = Uri.EscapeDataString($"{sourceText}");
            using var httpClient = new System.Net.Http.HttpClient();
            using var result = await httpClient.GetAsync($"{url}/api/translate/v1/?sourceLang={sourceLang}&targetLang={targetLang}&text={escapedData}");
            if (!result.IsSuccessStatusCode)
            {
                var badRes = await result.Content.ReadAsStringAsync();
                return Problem(detail: badRes, statusCode: (int)result.StatusCode);
            }

            var data = await result.Content.ReadAsStringAsync();
            return new OkObjectResult(data);
        }

    }
}