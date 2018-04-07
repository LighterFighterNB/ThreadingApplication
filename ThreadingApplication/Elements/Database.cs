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
