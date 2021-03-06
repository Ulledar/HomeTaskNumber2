using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace HomeTaskNumber2
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;

        private readonly string testPageUrl = "http://automationpractice.com/index.php";

        #region Locators
        private readonly By signinButtonMainPage = By.CssSelector("a.login"); //By.XPath("//a[contains(text(), 'Sign in')]");
        private readonly By emailField = By.XPath("//form[@class='box']//input[@name='email']"); //By.CssSelector("#email");
        private readonly By passwordField = By.XPath("//input[@name='passwd']"); //By.CssSelector("input[name='passwd']");
        private readonly By signinButton = By.CssSelector("button[name='SubmitLogin']"); //By.XPath("//button[@name='SubmitLogin']"); //By.Id("SubmitLogin");
        private readonly By error = By.XPath("//p[contains(text(), 'There is 1 error')]");
        private readonly By errorMessage = By.XPath("//li[contains(text(), 'Invalid email address.')]");
        #endregion

        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl(testPageUrl);
        }

        [Parallelizable]
        [TestCaseSource("TestData")]
        [Author("AlexGrech")]
        [Category("Test case ID: 2")]
        [Description("Verify that it is possible to login with valid credentials")]
        public void Test1(string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(signinButtonMainPage));
            driver.FindElement(signinButtonMainPage).Click();
            IWebElement emailInput = driver.FindElement(emailField);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(emailField));
            emailInput.Clear();
            emailInput.SendKeys(email);
            IWebElement passInput = driver.FindElement(passwordField);
            passInput.Clear();
            passInput.SendKeys(password);
            driver.FindElement(signinButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(error));
            Assert.IsTrue(driver.FindElement(error).Displayed, "No error");
            Assert.IsTrue(driver.FindElement(errorMessage).Displayed, "No error text shown");
        }

        [Parallelizable]
        [TestCase("JohnDoe", "passw0rd")]
        [TestCase("LiliaJY", "isNotMe")]
        [TestCase("GoingTo", "BeAuto!")]
        [Author("AlexGrech")]
        [Category("Test case ID: 2")]
        [Description("Verify that it is possible to login with valid credentials")]
        public void Test2(string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(signinButtonMainPage));
            driver.FindElement(signinButtonMainPage).Click();
            IWebElement emailInput = driver.FindElement(emailField);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(emailField));
            emailInput.Clear();
            emailInput.SendKeys(email);
            IWebElement passInput = driver.FindElement(passwordField);
            passInput.Clear();
            passInput.SendKeys(password);
            driver.FindElement(signinButton).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(error));
            Assert.IsTrue(driver.FindElement(error).Displayed, "No error");
            Assert.IsTrue(driver.FindElement(errorMessage).Displayed, "No error text shown");
        }

        public static IEnumerable<TestCaseData> TestData
        {
            get
            {
                yield return new TestCaseData("JohnDoe", "passw0rd");
                yield return new TestCaseData("LiliaJY", "isNotMe");
                yield return new TestCaseData("GoingTo", "BeAuto!");
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}