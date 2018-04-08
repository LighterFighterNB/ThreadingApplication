using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication.Elements
{
    class ApiRequestWeekly : AlphaManager
    {

        public ApiRequestWeekly(String from, String to)
        {
            apiString = "https://www.alphavantage.co/query?function=";
            function = "DIGITAL_CURRENCY_WEEKLY";
            symbol = "&symbol=" + from;
            market = "&market=" + to;
            toMarket = to;
            fromCurrency = from;
            bodyTag = "Time Series(Digital Currency Weekly)";
            apiKey = "&apikey=FQWIO3W4ONAZNLGL";
            apiString = apiString + function + symbol + market + apiKey;
            stocks = new List<Stock>();
        }

        public override Notification getNews()
        {
            return null;
        }

        public override List<Stock> getStocks()
        {
            return stocks;
        }
    }
}
