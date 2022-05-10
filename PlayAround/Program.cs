//using ChoPap.Features.Country;
//using ChoPapTest.Features.IsItHolidayOrNot;
//using Nager.Date;
//using System.Media;







using ChoPap.Features.GetStockInfo;
using static ChoPap.Model.StockModel;

var listOfStocks = ToFromJson.ImportJson();

var a = listOfStocks.Where(x => x.name == "Minesto").FirstOrDefault();
//rootobject stock = await DisplayStockFormat.SelectSpecifiedStockAsync(listOfStocks, "Minesto");


Console.WriteLine($"{a.name}");























//SystemSounds.Asterisk.Play();
//Console.ReadKey();
//SystemSounds.Asterisk.Play();
//Console.ReadKey();

//var a = GetDummyDataForTest.GetDummyDataONE();
//string todaysDate = "24-12-2022";

//var b = IsHoliday(todaysDate, a);

//static bool IsHoliday(string todaysDate, Countries contryInfo)
//{
//    if (contryInfo.IsMarketClosed == false && contryInfo.IsHalfDay == false)
//    {
//        var publicHolidays = DateSystem.GetPublicHoliday(2022, contryInfo.CountryCode);
//        foreach (var day in publicHolidays)
//        {
//            if (todaysDate == day.Date.ToString("dd/MM/yyyy"))
//            {
//                return true;
//            }
//        }
//        return false;
//    }
//    return false;
//}

//Console.WriteLine(b);

////using Nager.Date;
////using PlayAround;
////List<mom> moms = new List<mom>();
////var m = new mom[]
////{
////    new mom {name = "Kalle", done = true },
////    new mom {name = "Kallet", done = true }
////};
////moms.AddRange(m);





////Console.WriteLine();


////bool AllDone = m.Where(x => x.done == false).Any();
////if (AllDone == false)
////{
////    Console.WriteLine("Exit");
////    //isValid = true;
////}
////else { Console.WriteLine("Still running"); }












//////List<string> mcSWEDEN = new List<string>()
//////            {
//////                "26-12-2022",
//////                "04-11-2022",
//////                "24-06-2022",
//////                "06-06-2022",
//////                "26-05-2022",
//////                "18-04-2022",
//////                "15-04-2022",
//////                "14-04-2022",
//////                "06-01-2022",
//////                "05-01-2022",
//////                "14-04-2022",
//////                "25-05-2022"
//////            };

////var publicHolidays = DateSystem.GetPublicHoliday(2022, CountryCode.SE);
////foreach (var item in publicHolidays)
////{
////    Console.WriteLine($"[{item.Date}]{item.LocalName}");
////}
//////int a = 0;
//////int b = 0;

//////foreach (var publicHoliday in publicHolidays)
//////{
//////    Console.WriteLine(publicHoliday.Date.ToString("dd/MM/yyyy") + " " + publicHoliday.LocalName);
//////    //foreach (var date in mcSWEDEN)
//////    //{
//////    //    if(publicHoliday.Date.ToString("dd/MM/yyyy") == date)
//////    //    {
//////    //        Console.WriteLine(publicHoliday.Date.ToString("dd/MM/yyyy") + " - " + date);
//////    //        a++;
//////    //    }
//////    //}
//////}

//////Console.WriteLine(a);




////////List<string> hdSWEDEN = new List<string>()
////////            {
////////                "14-04-2022",
////////                "25-05-2022"
////////            };
