using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uk.co.edgewords.webdrivernunitdemo.Pom_Pages;
using uk.co.edgewords.webdrivernunitdemo.Utilities;

namespace uk.co.edgewords.webdrivernunitdemo.Pom_Tests
{
    [TestFixture]
    public class LoginWithPOMTest : TestBase
    {
        [Test]
        public void Login()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";

            HomePage_POM home = new HomePage_POM(driver);
            home.GoLogin();
            


            LoginPage_POM login = new LoginPage_POM(driver);

            //Now use login service methods:
            //login.SetUsername("edgewords");
            //login.SetPassword("edgewords123");
            //login.SubmitForm();

            //Or a helper method
            //login.Login("edgewords", "edgewords123");

            //Or with fluent coding style (with methods that return their class instance)
            //login.SetUsername("edgewords").SetPassword("edgewords123").SubmitForm();

            //Could use the page's body text property to verify we are not already logged in
            //Console.WriteLine(login.BodyText); 

            //If expecting success then use an appropriately named method
            //The method should not assert in the page class but instead return something
            //the test can assert on
            bool LoggedIn = login.LoginWithValidCredentials("edgewords", "edgewords123");
            Assert.That(LoggedIn, Is.True, "We did not login");
            Thread.Sleep(3000);
            
        }
        [Test]
        public void AttemptLoginWithInvalidCredentials()
        {
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/";

            HomePage_POM home = new HomePage_POM(driver);
            home.GoLogin();

            LoginPage_POM login = new LoginPage_POM(driver);
            //Negative test uses a different method...
            bool LoggedIn = login.LoginWithInvalidCredentials("edgewords", "edgewords123");
            Assert.That(LoggedIn, Is.True, "We somehow logged in!!!");
        }
    }
}
