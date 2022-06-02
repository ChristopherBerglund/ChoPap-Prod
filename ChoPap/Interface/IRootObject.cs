using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Interface
{
    public interface IRootObject
    {
        public class Rootobject
        {
            public string orderbookId { get; set; }
            public string name { get; set; }
            public string isin { get; set; }
            public string sectorName { get; set; }
            public string tradable { get; set; }
            public Listing listing { get; set; }
            public Historicalclosingprices historicalClosingPrices { get; set; }
            public Keyindicators keyIndicators { get; set; }
            public Quote quote { get; set; }
            public float previousClosingPrice { get; set; }
            public string type { get; set; }
        }

        public class Listing
        {
            public string shortName { get; set; }
            public string tickerSymbol { get; set; }
            public string countryCode { get; set; }
            public string currency { get; set; }
            public string marketPlaceCode { get; set; }
            public string marketPlaceName { get; set; }
            public string marketListName { get; set; }
            public string tickSizeListId { get; set; }
            public bool marketTradesAvailable { get; set; }
        }

        public class Historicalclosingprices
        {
            public float oneDay { get; set; }
            public float oneWeek { get; set; }
            public float oneMonth { get; set; }
            public float threeMonths { get; set; }
            public float startOfYear { get; set; }
            public float start { get; set; }
            public string startDate { get; set; }
        }

        public class Keyindicators
        {
            public int numberOfOwners { get; set; }
            public float directYield { get; set; }
            public float volatility { get; set; }
            public float priceEarningsRatio { get; set; }
            public float priceSalesRatio { get; set; }
            public float interestCoverageRatio { get; set; }
            public float returnOnEquity { get; set; }
            public float returnOnTotalAssets { get; set; }
            public float equityRatio { get; set; }
            public float capitalTurnover { get; set; }
            public float operatingProfitMargin { get; set; }
            public float grossMargin { get; set; }
            public float netMargin { get; set; }
            public Marketcapital marketCapital { get; set; }
            public Equitypershare equityPerShare { get; set; }
            public Turnoverpershare turnoverPerShare { get; set; }
            public Earningspershare earningsPerShare { get; set; }
            public Dividend dividend { get; set; }
            public int dividendsPerYear { get; set; }
            public Nextreport nextReport { get; set; }
            public Previousreport previousReport { get; set; }
        }

        public class Marketcapital
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Equitypershare
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Turnoverpershare
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Earningspershare
        {
            public float value { get; set; }
            public string currency { get; set; }
        }

        public class Dividend
        {
            public string exDate { get; set; }
            public float amount { get; set; }
            public string currencyCode { get; set; }
            public string exDateStatus { get; set; }
        }

        public class Nextreport
        {
            public string date { get; set; }
            public string reportType { get; set; }
        }

        public class Previousreport
        {
            public string date { get; set; }
            public string reportType { get; set; }
        }

        public class Quote
        {
            public float last { get; set; }
            public float highest { get; set; }
            public float lowest { get; set; }
            public float change { get; set; }
            public float changePercent { get; set; }
            public long timeOfLast { get; set; }
            public float totalValueTraded { get; set; }
            public int totalVolumeTraded { get; set; }
            public long updated { get; set; }
            public float volumeWeightedAveragePrice { get; set; }
        }
    }
}
