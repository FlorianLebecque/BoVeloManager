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

        // Management Querry
        public static string getUserPass(string user) {
            return "SELECT `psw` FROM `bv_user` WHERE `user` = '" + user + "'";
        }

        public static string getUserData_byName(string user) {
            return "SELECT `id`,`grade` FROM `bv_user` WHERE `user` = '" + user + "'";
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

        // Sales Querry

        //returns all sales from the shop
        public static string getSales()
        {
            return "SELECT S.id, CONCAT(`first_name` , ' ', `last_name`) AS Client, S.date FROM `bv_sale` AS S INNER JOIN `bv_client` AS C ON S.id_client = C.id INNER JOIN `bv_user` AS U ON S.id_seller = U.id";
        }
        // Returns the sale_id the Client fullname the sale date
        public static string getSale_by_id(int id)
        {
            return "SELECT S.id, U.user, CONCAT(`first_name` , ' ', `last_name`)  AS Client, C.enterprise_name, S.date FROM `bv_sale` AS S INNER JOIN `bv_user` AS U ON S.id_seller = U.id INNER JOIN `bv_client` AS C ON S.id_client = C.id  WHERE S.id = " + id.ToString(); //INNER JOIN `bv_seller` AS SE ON S.id_seller = SE.id
        }
        //Returns all types of bikes from one sale
        public static string gettBikes_by_sale(int id_sale)
        {
            return "SELECT id_tBike , qnt FROM `bv_sale_bike`  WHERE id_sale = " + id_sale.ToString();
        }
        // Returns id name price from a type of bike
        public static string gettBike(int id_Bike)
        {
            return "SELECT id , name, price FROM `bv_type_bike`  WHERE id = " + id_Bike.ToString();
        }
        // get all kit info
        public static string gettKit(int id_tBike)
        {
            return "SELECT K.name,K.category,K.properties FROM `bv_tBike_tKit` AS B INNER JOIN `bv_type_kit` AS K ON B.id_tKit = K.id WHERE B.id_tBike ="+ id_tBike.ToString();
        }

        public static string getClient_by_id(int id)
        {
            return "SELECT `id`,`first_name`,`last_name`,`enterprise_name`, `date` FROM `bv_client` WHERE `id` = " + id.ToString();
        }

        // Add kit Querry

        public static string addKit(string name, string prop, string cat)
        {
            return "INSERT INTO `bv_type_kit`(`name`, `properties`, `category`) VALUES ('" + name + "','" + prop + "','" + cat + "')";
        }

    }


}
