using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication
{
    class Database
    {
        private String conn;

        public void createUser(String user, String pass)
        {

        }

        public bool checkUser(String user, String pass)
        {
            return false;
        }

        public void addPreference(Preference p)
        {

        }

        private List<Preference> getPreferences()
        {
            return null;
        }

        public void addDashboard(Dashboard d)
        {

        }

        public List<Dashboard> getDashboard()
        {
            return null;
        }

        public void addChartToDashboard(Chart c, Dashboard d)
        {

        }
    }
}
