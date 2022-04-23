using ChoPap.Features.Country;
using ChoPap.Features.IsItHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChoPapTest.Features.IsItHolidayOrNot
{
    public class IsDeviatingOperningHoursTest
    {
        [Fact]
        public static void IsDeviatingOpeningHoursTRUE()
        {
            string todaysDate = "26-12-2022";
            var country = GetDummyDataForTest.GetDummyDataONE();

            //Arrange
            var arrange = true;
            //Actual
            var actual = IsItHoliday.IsDeviatingOpeningHours(todaysDate, country);
            //Assert
            Assert.Equal(arrange, actual);
        }

        [Fact]
        public static void IsDeviatingOpeningHoursFalse()
        {
            string todaysDate = "20-04-2022";
            var country = GetDummyDataForTest.GetDummyDataONE();

            //Arrange
            var arrange = false;
            //Actual
            var actual = IsItHoliday.IsDeviatingOpeningHours(todaysDate, country);
            //Assert
            Assert.Equal(arrange, actual);
        }





            
    }
}
