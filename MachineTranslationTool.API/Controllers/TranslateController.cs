using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Diagnostics;

namespace MachineTranslationTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TranslateController : ControllerBase
    {
        private readonly ILogger<TranslateController> _logger;
        private readonly string pyPath = @"C:\Users\DANIEL.DUBE\AppData\Local\Programs\Python\Launcher\py.exe";

        public TranslateController(ILogger<TranslateController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="langTgt">en,es,de</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string text, string langTgt)
        {
            var testScript = Path.Combine(AppContext.BaseDirectory, "Translate.py");

            ProcessStartInfo start = new()
            {
                FileName = pyPath,
                Arguments = string.Format("{0} {1} {2}", testScript, "\"" + text + "\"", "\"" + langTgt + "\""),
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            using Process process = Process.Start(start);
            using StreamReader reader = process.StandardOutput;
            string result = reader.ReadToEnd();
            return new OkObjectResult(result);

        }
    }
}
