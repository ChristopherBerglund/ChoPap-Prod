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
            Log.Logger = new LoggerConfiguration().WriteTo.File(fullPath, rollingInterval: RollingInterval.Day).CreateLogger();
            Logger(logType.Information, "ChoPap is running.");
        }

        public static void Logger(Enum logType, string message)
        {
            switch (logType.ToString())
            {
                case "Information":
                    Log.Information(message);
                    Console.WriteLine($"[INFO] {message}");
                    break;
                case "Warning":
                    Log.Warning(message);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[WAR] {message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Error":
                    Log.Error(message);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERR] {message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Buy":
                    Log.Error(message);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[BUY] {message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "Sell":
                    Log.Error(message);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"[SELL] {message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "Action":
                    Log.Error(message);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ACTION] {message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                default:

                    break;
            }
        }
        public enum logType
        {
            Warning,
            Error,
            Information,
            Buy,
            Sell,
            Action
        }


    }
}














