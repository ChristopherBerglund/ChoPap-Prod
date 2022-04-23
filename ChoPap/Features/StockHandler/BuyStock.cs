using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Model.StockModel;

namespace ChoPap.Features.StockHandler
{
    public class BuyStock
    {
        public static int BuyInPrice(rootobject stock)
        {
            if (stock != null)
            {
                var volume = Math.Round(stock.quote.totalvaluetraded / 1000000);
                
                if (volume > 1 && volume <= 3)
                {
                    return 2000;
                }
                else if (volume > 3 && volume <= 5)
                {
                    return 3000;
                }
                else if (volume > 5 && volume <= 10)
                {
                    return 4000;
                }
                else if (volume > 10)
                {
                    return 5000;
                }
            }
            return 0;

        }
    }
}
