using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication.GUI
{
    class ChartObjectPool
    {
        private Dictionary<String, Chart> charts;
        public ChartObjectPool()
        {
            charts = new Dictionary<string, Chart>();
        }

        public void setChart(String name, Chart chart)
        {
            try
            {
                charts[name] = chart;
            }catch(Exception)
            {
                charts.Add(name, chart);
            }
        }

        public Chart getChart(String name)
        {
            try
            {
                return charts[name];
            }catch(Exception)
            {
                return null;
            }
        }

    }
}
