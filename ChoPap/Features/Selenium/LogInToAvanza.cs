using ChoPap.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Model.StockModel;

namespace ChoPap.Features.Selenium
{
    public class LogInToAvanza
    {
        public static readonly bool LaptopConfiguration = false; //True for laptop, false for Computer
        public static void OpenSelenium(EdgeDriver drv)
        {
            List<Coordinates> Coords = new List<Coordinates>();
            //Coords = Coordinates.ImportJson();
            EdgeDriverService service = EdgeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            drv.Manage().Window.Maximize();
            drv.Navigate().GoToUrl("https://www.avanza.se");
            if (!IsSuccesful(drv))
            {
                Thread.Sleep(2000);
                if (LaptopConfiguration)
                {
                    cursor.MoveCur(1840, 130, true);
                    Thread.Sleep(1000);
                    cursor.MoveCur(1840, 210, true);
                    Thread.Sleep(1000);
                    EnterUsername(drv);
                    cursor.MoveCur(1840, 320, true);
                    Thread.Sleep(1000);
                    EnterPassword(drv);
                    cursor.MoveCur(1840, 400, true);
                    Thread.Sleep(1000);
                    GetAuthyCredentials();
                    cursor.MoveCur(1840, 300, true);
                    GetValueFromClipboard(drv);
                    Thread.Sleep(5000);
                    cursor.MoveCur(1400, 980, true);
                }
                else
                {
                    //ComputerConfiguration
                    cursor.MoveCur(3380, 130, true);
                    Thread.Sleep(1000);
                    cursor.MoveCur(3380, 210, true);
                    Thread.Sleep(1000);
                    EnterUsername(drv);
                    cursor.MoveCur(3380, 320, true);
                    Thread.Sleep(1000);
                    EnterPassword(drv);
                    cursor.MoveCur(3380, 400, true);
                    Thread.Sleep(1000);
                    GetAuthyCredentials();
                    cursor.MoveCur(3380, 300, true);
                    GetValueFromClipboard(drv);
                    Thread.Sleep(5000);
                    cursor.MoveCur(2980, 980, true);
                    
                }
                
            }
            if (IsSuccesful(drv))
            {
                Console.WriteLine("You succesfully logged in!");
                //Screenshot ss = ((ITakesScreenshot)drv).GetScreenshot();
                //ss.SaveAsFile(@"C:\Screenshots\Image.png",
                //ScreenshotImageFormat.Png);
                drv.Manage().Window.Minimize();
            }
            else
            {
                Console.WriteLine("Something went wrong");
                drv.Close();
                OpenSelenium(drv);
            }

        }
        public static void EnterUsername(EdgeDriver drv)
        {
            new Actions(drv).SendKeys("bober_x").Perform();
        }
        public static void EnterPassword(EdgeDriver drv)
        {
            new Actions(drv).SendKeys("Opalisal1").Perform();
        }
        public static void GetValueFromClipboard(EdgeDriver drv)
        {
            new Actions(drv).KeyDown(Keys.Control).SendKeys("v").KeyUp(Keys.Control).Perform();
            Thread.Sleep(1000);
            new Actions(drv).KeyDown(Keys.Enter).KeyUp(Keys.Control).Perform();
        }
        public static void GetAuthyCredentials()
        {
            var p = new Process();
            p.StartInfo.FileName = @"C:\Users\bergl\AppData\Local\authy\Authy Desktop.exe";
            p.Start();
            if (LaptopConfiguration)
            {
                Thread.Sleep(5000);
                cursor.MoveCur(1000, 300, true);
                Thread.Sleep(1000);
                cursor.MoveCur(1100, 700, true);
                Thread.Sleep(1000);
            }
            else
            {
                //ComputerConfiguration
                Thread.Sleep(5000);
                cursor.MoveCur(1750, 500, true);
                Thread.Sleep(1000);
                cursor.MoveCur(1870, 890, true);
                Thread.Sleep(1000);
            }
            p.CloseMainWindow();

        }

        public static void ShutEdgeDown(EdgeDriver drv)
        {
            drv.Close();
        }
        public static void goToStock(rootobject stock, BoughtStocks? stocky, EdgeDriver drv, string action)
        {
            var modifiedName = stock.name.Split(' ');
            string beginning;
            if (action == "Sell")
            {
                beginning = "https://www.avanza.se/handla/aktier.html/salj/";
            }
            else
            {
                beginning = "https://www.avanza.se/handla/aktier.html/kop/";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(beginning);
            sb.Append($"{stock.orderbookid}/");
            foreach (var letter in modifiedName.ToString())
            {
                if (letter == 'å' || letter == 'ä')
                {
                    sb.Append('a');
                }
                else if (letter == 'ö' || letter == 'ø')
                {
                    sb.Append('o');
                }
                else
                {
                    sb.Append(letter);
                }
                //sb.Append($"{letter}-");
            }
            sb.Append("?re=1");

            drv.Manage().Window.Maximize();
            drv.Navigate().GoToUrl(sb.ToString());
            if (action == "Sell")
            {
                MakeBuy(stocky, Convert.ToDecimal(stocky.currentPrice), drv);
            }
            else
            {
                MakeSell(stocky, Convert.ToDecimal(stocky.currentPrice), drv);
            }
        }


        public static void MakeBuy(BoughtStocks stocky, decimal price, EdgeDriver drv)
        {
            Thread.Sleep(4000);
            drv.FindElement(By.Id("volume")).SendKeys(stocky.Qty.ToString()); Thread.Sleep(1000);
            drv.FindElement(By.Id("price")).Clear(); Thread.Sleep(1000);
            drv.FindElement(By.Id("price")).SendKeys(price.ToString() /*+ Keys.Enter*/); Thread.Sleep(1000);

            Screenshot(drv, stocky, "buy");
            drv.Navigate().GoToUrl("https://www.avanza.se/hem/senaste.html");
            drv.Manage().Window.Minimize();
        }

        public static void MakeSell(BoughtStocks stocky, decimal price, EdgeDriver drv)
        {
            Thread.Sleep(4000);
            drv.FindElement(By.Id("volume")).SendKeys(stocky.Qty.ToString()); Thread.Sleep(1000);
            drv.FindElement(By.Id("price")).Clear(); Thread.Sleep(1000);
            drv.FindElement(By.Id("price")).SendKeys(price.ToString() /*+ Keys.Enter*/); Thread.Sleep(1000);

            Screenshot(drv, stocky, "buy");
            drv.Navigate().GoToUrl("https://www.avanza.se/hem/senaste.html");
            drv.Manage().Window.Minimize();
        }

        public static void Screenshot(EdgeDriver drv, BoughtStocks stocky, string action)
        {
            string modifiedName = stocky.Name.Replace(@"\", "");
            Screenshot ss = ((ITakesScreenshot)drv).GetScreenshot();
            string path = @"C:\Screenshots\";
            string name = $"{action}.{modifiedName}.{DateTime.Now.ToFileTimeUtc()}.png";
            string pathName = path + name;
            ss.SaveAsFile(pathName,
            ScreenshotImageFormat.Png);
        }


        static bool IsSuccesful(EdgeDriver drv)
        {
            if (drv.Url == "https://www.avanza.se/hem/senaste.html")
            {
                return true;
            }
            return false;

        }
    }
}
