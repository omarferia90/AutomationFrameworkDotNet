﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace AutomationFramework
{
    public class ClsBrowser
    {
        public IWebDriver driver;
        public String browserName;
        public WebDriverWait jsWait;


        public IWebDriver getDriver() 
        {
            return driver;
        }

        public void StartBrowser(string browserName) 
        {
            browserName = browserName == null ? "Chrome" : browserName;
            InitBrowser(browserName);
        }


        private void InitBrowser(string browserName) 
        {
            //Select Correct Driver
            switch (browserName.ToUpper()) 
            {
                case "FIREFOX":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    FirefoxOptions optionfirefox = new FirefoxOptions();
                    optionfirefox.AddArgument("no-sandbox");
                    driver = new FirefoxDriver(optionfirefox);
                    break;
                case "EDGE":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    EdgeOptions optionedge = new EdgeOptions();
                    optionedge.AddArgument("no-sandbox");
                    driver = new EdgeDriver(optionedge);
                    break;
                case "CHROME":
                default:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions optionchrome = new ChromeOptions();
                    optionchrome.AddArgument("no-sandbox");
                    //optionchrome.AddArgument("EnableChromeDriverAutomation"); //NVDA Testing
                    driver = new ChromeDriver(optionchrome);
                    break;
            }
            //Driver Setup
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);  //time before throwing an exception
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10); //to wait for a page to load completely before throwing an error
            driver.Manage().Window.Maximize();
            driver.Manage().Cookies.DeleteAllCookies();
            //Init WebDriverWait for JS calls
            jsWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        public void NavigateTo(string URL) { driver.Url = URL; }

        public void CloseBrowser() { driver.Close(); driver.Quit(); }
        

    }
}
