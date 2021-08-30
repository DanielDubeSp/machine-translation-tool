using Application.CustomExceptions;
using Domain.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Validators
{
    public class AllowedLanguagesValidator: IAllowedLanguagesValidator
    {
        private readonly List<string> allowedList = new()
        {
            "en", "es", "de"
        };
        private bool Allowed(string language)
        {
            return allowedList.Contains(language);
        }

        public void Validate(string sourceText, string sourceLang, string targetLang)
        {
            if (string.IsNullOrEmpty(sourceText))
                return;
            if (string.IsNullOrEmpty(sourceLang))
                throw new ArgumentNullException("Please, provide source language");
            if (string.IsNullOrEmpty(targetLang))
                throw new ArgumentNullException("Please, provide target language");

            if (!Allowed(sourceLang))
                throw new NotAllowedLanguageException(sourceLang);
            if (!Allowed(targetLang))
                throw new NotAllowedLanguageException(targetLang);
        }
    }
}
