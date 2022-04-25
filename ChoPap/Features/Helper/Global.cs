using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Features.Helper
{
    public class Global
    {
        public static readonly decimal sellStopp = 0.95m;
        public static readonly decimal stockPrice = 10000m;
        public static readonly string DoneForToday = "ChoPap is Done For the day" + "\n";
        public static readonly string Done = $"ChoPap Is Done / {DateTime.Now}";
        public static readonly string userName = "christopherberglund87@gmail.com";
        public static readonly string passWord = "qexqbanudpauccha";
        public static readonly string User = "ChoPap";
        public static readonly string CustomEmail = "christopherberglund87@gmail.com";
        public static readonly string Stop = "Program is done";
        public static readonly string Holiday = "ChoPap is only running on weekdays!";
        public static readonly string stringPath = @"C:\Users\bergl\Source\Repos\CHOPAP-1.0\CHOPAP\chopap2-317621-dc0cea62cce2.json";
        public static readonly string sheet = "Blad1";
        public static readonly string SpreadsheetId = "1J0TGjdcdlZ2-ZWQaVnMkHRCCVumQ2nhOYfhn9uRDsbM";
        public static readonly string ApplicationName = "Chopap2.0";
        public static readonly string NoDataMessage = "No data was found//ErrorNoDataWasFound.";
        public static readonly string ActionHandlerMess = "ActionsHandler: Is Done";
        public static readonly string BuyAbleStockMess = "BuyAbleStocks: Is Done";
        public static readonly string MrA = "Mr A";
        public static readonly string SaveDay = "SaveTheDay: Is Done";
        public static readonly string sqlConnectionStringTest = @"Server=(localdb)\MSSQLLocalDB;Database=chopap_test;Trusted_Connection=True;MultipleActiveResultSets=true";
        //public static readonly string sqlConnectionStringProduction = @"Data Source=tcp:mssql14.unoeuro.com;Initial Catalog=chopap_se_db_stock;Persist Security Info=False;User ID=chopap_se;Password=5abwEB3xHrep;;MultipleActiveResultSets=true;";
        public static readonly string todaysDay = DateTime.Now.ToString("dddd");
        public static bool isValid = true;
    }
}
