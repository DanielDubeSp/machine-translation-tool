using Application.CustomExceptions;
using Application.Validators;
using Domain.Shared.Interfaces;
using Moq;
using Services.MachineTranslationTool.API.Services;
using System.Threading.Tasks;
using Xunit;

namespace Services.MachineTranslationTool.API.ServicesTests
{
    public class TranslateServiceTests
    {
        [Trait("Type", "Translate_Services")]
        [Trait("Version", "1.0")]
        [Fact]
        public async Task Test_IsOk_Mocked()
        {
            // Arrange
            var translator = new Mock<ITranslator>();
            var validator = new Mock<IAllowedLanguagesValidator>();
            var translatedText = "Hola";
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(translatedText));

            var srv = new TranslateService(translator.Object, validator.Object);

            // Act
            var actual = await srv.Translate("Hello", "en", "es");

            // Assert
            Assert.True(actual.IsOk);
            Assert.Null(actual.Error);
            Assert.Equal(translatedText, actual.TranslatedText);

        }

        [Trait("Type", "Translate_Services")]
        [Trait("Version", "1.0")]
        [Fact]
        public async Task Test_SourceLang_IsNotOk()
        {
            // Arrange
            var translator = new Mock<ITranslator>();
            var validator = new AllowedLanguagesValidator();
            var translatedText = "Hola";
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(translatedText));
            var badLang = "enn";
            var srv = new TranslateService(translator.Object, validator);

            // Act
            var actual = await Assert.ThrowsAsync<NotAllowedLanguageException>(async () => await srv.Translate("Hello", badLang, "es"));

            // Assert

            Assert.Equal($"Language '{badLang}' is not allowed", actual.Message);

        }

        [Trait("Type", "Translate_Services")]
        [Trait("Version", "1.0")]
        [Fact]
        public async Task Test_TargetLang_IsNotOk()
        {
            // Arrange
            var translator = new Mock<ITranslator>();
            var validator = new AllowedLanguagesValidator();
            var translatedText = "Hola";
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(translatedText));
            var badLang = "enn";
            var srv = new TranslateService(translator.Object, validator);

            // Act
            var actual = await Assert.ThrowsAsync<NotAllowedLanguageException>(async () => await srv.Translate("Hello", "en", badLang));

            // Assert

            Assert.Equal($"Language '{badLang}' is not allowed", actual.Message);
        }

        [Trait("Type", "Translate_Services")]
        [Trait("Version", "1.1")]
        [Fact]
        public async Task Test_EmptyText_IsOk()
        {
            // Arrange
            var translator = new Mock<ITranslator>();
            var validator = new AllowedLanguagesValidator();
            var translatedText = "";
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(translatedText));

            var srv = new TranslateService(translator.Object, validator);

            // Act
            var actual = await srv.Translate("", "en", "es");

            // Assert
            Assert.True(actual.IsOk);
            Assert.Null(actual.Error);
            Assert.Equal(translatedText, actual.TranslatedText);

        }
    }
}
