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
            checkConnection();

            DataTable dt = new DataTable();

            MySqlCommand cmd = MSCon.CreateCommand();
            cmd.CommandText = query;

            MySqlDataAdapter DataRow = new MySqlDataAdapter(cmd);
            DataRow.Fill(dt);

            return dt;
        }

        public static int setData(string query) {
            checkConnection();

            MySqlCommand cmd = MSCon.CreateCommand();
            cmd.CommandText = query;

            return cmd.ExecuteNonQuery();
        }

        private static void checkConnection() {
            if ((MSCon == null) || (MSCon.State == ConnectionState.Closed) || (MSCon.State == ConnectionState.Broken)) {
                connectToDB();
            }
        }

        private static void connectToDB() {
            MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder {
                { "Database", Properties.Settings.Default.DBBase },
                { "Data Source", Properties.Settings.Default.DBHost },
                { "User Id", Properties.Settings.Default.DBUser },
                { "Password", Properties.Settings.Default.DBPass }
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
 
        // Sales Querry

        //returns all sales from the shop
        public static string getSales()
        {
            return "SELECT S.id,S.state, CONCAT(`first_name` , ' ', `last_name`) AS Client, S.date FROM `bv_sale` AS S INNER JOIN `bv_client` AS C ON S.id_client = C.id INNER JOIN `bv_user` AS U ON S.id_seller = U.id";
        }
        // Returns the sale_id the Client fullname the sale date
        public static string getSale_by_id(int id)
        {
            return "SELECT S.id ,CONCAT(`first_name` , ' ', `last_name`)  AS Client, U.user , C.enterprise_name, S.date FROM `bv_sale` AS S INNER JOIN `bv_client` AS C ON S.id_client = C.id INNER JOIN `bv_user` AS U ON U.id = S.id_seller  WHERE S.id = " + id.ToString(); //INNER JOIN `bv_seller` AS SE ON S.id_seller = SE.id
        }
        //Returns all types of bikes from one sale
        public static string gettBikes_by_sale(int id_sale)
        {
            return "SELECT id_tBike , qnt FROM `bv_sale_bike`  WHERE id_sale = " + id_sale.ToString();
        }
        // Returns id name price from a type of bike
        public static string gettBike(int id_Bike)
        {
            return "SELECT id , name FROM `bv_type_bike`  WHERE id = " + id_Bike.ToString();
        }
        // get all kit info
        public static string gettKit(int id_tBike)
        {
            return "SELECT K.name,K.category,K.properties,K.price FROM `bv_tBike_tKit` AS B INNER JOIN `bv_type_kit` AS K ON B.id_tKit = K.id WHERE B.id_tBike ="+ id_tBike.ToString();
        }

        // Add kit Querry

        public static string addKit(string name, string prop, string cat)
        {
            return "INSERT INTO `bv_type_kit`(`name`, `properties`, `category`) VALUES ('" + name + "','" + prop + "','" + cat + "')";
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
