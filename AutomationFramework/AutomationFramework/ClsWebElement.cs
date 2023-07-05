using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework
{
    public class ClsWebElement
    {
        private IWebDriver driver = null;


        /// <summary>
        /// Inits driver that will be used to take screenshot in the report during execution time.
        /// </summary>
        /// <param name="driver"></param>
        public void InitDriver(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IList<IWebElement> fnGetWebList(By by) 
        {
            try 
            {
                IList<IWebElement> objElement = driver.FindElements(by);
                return objElement;
            }
            catch (Exception ex) 
            {
                return null;
            }

        }




    }
}
