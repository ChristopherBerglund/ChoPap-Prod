using ChoPap.Api.Models;
using ChoPap.Features.GetStockInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Model.StockModel;

namespace ChoPap.Api.Features.StockInfo
{
    public class GetStockInfo
    {


        public static async Task<rootobject> SelectSpecifiedStockAsync(List<rootobject> listOfStocks, string input)
        {
            var thisStock = listOfStocks.Where(x => x.name.ToLower() == input.ToLower()).FirstOrDefault();

            if (thisStock != null)
            {
                //Kolla mot den ny Json-Listan
                int convertedNum = Convert.ToInt32(thisStock.orderbookid);
                return await StockProcesser.LoadStockAsync(convertedNum);
            }
            else
            {
                //Gå till hemsida, hämta ordernummer, lägg i ny Json-lista
                //SeriLog.Logger(SeriLog.logType.Warning, $"[2][GetStockFromJson] Couldnt find stock:  [{input}]");
                return null;
            }
        }
    }
}
