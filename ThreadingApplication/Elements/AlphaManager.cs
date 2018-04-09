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
    abstract class AlphaManager
    { 
        protected String apiString;
        protected String function;
        protected String symbol;
        protected String market;
        protected String apiKey;
        protected String fromCurrency;
        protected String toMarket;
        protected String bodyTag;
        protected List<Stock> stocks;
        public abstract List<Stock> getStocks();
        public abstract Notification getNews();

        /// <summary>
        /// This function gets the 
        /// </summary>
        /// <param name="from">The cryptocurrency from which you want to see the value</param>
        /// <param name="to"> The market available stock ex: EUR, RON, GBR (British Pound) </param>
        /// <returns></returns>
        public async Task setStocks()
        {

            stocks = new List<Stock>();//DIGITAL_CURRENCY_MONTHLY
            List<String> def = new List<String>();
            def.Add("1a. open ");
            def.Add("2a. high ");
            def.Add("3a. low ");
            def.Add("4a. close ");
            List<String> modify = new List<String>();
            foreach (String prop in def)
            {
                modify.Add(prop + "(" + toMarket + ")");
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
                            JObject body = (JObject)o[bodyTag];
                            foreach (JProperty p in body.Properties())
                            {
                               // Debug.WriteLine("\"" + p.Name + "\"" + " : " + p.Value);
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
    }
}
