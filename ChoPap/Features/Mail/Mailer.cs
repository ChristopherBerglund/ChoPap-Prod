using ChoPap.Features.Helper;
using ChoPap.Features.Serilog;
using ChoPap.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static ChoPap.Data.Program;

namespace ChoPap.Features.Mail
{
    public class Mailer
    {
        private static ChopapContext context;
        public Mailer(ChopapContext _context)
        {
            context = _context;
        }
        public static void EmailerSub()
        {
            //decimal sum = context.SoldStocks.Select(x => x.Balance).Sum();
            var SellEmail = new StringBuilder();
            SellEmail.Append(Global.DoneForToday/*, sum*/);
            var SellEmailSub = Global.Done;
            Mailer.SendEmail(SellEmail, SellEmailSub);
        }
        public static void SendEmail(StringBuilder _text, string _sub)
        {
            var idag = DateTime.Now.ToString("dddd");
            SmtpClient Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = Global.userName,
                    Password = Global.passWord
                }
            };

            MailAddress FromEmail = new MailAddress(Global.userName, Global.User);
            MailAddress ToEmail = new MailAddress(Global.CustomEmail);
            MailMessage Message = new MailMessage()
            {
                From = FromEmail,
                Subject = _sub,
                Body = _text.ToString()
            };

            Message.To.Add(ToEmail);
            try
            {
                Client.Send(Message);
                SeriLog.Logger(SeriLog.logType.Information, $"[Mailer] {_sub} successfully sent.");

            }
            catch (Exception e)
            {
                SeriLog.Logger(SeriLog.logType.Error, $"[Mailer] ErrorMessage: {e}.");
            }
        }

        public static void MailBuilder(BoughtStocks item)
        {
            var SellEmail = new StringBuilder();
            SellEmail.Append("Price: " + Convert.ToInt32(item.totalSum) + "\n");
            SellEmail.Append("Current Price: " + item.currentPrice + "\n");
            SellEmail.Append("Name: " + item.Name.ToString() + "\n");
            SellEmail.Append("SellPrice: " + item.sellPrice + "\n");
            SellEmail.Append("BuyPrice: " + item.pricePerShare + "\n");
            SellEmail.Append("Qty: " + item.Qty + "\n");
            SellEmail.Append("Balans: " + item.minimumBalance + "\n");
            SellEmail.Append("Owner: " + item.Owner + "\n");
            SellEmail.Append("Last Updated: " + item.lastUpdated + "\n");
            var SellEmailSub = $"SellAction / {item.Owner} / {item.minimumBalance}";

            SendEmail(SellEmail, SellEmailSub);
        }

        public static void SendLockedStocks(List<Stock> LockedStocksForExtra)
        {
            var SellEmailSub = $"Locked Stocks (test)";
            var locked = new StringBuilder();
            foreach (var stock in LockedStocksForExtra)
            {
                locked.Append($"{stock.DaySum} - {stock.Name} - {stock.Ath} \n");
            }
            SendEmail(locked, SellEmailSub);
        }
    }
}