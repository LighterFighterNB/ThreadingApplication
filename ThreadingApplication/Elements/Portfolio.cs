using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication
{
    class Portfolio: Element
    {
       private string title;
        private List<Currency> currencies;

        public Portfolio(String name)
        {
            this.title = name;
            currencies = new List<Currency>();
        }

        public void addCurrency(Currency c)
        {
            currencies.Add(c);
        }

        public List<Currency> getCurrencies()
        {
            return currencies;
        }

        public string getTitle()
        {
            return title;
        }

        private async void update()
        {
            foreach (Currency currency in currencies)
            {
                Task t = new Task(() =>
                {
                    lock (this)
                    {
                        //chart.setStock();
                    }

                });
            }
        }
    }
}
