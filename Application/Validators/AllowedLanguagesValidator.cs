using Application.CustomExceptions;
using Domain.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Validators
{
    public class AllowedLanguagesValidator: IAllowedLanguagesValidator
    {
        private List<string> allowedList = new List<string>
        {
            "en", "es", "de"
        };
        private bool Allowed(string language)
        {
            return allowedList.Contains(language);
        }

        public void Validate(string sourceText, string sourceLang, string targetLang)
        {
            if (string.IsNullOrEmpty(sourceText) || string.IsNullOrEmpty(sourceLang) || string.IsNullOrEmpty(targetLang))
                throw new ArgumentNullException("Please, provide all the arguments");

            if (!Allowed(sourceLang))
                throw new NotAllowedLanguageException(sourceLang);
            if (!Allowed(targetLang))
                throw new NotAllowedLanguageException(targetLang);
        }
    }
}
