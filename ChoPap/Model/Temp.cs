using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Model
{
    public class Temp
    {
        public int TempID { get; set; }
        public string Name { get; set; }
        public string Day { get; set; }
        public int BuyAction { get; set; }
        public int SellAction { get; set; }
        public decimal Balance { get; set; }
    }
}
