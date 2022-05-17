using ChoPap.Features.Country;
using ChoPap.Features.GetStockInfo;
using ChoPap.Config;
using ChoPap.Features.Helper;
using ChoPap.Features.Mail;
using ChoPap.Features.Selenium;
using ChoPap.Features.Serilog;
using ChoPap.Features.StockHandler;
using NinjaNye.SearchExtensions;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;
using static ChoPap.Features.Serilog.SeriLog;
using static ChoPap.Model.StockModel;

//Jämför & uppdaterar akiter med nuvarande pris.
//Ska köras om:
//# Det finns någon aktie i depån.
//# Om aktiens pris är 0, loggas det. 

namespace ChoPap.Model
{
    public class BoughtStocks
    {
        private static ChopapContext context = new ChopapContext();
        public BoughtStocks()
        {
        }

        [Key]
        public int BoughtStockID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public decimal Ath { get; set; }
        public decimal currentPrice { get; set; }
        public decimal sellPrice { get; set; }
        public decimal pricePerShare { get; set; }
        public decimal minimumBalance { get; set; }
        public DateTime lastUpdated { get; set; }
        public decimal Balance { get; set; }
        public decimal totalSum { get; set; }
        //public bool Won { get; set; }
        //public bool Lose { get; set; }
        public int Qty { get; set; }
        public int DayCounter { get; set; }
        public int sumOfDays { get; set; }
        public string sellDay { get; set; }
        public string BuyDay { get; set; }

        public string Owner { get; set; }
        //public decimal Percent { get; set; } //function
        public decimal LastCurrentPrice { get; set; } //function
        //public bool isBlue { get; set; }
        //public bool isRed { get; set; }
        //public bool isNoWhere { get; set; }
        public string countryCode { get; set; }


        public static async Task CheckCurrentStocksAsync(List<StockModel.rootobject> listOfStocks, EdgeDriver drv)
        {
            var active = context.BoughtStocks.Where(x => x.Active == true).ToList();
            foreach (var boughtStock in active)
            {
                rootobject stock = await DisplayStockFormat.SelectSpecifiedStockAsync(listOfStocks, boughtStock.Name);
                if(stock != null)
                {
                    if(stock.quote.last != 0)
                    {
                        if (boughtStock.Ath < Convert.ToDecimal(stock.quote.last))
                        {
                            UpdateStock.PriceIsHigher(boughtStock, stock, context);
                        }
                        else if (boughtStock.Ath > Convert.ToDecimal(stock.quote.last))
                        {
                            boughtStock.LastCurrentPrice = boughtStock.currentPrice;
                            boughtStock.currentPrice = Convert.ToDecimal(stock.quote.last);
                            boughtStock.lastUpdated = DateTime.Now;
                            if (boughtStock.currentPrice <= boughtStock.sellPrice)
                            {
                                decimal buyPrice = boughtStock.totalSum;
                                UpdateStock.SellBoughtStock(boughtStock, context, buyPrice);
                                //UpdateStock.UpdateAccounts(boughtStock, context, buyPrice);
                                //UpdateStock.UpdateTemps(context, boughtStock);
                                Mailer.MailBuilder(boughtStock);
                                if (ConfigSet.goToStock) { LogInToAvanza.goToStock(stock, boughtStock, drv, "Sell"); }                                                                           /////Screenshot
                            }
                        }
                    }
                    else
                    {
                        Logger(logType.Information, $"[SelectedSpecifiedStock]{boughtStock} was {stock.quote.last}(0?)");
                    }
                }
            }
            context.SaveChanges();
        }
        
