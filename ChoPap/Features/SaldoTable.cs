using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Features
{
    public class SaldoTable
    {
        private static ChopapContext context = new ChopapContext();
        public SaldoTable()
        {

        }
        public int ID { get; set; }
        public string Day { get; set; } = DateTime.Now.ToString("dddd");
        public string Month { get; set; } = DateTime.Now.ToString("MMMM");
        public int Year { get; set; } = DateTime.Now.Year;
        public decimal daySaldo { get; set; } = 0;
        public decimal totalSaldo { get; set; } = 0;
        public string OMX30 { get; set; } = string.Empty;
        public string Date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");

        public static void CreateST()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            if (!context.SaldoTables.Where(x => x.Date == date).Any())
            {
                SaldoTable saldoTable = new SaldoTable();
                context.SaldoTables.Add(saldoTable);
                context.SaveChanges();
            }
        }

        public static void UpdateDaySaldo(decimal a)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            var thisST = context.SaldoTables.Where(x => x.Date == date).FirstOrDefault();
            thisST.daySaldo += a;
            context.SaldoTables.Update(thisST);
            context.SaveChanges();
        }

        //public static void UpdateTotalSaldo()
        //{
        //    decimal tot = context.BoughtStocks.Where(x => x.Active != true).Select(a => a.minimumBalance).Sum();
        //    string date = DateTime.Now.ToString("dd/MM/yyyy");
        //    var yo = context.SaldoTables.Where(x => x.Date == date).FirstOrDefault();
        //    Console.WriteLine();
        //    yo.totalSaldo = tot;
        //    context.SaldoTables.Update(yo);
        //    context.SaveChanges();
        //}
    }
}
