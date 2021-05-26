using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace HomeTaskNumber2
{
    [TestFixture]
    public class Tests
    {
        IWebDriver driver;

        private readonly string testPageUrl = "http://automationpractice.com/index.php";

        #region Locators
        private readonly By loginButtonMainPage = By.CssSelector("a.login");
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
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);  //implicit wait declaration
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
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5)); //explicit wait declaration
            wait.Until(ExpectedConditions.ElementIsVisible(loginButtonMainPage));
            driver.FindElement(loginButtonMainPage).Click();
            IWebElement emailInput = driver.FindElement(emailField);
            wait.Until(ExpectedConditions.ElementIsVisible(emailField));
            emailInput.Clear();
            emailInput.SendKeys(email);
            IWebElement passInput = driver.FindElement(passwordField);
            passInput.Clear();
            passInput.SendKeys(password);
            driver.FindElement(signinButton).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(error));
            Assert.IsTrue(driver.FindElement(error).Displayed);
            Assert.IsTrue(driver.FindElement(errorMessage).Displayed);
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
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(5)); //explicit wait declaration
            wait.Until(ExpectedConditions.ElementIsVisible(loginButtonMainPage));
            driver.FindElement(loginButtonMainPage).Click();
            IWebElement emailInput = driver.FindElement(emailField);
            wait.Until(ExpectedConditions.ElementIsVisible(emailField));
            emailInput.Clear();
            emailInput.SendKeys(email);
            IWebElement passInput = driver.FindElement(passwordField);
            passInput.Clear();
            passInput.SendKeys(password);
            driver.FindElement(signinButton).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(error));
            Assert.IsTrue(driver.FindElement(error).Displayed);
            Assert.IsTrue(driver.FindElement(errorMessage).Displayed);
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