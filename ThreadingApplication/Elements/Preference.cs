using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadingApplication
{
    class Preference: Element
    {
        private Dictionary<string, string> preferences = new Dictionary<string, string>();

        public string getPreference(string str)
        {
            string result = "";

            foreach (KeyValuePair<string, string> entry in preferences)
            {
                if (entry.Key.Contains(str))
                {
                    result = entry.Value;
                }
            }
            return result;
        }

        public void changePreference(string str, string value)
        {
            preferences[str] = value;
        }

        public void addPreference(string name, string value)
        {
            preferences.Add(name, value);
        }
    }
}
