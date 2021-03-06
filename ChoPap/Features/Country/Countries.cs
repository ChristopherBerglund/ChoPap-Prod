using ChoPap.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Features.Country
{
    public class Countries
    {
        private static readonly string AllStock = @"\..\..\..\" + @"\Features\Country\Json\CountryInfo.json";
        private static readonly string path = Directory.GetCurrentDirectory();
        private static readonly string fullpath = Path.GetFullPath(path + AllStock);

        private static ChopapContext context = new ChopapContext();

        public static List<Countries> countries = new List<Countries>();

        //public static List<Stock> TopList = new List<Stock>();
        public string CountryCode { get; set; } = string.Empty;
        public bool IsMarketClosed { get; set; } = false;
        public bool IsHalfDay { get; set; } = false;
        public List<string> HalfDays { get; set; } = new List<string>();
        public List<string> RedDays { get; set; } = new List<string>();
        public List<Stock> TopList { get; set; } = new List<Stock>();
        public List<Stock> TopListTest { get; set; } = new List<Stock>();
        public List<Stock> LockedStocks { get; set; } = new List<Stock>();
        public TimeSpan Opens { get; set; }
        public TimeSpan Closes { get; set; } 
        public TimeSpan ClosesHalfDay { get; set; }
        public TimeSpan CheckOne { get { return Closes.Subtract(TimeSpan.FromMinutes(25)); ; } }
        public TimeSpan CheckTwo { get { return Closes.Subtract(TimeSpan.FromMinutes(5)); ; } }
        public TimeSpan EndOfDay { get; set; }
        public bool CheckOneFinish { get; set; } = false;
        public bool CheckTwoFinish { get; set; } = false;
        public bool DoneForTheDay { get; set; } = false;
        public bool GotTheList { get; set; } = false;

        public static List<Countries> ReadInContryInfo()
        {
            string json = File.ReadAllText(fullpath);
            return JsonConvert.DeserializeObject<List<Countries>>(json);
            //return stockies;
        }

        public static void ResetCountries(string todaysDay)
        {
            var allContextCountries = context.CountryConfig.ToList();
            foreach (var country in allContextCountries)
            {
                if(country.Day != todaysDay)
                {
                    country.DoneForTheDay = false;
                }
                else { 
                }
            }
            context.SaveChanges();
        }

        public static void FinishCountryOff(Countries country, string todaysDay)
        {
            var a = context.CountryConfig.Where(a => a.CountryCode == country.CountryCode).FirstOrDefault();
            a.DoneForTheDay = true;
            a.Day = todaysDay;
            context.SaveChanges();
        }

        public static void IsIsAReRun(List<Countries> contryInfoList)
        {
            foreach (var country in contryInfoList)
            {
                if(context.CountryConfig.Where(x => x.CountryCode == country.CountryCode).Select(x => x.DoneForTheDay).FirstOrDefault())
                {
                    country.DoneForTheDay = true;
                }


            }
        }
    }
}






















//WriteAllJson
//string json = JsonConvert.SerializeObject(contryInfoList);
//File.WriteAllText(fullpath, json);






//List<string> mcSWEDEN = new List<string>()
//{
//    "26-12-2022",
//    "04-11-2022",
//    "24-06-2022",
//    "06-06-2022",
//    "26-05-2022",
//    "18-04-2022",
//    "15-04-2022",
//    "14-04-2022",
//    "06-01-2022",
//    "05-01-2022"
//};
//List<string> hdSWEDEN = new List<string>()
//{
//    "14-04-2022", 
//    "25-05-2022"
//};

//List<string> mcFINLAND = new List<string>()
//{
//    "26-12-2022",
//    "06-12-2022",
//    "24-06-2022",
//    "06-06-2022",
//    "26-05-2022",
//    "18-04-2022",
//    "15-04-2022",
//    "06-01-2022",
//};
//List<string> hdFINLAND = new List<string>()
//{

//};

//List<string> mcDENMARK = new List<string>()
//{
//    "26-12-2022",
//    "06-06-2022",
//    "27-05-2022",
//    "26-05-2022",
//    "13-05-2022",
//    "18-04-2022",
//    "15-04-2022",
//    "14-04-2022",
//};
//List<string> hdDENMARK = new List<string>()
//{

//};

//List<string> mcNORWAY = new List<string>()
//{
//    "26-12-2022",
//    "06-06-2022",
//    "27-05-2022",
//    "26-05-2022",
//    "17-05-2022",
//    "18-04-2022",
//    "15-04-2022",
//    "14-04-2022",
//};
//List<string> hdNORWAY = new List<string>()
//{
//    "13-04-2022"

//};

//List<string> mcUSA = new List<string>()
//{
//    "26-12-2022",
//    "24-11-2022",
//    "05-08-2022",
//    "04-07-2022",
//    "20-06-2022",
//    "27-05-2022",
//    "30-05-2022",
//    "15-04-2022",
//    "21-02-2022",
//    "17-01-2022"
//};
//List<string> hdUSA = new List<string>()
//{
//    "25-11-2022"

//};


//var country = new Countries[]
//{
//    new Countries { CountryCode = "SE", RedDays = mcSWEDEN, HalfDays = hdSWEDEN, Opens = new TimeSpan(09, 00, 00), Closes = new TimeSpan(17, 30, 00), ClosesHalfDay = new TimeSpan(13, 00, 00), },
//    new Countries { CountryCode = "FI", RedDays = mcFINLAND, HalfDays = hdFINLAND, Opens = new TimeSpan(09, 00, 00), Closes = new TimeSpan(17, 30, 00), ClosesHalfDay = new TimeSpan(13, 00, 00) },
//    new Countries { CountryCode = "DK", RedDays = mcDENMARK, HalfDays = hdDENMARK, Opens = new TimeSpan(09, 00, 00), Closes = new TimeSpan(17, 00, 00), ClosesHalfDay = new TimeSpan(13, 00, 00) },
//    new Countries { CountryCode = "NO", RedDays = mcNORWAY, HalfDays = hdNORWAY, Opens = new TimeSpan(09, 00, 00), Closes = new TimeSpan(16, 25, 00), ClosesHalfDay = new TimeSpan(13, 00, 00) },
//    new Countries { CountryCode = "US", RedDays = mcUSA, HalfDays = hdUSA, Opens = new TimeSpan(15, 30, 00), Closes = new TimeSpan(22, 00, 00), ClosesHalfDay = new TimeSpan(19, 00, 00) }
//};

//countries.AddRange(country);
//return countries;




