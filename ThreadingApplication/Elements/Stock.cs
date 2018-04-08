using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication.Elements
{
    class Stock
    {
        public String date;
        public Dictionary<String, String> proprieties;
        public Stock()
        {
            proprieties = new Dictionary<string, string>();
        }

        public Stock(String date)
        {
            this.date = date;
            proprieties = new Dictionary<string, string>();
        }

        public String Date {
            get { return date; }
            set { date = value; }
        }

        public Dictionary<String, String> Proprieties
        {
            get { return proprieties; }
        }
    }
}
