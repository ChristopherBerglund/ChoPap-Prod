using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Model
{
    public class StockModel
    {
        public class rootobject
        {
            public string orderbookid { get; set; }
            public string name { get; set; }
            public string isin { get; set; }
            public string sectorname { get; set; }
            public string tradable { get; set; }
            public listing listing { get; set; }
            public keyindicators keyindicators { get; set; }
            public quote quote { get; set; }
            public float previousclosingprice { get; set; }
            public string type { get; set; }
            public historicalclosingprices historicalclosingprices { get; set; }
        }

        public class listing
        {
            public string shortname { get; set; }
            public string tickersymbol { get; set; }
            public string countrycode { get; set; }
            public string currency { get; set; }
            public string marketplacecode { get; set; }
            public string marketplacename { get; set; }
            public string ticksizelistid { get; set; }
            public bool markettradesavailable { get; set; }
        }

        public class keyindicators
        {
            public int numberofowners { get; set; }
            public float directyield { get; set; }
            public float volatility { get; set; }
            public marketcapital marketcapital { get; set; }
            public int dividendsperyear { get; set; }
            public nextreport nextreport { get; set; }
            public previousreport previousreport { get; set; }
        }

        public class marketcapital
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class nextreport
        {
            public string date { get; set; }
            public string reporttype { get; set; }
        }

        public class previousreport
        {
            public string date { get; set; }
            public string reporttype { get; set; }
        }

        public class quote
        {
            public float buy { get; set; }
            public float sell { get; set; }
            public float last { get; set; }
            public float highest { get; set; }
            public float lowest { get; set; }
            public float change { get; set; }
            public float changepercent { get; set; }
            public float spread { get; set; }
            public long timeoflast { get; set; }
            public float totalvaluetraded { get; set; }
            public long totalvolumetraded { get; set; }
            public long updated { get; set; }
            public float volumeweightedaverageprice { get; set; }
        }

        public class historicalclosingprices
        {
            public float oneday { get; set; }
            public float oneweek { get; set; }
            public float onemonth { get; set; }
            public float threemonths { get; set; }
            public float startofyear { get; set; }
            public float start { get; set; }
            public string startdate { get; set; }
        }

    }
}
