using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.webdrivernunitdemo.Pom_Pages
{
    public class HomePage_POM
    {

        IWebDriver driver; //We need a driver instance to work with

        public HomePage_POM(IWebDriver driver) //Which we will get when the class is instantiated
        {
            this.driver = driver;
        }

        //Locators
        IWebElement LoginLink => driver.FindElement(By.LinkText("Login To Restricted Area"));

        //Service Methods
        public void GoLogin()
        {
            LoginLink.Click();
        }

        //Only model what is needed - no tests need the basic examples area yet...
        //...but when/if they do we can return back here to add them

    }
}
