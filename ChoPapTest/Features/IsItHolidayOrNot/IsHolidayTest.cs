using ChoPap.Features.IsItHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChoPapTest.Features.IsItHolidayOrNot
{
    public class IsHolidayTest
    {
        [Fact]
        public void IsHoliday_returnTrue()
        {
            string todaysDate = "24-12-2022";
            var country = GetDummyDataForTest.GetDummyDataONE();

            var arrange = true;
            
            var actual = IsItHoliday.IsHoliday(todaysDate, country);

            Assert.Equal(arrange, actual);
        }
        [Fact]

        public void IsHoliday_returnFalse()
        {
            string todaysDate = "20-04-2022";
            var country = GetDummyDataForTest.GetDummyDataONE();

            var arrange = false;

            var actual = IsItHoliday.IsHoliday(todaysDate, country);

            Assert.Equal(arrange, actual);
        }

    }
}
