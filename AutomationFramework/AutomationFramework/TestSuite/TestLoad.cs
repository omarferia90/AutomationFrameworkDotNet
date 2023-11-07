using AutomationFramework.JsonModel;
using AventStack.ExtentReports;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.TestSuite
{
    [TestFixture]
    public class TestLoad : BaseSetup
    {

        [Test]
        public void ReadJsonFiles()
        {
            

            var _json = File.ReadAllText(@"W:\Automation Projects\TestData\LoginData.json");
            
            var a = JObject.Parse(_json.ToString());

            foreach (JObject o in a.Children<JObject>())
            {
                foreach (JProperty p in o.Properties())
                {
                    string name = p.Name;
                    string value = (string)p.Value;
                    Console.WriteLine(name + " -- " + value);
                }
            }


            //JObject rss = JObject.Parse(_json.ToString());
            //string name = (string)rss["data"]["UserName"][0];
            //var jsonData = JsonConvert.SerializeObject(_json, Formatting.None);

            /*
            var parsed = JObject.Parse(json);
            var forecast = new WeatherForeCast();

            forecast.City = parsed.SelectToken("city.name").Value<string>();
            forecast.Day = parsed.SelectToken("list[0].temp.day").Value<decimal>();
            forecast.Description = parsed.SelectToken("list[0].weather[0].description").Value<string>();
            forecast.Min = parsed.SelectToken("list[0].temp.min").Value<decimal>();
            forecast.Max = parsed.SelectToken("list[0].temp.max").Value<decimal>();
            forecast.Night = parsed.SelectToken("list[0].temp.night").Value<decimal>();
            */
        }



        [Test]
        public void MyFirstTest() 
        {
            try
            {
                SetUpTC("My Test");
                objBrowser.NavigateTo("https://intake-uat.sedgwick.com/");
                objWE.fnSendKeys(objWE.fnGetWebElement(By.Id("orangeForm-name")), "Text", "oferiaDelta");
                objReport.fnLog("Info", Status.Info, false);
                objReport.fnLog("Pass", Status.Pass, false);
                objReport.fnLog("Warning", Status.Warning, false);
                CloseTC();
            }
            catch (Exception ex) 
            { objReport.fnLog($"An Exception has occurred: {ex.Message}", Status.Fail, false); }
            finally
            {
                CloseTC();
            }
        }

        [Test]
        public void MyFirstTest2()
        {
            SetUpTC("My Test 2");
            objBrowser.NavigateTo("https://intake-uat.sedgwick.com/");
            objWE.fnSendKeys(objWE.fnGetWebElement(By.Id("orangeForm-name")), "Text", "ABDEX");
            objReport.fnLog("Info", Status.Info, false);
            objReport.fnLog("Pass", Status.Pass, false);
            objReport.fnLog("Faillkljhlkjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj hhjlkkllk", Status.Fail, true);
            objReport.fnLog("Warning", Status.Warning, false);
            CloseTC();
        }

    }
}