        public static void BuyAbleStocks(Countries country)
        {
            List<Stock> tempList = new List<Stock>();
            tempList.Clear();
            var Candidates = new List<Stock>();
            var BuyAbleS = context.Stocks.Where(a => a.DayCounter == a.Sum && a.Sum >= 2 && a.CountryCode == country.CountryCode).ToList();
            var ActiveStocks = context.BoughtStocks.Where(a => a.Active == true).ToList();

            foreach (var item1 in BuyAbleS)
            {
                foreach (var item in country.TopList)
                {
                    if (item.Name == item1.Name)
                    {
                        item.DayCounter = item1.DayCounter;
                        Candidates.Add(item);
                    }
                }
            }
            foreach (var item in Candidates)
            {
                var result = ActiveStocks.Search(x => x.Name).Containing(item.Name).SingleOrDefault();

                if (result == null)
                {
                    tempList.Add(item);
                }
            }
            country.LockedStocks = new List<Stock>();
            country.LockedStocks.AddRange(tempList);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"------[{country.CountryCode}]--------");              
            foreach (var item in country.LockedStocks)
            {
                Console.WriteLine($"Name: {item.Name}({item.Sek})");
                SeriLog.Logger(SeriLog.logType.Information, $"[LockedStock] Name: {item.Name}({item.Sek}) [{country.CountryCode}]");
            }
            Console.WriteLine("------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            System.Media.SystemSounds.Asterisk.Play();
          

        }
        public static async Task ActionHandlerAsync(Countries country, List<rootobject> listOfStocks, EdgeDriver drv)
        {
            if (country.LockedStocks.Any())
            {
                //Console.WriteLine("LockedStocks is not null");
                foreach (var locked in country.LockedStocks)
                {
                    //Console.WriteLine($"Loop = {locked.Name}");

                    if (locked.CountryCode == country.CountryCode)
                    {
                        //Console.WriteLine($"{country.CountryCode} == {locked.CountryCode}");
                        rootobject stock = await DisplayStockFormat.SelectSpecifiedStockAsync(listOfStocks, locked.Name);
                        //SeriLog.Logger(SeriLog.logType.Information, $"[ActionHandler] Name: {locked.Ath}({stock.quote.last})");
                        if (stock != null)
                        {
                            //Console.WriteLine($"Stock is not null - {stock.name}");
                            if (stock.quote.last != 0)
                            {
                                //Console.WriteLine($"Stock quote is not 0 - {stock.quote.last}");
                                if (Convert.ToDecimal(stock.quote.last) > Convert.ToDecimal(locked.Ath))
                                {
                                    //Console.WriteLine($"Stock price is over locked ath - [{stock.name}]{stock.quote.last} > [{locked.Name}]{locked.Ath}");

                                    string ownerSet = "Mr A";
                                    if (locked.DayCounter == locked.Sum && locked.Sum == 1)
                                    {
                                        ownerSet = "Mr A";
                                    }
                                    else if (locked.DayCounter == locked.Sum && locked.Sum == 2)
                                    {
                                        ownerSet = "Mr B";
                                    }
                                    else if (locked.DayCounter == locked.Sum && locked.Sum == 3)
                                    {
                                        ownerSet = "Mr C";
                                    }
                                    else if (locked.DayCounter == locked.Sum && locked.Sum == 4)
                                    {
                                        ownerSet = "Mr D";
                                    }
                                    else
                                    {
                                        ownerSet = "Mr A";
                                    }

                                    decimal SellStopp = Global.sellStopp;
                                    decimal stockprice = BuyStock.BuyInPrice(stock);
                                    if (stockprice > 0)
                                    {
                                        System.Media.SystemSounds.Asterisk.Play();
                                        Thread.Sleep(1000);
                                        System.Media.SystemSounds.Asterisk.Play(); 
                                        Thread.Sleep(1000);
                                        System.Media.SystemSounds.Asterisk.Play();
                                        //Här händer något!
                                        decimal qty = Math.Round((decimal)stockprice / (decimal)stock.quote.last);
                                        decimal buyIn = ((decimal)qty * (decimal)stock.quote.buy);
                                        decimal SellStoppPrice = ((decimal)SellStopp * (decimal)stock.quote.last);
                                        decimal sellOut = ((decimal)qty * (decimal)SellStoppPrice);
                                        decimal mini = ((decimal)sellOut - (decimal)buyIn);

                                        var SellPrice = Convert.ToDecimal(locked.Ath) * (decimal)SellStopp;
                                        BoughtStocks newStock = new BoughtStocks()
                                        {
                                            Name = stock.name,
                                            Qty = Convert.ToInt32(qty),
                                            pricePerShare = Convert.ToDecimal(stock.quote.last),
                                            totalSum = qty * Convert.ToDecimal(stock.quote.last),
                                            Ath = Convert.ToDecimal(stock.quote.last),
                                            currentPrice = Convert.ToDecimal(stock.quote.last),
                                            Owner = ownerSet,
                                            DayCounter = locked.DayCounter,
                                            sumOfDays = locked.DaySum,
                                            Active = true,
                                            sellPrice = SellPrice,
                                            minimumBalance = mini,
                                            Balance = 0,
                                            lastUpdated = DateTime.Now,
                                            BuyDay = DateTime.Now.ToString("dddd"),
                                            countryCode = stock.listing.countrycode,
                                            sellDay = "NA"
                                        };

                                        if (ConfigSet.goToStock) {LogInToAvanza.goToStock(stock, newStock, drv, "buy");}                                                                           /////Screenshot
                                        context.BoughtStocks.Add(newStock);

                                        var BuyerSaldo = context.Accounts.Where(a => a.Name == ownerSet).FirstOrDefault();
                                        BuyerSaldo.Saldo = BuyerSaldo.Saldo - newStock.totalSum;
                                        BuyerSaldo.qtyInPossession++;
                                        BuyerSaldo.qtyTotal++;
                                        BuyerSaldo.lastUpdated = DateTime.Now.ToString();
                                        context.Accounts.Update(BuyerSaldo);

                                        var tempo = context.Temps.Where(a => a.Name == ownerSet).FirstOrDefault();
                                        tempo.BuyAction++;
                                        context.Temps.Update(tempo);

                                        var stocky = context.Stocks.Where(a => a.Name == stock.name).FirstOrDefault();
                                        stocky.Bought = true;
                                        context.Stocks.Update(stocky);

                                        Console.WriteLine($"BuyAction: {locked.Name} | Owner: {ownerSet}");

                                        var SellEmail = new StringBuilder();
                                        SellEmail.Append("Name: " + locked.Name.ToString() + "\n");
                                        SellEmail.Append("Price: " + Convert.ToInt32(locked.Sek) + "\n");
                                        SellEmail.Append("Owner: " + BuyerSaldo.Name + "\n");
                                        SellEmail.Append("Last Updated: " + locked.lastUpdated + "\n");
                                        var SellEmailSub = $"BuyAction / {locked.Name}";
                                        Mailer.SendEmail(SellEmail, SellEmailSub);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"Bought: Name: {locked.Name} - {buyIn}:-");
                                        Console.ForegroundColor = ConsoleColor.White;

                                    }
                                    else
                                    {
                                        //Console.WriteLine($"[2][ActionHandler] Didnt buy {stock.name} because the volym({stock.quote.totalvaluetraded}) was < ");
                                        SeriLog.Logger(SeriLog.logType.Information, $"[2][ActionHandler] Didnt buy {stock.name} because the volym({stock.quote.totalvaluetraded}) was < ");
                                    }
                                }
                            }
                            else
                            {
                                //Console.WriteLine($"[2][ActionHandler] This stock, {stock.name} was 0 ({stock.quote.last})*");
                                SeriLog.Logger(SeriLog.logType.Warning, $"[2][ActionHandler] This stock, {stock.name} was 0 ({stock.quote.last})*");
                            }
                        }
                        else
                        {
                            NotFoundStocks notFoundStocks = new NotFoundStocks()
                            {
                                Name = locked.Name,
                                CountryCode = locked.CountryCode,
                            };
                            context.NotFoundStocks.Add(notFoundStocks);
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"[2][ActionHandler] Name: {locked.Name} couldnt be found");
                        SeriLog.Logger(SeriLog.logType.Warning, $"[2][ActionHandler] Name: {locked.Name} couldnt be found");
                    }
                }
                context.SaveChanges();
            }
        }
    }
}


