using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication.GUI
{
    class ViewManager
    {
        private StateView currentView;
        private StateView nextView;
        private StateView prevView;
        private MainPage main;
        public ViewManager(MainPage main)
        {
            currentView = null;
            nextView = null;
            prevView = null;
            this.main = main;
        }

        public void setCurrentView(StateView view)
        {
            currentView = view;
        }

        public void updateMain()
        {
            main.update();
        }

        public void setNextView(StateView view)
        {
            nextView = view;
        }

        public void setPreviousView(StateView view)
        {
            prevView = view;
        }

        public StateView getCurrentView()
        {
            return currentView;
        }
        public StateView getNextView()
        {
            return nextView;
        }
        public StateView getPerviousView()
        {
            return prevView;
        }
    }
}
