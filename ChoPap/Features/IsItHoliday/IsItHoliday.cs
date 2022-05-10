using ChoPap.Features.Country;
using ChoPap.Features.Helper;
using ChoPap.Features.Serilog;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Features.IsItHoliday
{
    public class IsItHoliday
    {
        private static ChopapContext context = new ChopapContext();

        private static readonly string Saturday = "lördag";
        private static readonly string Sunday = "söndag";
        private static readonly int Year = 2022;
        //private static readonly string todaysDate = DateTime.Now.ToString("dd/MM/yyyy");
        public static List<Countries> IsItHolidayOrNot(string today, List<Countries> contryInfoList)
        {
            if (MarketIsClosed(today)) { Console.WriteLine("Its holiday, no markets are open."); Environment.Exit(0); };
            string todaysDate = DateTime.Now.ToString("dd/MM/yyyy");
            foreach (var country in contryInfoList)
            {
                country.IsMarketClosed = IsDeviatingOpeningHours(todaysDate, country);
                country.IsHalfDay = IsHalfDay(todaysDate, country);
                if(IfNotClosedOrHalfDay(country))
                {
                    country.IsMarketClosed = IsHoliday(todaysDate, country);
                }

                if (country.IsMarketClosed == true)
                {
                    var contextCountry = context.CountryConfig.Where(a => a.CountryCode == country.CountryCode).FirstOrDefault();
                    contextCountry.DoneForTheDay = true;
                    contextCountry.Day = today;
                }
            }
            context.SaveChanges();
            return contryInfoList;
        }

        public static bool IsItHolidayOrNot(string todaysDay, object contryInfoList)
        {
            throw new NotImplementedException();
        }

        public static bool MarketIsClosed(string today)
        {
            if (today == Saturday || today == Sunday)
            {
                SeriLog.Logger(SeriLog.logType.Information, Global.Holiday);
                return true;
                
            }
            return false;
        }

        public static bool IsDeviatingOpeningHours(string todaysDate, Countries contryInfo)
        {
            foreach (var redDay in contryInfo.RedDays)
            {
                if (redDay == todaysDate)
                {
                    SeriLog.Logger(SeriLog.logType.Information, $"Marked is closed today for [{contryInfo.CountryCode}]");
                    return true;
                }
            }
            return false;
        }

        public static bool IsHalfDay(string todaysDate, Countries contryInfo)
        {
            foreach (var halfDay in contryInfo.HalfDays)
            {
                return (halfDay == todaysDate);
                {
                    SeriLog.Logger(SeriLog.logType.Information, $"Red day for [{contryInfo.CountryCode}]");
                    return true;
                }
            }
            return false;
        }

        public static bool IsHoliday(string todaysDate, Countries contryInfo)
        {
            var publicHolidays = DateSystem.GetPublicHoliday(Year, contryInfo.CountryCode);
            foreach (var day in publicHolidays)
            {
                return (todaysDate == day.Date.ToString("dd/MM/yyyy"));
            }
            return false;
        }

        public static bool IfNotClosedOrHalfDay(Countries country)
        {
            return(country.IsMarketClosed == false && country.IsHalfDay == false);
        }
    }
}

       