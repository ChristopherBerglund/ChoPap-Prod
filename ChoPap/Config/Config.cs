using ChoPap.Features.Country;
using ChoPap.Features.GetStockInfo;
using ChoPap.Features.Helper;
using ChoPap.Features.IsItHoliday;
using ChoPap.Features.Migration;
using ChoPap.Features.Selenium;
using ChoPap.Features.Serilog;
using ChoPap.Model;
using Newtonsoft.Json;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Config
{
    public class ConfigSet
    {
        public static bool goToStock = true;
        public static bool openSelenium  = true;
        public static bool isItHoliday = true;
        public static bool LaptopConfiguration = true;

        public static int timesGotTheList = 0;
        public static int timesForBuyAbles = 0;
        public static int timesForAction = 0;
        public static int runner = 0;

        public static ChopapContext context = new ChopapContext();
        public static List<Countries> contryInfoList = new List<Countries>();
        public static List<Stock> temp = new List<Stock>();
        public static List<StockModel.rootobject> listOfStocks = new List<StockModel.rootobject>();
        public static EdgeDriver drv = new EdgeDriver();

        public static void ConfigBuilder()
        {
            drv.Manage().Window.Minimize();
            ExcuteScript.Page_Load();
            if (openSelenium) { LogInToAvanza.OpenSelenium(drv); }
            SeriLog.SerilogBuild();
            contryInfoList = Countries.ReadInContryInfo();
            Countries.ResetCountries(Global.todaysDay);
            if (isItHoliday) { contryInfoList = IsItHoliday.IsItHolidayOrNot(Global.todaysDay, contryInfoList); }
            Countries.IsIsAReRun(contryInfoList);
            listOfStocks = ToFromJson.ImportJson();
            ApiHelper.InitializeClient();
            Console.WriteLine("start");
        }

        public static bool CheckForExit()
        {
            return context.CountryConfig.Where(x => x.DoneForTheDay == false).Any();
        }

    }
}
