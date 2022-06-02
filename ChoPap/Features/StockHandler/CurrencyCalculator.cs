using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPap.Features.StockHandler
{
    public class CurrencyCalculator
    {
        public static float GetCurrencyRateInEuro(string currency)
        {
            try
            {
                string rssUrl = string.Concat("http://www.ecb.int/rss/fxref-", currency.ToLower() + ".html");
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(rssUrl);

                System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
                nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");

                System.Xml.XmlNodeList nodeList = doc.SelectNodes("//rdf:item", nsmgr);

                foreach (System.Xml.XmlNode node in nodeList)
                {
                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";
                    try
                    {
                        float exchangeRate = float.Parse(
                            node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr).InnerText,
                            NumberStyles.Any,
                            ci);

                        return exchangeRate;
                    }
                    catch { }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
