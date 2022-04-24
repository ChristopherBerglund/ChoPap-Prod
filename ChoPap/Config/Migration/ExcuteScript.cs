using ChoPap.Config;
using ChoPap.Features.Serilog;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Serilog;
using System.IO;
using static ChoPap.Features.Serilog.SeriLog;

namespace ChoPap.Features.Migration
{

    public partial class ExcuteScript
    {
        private static readonly string AllStock = @"\..\..\..\" + @"\Config\Migration\Scripts";
        private static readonly string path = Directory.GetCurrentDirectory();
        private static readonly string fullPath = Path.GetFullPath(path + AllStock);
        public static void Page_Load()
        {
            SqlConnection conn = new SqlConnection(Global.sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            string[] scriptFiles = Directory.GetFiles(fullPath, "*.*", SearchOption.AllDirectories);
            foreach (var file in scriptFiles)
            {
                try
                {
                    string scriptFile = File.ReadAllText(file);
                    server.ConnectionContext.ExecuteNonQuery(scriptFile);
                }
                catch (Exception e)
                {
                    SeriLog.Logger(SeriLog.logType.Error, $"[ExecuteScript, Page_Load()] error: {e})");
                    throw;
                }
            }
        }
    }
}

