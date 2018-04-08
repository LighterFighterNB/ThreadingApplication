using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadingApplication.Elements;

namespace ThreadingApplication
{
    class Chart: Element
    {
        private String name;
        private List<Stock> stocks;
        /// <summary>
        /// Constructor Chart
        /// </summary>
        /// <param name="name">The name of the chart</param>
        /// <param name="from">Digital Currency (Ex: BTC, XMR, ETH, BNC) </param>
        /// <param name="to">Market Currency (Ex: EUR, USD, RON, YEN) </param>
        /// <param name="function"> sets preferance regarding cryptocurrency </param>
        public Chart(String name, String from, String to, String function)
        {
            this.name = name;
            alphaManager = alphaFactory.getApiRequest(function, from, to);
            setStock();
        }
        public Chart()
        {

        }

        public async 
        Task
setStock()
        { 
            await alphaManager.setStocks();
            stocks = new List<Stock>();
            stocks = alphaManager.getStocks();
        }

        public List<Stock> Stocks {
            get { return stocks; }
        }
        public String Date { get; set; }
        public double Amount { get; set; }

        //public String Name
        //{
        //    get{ return name; }
        //    set { name = value; }
        //}
    }
}
