
namespace Services.MachineTranslationTool.API.Services
{
    /// <summary>
    ///     With this class we can give more info than a single value result
    /// </summary>
    public sealed class TranslateResponse
    {
        public string TranslatedText { get; set; }
        public string Error { get; set; }
        public bool IsOk => Error == null;
    }
}
