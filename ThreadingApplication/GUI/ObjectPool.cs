using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ThreadingApplication.GUI
{
    class ObjectPool
    {
        private Dictionary<string, Grid> poolViews;
        public ObjectPool()
        {
            poolViews = new Dictionary<string, Grid>();
        }

        public void setObjectState(String name, Grid view)
        {
            try
            {
                if (poolViews[name] != null)
                {
                    poolViews[name] = view;
                }
            }
            catch (Exception)
            {
                poolViews.Add(name, view);
            }
            
        }

        public Grid getState(String name)
        {
            try
            {
                return poolViews[name];
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
