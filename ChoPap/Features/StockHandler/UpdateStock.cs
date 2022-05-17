using ChoPap.Features.Helper;
using ChoPap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;
using static ChoPap.Model.StockModel;

namespace ChoPap.Features.StockHandler
{
    public class UpdateStock
    {
        public static string PriceIsHigher(BoughtStocks item, rootobject item1, ChopapContext context)
        {
            decimal cP = item.currentPrice;
            item.LastCurrentPrice = item.currentPrice;
            item.Ath = Convert.ToDecimal(item1.quote.last);
            item.currentPrice = Convert.ToDecimal(item1.quote.last);
            var SellPrice = (decimal)item.Ath * (decimal)Global.sellStopp;
            item.sellPrice = SellPrice;
            item.lastUpdated = DateTime.Now;
            var MinBalance = (decimal)item.sellPrice * (decimal)item.Qty;
            item.minimumBalance = (item.Qty * SellPrice) - item.totalSum;
            item.Balance = (item.Qty * cP) - item.totalSum;
            context.BoughtStocks.Update(item);
            string a = ($"Updated price on: {item.Name} buyin: {item.pricePerShare}, currp: {item.currentPrice}");
            return a;
        }

        public static decimal SellBoughtStock(BoughtStocks item, ChopapContext context, decimal buyPrice)
        {
            System.Media.SystemSounds.Asterisk.Play();
            Thread.Sleep(1000);
            System.Media.SystemSounds.Asterisk.Play();
            decimal cP = item.currentPrice;
            buyPrice = item.Qty * item.pricePerShare;
            var SellPrice = (decimal)item.Ath * (decimal)Global.sellStopp;
            //if (SellPrice >= item.pricePerShare) { item.Won = true; } else { item.Lose = true; };
            item.Balance = (item.Qty * SellPrice) - buyPrice;
            item.lastUpdated = DateTime.Now;
            item.sellDay = DateTime.Now.ToString("dddd");
            item.Active = false;
            item.minimumBalance = (item.Qty * SellPrice) - buyPrice;
            decimal AccBal = buyPrice + item.minimumBalance;
            context.BoughtStocks.Update(item);
            //SaldoTable.UpdateDaySaldo(item.minimumBalance);
            SoldStocks.AddSoldStock(item.Name, item.minimumBalance);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Sold {item.Name} - {item.minimumBalance}");
            Console.ForegroundColor = ConsoleColor.White;

            return buyPrice;
        }

        public static void UpdateAccounts(BoughtStocks item, ChopapContext context, decimal buyPrice)
        {
            var BuyerSaldo = context.Accounts.Where(a => a.Name == item.Owner).FirstOrDefault();
            var newSaldo = buyPrice + item.minimumBalance;
            BuyerSaldo.Saldo += newSaldo; //?
            BuyerSaldo.qtyInPossession--;
            //if (item.Won == true) { BuyerSaldo.Wins++; } else { BuyerSaldo.Losses++; }
            BuyerSaldo.Balance += item.minimumBalance;
            BuyerSaldo.lastUpdated = DateTime.Now.ToString();
            context.Accounts.Update(BuyerSaldo);
        }

        public static void UpdateTemps(ChopapContext context, BoughtStocks item)
        {
            var tempo = context.Temps.Where(a => a.Name == item.Owner).FirstOrDefault();
            tempo.SellAction++;
            context.Temps.Update(tempo);
        }

        public static string Balance(BoughtStocks item)
        {
            decimal totalbalance = item.currentPrice * item.Qty;
            decimal newtotalbalance = totalbalance - item.totalSum;
            string a = ($"Sold: {item.Name}, balance: {newtotalbalance}, time: {DateTime.Now}");
            return a;
        }
    }
}