using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace BoVeloManager.tools {
    class Database {

        private static MySqlConnection MSCon;

        private static List<object[]> formatData(MySqlDataReader data) {

            List<object[]> resultat = new List<object[]>();

            while (data.Read()) {

                object[] row = new object[data.FieldCount];

                for (int i = 0; i < data.FieldCount; i++) {
                    row[i] = data[i];
                }

                resultat.Add(row);
            }

            data.Close();

            return resultat;
        }

        public static List<object[]> getData(string query) {

            if ((MSCon == null) || (MSCon.State == System.Data.ConnectionState.Closed)) {
                connectToDB();
            }

            MySqlCommand cmd = MSCon.CreateCommand();

            cmd.CommandText = query;

            MySqlDataReader MSres = cmd.ExecuteReader();


            return formatData(MSres);
        }

        public static void connectToDB() {
            MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();

            connBuilder.Add("Database", "sql2390507");
            connBuilder.Add("Data Source", "sql2.freemysqlhosting.net");
            connBuilder.Add("User Id", "sql2390507");
            connBuilder.Add("Password", "zG2%rD3!");

            MSCon = new MySqlConnection(connBuilder.ConnectionString);

            MSCon.Open();

        }

    }

    class DatabaseQuery {
        public static string getUserPass(string user) {
            return "SELECT `psw` FROM `bv_user` WHERE `user` = '" + user + "'";
        }

        public static string getUserGrade(string user) {
            return "SELECT `grade` FROM `bv_user` WHERE `user` = '" + user + "'";
        }
    }


}
