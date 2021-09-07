using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using Xunit;

namespace SeleniumTests
{
    public class ClientTests : IDisposable
    {
        ChromeDriver driver;
        public ClientTests()
        {
            driver = new ChromeDriver
            {
                Url = "http://localhost:5010"
            };
        }
        public void Dispose()
        {
            driver?.Dispose();
        }

        [Fact]
        public void Test_English_Spanish()
        {
            var sourceLang = driver.FindElement(By.Id("sourceLang"));
            sourceLang.SendKeys("e");

            var targetLang = driver.FindElement(By.Id("targetLang"));
            targetLang.SendKeys("s");

            var text = driver.FindElement(By.Id("text"));
            text.SendKeys("Hello world!");

            var translateBtn = driver.FindElement(By.Id("translateBtn"));
            translateBtn.Click();

            var translatedText = driver.FindElement(By.Id("translatedText")).Text;
            Assert.Equal("\"¡Hola Mundo!\"", translatedText);
        }
        [Fact]
        public void Test_Spanish_English()
        {
            var sourceLang = driver.FindElement(By.Id("sourceLang"));
            sourceLang.SendKeys("s");

            var targetLang = driver.FindElement(By.Id("targetLang"));
            targetLang.SendKeys("e");

            var text = driver.FindElement(By.Id("text"));
            text.SendKeys("Hola mundo!");

            var translateBtn = driver.FindElement(By.Id("translateBtn"));
            translateBtn.Click();

            var translatedText = driver.FindElement(By.Id("translatedText")).Text;
            Assert.Equal("\"Hello World!\"", translatedText);
        }
        [Fact]
        public void Test_Spanish_Non_Existing_Lang()
        {
            var sourceLang = driver.FindElement(By.Id("sourceLang"));
            sourceLang.SendKeys("s");

            var targetLang = driver.FindElement(By.Id("targetLang"));
            targetLang.SendKeys("f");

            var text = driver.FindElement(By.Id("text"));
            text.SendKeys("Hola mundo!");

            var translateBtn = driver.FindElement(By.Id("translateBtn"));
            translateBtn.Click();

            var errorText = driver.FindElement(By.Id("translatedText")).Text;
            Assert.Contains("Language 'fr' is not allowed", errorText);
        }
    }
}
