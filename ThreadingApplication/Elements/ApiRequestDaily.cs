using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using ThreadingApplication.Elements;

namespace ThreadingApplication
{
    class ApiRequestDaily : AlphaManager
    {
        public ApiRequestDaily(String from, String to)
        {
            apiString = "https://www.alphavantage.co/query?function=";
            function = "DIGITAL_CURRENCY_DAILY";
            symbol = "&symbol=" + from;
            market = "&market=" + to;
            toMarket = to;
            fromCurrency = from;
            bodyTag = "Time Series (Digital Currency Daily)";
            apiKey = "&apikey=FQWIO3W4ONAZNLGL";
            apiString = apiString + function + symbol + market + apiKey;
            stocks = new List<Stock>();
        }

        public override List<Stock> getStocks()
        {
            return stocks;
        }

        public override Notification getNews()
        {
            return null;
        }

    }
}
