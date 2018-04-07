using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Threading;

namespace ThreadingApplication
{
    class Dashboard: Element
    {
        private List<Chart> charts;

        public Dashboard(String name)
        {
            this.title = name;
            charts = new List<Chart>();
        }

        public void addChart(Chart c)
        {
            charts.Add(c);
        }

        public List<Chart> getCharts()
        {
            return charts;
        }

        private async void update()
        {
            foreach(Chart chart in charts)
            {
                Task t = new Task(() =>
                {
                    lock (this)
                    {
                        chart.setStock();
                    }

                });
            }
        }
    }
}
