using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static uk.co.edgewords.webdrivernunitdemo.Utilities.Helpers;


namespace uk.co.edgewords.webdrivernunitdemo.Test_Cases
{
    [TestFixture] //This annotation is optional, but I like it as it does clearly show this class is ment to contain nunit tests.
    public class FirstTests : Utilities.TestBase
    {
        

        //driver setup and fields come from TestBase inheritance. Ctrl-Click Utilities.TestBase above

        [Test, Order(2), Category("Smoke"), Category("And another")] //Order shouldnt matter for good tests. Category specifies a trait
        [Category("Another")] //You can "multi tag" by repeating the category annotation or adding again
        public void DragDropTest()
        {

            string username = Environment.GetEnvironmentVariable("SECRET_USERNAME");
            Console.WriteLine(username);
            Console.WriteLine(Environment.GetEnvironmentVariable("SECRET_PASSWORD"));

            //ChromeOptions options = new ChromeOptions();
            //options.AddArgument("start-maximized");
            //IWebDriver driver = new ChromeDriver(options);

            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2/docs/cssXPath.html";

            IWebElement gripper = driver.FindElement(By.CssSelector("#slider > a"));

            //Actions MyAction = new Actions(driver);
            //IAction DragAndDrop = MyAction.ClickAndHold(gripper)
            //                      .MoveByOffset(10, 0)
            //                      .MoveByOffset(10, 0)
            //                      .MoveByOffset(10, 0)
            //                      .MoveByOffset(10, 0)
            //                      .MoveByOffset(10, 0)
            //                      .MoveByOffset(10, 0)
            //                      .MoveByOffset(10, 0)
            //                      .Release() //Remember to release the mouse button
            //                      .Build(); //Finish the chain with a Build()


            //DragAndDrop.Perform();
            //DragAndDrop.Perform();



            IAction DragAndDrop = DragDropHelper(gripper, 100, 5, driver);
            DragAndDrop.Perform();
        }






        [Test]
        public void LoginLogoutEndToEnd([Values("edgewords","webdriver")]string testusername, [Values("edgewords123","notgoingtowork")]string testpassword)
        {
            Console.WriteLine("Start test"); //Log key events/times in your test
            
            //Navigate to a URL
            driver.Url = "https://www.edgewordstraining.co.uk/webdriver2";
            //Chrome - Find an element - then click it
            driver.FindElement(By.LinkText("Login To Restricted Area")).Click();

            //Where you need to do the same sort of thing in multiple places use helper methods
            //e.g. waiting - ctrl-click method name to have VS take you to the definition
            
            Console.WriteLine("On login page");

            //WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            //wait2.Until(drv => drv.FindElement(By.Id("username")).Displayed);
            WaitForElementToBeVisible(driver, By.Id("username"), 3);

            //Capturing and logging text from <elm>inside an element</elm>
            string heading = driver.FindElement(By.CssSelector("#right-column > h1")).Text;
            Console.WriteLine("The heading is:" + heading);

            IWebElement username = driver.FindElement(By.Id("username"));
            username.SendKeys("edgewords");
            username.SendKeys(Keys.Control + "a"); //Sending "special" keys that cab't be typed as part of a string
            username.SendKeys(Keys.Delete);
            // username.Clear(); //Also clears but is shorter and "magic"
            username.SendKeys(testusername); //Hardcoded Username

            //Capturing text in input elms text box
            string inputtext = username.GetAttribute("value");
            Console.WriteLine("The username is: " + inputtext);

            //Title of the page - not rendered on screen so get's a special method.
            string pagetitle = driver.Title;
            Console.WriteLine("Page title is:" + pagetitle);
     
            //Wont work - <title> is not rendered on screen in the page canvas
            string notpagetitle = driver.FindElement(By.CssSelector("head > title")).Text;
            Console.WriteLine("No text here:" + notpagetitle);

            driver.FindElement(By.Id("password")).SendKeys(testpassword); //Hardcoded password


            //Hi readers - Selenium WebDriver 4 has relative locators
            //If you cant make an easy locator for a target element, but you can for something near to it on screen
            //You could yse a relative locator - find x, then y will be above/below/left/right within 50px (by default)
            //seems to me like this might be brittle (easy to break), as sites can change layout (css) by window size
            //or on a whim: http://www.csszengarden.com/
            //IWebElement pw = driver.FindElement(RelativeBy.WithLocator(By.TagName("input")).Below(username));

            driver.FindElement(By.LinkText("Submit")).Click();
            Console.WriteLine("Hello");
            //After submit wait for login page to load

            //Explicit unconditional wait - we are "locked in" to the wait - not good.
            //Thread.Sleep(7000);

            //Use a webdriver wait ideally
            //WebDriverWait MyWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //MyWait.Until(anything => anything.FindElement(By.LinkText("Log Out")).Displayed);

            //But since we need lots of those typically, use a generic helper.
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until(drv => drv.FindElement(By.LinkText("Log Out")).Displayed);
            WaitForElementToBeVisible(driver, By.LinkText("Log Out"), 5);
            
            string bodyText = driver.FindElement(By.TagName("body")).Text;
            
            try
            {
                Assert.That(bodyText, Does.Contain("User is Logged in"), "We are not logged in");
            }
            catch (Exception)
            {
                //You can catch failed assertions - the test will still fail, but code execution continues beyond here
                Console.WriteLine("Continue...");
            }

            


            //Click log out - spawns js alert (/prompt)
            driver.FindElement(By.LinkText("Log Out")).Click();

            //Handle alert before doing anything else
            driver.SwitchTo().Alert().Accept();

            //Wait to complete logout
            Thread.Sleep(7000);
            driver.FindElement(By.LinkText("Register")).Click();

            string EndHeading = driver.FindElement(By.CssSelector("#right-column > h1")).Text;
 
            //"Classic" assertion style
            //Assert.True(EndHeading.Contains("Access and AuthenticatioN"), "Wrong heading");

            //"Constraint" assertion style - gives better errors and reads better in code
            Assert.That(EndHeading, Does.Contain("Access and Authentication"),"Wrong Heading");

            Console.WriteLine("Finished!");



            //Taking screenshots
            //Should be turned in to helper methods
            //Take a screenshot of the whole page
            ITakesScreenshot? ssdriver = driver as ITakesScreenshot;
            Screenshot screenshot = ssdriver.GetScreenshot();
            screenshot.SaveAsFile(@"d:\screenshots\myscreenshot.png", ScreenshotImageFormat.Png);
            //Take a screenshot of an element (WebDriver 4)
            IWebElement screenarea = driver.FindElement(By.CssSelector("#right-column"));
            ITakesScreenshot screenareass = screenarea as ITakesScreenshot;
            screenareass.GetScreenshot().SaveAsFile(@"d:\screenshots\myscreenshotarea.png", ScreenshotImageFormat.Png);


            //More writing to the report
            TestContext.AddTestAttachment(@"d:\screenshots\myscreenshotarea.png","Screen area");
            TestContext.WriteLine("Hi from test context");
            Console.WriteLine("Hi from console");

        }



    }
}
