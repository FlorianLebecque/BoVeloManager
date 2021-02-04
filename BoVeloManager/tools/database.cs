using System;
using System.Collections.Generic;
using System.Data;
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

        public static MySqlDataReader getRawData(string query) {
            if ((MSCon == null) || (MSCon.State == System.Data.ConnectionState.Closed)) {
                connectToDB();
            }

            MySqlCommand cmd = MSCon.CreateCommand();

            cmd.CommandText = query;

            return cmd.ExecuteReader();
        }

        public static List<object[]> getData(string query) {
            MySqlDataReader MSres = getRawData(query);

            return formatData(MSres);
        }

        public static DataTable getDataTable(string query) {
            DataTable dt = new DataTable();

            MySqlCommand cmd = MSCon.CreateCommand();
            cmd.CommandText = query;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);

            return dt;
        }

        public static void connectToDB() {
            MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder {
                { "Database", "sql2390507" },
                { "Data Source", "sql2.freemysqlhosting.net" },
                { "User Id", "sql2390507" },
                { "Password", "zG2%rD3!" }
            };

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

        public static string getUsers() {
            return "SELECT * FROM `bv_user`";
        }
    }


}
