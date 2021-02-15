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
            return "SELECT `id`,`user`, `grade` FROM `bv_user` WHERE `id` = " + id.ToString();
        }

        public static string addUser(string name, string pass, int grade) {
            return "INSERT INTO `bv_user`(`user`, `psw`, `grade`) VALUES ('" + name + "','" + pass + "'," + grade.ToString() + ")";
        }

        public static string setUserGrade(int id, int grade) {
            return "UPDATE `bv_user` SET `grade`= " + grade.ToString() + " WHERE `id` = " + id.ToString();
        }

        public static string setUserPass(int id, string pass) {
            return "UPDATE `bv_user` SET `psw`='" + pass + "'  WHERE `id` =" + id.ToString();
        }

        public static string delUser(int id) {
            return "DELETE FROM `bv_user` WHERE `id` = " + id.ToString();
        }

        public static string getKits(){
            return "SELECT * FROM `bv_type_kit`";
        }



        public static string getItem()
        {
            return "SELECT * FROM `bv_catalog`";
        }
        public static string getItem_by_id(int id)
        {
            return "SELECT `id`,`name` FROM `bv_catalog` WHERE `id` = " + id.ToString();
        }
        public static string addItem(string name)
        {
            return "INSERT INTO `bv_catalog` (`name`) VALUES ('" + name + "')";
        }

        public static string setItemName(int id, string newName)
        {
            return "UPDATE `bv_catalog` SET `name` = '" + newName + "' WHERE `id` = " + id.ToString();
        }

        public static string delItem(int id)
        {
            return "DELETE FROM `bv_catalog` WHERE `id` = " + id.ToString();
        }

    }



 


}
