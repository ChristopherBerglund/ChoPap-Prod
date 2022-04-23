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
    public class MarketIsClosedTest
    {
        [Fact]
        public void MarketIsClosed_returnFalse()
        {
            string todaysDate = "lördag";

            //Arrange
            var arrange = true;
            //Actual
            bool actual = IsItHoliday.MarketIsClosed(todaysDate);
            //Assert
            Assert.Equal(arrange, actual);
        }

        [Fact]
        public void MarketIsClosed_returnTrue()
        {
            string todaysDate = "måndag";

            //Arrange
            var arrange = false;
            //Actual
            bool actual = IsItHoliday.MarketIsClosed(todaysDate);
            //Assert
            Assert.Equal(arrange, actual);
        }


       
    }
}
