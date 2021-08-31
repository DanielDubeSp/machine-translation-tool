using Application.CustomExceptions;
using Application.Validators;
using Domain.Shared.Interfaces;
using Moq;
using Serilog;
using Services.MachineTranslationTool.API.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Services.MachineTranslationTool.API.ServicesTests
{
    public class TranslateServiceTests
    {
        private Mock<ILogger> loggerMock;

        public TranslateServiceTests()
        {
            loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.ForContext<It.IsAnyType>()).Returns(loggerMock.Object);
            loggerMock.Setup(x => x.Error(It.IsAny<string>()))
                .Callback((string messageTemplate) => { });
        }

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

            var srv = new TranslateService(translator.Object, validator.Object, loggerMock.Object);

            // Act
            var actual = await srv.Translate("Hello", "en", "es");

            // Assert
            Assert.True(actual.IsOk);
            Assert.Null(actual.Error);
            Assert.Equal(translatedText, actual.TranslatedText);

            // Verify mocked methods called

            validator.Verify(x => x.Validate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            loggerMock.Verify(x => x.Information(It.IsAny<string>()), Times.Once);
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Exactly(3));
            loggerMock.Verify(x => x.Verbose(It.IsAny<string>()), Times.Exactly(2));
            loggerMock.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never);

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
            var srv = new TranslateService(translator.Object, validator, loggerMock.Object);

            // Act
            var actual = await Assert.ThrowsAsync<NotAllowedLanguageException>(async () => await srv.Translate("Hello", badLang, "es"));

            // Assert

            Assert.Equal($"Language '{badLang}' is not allowed", actual.Message);

            // Verify mocked methods called
            translator.Verify(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            loggerMock.Verify(x => x.Information(It.IsAny<string>()), Times.Never);
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Exactly(2));
            loggerMock.Verify(x => x.Verbose(It.IsAny<string>()), Times.Exactly(1));
            loggerMock.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never);

        }

        [Trait("Type", "Translate_Services")]
        [Trait("Version", "1.0")]
        [Fact]
        public async Task Test_Translator_Throws_Exception()
        {
            // Arrange
            var translator = new Mock<ITranslator>();
            var validator = new AllowedLanguagesValidator();
            translator.Setup(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();
            var srv = new TranslateService(translator.Object, validator, loggerMock.Object);

            // Act
            var actual = await srv.Translate("Hello", "en", "es");

            // Assert
            Assert.False(actual.IsOk);
            Assert.NotNull(actual.Error);

            // Verify mocked methods called
            translator.Verify(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            loggerMock.Verify(x => x.Information(It.IsAny<string>()), Times.Never);
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Exactly(3));
            loggerMock.Verify(x => x.Verbose(It.IsAny<string>()), Times.Exactly(1));
            loggerMock.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);

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
            var srv = new TranslateService(translator.Object, validator, loggerMock.Object);

            // Act
            var actual = await Assert.ThrowsAsync<NotAllowedLanguageException>(async () => await srv.Translate("Hello", "en", badLang));

            // Assert

            Assert.Equal($"Language '{badLang}' is not allowed", actual.Message);

            // Verify mocked methods called
            translator.Verify(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            loggerMock.Verify(x => x.Information(It.IsAny<string>()), Times.Never);
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Exactly(2));
            loggerMock.Verify(x => x.Verbose(It.IsAny<string>()), Times.Exactly(1));
            loggerMock.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never);
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

            var srv = new TranslateService(translator.Object, validator, loggerMock.Object);

            // Act
            var actual = await srv.Translate("", "en", "es");

            // Assert
            Assert.True(actual.IsOk);
            Assert.Null(actual.Error);
            Assert.Equal(translatedText, actual.TranslatedText);


            // Verify mocked methods called
            translator.Verify(x => x.Translate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            loggerMock.Verify(x => x.Information(It.IsAny<string>()), Times.Once);
            loggerMock.Verify(x => x.Debug(It.IsAny<string>()), Times.Exactly(3));
            loggerMock.Verify(x => x.Verbose(It.IsAny<string>()), Times.Exactly(2));
            loggerMock.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>()), Times.Never);
        }
    }
}
