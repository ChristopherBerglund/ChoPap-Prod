using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Config
{
    public class Global
    {
        public static readonly string sqlConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=chopap_test;Trusted_Connection=True;MultipleActiveResultSets=true";
        public static readonly string todaysDay = DateTime.Now.ToString("dddd");
        public static bool isValid = true;



    }
}
