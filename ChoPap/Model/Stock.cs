using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Model
{
    public class Stock
    {
        //public static ChopapContext? _context;
        //public Stock(ChopapContext Context) => _context = Context;

   
        public int StockID { get; set; }
        public string Name { get; set; }
        public decimal Procent { get; set; }
        //public string Percent { get; set; }
        public decimal Sek { get; set; }
        public string Day { get; set; }
        public int DayCounter { get; set; }
        public int DaySum { get; set; }
        public int Sum { get; set; }
        public decimal Ath { get; set; }
        public bool IsItBlue { get; set; }
        public bool DayUpdated { get; set; }
        public DateTime lastUpdated { get; set; }
        public bool Bought { get; set; }
        public string CountryCode { get; set; }
    }
}
