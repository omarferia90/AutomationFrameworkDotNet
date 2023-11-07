using AventStack.ExtentReports;
using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V112.DOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework
{
    public class ClsDataJson : BaseSetup
    {
        public DataTable getDataTable(string FilePath, string TableId) 
        {
            DataTable dataTable = null;
            try 
            {
                string jsonPath = FilePath;
                var json = File.ReadAllText(jsonPath);

                //DataSet
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);
                dataTable = dataSet.Tables["User2"];
            }
            catch 
            { dataTable = null; }

            return dataTable;
        }


    }
}
