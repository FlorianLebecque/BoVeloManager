using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;



namespace BoVeloManager.tools {
    class database {

        private static MySqlConnection MSCon;

        private static object formatData(MySqlDataReader data){

            while (data.Read()) {
                return data[0].ToString();
            }


            return "";
        }

        public static object getData(string query) {

            if((MSCon == null)||(MSCon.State == System.Data.ConnectionState.Closed)){
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
}
