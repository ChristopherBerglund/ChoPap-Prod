using ChoPap.Features;
using ChoPap.Features.Country;
using ChoPap.Features.GetStockInfo;
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
List<Stock> Today = new List<Stock>();
List<Stock> LockedStocks = new List<Stock>();
bool isValid = true;
string todaysday = DateTime.Now.ToString("dddd");


ExcuteScript.Page_Load();
Console.WriteLine("start");
//LogInToAvanza.OpenSelenium(drv);
SeriLog.SerilogBuild();
contryInfoList = Countries.ReadInContryInfo();
contryInfoList = IsItHoliday.IsItHolidayOrNot(todaysday, contryInfoList);                                               
//SoldStocks.DeleteAllSoldStocks();
//SaldoTable.CreateST();
var listOfStocks = ToFromJson.ImportJson();
ApiHelper.InitializeClient();
while (isValid)
{
    Console.Clear();
    Console.WriteLine("1.1 Check for GetStockList");
    foreach (var country in contryInfoList)
    {
        
        
        if (country.IsMarketClosed == false && DateTime.Now.TimeOfDay >= country.CheckTwo && country.GotTheList == false)
        {
            Console.WriteLine("1.2 Check for GetStockList => true");
            Today = await StockListHandler.GetStockList(country);
        }
    }

    Console.WriteLine("2.1 Check for boughtStock");

    if (context.BoughtStocks.Any())
    {
        await BoughtStocks.CheckCurrentStocksAsync(listOfStocks, drv);
        Console.WriteLine("2.2 Check for boughtStock => true");
    }
    Console.WriteLine("3.1 Check for TimeForActionHandler");

    foreach (var country in contryInfoList)
    {
        //Kollar varje land för tid-punkter. 
        if (TimeOfDay.TimeForActionHandler(country))
        {
            SystemSounds.Asterisk.Play();
            Console.WriteLine("3.2 Check for TimeForActionHandler =>");
            await BoughtStocks.ActionHandlerAsync(Today, listOfStocks, drv);
        }
    }
    Console.WriteLine("4.1 Check for TimeForBuyAbleStocks");
    foreach (var country in contryInfoList)
    {
        if (TimeOfDay.TimeForBuyAbleStocks(country) == true)
        {
            SystemSounds.Asterisk.Play();
            BoughtStocks.BuyAbleStocks(Today, LockedStocks);
            Console.WriteLine("4.2 Check for BuyAbleStocks => true");
        }
    }
    foreach (var country in contryInfoList)
    {
        Console.WriteLine("5.1 Check for EndTheDay");

        if (TimeOfDay.EndTheDay(country) == true)
        {
            SystemSounds.Asterisk.Play();
            Console.WriteLine("5.2 Check for EndTheDay => true");
            //if (IsThisTheDay.isValid() == true) //För att inte spara dagen två gånger om. //Spara alla listor, 23.00?
            //{
                IsThisTheDay.SaveTheDay_part1_UpdateExistingStocks(Today, country);
                Console.WriteLine("5.3 SaveTheDay_part1_UpdateExistingStocks => true");

                IsThisTheDay.SaveTheDay_part2_RemoveOldStocks(country);
                Console.WriteLine("5.4 SaveTheDay_part2_RemoveOldStocks => true");

                IsThisTheDay.SaveTheDay_part3_AddNewStocks(Today, country);
                Console.WriteLine("5.5 SaveTheDay_part3_AddNewStocks => true");

                IsThisTheDay.ChangeTheDayInTemp();
                Console.WriteLine("5.6 ChangeTheDayInTemp => true");

                bool AllDone = contryInfoList.Where(x => x.DoneForTheDay == false).Any();
                if(AllDone == false)
                {
                    Console.WriteLine("IsValid = true");
                    isValid = true;
                    
                }
            foreach (var item in contryInfoList)
            {
                Console.WriteLine($"Done for the day: {item.DoneForTheDay} - {item.CountryCode}");
            }
            //}
        }
    }
    Console.WriteLine("Sleep 1 min");
    Thread.Sleep(60000);
}
Console.WriteLine("All done for today!");
LogInToAvanza.ShutEdgeDown(drv);
Environment.Exit(0);

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

