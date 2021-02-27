using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BoVeloManager.Classes;

namespace BoVeloManager.tools {
    class Database {

        private static MySqlConnection MSCon;

        public static DataTable getData(string query) {
            checkConnection();

            int nbrTry = 0;

            DataTable dt = new DataTable();

            while (nbrTry < 5) {
                try {
                    MySqlCommand cmd = MSCon.CreateCommand();
                    cmd.CommandText = query;

                    MySqlDataAdapter DataRow = new MySqlDataAdapter(cmd);
                    DataRow.Fill(dt);

                    return dt;
                } catch {
                    nbrTry++;
                }

            }

            throw new Exception("Can't get data from database, code 1");
        }

        public static int setData(string query) {
            checkConnection();

            int nbrTry = 0;

            while (nbrTry < 5) {
                try {
                    MySqlCommand cmd = MSCon.CreateCommand();
                    cmd.CommandText = query;

                    return cmd.ExecuteNonQuery();
                } catch {
                    nbrTry++;
                }
            }

            throw new Exception("Can't set data to database, code 2");

        }

        private static void checkConnection() {

            if ((MSCon == null) || (MSCon.State == ConnectionState.Closed) || (MSCon.State == ConnectionState.Broken)) {
                int nbrTry = 0;

                while (nbrTry < 5) {
                    try {
                        connectToDB();
                        return;
                    } catch {
                        nbrTry++;
                    }
                }
                throw new Exception("Could not connect to database, code 0");
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

        public static string getUsers(int filter) {
            string f = "";
            switch (filter) {
                case 0:
                    break;
                case 1:
                    f = "WHERE `grade` = 2";
                    break;
                case 2:
                    f = "WHERE `grade` = 1";
                    break;
                case 3:
                    f = "WHERE `grade` = 0";
                    break;

            }


            return "SELECT * FROM `bv_user`" + f;
        }
        public static string getUserGrade_by_id(int id)
        {
            return "SELECT `grade` FROM `bv_user` WHERE `id` = " + id.ToString();
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

        #region Item

        public static string getKits() {
            return "SELECT * FROM `bv_type_kit`";
        }

        public static string getKits_maxId(int maxId)
        {
            return "SELECT * FROM `bv_type_kit` WHERE `id` < " + maxId.ToString();
        }

        public static string getItem()
        {
            return "SELECT * FROM `bv_catalog`";
        }
        public static string getItem_by_id(int id)
        {
            return "SELECT `id`,`name`,`PriceMul` FROM `bv_catalog` WHERE `id` = " + id.ToString();
        }
        public static string addItem(string name, int pm)
        {
            return "INSERT INTO `bv_catalog` (`name`,`PriceMul`) VALUES ('" + name + "'," + pm.ToString() + ")";
        }

        public static string updateItem(int id, string newName, int priceMul)
        {
            return "UPDATE `bv_catalog` SET `name` = '" + newName + "' , `PriceMul` = '" + priceMul.ToString() + "' WHERE `id` = " + id.ToString();
        }

        public static string delItem(int id)
        {
            return "DELETE FROM `bv_catalog` WHERE `id` = " + id.ToString();
        }

        #endregion

        #region Kit
        public static string getKit_by_category(int cat)
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = " + cat.ToString();
        }

        public static string getCompatibleKitId_with_categoryId(int id_cat)
        {
            return "SELECT `id_tKit` FROM `bv_cat_tKit` WHERE `id_cat` = " + id_cat.ToString();
        }

        public static string getCompatibleCatId_with_KitId(int id_kit)
        {
            return "SELECT `id_cat` FROM `bv_cat_tKit` WHERE `id_tKit` = " + id_kit.ToString();
        }

        public static string getKit_by_id(int id)
        {
            return "SELECT `id`,`name`,`properties`,`category`,`price` FROM `bv_type_kit` WHERE `id` = " + id.ToString();
        }

        public static string delKit(int id)
        {
            return "DELETE FROM `bv_type_kit` WHERE `id` = " + id.ToString();
        }

        public static string setKitName(int id, string name)
        {
            return "UPDATE `bv_type_kit` SET `name` = '" + name + "' WHERE `id` = " + id.ToString();
        }

        public static string setKitProperties(int id, string newProperties)
        {
            return "UPDATE `bv_type_kit` SET `properties` = '" + newProperties + "' WHERE `id` = " + id.ToString();
        }

        // get all kit info
        public static string gettKit(int id_tBike)
        {
            return "SELECT K.name,K.price,K.category,K.properties FROM `bv_tBike_tKit` AS B INNER JOIN `bv_type_kit` AS K ON B.id_tKit = K.id WHERE B.id_tBike =" + id_tBike.ToString();
        }

        // Add kit Querry
        public static string addKit(string name, string prop, string cat)
        {
            return "INSERT INTO `bv_type_kit`(`name`, `properties`, `category`) VALUES ('" + name + "','" + prop + "','" + cat + "')";
        }

        public static string addCompatibleKit(int id_cat, int id_tKit)
        {
            return "INSERT INTO `bv_cat_tKit` (`id_cat`, `id_tKit`) VALUES('" + id_cat + "', '" + id_tKit + "')";
        }
        ////////////////////////
        public static string delCompatibleKit(int id_cat, int id_tKit)
        {
            ///REQUETE SQL A ECRIRE
            //return "DELETE FROM `bv_cat_tKit` WHERE id_cat = " + id_cat.ToString() + "AND id_tKit = " + id_tKit.ToString();
            return "DELETE FROM `bv_cat_tKit` WHERE `bv_cat_tKit`.`id_cat` = " + id_cat.ToString() + " AND `bv_cat_tKit`.`id_tKit` = " + id_tKit.ToString();
        }
        ///////////////////////

        //VOIR INNER JOIN POUR AFFICHER LES KITS
        //https://sql.sh/cours/jointures/inner-join
        #endregion

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
            return "SELECT id_tBike , qnt , PriceMul, name FROM `bv_sale_bike` as SB INNER JOIN bv_type_bike AS TB ON TB.id = SB.`id_tBike` INNER JOIN bv_catalog AS C ON TB.id_cat = C.id WHERE id_sale = " + id_sale.ToString();
        }
        // Returns id name price from a type of bike
        public static string gettBike(int id_Bike)
        {
            return "SELECT id FROM `bv_type_bike`  WHERE id = " + id_Bike.ToString();
        }

        public static string getClients()
        {
            return "SELECT `id`,`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num` , `date` FROM `bv_client`";
        }

        public static string getClient_by_id(int id)
        {
            return "SELECT `id`,`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num` , `date` FROM `bv_client` WHERE `id` = " + id.ToString();
        }


        public static string addClient(string first_name, string last_name, string entreprise_name, string entreprise_adress, string email, string phone_num)
        {
            return "INSERT INTO `bv_client`(`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num`,`date`) VALUES ('" + first_name + "','" + last_name + "','" + entreprise_name + "','" + entreprise_adress + "','" + email + "','" + phone_num + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
        }

        // Get available Kits
        public static string getAllKits()
        {
            return "SELECT * FROM `bv_type_kit`";
        }
        public static string getNameOfKitById(int Id)
        {
            return "SELECT * FROM `bv_type_kit` WHERE `id` = " + Id.ToString();
        }
        public static string getFrameKit()
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = 0";
        }
        public static string getWheelsKit()
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = 1";
        }
        public static string getBrakesKit()
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = 2";
        }
        public static string getSaddleKit()
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = 3";
        }
        public static string getHandlebarKit()
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = 4";
        }
        public static string getAddonKit()
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = 5";
        }
        public static string getModel()
        {
            return "SELECT * FROM `bv_catalog`";
        }

        #region Sale

        public static string addSale(int id_client, int id_seller, DateTime prevision_date, DateTime date)
        {
            return "INSERT INTO `bv_sale`(`id_client`, `id_seller`,`state`, `prevision_date`, `date`) VALUES ('" + id_client + "','" + id_seller + "','Open','" + prevision_date.ToString("yyyy-MM-dd") + "','" + date.ToString("yyyy-MM-dd") + "')";
        }

        public static string link_sale_to_Sale_bike()
        {
            return "";
        }


        #endregion
    }

    class DatabaseClassInterface{
        
        public static List<user> getUsers(){

                //get the user query and data from the database
            string query = DatabaseQuery.getUsers(0);
            DataTable dt = tools.Database.getData(query);

                //convert all the user into a user object
            List<user> temp = new List<user>();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string name = (string)dt.Rows[i]["user"];
                int grade = Convert.ToInt32(dt.Rows[i]["grade"]);
                string psw = (string)dt.Rows[i]["psw"];

                temp.Add(new user(id, name, grade, psw));
            }

            return temp;
        }
        
        public static int updateUser(user moduser) {
            string q = DatabaseQuery.setUserGrade(moduser.id, moduser.grade);
            Database.setData(q);
            q = DatabaseQuery.setUserPass(moduser.id, moduser.hashPass);
            Database.setData(q);

            return 0;
        }

    }

    

}
