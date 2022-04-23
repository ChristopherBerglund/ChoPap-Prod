using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Features.Serilog
{
    public class SeriLog
    {
        private static readonly string path = Directory.GetCurrentDirectory() + @"\logs";
        private static readonly string name = "log-.txt";
        private static readonly string fullPath = Path.Combine(path, name);
        public static void SerilogBuild()
        {
            //string path = Directory.GetCurrentDirectory() + @"\logs";
            //string name = "log-.txt";
            //string fullPath = Path.Combine(path, name);
            Log.Logger = new LoggerConfiguration().WriteTo.File(fullPath, rollingInterval: RollingInterval.Day).CreateLogger();
            Logger(logType.Information, "ChoPap is running.");
        }

        public static void Logger(Enum logType, string message)
        {
            Console.WriteLine(logType.ToString());

            switch (logType.ToString())
            {
                case "Information":
                    Log.Information(message);
                    break;
                case "Warning":
                    Log.Warning(message);
                    break;
                case "Error":
                    Log.Error(message);
                    break;
                default:

                    break;
            }
        }
        public enum logType
        {
            Warning,
            Error,
            Information
        }
    }
}
