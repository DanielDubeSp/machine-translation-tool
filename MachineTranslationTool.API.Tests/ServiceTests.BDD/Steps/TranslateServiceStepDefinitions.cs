using Application.CustomExceptions;
using Application.Validators;
using Instrastructure.TranslationApis;
using Services.MachineTranslationTool.API.Services;
using ServiceTests.BDD.Drivers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace ServiceTests.BDD.Steps
{
    [Binding]
    public sealed class TranslateServiceStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly TranslateServiceDriver translateServiceDriver;

        public TranslateServiceStepDefinitions(ScenarioContext scenarioContext, TranslateServiceDriver translateServiceDriver)
        {
            this.scenarioContext = scenarioContext;
            this.translateServiceDriver = translateServiceDriver;
        }
        [Scope(Feature = "TranslateService")]
        [Given("the text to translate (.*)")]
        public void GivenThetext(string text)
        {
            scenarioContext["text"] = text;
        }
        [Scope(Feature = "TranslateService")]
        [Given("an empty text to translate")]
        public void GivenTheEmptyText()
        {
            scenarioContext["text"] = string.Empty;
        }

        [Scope(Feature = "TranslateService")]
        [Given("the source language (.*)")]
        public void GivenTheSourceLanguage(string sourceLang)
        {
            scenarioContext["srcLang"] = sourceLang;
        }

        [Scope(Feature = "TranslateService")]
        [Given("the target language (.*)")]
        public void GivenTheTargetLanguage(string targetLang)
        {
            scenarioContext["tgtLang"] = targetLang;
        }

        [Scope(Feature = "TranslateService")]
        [When("the text is translated")]
        public async Task WhenThetextIsTranslated()
        {
            try
            {
                scenarioContext["result"] = await translateServiceDriver.Translate($"{scenarioContext["text"]}", $"{scenarioContext["srcLang"]}", $"{scenarioContext["tgtLang"]}");
            }
            catch (NotAllowedLanguageException ex)
            {
                scenarioContext["exception"] = ex;
            }
        }

        [Scope(Feature = "TranslateService")]
        [Then("the result text should be (.*)")]
        public void ThenTheResultShouldBe(string translatedText)
        {
            Assert.Equal(translatedText, ((TranslateResponse)scenarioContext["result"]).TranslatedText);
        }
        [Scope(Feature = "TranslateService")]
        [Then("the result text is empty")]
        public void ThenTheResultShouldBeEmpty()
        {
            Assert.Equal(string.Empty, ((TranslateResponse)scenarioContext["result"]).TranslatedText);
        }

        [Scope(Feature = "TranslateService")]
        [Then("a NotAllowedLanguageException exception should be thrown for language (.*)")]
        public void ThenTheResultShouldBeANotAllowedLanguageException(string language)
        {
            Assert.Equal(typeof(NotAllowedLanguageException), scenarioContext["exception"].GetType());
            Assert.Equal($"Language '{language}' is not allowed", ((NotAllowedLanguageException)scenarioContext["exception"]).Message);
        }
    }
}
