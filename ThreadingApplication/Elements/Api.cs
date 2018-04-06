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
    class Api
    {
        private String apiString;
        private String function;
        private String symbol;
        private String market;
        private String apiKey;
        private List<Stock> stocks;
        public Api()
        {
            apiString = "https://www.alphavantage.co/query?function=";
            function = "DIGITAL_CURRENCY_DAILY";
            symbol = "&symbol=BTC";
            market = "&market=EUR";
            apiKey = "&apikey=FQWIO3W4ONAZNLGL";
            apiString = apiString + function + symbol + market + apiKey;
            stocks = new List<Stock>();
        }
        /// <summary>
        /// This function gets the 
        /// </summary>
        /// <param name="from">The cryptocurrency from which you want to see the value</param>
        /// <param name="to"> The market available stock ex: EUR, RON, GBR (British Pound) </param>
        /// <returns></returns>
        public async Task setCryptoDayly(String from, String to)
        {
            stocks = new List<Stock>();
            apiString = "https://www.alphavantage.co/query?function=";
            function = "DIGITAL_CURRENCY_DAILY";
            symbol = "&symbol="+from;
            market = "&market="+to;
            apiKey = "&apikey=FQWIO3W4ONAZNLGL";
            apiString = apiString + function + symbol + market + apiKey;
            List<String> def = new List<String>();
            def.Add("1a. open ");
            def.Add("2a. high ");
            def.Add("3a. low ");
            def.Add("4a. close ");
            List<String> modify = new List<String>();
            foreach(String prop in def)
            {
                modify.Add(prop + "(" + to + ")");
            }
            using (HttpClient c = new HttpClient())
            {
                using (HttpResponseMessage get = await c.GetAsync(apiString))
                {
                    using (HttpContent cont = get.Content)
                    {
                        String result = await cont.ReadAsStringAsync();
                        if(result != null)
                        {
                            JObject o = JObject.Parse(result);
                            JObject body = (JObject)o["Time Series (Digital Currency Daily)"];
                            foreach(JProperty p in body.Properties())
                            {                                
                                //Debug.WriteLine("\""+ p.Name + "\"" + " : " + p.Value);
                                JObject propriety = JObject.Parse(p.Value.ToString());
                                if (p.Name.Contains("2018"))
                                {

                                    Stock s = new Stock(p.Name);
                                    foreach (String prop in modify)
                                    {
                                        s.Proprieties.Add(prop, propriety.GetValue(prop).ToString());
                                        ///Debug.WriteLine(propriety.GetValue(prop));
                                    }
                                    this.stocks.Add(s);
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                        else
                        {
                            Debug.WriteLine("There is nothing to be displayed");
                        }
                    }
                }
            }
        }


        /// <summary>
        /// This function gets the 
        /// </summary>
        /// <param name="from">The cryptocurrency from which you want to see the value</param>
        /// <param name="to"> The market available stock ex: EUR, RON, GBR (British Pound) </param>
        /// <returns></returns>
        public async Task setCryptoMontly(String from, String to)
        {
            stocks = new List<Stock>();//DIGITAL_CURRENCY_MONTHLY
            apiString = "https://www.alphavantage.co/query?function=";
            function = "DIGITAL_CURRENCY_MONTHLY";
            symbol = "&symbol=" + from;
            market = "&market=" + to;
            apiKey = "&apikey=FQWIO3W4ONAZNLGL";
            apiString = apiString + function + symbol + market + apiKey;
            List<String> def = new List<String>();
            def.Add("1a. open ");
            def.Add("2a. high ");
            def.Add("3a. low ");
            def.Add("4a. close ");
            List<String> modify = new List<String>();
            foreach (String prop in def)
            {
                modify.Add(prop + "(" + to + ")");
            }
            using (HttpClient c = new HttpClient())
            {
                using (HttpResponseMessage get = await c.GetAsync(apiString))
                {
                    using (HttpContent cont = get.Content)
                    {
                        String result = await cont.ReadAsStringAsync();
                        if (result != null)
                        {
                            JObject o = JObject.Parse(result);
                            JObject body = (JObject)o["Time Series (Digital Currency Monthly)"];
                            foreach (JProperty p in body.Properties())
                            {
                                Debug.WriteLine("\"" + p.Name + "\"" + " : " + p.Value);
                                JObject propriety = JObject.Parse(p.Value.ToString());
                                if (p.Name.Contains("2018"))
                                {
                                    Stock s = new Stock(p.Name);
                                    foreach (String prop in modify)
                                    {
                                        s.Proprieties.Add(prop, propriety.GetValue(prop).ToString());
                                        ///Debug.WriteLine(propriety.GetValue(prop));
                                    }
                                    this.stocks.Add(s);
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                        else
                        {
                            Debug.WriteLine("There is nothing to be displayed");
                        }
                    }
                }
            }
        }
        public List<Stock> getStocks()
        {
            return stocks;
        }

        public Notification getNews()
        {
            return null;
        }

    }
}
