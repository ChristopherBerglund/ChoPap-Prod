using ChoPap.Features.Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Features.StockHandler
{
    public class SoldStocks
    {
        private static ChopapContext context = new ChopapContext();


        public SoldStocks()
        {
        }

     
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string Date { get; set; } = DateTime.Now.ToShortDateString();
        public bool Active { get; set; } = false;
        public static void AddSoldStock(string name, decimal balance)
        {
            SoldStocks soldStocks = new SoldStocks() { Name = name, Balance = balance };
            context.SoldStocks.Add(soldStocks);
            context.SaveChanges();
            SeriLog.Logger(SeriLog.logType.Information, $"[Added sold stocks]: Name({name}), Balance({balance})");

        }
        public static void DeleteAllSoldStocks()
        {
            if (context.SoldStocks.Any())
            {
                string now = DateTime.Now.ToShortDateString();
                var allStocks = context.SoldStocks.Where(x => x.Date != now).ToList();
                context.SoldStocks.RemoveRange(allStocks);
                context.SaveChanges();
                foreach (var stock in allStocks)
                {
                    SeriLog.Logger(SeriLog.logType.Information, $"[Deleted All Sold Stocks]");
                }
            }
        }
    }
}
