using ChoPap.Features.Country;
using ChoPap.Features.Helper;
using ChoPap.Features.Serilog;
using ChoPap.Model;
using NinjaNye.SearchExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Features.Time
{
    public class IsThisTheDay
    {
        private static ChopapContext context = new ChopapContext();


        public static bool isValid()
        {
            using (var context = new ChopapContext())
            {
                var t = context.Temps.Where(a => a.Name == Global.MrA).FirstOrDefault();
                if (t.Day != DateTime.Now.ToString("dddd"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void ChangeTheDayInTemp()
        {
            using (var context = new ChopapContext())
            {
                var temp = context.Temps.ToList();
                foreach (var item in temp)
                {
                    item.Day = DateTime.Now.ToString("dddd");
                    context.Temps.Update(item);
                }
                context.SaveChanges();
                Console.WriteLine(Global.SaveDay);
            }
        }

        public static void SaveTheDay_part1_UpdateExistingStocks(Countries country)
        {
            foreach (var item in country.TopList)
            {
                foreach (var item1 in context.Stocks)
                {
                    if (item.Name == item1.Name && item.CountryCode == item1.CountryCode)
                    {
                        item1.Procent = item.Procent;
                        item1.Sek = item.Sek;
                        item1.DayCounter++;
                        item1.Ath = item.Sek.ToString();
                        item1.lastUpdated = DateTime.Now;
                        item1.DayUpdated = true;
                        context.Stocks.Update(item1);
                        //UppdatedStock++;
                        Console.WriteLine($"Updated: {item1.Name}");
                    }
                }
            }
            context.SaveChanges();
            //Console.WriteLine($"[SaveTheDay] Part 1: Done for country [{country.CountryCode}]");
            SeriLog.Logger(SeriLog.logType.Information, $"[SaveTheDay] Part 1: Done for country [{country.CountryCode}]");
        }
        public static void SaveTheDay_part2_RemoveOldStocks(Countries country)
        {
            string todayDay = DateTime.Now.ToString("dddd");
            var oldStocks = context.Stocks.Where(a => a.DayCounter > a.DaySum && a.DayUpdated == false && a.CountryCode == country.CountryCode).ToList();
            foreach (var item in oldStocks)
            {
                context.Stocks.Remove(item);
                //Console.WriteLine($"removed: {item.Name}");
            }
            context.SaveChanges();
            //Console.WriteLine($"[SaveTheDay] Part 2: Done for country {country.CountryCode}");
            SeriLog.Logger(SeriLog.logType.Information, $"[SaveTheDay] Part 2: Done for country {country.CountryCode}");

        }
        public static void SaveTheDay_part3_AddNewStocks(Countries country)
        {
            string todayDay = DateTime.Now.ToString("dddd");

            foreach (var item in country.TopList)
            {
                var result = context.Stocks.Search(x => x.Name).Containing(item.Name).FirstOrDefault();
                if (result == null)
                {
                    var a = new Stock()
                    {
                        Name = item.Name,
                        Sek = item.Sek,
                        DayCounter = 1,
                        Day = todayDay,
                        Ath = item.Sek.ToString(),
                        lastUpdated = DateTime.Now,
                        Bought = false,
                        CountryCode = country.CountryCode,
                        Procent = 0
                    };
                    context.Stocks.Add(a);
                }
            }
            context.SaveChanges();

            var all = context.Stocks.Where(a => a.CountryCode == country.CountryCode).ToList();
            foreach (var item in all)
            {
                item.DayUpdated = false;
                item.Sum++;
                context.Stocks.Update(item);
            }
            context.SaveChanges();
            Console.WriteLine($"[SaveTheDay] Part 3: Done for country {country.CountryCode}");
            SeriLog.Logger(SeriLog.logType.Information, $"[SaveTheDay] Part 3: Done for country {country.CountryCode}");

            Countries.FinishCountryOff(country, Global.todaysDay);


            country.DoneForTheDay = true;
        }
    }
}
