using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Config
{
    public class Global
    {
        public static readonly string sqlConnectionStringTest = @"Server=(localdb)\MSSQLLocalDB;Database=chopap_test;Trusted_Connection=True;MultipleActiveResultSets=true";
        //public static readonly string sqlConnectionStringProduction = @"Data Source=tcp:mssql14.unoeuro.com;Initial Catalog=chopap_se_db_stock;Persist Security Info=False;User ID=chopap_se;Password=5abwEB3xHrep;;MultipleActiveResultSets=true;";
        public static readonly string todaysDay = DateTime.Now.ToString("dddd");
        public static bool isValid = true;



    }
}


