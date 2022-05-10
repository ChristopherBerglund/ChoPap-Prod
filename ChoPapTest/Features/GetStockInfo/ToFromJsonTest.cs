//using ChoPap.Features.Country;
//using ChoPap.Features.GetStockInfo;
//using ChoPap.Features.Helper;
//using ChoPap.Features.IsItHoliday;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace ChoPapTest.Features.GetStockInfo
//{
//    public class ToFromJsonTest
//    {
//        //private static readonly string AllStock = @"\..\..\..\" + @"\Features\GetStockInfo\Json\Stockies.json";
//        //private static readonly string path = Directory.GetCurrentDirectory();
//        //private static readonly string fullpath = Path.GetFullPath(path + AllStock);

//        [Fact]
//        public static void ImportJsonTest()
//        {
//            string path = @"C:\myCode\ChoPap-Prod\ChoPap\Features\GetStockInfo\Json\Stockies.json";
//            //Arrange
//            var arrange = 37821;
//            //Actual
//            var actual = ToFromJson.ImportJson(path);
//            //Assert
//            Assert.Equal(arrange, actual.Count);
//        }

//    }
//}