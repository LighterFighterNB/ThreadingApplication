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

        public void createUser(string email, string user, string password)
        {
            try
            {
                cmd.CommandText = "INSERT INTO `user`(`email`, `name`, `password`) VALUES ('" + email + "','" + user + "','" + password + "')";
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
            try
            {
                cmd.CommandText = "SELECT `password` FROM `user` WHERE `email` = '" + email + "'";
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                mySqlDataReader.Read();
                if (mySqlDataReader.HasRows && mySqlDataReader.GetValue(0).Equals(password))
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return success;
        }

        public void savePreferences(string email, Preference preferences)
        {
            try
            {
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

        public Preference loadPreferences(string email)
        {
            Preference preferances = new Preference();
            try
            {
                cmd.CommandText = "SELECT * FROM `preference` WHERE `email` = '" + email + "'";
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();
                mySqlDataReader.Read();
                for (int i = 0; i < mySqlDataReader.FieldCount; i++)
                {
                    preferances.addPreference(mySqlDataReader.GetName(i), mySqlDataReader.GetString(i));
                }
                mySqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return preferances;
        }

        public void addDashboard(string email, string name)
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
                cmd.CommandText = "INSERT INTO `chart`(`dashboardName`, `name`,`fromCur`,`toMark`,`refreshRate`) VALUES ('" + dashboard + "','" + name + "','" + fromCur + "','" + toMarket + "','" + refreshRate + "')";
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
                cmd.CommandText = "SELECT `fromCur`,`toMark`,`refreshRate`, `name` FROM `chart` WHERE `dashboard` = '" + dashboard.getTitle() + "'";
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

        public Dashboard loadDashboard(string name, string email)
        {
            Dashboard dashboard = null;
            try
            {
                dashboard = new Dashboard(name);
                cmd.CommandText = "SELECT `name`, `fromCur`, `toMark`, `refreshRate` FROM `chart` WHERE `dashboard` = '" + name + "'";
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

        public void addPortfolio(string email, string name)
        {
            try
            {
                cmd.CommandText = "INSERT INTO `porfolio`(`email`, `name`) VALUES ('" + email + "','" + name + "')";
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
                cmd.CommandText = "INSERT INTO `currency`(`portfolio`, `name`,`owned`) VALUES ('" + portfolio + "','" + name + "','" + owned + "')";
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
                cmd.CommandText = "SELECT `owned`, `name` FROM `currency` WHERE `portfolio` = '" + portfolio.getTitle() + "'";
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
                portfolio = new Portfolio(name);
                cmd.CommandText = "SELECT `name`, `owned` FROM `currency` WHERE `portfolio` = '" + name + "'";
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
    }
}
