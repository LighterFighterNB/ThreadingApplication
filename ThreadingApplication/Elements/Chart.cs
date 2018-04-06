using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication
{
    class Chart: Element
    {
        private String name;
        public Chart(string name)
        {

        }
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

        }

        public Chart()
        {

        }

        public string Name { get; set; }
        public int Amount { get; set; }
    }
}
