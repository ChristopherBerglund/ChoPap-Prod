using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace ChoPap.Features.Migration
{

    public partial class ExcuteScript 
    { 
        public static void Page_Load()
        {
            string sqlConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=chopap_test;Trusted_Connection=True;MultipleActiveResultSets=true";

            string script = File.ReadAllText(@"C:\myCode\ChoPap-Prod\ChoPap\Features\Migration\Scripts\testScript.sql");

            SqlConnection conn = new SqlConnection(sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));

            server.ConnectionContext.ExecuteNonQuery(script);
        }
    }

}

