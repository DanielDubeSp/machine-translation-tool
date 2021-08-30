
namespace Services.MachineTranslationTool.API.Services
{
    /// <summary>
    ///     With this class we can give more info than a single value result
    /// </summary>
    public sealed class TranslateResponse
    {
        public string TranslatedText { get; init; }
        public string Error { get; init; }
        public bool IsOk => Error == null;
    }
}
