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

            while (nbrTry < Properties.Settings.Default.MAX_DB_TRY) {
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

            while (nbrTry < Properties.Settings.Default.MAX_DB_TRY) {
                try {
                    MySqlCommand cmd = MSCon.CreateCommand();
                    cmd.CommandText = query;

                    return cmd.ExecuteNonQuery();
                } catch {
                    nbrTry++; 
                    if(nbrTry % 5 == 0) {
                        tools.UI.MessageBox.Show("Trying to reconnect", "Error");
                        connectToDB();
                    }

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

        public static string updateClient(int id, string entreprise_name, string enterprise_adress, string email, string phone_num) {
            return "UPDATE `bv_client` SET `enterprise_name`= '" + entreprise_name + "', `enterprise_adress`= '" + enterprise_adress + "',  `email`= '" + email + "', `phone_num`= '" + phone_num + "' WHERE `id` = " + id.ToString();
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
        public static string addCatalogBike(CatalogBike cb)
        {
            return "INSERT INTO `bv_catalog` (`id`,`name`,`PriceMul`,`picture`) VALUES (" + cb.getId().ToString() + ",'" + cb.getName() + "'," + cb.getPriceMul().ToString() + ", 'Bike0.jpg')";
        }

        public static string updateCatalogBike(CatalogBike kt)
        {
            return "UPDATE `bv_catalog` SET `name` = '" + kt.getName() + "' , `PriceMul` = '" + kt.getPriceMul().ToString() + "' WHERE `id` = " + kt.getId().ToString();
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

        public static string getKit_by_catalogBikeId(int id_cat)
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
            return "SELECT K.id FROM `bv_tBike_tKit` AS B INNER JOIN `bv_type_kit` AS K ON B.id_tKit = K.id WHERE B.id_tBike =" + id_tBike.ToString();
        }

        // Add kit Querry
        public static string addKit(int id,string name, string prop,int price, int cat)
        {
            return "INSERT INTO `bv_type_kit`(`id`,`name`, `properties`,`price`, `category`) VALUES ('" + id.ToString() + "','" + name + "','" + prop + "','" + price.ToString() + "','" + cat.ToString() + "')";
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
        public static string getSales() {
            return "SELECT `id` ,`id_client`, `id_seller`, `state`, `prevision_date`, `date` FROM `bv_sale`";
        }
        public static string getOldSales()
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


        public static string addClient(Client c)
        {
            return "INSERT INTO `bv_client`(`id`,`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num`,`date`) VALUES (" + c.getId() + ",'" + c.getName() + "',' ','" + c.getEtpName() + "','" + c.getEtpAdress() + "','" + c.getEmail() + "','" + c.getPhoneNumb() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
        }

        // Get available Kits
        #region Kits
        public static string getAllKits()
        {
            return "SELECT * FROM `bv_type_kit`";
        }
        public static string getNameOfKitById(int Id)
        {
            return "SELECT * FROM `bv_type_kit` WHERE `id` = " + Id.ToString();
        }
        
        public static string getColor()
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = 1";
        }

        public static string getSize()
        {
            return "SELECT * FROM `bv_type_kit` WHERE `category` = 0";
        }

        public static string updateKitTemplate(int id,string name,int cat,int price,string prop) {
            return "UPDATE `bv_type_kit` SET `name`= '"+name+"',`category`='"+cat.ToString()+ "',`Price`='"+price.ToString()+"',`properties`='" + prop + "' WHERE `id`=" + id;
        }

        #endregion 

        #region Sale

        public static string addSale(Sale s)
        {
            return "INSERT INTO `bv_sale`(`id`, `id_client`, `id_seller`,`state`, `prevision_date`, `date`) VALUES ('" + s.getId() + "', '" + s.getClient().getId() + "','" + s.getSeller().getId() + "','Open','" + s.getPreSaleDate().ToString("yyyy-MM-dd") + "','" + s.getSaleDate().ToString("yyyy-MM-dd") + "')";
        }

        public static string link_sale_to_Sale_bike(Sale s, Bike b)
        {
            return "";
        }

        public static string addBikeTemplate(BikeTemplate t)
        {
            return "INSERT INTO `bv_type_bike` (`id`, `id_cat`) VALUES (" + t.getId() + ", " + t.getCat().getId() + ")";
        }

        public static string addBike(Bike b)
        {
            //return "INSERT INTO `bv_bike` (`id`, `id_tBike`, `id_sale`, `state`, `planne_cDate`, `poste`) VALUES (" + b.getId() + ", " + b.getBikeTempl().getId() + ", " + b.getSaleId() + ", " + b.getState() + ", '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', '" + b.getPoste() + ")";
            return "INSERT INTO `bv_bike` (`id`, `id_tBike`, `id_sale`, `state`, `planne_cDate`, `create_Date`, `poste`) VALUES (" + b.getId() + ", " + b.getBikeTempl().getId() + ", " + b.getSaleId() + ", " + b.getState() + ", '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', " + b.getPoste() + ")";
        }

        public static string link_kit_to_tbike(BikeTemplate bt, KitTemplate kt)
        {
            return "INSERT INTO `bv_tBike_tKit` (`id_tBike`, `id_tKit`) VALUES (" + bt.getId() + ", " + kt.getId() + ")";
        }


        #endregion

        //bike
        public static string getBike()
        {
            return "SELECT * FROM `bv_bike`";
        }

        public static string updateBike(Bike bk) {
            return "UPDATE `bv_bike` SET `state`= "+ bk.getState().ToString() +" ,`planne_cDate`='"+ bk.getPlannedtDate().ToString("yyyy-MM-dd") +"' ,`poste`= "+bk.getPoste().ToString()+ ",`create_Date`='" + bk.getConstructionDate().ToString("yyyy-MM-dd") + "' WHERE `id` = " + bk.getId();
        }

        public static string getTBike() {
            return "SELECT * FROM `bv_type_bike`";
        }
        //public static string addBike(int id, int status, int id_sale, int Poste, BikeTemplate bt, DateTime planned_date, DateTime constr_date)
        //{
            //if (id_sale < 0)
            //{
            //    string str = "NULL";
            //    return "INSERT INTO `bv_bike`(`id`, `id_tBike`, `id_sale`, `state`, `poste`, `planne_cDate`, `create_Date`) VALUES ('" + id + "','" + bt.getId() + "','" + str + "','" + status + "','" + Poste + "','" + planned_date.ToString("yyyy-MM-dd") + "','" + constr_date.ToString("yyyy-MM-dd") + "')";
            //}
            //else
            //{
                //return "INSERT INTO `bv_bike`(`id`, `id_tBike`, `id_sale`, `state`, `planne_cDate`, `create_Date`, `poste`) VALUES ('" + id + "','" + bt.getId() + "','" + id_sale + "','" + status + "','" + planned_date.ToString("yyyy-MM-dd") + "','" + constr_date.ToString("yyyy-MM-dd") + "','" + Poste.ToString() + "')";
            //}   
                
        //}

    }

    class DatabaseClassInterface{

        #region User

        public static List<User> getUsers(){

                //get the user query and data from the database
            string query = DatabaseQuery.getUsers(0);
            DataTable dt = tools.Database.getData(query);

                //convert all the user into a user object
            List<User> temp = new List<User>();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string name = (string)dt.Rows[i]["user"];
                int grade = Convert.ToInt32(dt.Rows[i]["grade"]);
                string psw = (string)dt.Rows[i]["psw"];

                temp.Add(new User(id, name, grade, psw));
            }

            return temp;
        }
        
        public static int updateUser(User moduser) {
            string q = DatabaseQuery.setUserGrade(moduser.getId(), moduser.getGrade()) + "\n";
            q += DatabaseQuery.setUserPass(moduser.getId(), moduser.getHashPass());
            
            return Database.setData(q);
        }

        public static int addUser(User NewUser) {
            string q = DatabaseQuery.addUser(NewUser.getName(), NewUser.getHashPass(), NewUser.getGrade());
            return Database.setData(q);
        }


        #endregion

        #region client

        public static List<Client> getClients() {

            //get the user query and data from the database
            string query = DatabaseQuery.getClients();
            DataTable dt = tools.Database.getData(query);

            //convert all the user into a user object
            List<Client> temp = new List<Client>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string first_name = (string)dt.Rows[i]["first_name"];
                string last_name = (string)dt.Rows[i]["last_name"];
                string enter_name = (string)dt.Rows[i]["enterprise_name"];
                string enter_add = (string)dt.Rows[i]["enterprise_adress"];
                string email = (string)dt.Rows[i]["email"];
                string phone = (string)dt.Rows[i]["phone_num"];
                DateTime datet = DateTime.Today;//DateTime.Parse((string)dt.Rows[i]["date"]);

                temp.Add(new Client(id, first_name, last_name,enter_name,enter_add,email,phone,datet));
            }

            return temp;
        }
        public static int updateClient(Client modClient) {
            string q = DatabaseQuery.updateClient(modClient.getId(), modClient.getEtpName(), modClient.getEtpAdress(), modClient.getEmail(), modClient.getPhoneNumb());
            return Database.setData(q);
        }
        public static int addClient(Client c) {
            string q = tools.DatabaseQuery.addClient(c);
            return Database.setData(q);
        }

        #endregion

        #region sales

        public static List<Sale> getSales(List<Bike> bikeList, List<User> userList, List<Client> clientList) {

            //get the user query and data from the database
            string query = DatabaseQuery.getSales();
            DataTable st = tools.Database.getData(query);

            //convert all the user into a user object
            List<Sale> temp = new List<Sale>();
            for (int i = 0; i < st.Rows.Count; i++) {
                int id = Convert.ToInt32(st.Rows[i]["id"]);
                int id_client = Convert.ToInt32(st.Rows[i]["id_client"]);
                int id_seller = Convert.ToInt32(st.Rows[i]["id_seller"]);
                string state = (string)st.Rows[i]["state"];
                DateTime prevision_date = DateTime.Today;//DateTime.Parse((string)dt.Rows[i]["date"]);
                DateTime date = DateTime.Today;//DateTime.Parse((string)dt.Rows[i]["date"]);

                temp.Add(new Sale(id, id_seller, id_client, state, date, prevision_date, bikeList, userList, clientList));
               }

            return temp;
        }

        public static int addSale(Sale s)
        {
            string q = tools.DatabaseQuery.addSale(s);
            return Database.setData(q);
        }

        #endregion

        #region bike

        public static int addBike(Bike b)
        {
            string q = tools.DatabaseQuery.addBike(b);
            return Database.setData(q);
        }

        public static List<Bike> getBikes(List<BikeTemplate> btList){
            string query = DatabaseQuery.getBike();
            DataTable dt = tools.Database.getData(query);

            List<Bike> temp = new List<Bike>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                int id_tBike = Convert.ToInt32(dt.Rows[i]["id_tBike"]);

                int id_sale;

                if (dt.Rows[i]["id_sale"] != DBNull.Value) {
                    id_sale  = Convert.ToInt32(dt.Rows[i]["id_sale"]);
                } else {
                    id_sale = -1;
                }


                
                int state = Convert.ToInt32(dt.Rows[i]["state"]);
                int poste = Convert.ToInt32(dt.Rows[i]["poste"]);



                DateTime planned_date = (DateTime)dt.Rows[i]["planne_cDate"];
                DateTime Constr_date;
                if (dt.Rows[i]["create_Date"] != DBNull.Value) {
                    Constr_date = (DateTime)dt.Rows[i]["create_Date"];
                } else {
                    Constr_date = DateTime.MinValue;
                }
                

                foreach (BikeTemplate bt in btList) {
                    if(bt.getId() == id_tBike) {
                        temp.Add(new Bike(id,state, id_sale,poste,bt, planned_date, Constr_date)) ;
                    }
                }
                
            }

            return temp;
        }

        public static int updateBike(Bike bk) {
            string q = DatabaseQuery.updateBike(bk);
            return Database.setData(q);
        }
        public static int setBikeState(Bike bk)
        {
            string q = DatabaseQuery.updateBike(bk);
            return Database.setData(q);
        }

        #endregion

        #region KitTemplate

        public static List<KitTemplate> getKitTemplates() {

            //get the user query and data from the database
            string query = DatabaseQuery.getKits();
            DataTable dt = tools.Database.getData(query);

            //convert all the user into a user object
            List<KitTemplate> temp = new List<KitTemplate>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string name = (string)dt.Rows[i]["name"];
                int cat = Convert.ToInt32(dt.Rows[i]["category"]);
                int p = Convert.ToInt32(dt.Rows[i]["Price"]);

                string prop;
                if (dt.Rows[i]["properties"] != DBNull.Value) {
                    prop = (string)dt.Rows[i]["properties"];
                } else {
                    prop = "";
                }

                

                temp.Add(new KitTemplate(id, name, cat,p, prop));
            }

            return temp;
        }


        

        public static int addKitTemplate(KitTemplate kt) {
            string q = DatabaseQuery.addKit(kt.getId(),kt.getName(),kt.getProperties(),kt.getPrice(),kt.getCategory());
            return Database.setData(q);
        }

        public static int updateKitTemplate(KitTemplate kt) {
            string q = DatabaseQuery.updateKitTemplate(kt.getId(), kt.getName(), kt.getCategory(), kt.getPrice(),kt.getProperties());
            return Database.setData(q);
        }

        #endregion

        #region BikeTemplate

        public static int addBikeTemplate(BikeTemplate t)
        {
            string q = tools.DatabaseQuery.addBikeTemplate(t);
            return Database.setData(q);
        }

        public static int link_kit_to_tbike(BikeTemplate bt, KitTemplate kt)
        {
            string q = tools.DatabaseQuery.link_kit_to_tbike(bt, kt);
            return Database.setData(q);
        }

        public static List<BikeTemplate> getBikeTemplates(List<CatalogBike> cbList,List<KitTemplate> ktList)
        {
            string query = DatabaseQuery.getTBike();
            DataTable dt = tools.Database.getData(query);
            List<BikeTemplate> Btemp = new List<BikeTemplate>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                int id_cat = Convert.ToInt32(dt.Rows[i]["id_cat"]);
                
                foreach(CatalogBike cb in cbList) {
                    if (cb.getId() == id_cat) {

                        BikeTemplate bt = new BikeTemplate(id, cb);

                        string q = DatabaseQuery.gettKit(id);
                        DataTable kdt = Database.getData(q);

                        for(int j = 0; j < kdt.Rows.Count; j++) {
                            int idK = Convert.ToInt32(kdt.Rows[j]["id"]);

                            foreach(KitTemplate kt in ktList) {
                                if(kt.getId() == idK) {
                                    bt.linkKitTemplate(kt);
                                }
                            }

                        }
                        

                        Btemp.Add(bt);
                    }
                }

                
            }

            return Btemp;
            //SELECT* FROM `bv_tBike_tKit` WHERE 1
        }
        //public static int addBikeTemplate(BikeTemplate kt)
        //{
        //string q = DatabaseQuery.addBikeTemplate(kt.getId(), kt.getName(), kt.getPriceMul());
        //return Database.setData(q);
        //}

        //public static int updateBikeTemplate(BikeTemplate kt)
        //{
        //string q = DatabaseQuery.updateBikeTemplate(kt.getId(), kt.getName(),kt.getPriceMul());
        //return Database.setData(q);
        //}

        #endregion

        #region CatalogBike

        public static List<CatalogBike> getCatalogBikes(List<KitTemplate> ktList) {

            string q = DatabaseQuery.getItem();
            DataTable dt = Database.getData(q);

            //convert all the user into a user object
            List<CatalogBike> temp = new List<CatalogBike>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string name = (string)dt.Rows[i]["name"];
                int PriceMul = Convert.ToInt32(dt.Rows[i]["PriceMul"]);
                string pic_filename = (string)dt.Rows[i]["picture"];

                string query = DatabaseQuery.getKit_by_catalogBikeId(id);
                DataTable kdt = Database.getData(query);

                CatalogBike cb = new CatalogBike(id, name, PriceMul, pic_filename);

                for (int j = 0; j < kdt.Rows.Count; j++) {
                    foreach(KitTemplate kt in ktList) {
                        if(kt.getId() == Convert.ToInt32(kdt.Rows[j]["id_tKit"])) {
                            cb.linkKitTemplate(kt);
                        }
                    }
                }


                temp.Add(cb);
            }

            return temp;


        }

        public static int updateCatalogBike(CatalogBike kt) {
            string q = DatabaseQuery.updateCatalogBike(kt);
            return Database.setData(q);
        }

        public static int linkKTCB(CatalogBike cb,KitTemplate kt) {
            string q = DatabaseQuery.addCompatibleKit(cb.getId(), kt.getId());
            return Database.setData(q);
        }

        public static int unlinkKTCB(CatalogBike cb,KitTemplate kt) {
            string q = DatabaseQuery.delCompatibleKit(cb.getId(), kt.getId());
            return Database.setData(q);
        }

        public static int addCatalogBike(CatalogBike cb) {
            string q = DatabaseQuery.addCatalogBike(cb);
            return Database.setData(q);
        }

        #endregion

    }

}
