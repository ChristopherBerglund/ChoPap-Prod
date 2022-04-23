using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using static ChoPap.Model.StockModel;

namespace ChoPap.Features.GetStockInfo
{
    public class StockProcesser
    {


        public static async Task<rootobject> LoadStockAsync(int num)
        {
            string url = $"https://www.avanza.se/_api/market-guide/{num}";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        rootobject stock = await response.Content.ReadAsAsync<rootobject>();
                        return stock;
                    }
                    else
                    {
                        return null;
                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
