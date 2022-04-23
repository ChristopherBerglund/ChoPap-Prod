using ChoPap.Features.IsItHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ChoPapTest.Features.IsItHolidayOrNot
{
    public class IsHalfDayTest
    {
        [Fact]
        public void IsHalfDay_returnTrue()
        {
            string todaysDate = "14-04-2022";
            var country = GetDummyDataForTest.GetDummyDataONE();

            //Arrange
            var arrange = true;
            //Actual
            var actual = IsItHoliday.IsHalfDay(todaysDate, country);
            //Assert
            Assert.Equal(arrange, actual);
        }

        public void IsHalfDay_returnFalse()
        {
            string todaysDate = "20-04-2022";
            var country = GetDummyDataForTest.GetDummyDataONE();

            //Arrange
            var arrange = false;
            //Actual
            var actual = IsItHoliday.IsHalfDay(todaysDate, country);
            //Assert
            Assert.Equal(arrange, actual);
        }
    }
}
