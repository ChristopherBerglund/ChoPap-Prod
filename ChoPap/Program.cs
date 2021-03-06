using ChoPap.Config;
using ChoPap.Features.Counter;
using ChoPap.Features.Helper;
using ChoPap.Features.Selenium;
using ChoPap.Features.StockLists;
using ChoPap.Features.Time;
using ChoPap.Model;



ConfigSet.ConfigBuilder();
while (Global.isValid)
{
    ConfigSet.runner++;
    foreach (var country in ConfigSet.contryInfoList)
    {
        if (country.IsMarketClosed == false && DateTime.Now.TimeOfDay >= country.CheckOne && country.GotTheList == false)
        {
            ConfigSet.temp.Clear();
            ConfigSet.temp = await StockListHandler.GetStockList(country);
            country.TopList = new List<Stock>();
            country.TopList.AddRange(ConfigSet.temp);
            ConfigSet.timesGotTheList++;
        }
    }

    if (ConfigSet.context.BoughtStocks.Any())
    {
        if(ConfigSet.runner % 2 == 0 || ConfigSet.runner == 1)
        {
            await BoughtStocks.CheckCurrentStocksAsync(ConfigSet.listOfStocks, ConfigSet.drv);
        }
    }

    foreach (var country in ConfigSet.contryInfoList)
    {
        if (TimeOfDay.TimeForActionHandler(country))
        {
            await BoughtStocks.ActionHandlerAsync(country, ConfigSet.listOfStocks, ConfigSet.drv);
            ConfigSet.timesForAction++;
        }
    }
    foreach (var country in ConfigSet.contryInfoList)
    {
        if (TimeOfDay.TimeForBuyAbleStocks(country) == true)
        {
            BoughtStocks.BuyAbleStocks(country);
            ConfigSet.timesForBuyAbles++;
        }
    }
    foreach (var country in ConfigSet.contryInfoList)
    {
        if (TimeOfDay.EndTheDay(country) == true)
        {
            IsThisTheDay.SaveTheDay_part1_UpdateExistingStocks(country);
            IsThisTheDay.SaveTheDay_part2_RemoveOldStocks(country);
            IsThisTheDay.SaveTheDay_part3_AddNewStocks(country);
            IsThisTheDay.ChangeTheDayInTemp();
        }
    }

    if (!ConfigSet.CheckForExit())
    {
        Console.WriteLine("The end..");
        Environment.Exit(0);
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