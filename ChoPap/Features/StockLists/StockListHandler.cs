using ChoPap.Features.Country;
using ChoPap.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Hämtar in topplistor för aktuellt land
//Ska köras om:
//# Marknaden inte är stängd.
//# Om tiden är över CheckTwo (5 min innan stängning)
//# Om GotTheList inte blivit true

namespace ChoPap.Features.StockLists
{
    public class StockListHandler
    {
        public static List<Stock> stocks = new List<Stock>();
        public static async Task<List<Stock>> GetStockList(Countries country)
        {

            string url = "";
            if (country.CountryCode == "SE")
            {
                url = "https://www.avanza.se/aktier/vinnare-forlorare.html";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("https://www.avanza.se/aktier/vinnare-forlorare.html?countryCode=");
                sb.Append(country.CountryCode);
                sb.Append("&timeUnit=TODAY");
                url = sb.ToString();
                        
            }

            var client = new HttpClient();
            var html = await client.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var stockPercent = htmlDocument.DocumentNode.Descendants("span")
                  .Where(node => node.GetAttributeValue("class", "")
                  .Equals("pushBox roundCorners3")).ToList();

            var stockName = htmlDocument.DocumentNode.Descendants("a")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("link")).ToList();

            for (int i = 0; i < 40; i++)
            {
                Stock stock = new Stock()
                {
                    Name = stockName[i].InnerHtml.Trim(),
                    Ath = stockPercent[i].InnerHtml.Trim(),
                    CountryCode = country.CountryCode
                };
                stocks.Add(stock);
            }

            country.GotTheList = true;
            return stocks;
          
        }
    }
}
