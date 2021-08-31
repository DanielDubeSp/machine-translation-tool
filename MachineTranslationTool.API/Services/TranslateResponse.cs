
namespace Services.MachineTranslationTool.API.Services
{
    /// <summary>
    ///     With this class we can give more info than a single value result
    /// </summary>
    public sealed class TranslateResponse
    {
        public TranslateResponse(string translatedText)
        {
            TranslatedText = translatedText;
        }
        private TranslateResponse()
        {

        }
        public static TranslateResponse TranslateResponseError(string error) // Error builder
        {
            return new TranslateResponse
            {
                Error = error
            };
        }
        /// <summary>
        ///     Gets translated text. Null if there is an error
        /// </summary>
        public string TranslatedText { get; }

        /// <summary>
        ///     Gets error text. Null when there is no errors
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        ///     Gets the response status
        /// </summary>
        public bool IsOk => Error == null;
    }
}
