using ChoPap.Features.Country;
using ChoPap.Features.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Features.Time
{
    public class TimeOfDay
    {
        //static readonly TimeSpan endOfDay = new TimeSpan(17, 50, 00);
        //static readonly TimeSpan CheckOne = new TimeSpan(17, 25, 00);
        //static readonly TimeSpan CheckTwo = new TimeSpan(17, 40, 00);

        //static bool CheckTwoFinish;
        //static bool CheckOneFinish;


        //Retunerar True för om tiden är inne för att låsa köpbara aktier och ändrar CheckOneFinish = true:
        //# tiden är över country.checkOne(SE = 17.05).
        //# Om CheckOneFinish är false (att denna funktionen inte körts tidigare).
        //# Om DoneForTheyDay är false, att dagen inte är över. 

        public static bool TimeForBuyAbleStocks(Countries country)
        {
            if (DateTime.Now.TimeOfDay > country.CheckOne && country.CheckOneFinish == false && country.IsMarketClosed == false)
            {
                country.CheckOneFinish = true;
                Console.WriteLine(Global.BuyAbleStockMess);
                return true;
            }
            return false;
        }

        //Retunerar True för om tiden är inne för att köpa aktier och ändrar CheckTwoFinish = true:
        //# Tiden är över country.CheckOne(SE = 17.25).
        //# CheckOneFinish = true (Att föregående funktion har körts).
        //# CheckTwoFinish = false (Att denna funktionen inte körts förut).
        //# IsMarketClosed = false (Marknaden har inte stängt)

        public static bool TimeForActionHandler(Countries country)
        {
            if (DateTime.Now.TimeOfDay > country.CheckTwo && country.CheckOneFinish == true && country.CheckTwoFinish == false && country.IsMarketClosed == false)
            {
                country.CheckTwoFinish = true;
                Console.WriteLine(Global.ActionHandlerMess);
                return true;
            }
            return false;
        }
        //Retunerar True för om tiden är inne för att marknaden har stängt.
        //# Tiden är över country.EndOfDay(SE = 17.30).
        //# CheckTwoFinish = true (att föregånde funktion är körd).
        //# IsMarketClosed = false (Marknaden har inte stängt)
        public static bool EndTheDay(Countries country)
        {
            if (DateTime.Now.TimeOfDay > country.Closes && country.CheckOneFinish == true && country.IsMarketClosed == false)
            {
                country.IsMarketClosed = true;
                Console.WriteLine($"BuyAbleStocks: market is closed {country.CountryCode}");
                return true;
            }
            return false;
        }


    }
}
