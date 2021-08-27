
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Instrastructure.TranslationApis
{
    [Obsolete("Not working anymore")]
    public class GoogleTransNew : Domain.Shared.Interfaces.ITranslator
    {
        private readonly string pyPath = @"C:\Users\DANIEL.DUBE\AppData\Local\Programs\Python\Launcher\py.exe";
        public Task<string> Translate(string sourceText, string sourceLang, string targetLang)
        {
            var testScript = Path.Combine(AppContext.BaseDirectory, "Translate.py");

            ProcessStartInfo start = new()
            {
                FileName = pyPath,
                Arguments = string.Format("{0} {1} {2} {3}", testScript, "\"" + sourceText + "\"", "\"" + sourceLang + "\"", "\"" + targetLang + "\""),
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            using Process process = Process.Start(start);
            using StreamReader reader = process.StandardOutput;
            string result = reader.ReadToEnd();
            return Task.FromResult(result);
        }

        public Task<string> Translate(string sourceText, string targetLang)
        {
            var testScript = Path.Combine(AppContext.BaseDirectory, "Translate.py");

            ProcessStartInfo start = new()
            {
                FileName = pyPath,
                Arguments = string.Format("{0} {1} {2}", testScript, "\"" + sourceText + "\"", "\"" + targetLang + "\""),
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            using Process process = Process.Start(start);
            using StreamReader reader = process.StandardOutput;
            string result = reader.ReadToEnd();
            return Task.FromResult(result);
        }
    }
}
