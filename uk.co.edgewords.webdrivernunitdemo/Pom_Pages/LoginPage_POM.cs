using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace uk.co.edgewords.webdrivernunitdemo.Pom_Pages
{
    public class LoginPage_POM
    {
        IWebDriver driver;

        public LoginPage_POM(IWebDriver driver)
        {
            this.driver = driver;
            string pagetitle = driver.Title;
            Assert.That(pagetitle, Is.EqualTo("Automated Tools Test Site"));
        }

        public string BodyText { get => driver.FindElement(By.TagName("body")).Text; }

        //Locators
        //wait when finding UsernameField
        IWebElement UsernameField => new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(drv => drv.FindElement(By.Id("username")));
        //public fields can be used from the test class
        //if we consider it the job of a test to interact with elements directly.
        public IWebElement PasswordField => driver.FindElement(By.Id("password"));
        IWebElement Submit => driver.FindElement(By.LinkText("Submit"));

        //Service

        //These are written a bit "Java"ish. In C# land these should probably be written as getters and setters (accessors) on a field.
        public LoginPage_POM SetUsername(string username)
        {
            UsernameField.Clear();
            UsernameField.SendKeys(username);
            return this;
        }

        public LoginPage_POM SetPassword(string password)
        {
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            return this;
        }

        public void SubmitForm()
        {
            Submit.Click();
        }

        //Helpers
        public void Login(string username, string password)
        {
            SetUsername(username);
            SetPassword(password);
            SubmitForm();
        }

        //If different inputs lead to expected different results, model them as different methods.
        public bool LoginWithValidCredentials(string username, string password)
        {
            Login(username, password);
            Thread.Sleep(3000);

            try
            {
                driver.SwitchTo().Alert(); //If alert is present we did not login
            }
            catch (Exception)
            {
                return true; //No alert we logged in, return true
            
            }
            return false; //Alert was present so return false

        }

        public bool LoginWithInvalidCredentials(string username, string password)
        {
            Login(username, password);
            Thread.Sleep(3000);

            try
            {
                driver.SwitchTo().Alert(); //If alert is present we did not login
            }
            catch (Exception)
            {
                return false; //No alert we logged in, return true

            }
            return true; //Alert was present so return false

        }

    }
}
