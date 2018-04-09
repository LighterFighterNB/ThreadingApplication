using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace ThreadingApplication
{
    class Database
    {
        private System.Text.EncodingProvider ppp;
        private string address;
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private string email;

        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public Database()
        {
            ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
            address = "server=localhost;uid=root;pwd=;database=threading_db;SslMode=None";
            conn = new MySqlConnection(address);
            conn.Open();
            cmd = conn.CreateCommand();
        }

        public void closeConnection()
        {
            conn.Close();
        }

        public void logout()
        {
            email = "";
        }

        public void createUser(string email, string user, string password)
        {
            try
            {
                cmd.CommandText = "INSERT INTO `user`(`email`, `name`, `password`) VALUES ('" + email + "','" + user + "','" + password + "')";
                this.email = email;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool checkUser(string email, string password)
        {
            bool success = false;
            MySqlDataReader mySqlDataReader = null;
            try
            {
                cmd.CommandText = "SELECT `password` FROM `user` WHERE `email` = '" + email + "'";
                mySqlDataReader = cmd.ExecuteReader();
                mySqlDataReader.Read();
                if (mySqlDataReader.HasRows && mySqlDataReader.GetValue(0).Equals(password))
                {
                    success = true;
                    localSettings.Values["email"] = email;
                }
                mySqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                mySqlDataReader.Close();
            }
            return success;
        }

        public void addDefaultPreferences()
        {
            try
            {
                cmd.CommandText = "INSERT INTO `preference`(`email`, `displayType`, ` currency`) VALUES ('" + email + "','Light','EUR')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void savePreferences(Preference preferences)
        {
            try
            {
                email = localSettings.Values["email"].ToString();
                cmd.CommandText = "UPDATE `preference` " +
                    "SET `email` = '" + email + "'," +
                    "`displayType` = '" + preferences.getPreference("displayType") + "'," +
                    "` currency` = '" + preferences.getPreference("currency") + "'" +
                    " WHERE `email` = '" + email + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public Preference loadPreferences()
        {
            Preference preferences = new Preference();
            try
            {
                email = localSettings.Values["email"].ToString();
                cmd.CommandText = "SELECT `displayType`,` currency` FROM `preference` WHERE `email` = '" + email + "'";
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while(mySqlDataReader.Read())
                {
                    Debug.WriteLine(mySqlDataReader.GetString(0));
                    preferences.addPreference(mySqlDataReader.GetName(0), mySqlDataReader.GetString(0));
                    preferences.addPreference(mySqlDataReader.GetName(1), mySqlDataReader.GetString(1));
                }
                mySqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return preferences;
        }

        public void addDashboard(string name)
        {
            try
            {
                cmd.CommandText = "INSERT INTO `dashboard`(`email`, `dashboardName`) VALUES ('" + email + "','" + name + "')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void addChart(string dashboard, string name, string fromCur, string toMarket, string refreshRate)
        {
            try
            {
                email = localSettings.Values["email"].ToString();
                cmd.CommandText = "INSERT INTO `chart`(`dashboard`, `name`,`fromCur`,`toMark`,`refreshRate`,`email`) VALUES ('" + dashboard + "','" + name + "','" + fromCur + "','" + toMarket + "','" + refreshRate + "','" + email + "')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void saveDashboard(Dashboard dashboard)
        {
            List<string> commands = new List<string>();
            try
            {
                email = localSettings.Values["email"].ToString();
                cmd.CommandText = "SELECT `fromCur`,`toMark`,`refreshRate`, `name` FROM `chart` WHERE `dashboard` = '" + dashboard.getTitle() + "'&& `email` = '" + email + "'";
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    string name = mySqlDataReader.GetString(3);
                    foreach (Chart chart in dashboard.getCharts())
                    {
                        if (chart.getName().Equals(name))
                        {
                            commands.Add("UPDATE `chart` " +
                    "SET `fromCur` = '" + chart.getFrom() + "'," +
                    "`toMark` = '" + chart.getTo() + "'," +
                    "`refreshRate` = '" + chart.getFunction() + "'" +
                    " WHERE `name` = '" + name + "'");
                        }
                    }
                }
                mySqlDataReader.Close();
                foreach (string command in commands)
                {
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public Dashboard loadDashboard(string name)
        {
            Dashboard dashboard = null;
            try
            {
                email = localSettings.Values["email"].ToString();
                dashboard = new Dashboard(name);
                cmd.CommandText = "SELECT `name`, `fromCur`, `toMark`, `refreshRate` FROM `chart` WHERE `dashboard` = '" + name + "' && `email` = '" + email + "'";
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    dashboard.addChart(new Chart(mySqlDataReader.GetString(0), mySqlDataReader.GetString(1), mySqlDataReader.GetString(2), mySqlDataReader.GetString(3)));
                }
                mySqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return dashboard;
        }

        public void addPortfolio(string name)
        {
            try
            {
                cmd.CommandText = "INSERT INTO `portfolio`(`email`, `name`) VALUES ('" + email + "','" + name + "')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void addCurrency(string portfolio, string name, double owned)
        {
            try
            {
                email = localSettings.Values["email"].ToString();
                cmd.CommandText = "INSERT INTO `currency`(`portfolio`, `type`,`owned`,`email`) VALUES ('" + portfolio + "','" + name + "','" + owned + "','" + email + "')";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void savePortfolio(Portfolio portfolio)
        {
            List<string> commands = new List<string>();
            try
            {
                email = localSettings.Values["email"].ToString();
                cmd.CommandText = "SELECT `owned`, `name` FROM `currency` WHERE `portfolio` = '" + portfolio.getTitle() + "' && `email` = '" + email+ "'";
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    string name = mySqlDataReader.GetString(2);
                    foreach (Currency currency in portfolio.getCurrencies())
                    {
                        if (currency.getName().Equals(name))
                        {
                            commands.Add("UPDATE `currency` " +
                    "SET `owned` = '" + currency.getOwned() +
                    " WHERE `name` = '" + name + "'");
                        }
                    }
                }
                mySqlDataReader.Close();
                foreach (string command in commands)
                {
                    cmd.CommandText = command;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public Portfolio loadPortfolio(string name)
        {
            Portfolio portfolio = null;
            try
            {
                email = localSettings.Values["email"].ToString();
                portfolio = new Portfolio(name);
                cmd.CommandText = "SELECT `type`, `owned` FROM `currency` WHERE `portfolio` = '" + name + "' && `email` = '"+email+"'";
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    portfolio.addCurrency(new Currency(mySqlDataReader.GetString(0), mySqlDataReader.GetDouble(1)));
                }
                mySqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return portfolio;
        }

        public void setLocalSettingEmail()
        {
            email = localSettings.Values["email"].ToString();
        }
    }
}
