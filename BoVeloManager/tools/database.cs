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


        public static DataTable getData(string query) {

            if((MSCon == null)||(MSCon.State == ConnectionState.Closed)||(MSCon.State == ConnectionState.Broken)) {
                connectToDB();
            }

            DataTable dt = new DataTable();

            MySqlCommand cmd = MSCon.CreateCommand();
            cmd.CommandText = query;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);

            return dt;
        }

        public static int setData(string query) {

            MySqlCommand cmd = MSCon.CreateCommand();
            cmd.CommandText = query;

            return cmd.ExecuteNonQuery();
        }

        private static void connectToDB() {
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
            return "SELECT `id`,`user`, `grade` FROM `bv_user`";
        }
        public static string getUser_by_id(int id) {
            return "SELECT `id`,`user`, `grade` FROM `bv_user` WHERE `id` = "+id.ToString();
        }

        public static string addUser(string name,string pass,int grade) {
            return "INSERT INTO `bv_user`(`user`, `psw`, `grade`) VALUES ('"+name+"','"+pass+"',"+grade.ToString()+")";
        }

        public static string setUserGrade(int id,int grade) {
            return "UPDATE `bv_user` SET `grade`= "+grade.ToString()+" WHERE `id` = " + id.ToString();
        }

        public static string delUser(int id) {
            return "DELETE FROM `bv_user` WHERE `id` = " + id.ToString();
        }

        public static string getClients()
        {
            return "SELECT `id`,`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num` , `date` FROM `bv_client`";
        }

        public static string getClient_by_id(int id)
        {
            return "SELECT `id`,`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num` , `date` FROM `bv_client` WHERE `id` = " + id.ToString();
        }

        public static string addClient(char first_name, char last_name, char entreprise_name, char entreprise_adress, char email, string phone_num, DateTime date)
        {
            return "INSERT INTO `bv_client`(`first_name`, `last_name`, `enterprise_name`, enterprise_adress`, `email`, `phone_num` , `date`) VALUES ('" + first_name + "','" + last_name + "','" + entreprise_name + "','" + entreprise_adress + "','" + email + "','" + phone_num + "'," + date.ToString() + ")";
        }


    }


}
