using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ThreadingApplication
{
    abstract class StateView
    {
        private Api api;
        private Database db;
        public abstract Grid getView();
    }
}
