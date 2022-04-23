using ChoPap.Features.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPapTest.Features.IsItHolidayOrNot
{
    public class GetDummyDataForTest
    {
        public static Countries GetDummyDataONE()
        {
            List<string> mcSWEDEN = new List<string>()
            {
                "26-12-2022",
                "04-11-2022",
                "24-06-2022",
                "06-06-2022",
                "26-05-2022",
                "18-04-2022",
                "15-04-2022",
                "14-04-2022",
                "06-01-2022",
                "05-01-2022"
            };
            List<string> hdSWEDEN = new List<string>()
            {
                "14-04-2022",
                "25-05-2022"
            };
            var country = new Countries()
            {
                CountryCode = "SE",
                RedDays = mcSWEDEN,
                HalfDays = hdSWEDEN,
                Opens = new TimeSpan(09, 00, 00),
                Closes = new TimeSpan(17, 30, 00),
                ClosesHalfDay = new TimeSpan(13, 00, 00)
            };

            return country;
        }
    }
}
