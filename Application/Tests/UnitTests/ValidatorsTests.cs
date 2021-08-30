using Application.CustomExceptions;
using Application.Validators;
using Domain.Shared.Interfaces;
using System;
using Xunit;

namespace Application.UnitTests
{
    public class ValidatorsTests
    {
        [Fact]
        public void Test_Allowed_Lang_Validator()
        {
            // Arrange
            IAllowedLanguagesValidator validator = new AllowedLanguagesValidator();

            // Act
            validator.Validate(text: "A text", sourceLang: "en", targetLang: "es");

            // Assert
            Assert.True(true);

        }
        [Fact]
        public void Test_Allowed_Lang_Validator_Null_Text()
        {
            // Arrange
            IAllowedLanguagesValidator validator = new AllowedLanguagesValidator();

            // Act
            validator.Validate(text: null, sourceLang: "en", targetLang: "es");

            // Assert
            Assert.True(true);

        }
        [Fact]
        public void Test_Allowed_Lang_Validator_Empty_Text()
        {
            // Arrange
            IAllowedLanguagesValidator validator = new AllowedLanguagesValidator();

            // Act
            validator.Validate(text: string.Empty, sourceLang: "en", targetLang: "es");

            // Assert
            Assert.True(true);

        }
        [Fact]
        public void Test_Allowed_Lang_Validator_Null_Src_Lang()
        {
            // Arrange
            IAllowedLanguagesValidator validator = new AllowedLanguagesValidator();
            var expetedExceptionMessage = "Value cannot be null. (Parameter 'Please, provide source language')";

            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => validator.Validate(text: "Hello", sourceLang: null, targetLang: null));

            // Assert
            Assert.Equal(expetedExceptionMessage, actual.Message);

        }
        [Fact]
        public void Test_Allowed_Lang_Validator_Null_Target_Lang()
        {
            // Arrange
            IAllowedLanguagesValidator validator = new AllowedLanguagesValidator();
            var expetedExceptionMessage = "Value cannot be null. (Parameter 'Please, provide target language')";

            // Act
            var actual = Assert.Throws<ArgumentNullException>(() => validator.Validate(text: "Hello", sourceLang: "es", targetLang: null));

            // Assert
            Assert.Equal(expetedExceptionMessage, actual.Message);

        }
        [Fact]
        public void Test_Allowed_Lang_Validator_Bad_Source_Lang()
        {
            // Arrange
            IAllowedLanguagesValidator validator = new AllowedLanguagesValidator();
            var badLang = "ess";
            // Act
            var actual = Assert.Throws<NotAllowedLanguageException>(() => validator.Validate(text: "Hello", sourceLang: badLang, targetLang: "en"));

            // Assert
            Assert.Equal($"Language '{badLang}' is not allowed", actual.Message);

        }
        [Fact]
        public void Test_Allowed_Lang_Validator_Bad_Target_Lang()
        {
            // Arrange
            IAllowedLanguagesValidator validator = new AllowedLanguagesValidator();
            var badLang = "ess";
            // Act
            var actual = Assert.Throws<NotAllowedLanguageException>(() => validator.Validate(text: "Hello", sourceLang: "en", targetLang: badLang));

            // Assert
            Assert.Equal($"Language '{badLang}' is not allowed", actual.Message);

        }
    }
}
