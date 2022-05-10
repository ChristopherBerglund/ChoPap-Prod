using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Model.StockModel;

namespace ChoPap.Features.GetStockInfo
{
    public class ToFromJson
    {
        private static readonly string AllStock = @"\..\..\..\" + @"\Features\GetStockInfo\Json\Stockies.json";
        private static readonly string path = Directory.GetCurrentDirectory();
        private static readonly string fullpath = Path.GetFullPath(path + AllStock);
        public static void ExportJson(List<rootobject> stockies)
        {
            string json = JsonConvert.SerializeObject(stockies);
            File.WriteAllText(fullpath, json);
        }

        public static List<rootobject> ImportJson()
        {
            string json = File.ReadAllText(fullpath);
            var stockies = JsonConvert.DeserializeObject<List<rootobject>>(json);
            return stockies;
        }

    }
}
