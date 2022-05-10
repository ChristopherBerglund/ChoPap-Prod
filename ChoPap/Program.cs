using ChoPap.Features;
using ChoPap.Features.Counter;
using ChoPap.Features.Country;
using ChoPap.Features.GetStockInfo;
using ChoPap.Features.Helper;
using ChoPap.Features.IsItHoliday;
using ChoPap.Features.Migration;
using ChoPap.Features.Selenium;
using ChoPap.Features.Serilog;
using ChoPap.Features.StockHandler;
using ChoPap.Features.StockLists;
using ChoPap.Features.Time;
using ChoPap.Model;
using OpenQA.Selenium.Edge;
using System.Media;
using static ChoPap.Data.Program;

EdgeDriver drv = new EdgeDriver();
ChopapContext context = new ChopapContext();
List<Countries> contryInfoList = new List<Countries>();
List<Stock> temp = new List<Stock>();

int timesGotTheList = 0;
int timesForBuyAbles = 0;
int timesForAction = 0;
ExcuteScript.Page_Load();
Console.WriteLine("start");
//LogInToAvanza.OpenSelenium(drv);
SeriLog.SerilogBuild();
contryInfoList = Countries.ReadInContryInfo();
Countries.ResetCountries(Global.todaysDay);
contryInfoList = IsItHoliday.IsItHolidayOrNot(Global.todaysDay, contryInfoList);
var listOfStocks = ToFromJson.ImportJson();
ApiHelper.InitializeClient();
int runner = 0;
while (Global.isValid)
{
    runner++;
    foreach (var country in contryInfoList)
    {
        if (country.IsMarketClosed == false && DateTime.Now.TimeOfDay >= country.CheckOne && country.GotTheList == false)
        {
            temp.Clear();
            temp = await StockListHandler.GetStockList(country);
            country.TopList = new List<Stock>();
            country.TopList.AddRange(temp);
            timesGotTheList++;
        }
    }

    if (context.BoughtStocks.Any())
    {
        if(runner % 2 == 0 || runner == 1)
        {
            await BoughtStocks.CheckCurrentStocksAsync(listOfStocks, drv);
            Console.WriteLine($"Checked for bought stocks");
        }
    }

    foreach (var country in contryInfoList)
    {
        if (TimeOfDay.TimeForActionHandler(country))
        {
            await BoughtStocks.ActionHandlerAsync(country, listOfStocks, drv);
            Console.WriteLine($"ActionsHandlder for {country.CountryCode}");
            timesForAction++;
        }
    }
    foreach (var country in contryInfoList)
    {
        if (TimeOfDay.TimeForBuyAbleStocks(country) == true)
        {
            BoughtStocks.BuyAbleStocks(country);
            Console.WriteLine($"BuyAbleStocks for {country.CountryCode}");
            timesForBuyAbles++;
        }
    }
    foreach (var country in contryInfoList)
    {
        if (TimeOfDay.EndTheDay(country) == true)
        {
            Console.WriteLine("5.2 Check for EndTheDay => true");
            IsThisTheDay.SaveTheDay_part1_UpdateExistingStocks(country);
            Console.WriteLine("5.3 SaveTheDay_part1_UpdateExistingStocks => true");

            IsThisTheDay.SaveTheDay_part2_RemoveOldStocks(country);
            Console.WriteLine("5.4 SaveTheDay_part2_RemoveOldStocks => true");

            IsThisTheDay.SaveTheDay_part3_AddNewStocks(country);
            Console.WriteLine("5.5 SaveTheDay_part3_AddNewStocks => true");

            IsThisTheDay.ChangeTheDayInTemp();
            Console.WriteLine($"EndOfDay for {country.CountryCode}");
            
            if (country.CountryCode == "US" && country.CheckTwoFinish == true)
            {
                Console.WriteLine("US is DONE, exit system...");
                Console.WriteLine($"Got the list: {timesGotTheList}");
                Console.WriteLine($"Buyables: {timesForBuyAbles}");
                Console.WriteLine($"ActionHandler: {timesForAction}");
                LogInToAvanza.ShutEdgeDown(drv);
                Environment.Exit(0);
            }
        }
    }
    TimeCounter.Counter();
}































//AllStocks.ErrorMessages();
////Stock.PrintStockInfo();
////StocksInPossession.PrintStocksInPoss();
///
//UpdateAccount.UpdateDay();
//SaldoTable.UpdateTotalSaldo();

//MonthlySaldo.UpdateMonthlySaldo();

//Mailer.EmailerSub();
//Console.WriteLine(Global.Stop);
//MoveCursor.MoveCur();

//32SoldStocks.DeleteAllSoldStocks();
//33SaldoTable.CreateST();