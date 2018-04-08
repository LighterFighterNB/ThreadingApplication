using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication
{
    class Currency: Element
    {
        private string name;
        private double owned;

        public Currency(String name, double owned)
        {
            this.name = name;
            this.owned = owned;
        }

        public string getName()
        {
            return name;
        }

        public void setOwned(double owned)
        {
            this.owned = owned;
        }
        public double getOwned()
        {
            return owned;
        }

    }
}
