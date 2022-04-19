using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uk.co.edgewords.webdrivernunitdemo.Utilities
{
    public static class Helpers
    {
        public static void WaitForElementToBeVisible(IWebDriver driver, By locator, int timeoutInSeconds)
        {
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait2.Until(drv => drv.FindElement(locator).Displayed);
        }

        public static Actions DragDropHelper(IWebElement Elm, int XOffsetDistance, int Smooth, IWebDriver driver)
        {
            int StepSize = XOffsetDistance / Smooth;

            Actions Actions = new Actions(driver);
            Actions.ClickAndHold(Elm); //Start building the action chain
            for (int i = 1; i <= Smooth; i++) //Loop adding small offsets
            {
                Actions.MoveByOffset(StepSize, 0);
            }
            Actions.Release().Build(); //Finish the action chain

            return Actions; //Return the chain
        }
    }
}
