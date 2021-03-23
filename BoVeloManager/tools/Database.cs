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

        private static Dictionary<string, string> tableCheck = new Dictionary<string, string>();

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

                    int nbr = cmd.ExecuteNonQuery();

                    if (Properties.Settings.Default.SYNC_ENABLE) {
                        Controler.Instance.resync(checkTable());
                    }       

                    return nbr;
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
            
        private static List<string> checkTable() {
            string q = DatabaseQuery.getTable();
            DataTable dt = getData(q);

            List<string> changed_table = new List<string>();

            for(int i = 0; i < dt.Rows.Count; i++) {
                string[] temp = new string[2];
                temp[0] = (string)dt.Rows[i]["Tables_in_"+Properties.Settings.Default.DBUser];
                string q2 = DatabaseQuery.getCheckSum_tableQuery(temp[0]);
                DataTable dt2 = getData(q2);
                temp[1] = Convert.ToInt64(dt2.Rows[0]["Checksum"]).ToString();

                if (tableCheck.ContainsKey(temp[0])){
                    if(tableCheck[temp[0]] != temp[1]) {
                        changed_table.Add(temp[0]);
                    }
                } else {
                    tableCheck.Add(temp[0], temp[1]);
                }

            }

            return changed_table;

        }

    }

    class DatabaseQuery {

        public static string getCheckSum_tableQuery(string table){
            return "CHECKSUM TABLE `"+table+"`";
        }

        public static string getTable() {
            return "SHOW TABLES;";
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

        public static string addUser(string name, string pass, int grade) {
            return "INSERT INTO `bv_user`(`user`, `psw`, `grade`) VALUES ('" + name + "','" + pass + "'," + grade.ToString() + ")";
        }

        public static string setUserPass(int id, string pass) {
            return "UPDATE `bv_user` SET `psw`='" + pass + "'  WHERE `id` =" + id.ToString();
        }

        public static string setUserGrade(int id, int grade) {
            return "UPDATE `bv_user` SET `grade`= " + grade.ToString() + " WHERE `id` = " + id.ToString();
        }



        public static string updateHumans(int id, string entreprise_name, string enterprise_adress, string email, string phone_num) {
            return "UPDATE `bv_human` SET `enterprise_name`= '" + entreprise_name + "', `enterprise_adress`= '" + enterprise_adress + "',  `email`= '" + email + "', `phone_num`= '" + phone_num + "' WHERE `id` = " + id.ToString();
        }

        public static string getHumans(int fct) {
            return "SELECT *  FROM `bv_human` WHERE `fct` = " + fct.ToString();
        }

        public static string addHuman(Human c) {
            if (c is Client ) {
                return "INSERT INTO `bv_human`(`id`,`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num`,`date`,`fct`) VALUES (" + c.getId() + ",'" + c.getName() + "',' ','" + c.getEtpName() + "','" + c.getEtpAdress() + "','" + c.getEmail() + "','" + c.getPhoneNumb() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "',0)";
            } else {
                return "INSERT INTO `bv_human`(`id`,`first_name`, `last_name`, `enterprise_name`, `enterprise_adress`, `email`, `phone_num`,`date`,`fct`) VALUES (" + c.getId() + ",'" + c.getName() + "',' ','" + c.getEtpName() + "','" + c.getEtpAdress() + "','" + c.getEmail() + "','" + c.getPhoneNumb() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "',1)";

            }
        }



        public static string getCatalogBike() {
            return "SELECT * FROM `bv_catalog`";
        }

        public static string addCatalogBike(CatalogBike cb) {
            return "INSERT INTO `bv_catalog` (`id`,`name`,`PriceMul`,`picture`) VALUES (" + cb.getId().ToString() + ",'" + cb.getName() + "'," + cb.getPriceMul().ToString() + ", 'Bike0.jpg')";
        }

        public static string updateCatalogBike(CatalogBike kt) {
            return "UPDATE `bv_catalog` SET `name` = '" + kt.getName() + "' , `PriceMul` = '" + kt.getPriceMul().ToString() + "' WHERE `id` = " + kt.getId().ToString();
        }



        public static string getKits() {
            return "SELECT * FROM `bv_type_kit`";
        }

        public static string getKit_by_catalogBikeId(int id_cat) {
            return "SELECT `id_tKit` FROM `bv_cat_tKit` WHERE `id_cat` = " + id_cat.ToString();
        }

        public static string getKitId_byTBike(int id_tBike) {
            return "SELECT K.id FROM `bv_tBike_tKit` AS B INNER JOIN `bv_type_kit` AS K ON B.id_tKit = K.id WHERE B.id_tBike =" + id_tBike.ToString();
        }

        public static string addKit(int id,string name, string prop,int price, int cat) {
            return "INSERT INTO `bv_type_kit`(`id`,`name`, `properties`,`price`, `category`) VALUES ('" + id.ToString() + "','" + name + "','" + prop + "','" + price.ToString() + "','" + cat.ToString() + "')";
        }

        public static string addCompatibleKit(int id_cat, int id_tKit) {
            return "INSERT INTO `bv_cat_tKit` (`id_cat`, `id_tKit`) VALUES('" + id_cat + "', '" + id_tKit + "')";
        }

        public static string delCompatibleKit(int id_cat, int id_tKit) {
            return "DELETE FROM `bv_cat_tKit` WHERE `bv_cat_tKit`.`id_cat` = " + id_cat.ToString() + " AND `bv_cat_tKit`.`id_tKit` = " + id_tKit.ToString();
        }



        public static string getSales() {
            return "SELECT `id` ,`id_client`, `id_seller`, `state`, `prevision_date`, `date` FROM `bv_sale`";
        } 

        public static string updateKitTemplate(int id,string name,int cat,int price,string prop) {
            return "UPDATE `bv_type_kit` SET `name`= '"+name+"',`category`='"+cat.ToString()+ "',`Price`='"+price.ToString()+"',`properties`='" + prop + "' WHERE `id`=" + id;
        }

        public static string addSale(Sale s)
        {
            return "INSERT INTO `bv_sale`(`id`, `id_client`, `id_seller`,`state`, `prevision_date`, `date`) VALUES ('" + s.getId() + "', '" + s.getClient().getId() + "','" + s.getSeller().getId() + "','Open','" + s.getPreSaleDate().ToString("yyyy-MM-dd") + "','" + s.getSaleDate().ToString("yyyy-MM-dd") + "')";
        }


        public static string addBikeTemplate(BikeTemplate t)
        {
            return "INSERT INTO `bv_type_bike` (`id`, `id_cat`) VALUES (" + t.getId() + ", " + t.getCat().getId() + ")";
        }

        public static string addBike(Bike b)
        {
            //return "INSERT INTO `bv_bike` (`id`, `id_tBike`, `id_sale`, `state`, `planne_cDate`, `poste`) VALUES (" + b.getId() + ", " + b.getBikeTempl().getId() + ", " + b.getSaleId() + ", " + b.getState() + ", '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', '" + b.getPoste() + ")";
            if(b.getSaleId() >= 0){
                return "INSERT INTO `bv_bike` (`id`, `id_tBike`, `id_sale`, `state`, `planne_cDate`, `create_Date`, `poste`) VALUES (" + b.getId() + ", " + b.getBikeTempl().getId() + ", " + b.getSaleId() + ", " + b.getState() + ", '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', " + b.getPoste() + ")";
            } else {
                return "INSERT INTO `bv_bike` (`id`, `id_tBike`, `state`, `planne_cDate`, `create_Date`, `poste`) VALUES (" + b.getId() + ", " + b.getBikeTempl().getId() + ", " + b.getState() + ", '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', '" + b.getPlannedtDate().ToString("yyyy-MM-dd") + "', " + b.getPoste() + ")";  
            }
        }
        
        public static string link_kit_to_tbike(BikeTemplate bt, KitTemplate kt) {
            return "INSERT INTO `bv_tBike_tKit` (`id_tBike`, `id_tKit`) VALUES (" + bt.getId() + ", " + kt.getId() + ")";
        }

        public static string getBike() { 
            return "SELECT * FROM `bv_bike`";
        }

        public static string updateBike(Bike bk) {
            return "UPDATE `bv_bike` SET `state`= "+ bk.getState().ToString() +" ,`planne_cDate`='"+ bk.getPlannedtDate().ToString("yyyy-MM-dd") +"' ,`poste`= "+bk.getPoste().ToString()+ ",`create_Date`='" + bk.getConclassionDate().ToString("yyyy-MM-dd") + "' WHERE `id` = " + bk.getId();
        }

        public static string getTBike() {
            return "SELECT * FROM `bv_type_bike`";
        }
    
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
            string query = DatabaseQuery.getHumans(0);
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
        public static List<Supplier> getSupplier() {

            //get the user query and data from the database
            string query = DatabaseQuery.getHumans(1);
            DataTable dt = tools.Database.getData(query);

            //convert all the user into a user object
            List<Supplier> temp = new List<Supplier>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string first_name = (string)dt.Rows[i]["first_name"];
                string last_name = (string)dt.Rows[i]["last_name"];
                string enter_name = (string)dt.Rows[i]["enterprise_name"];
                string enter_add = (string)dt.Rows[i]["enterprise_adress"];
                string email = (string)dt.Rows[i]["email"];
                string phone = (string)dt.Rows[i]["phone_num"];
                DateTime datet = DateTime.Today;//DateTime.Parse((string)dt.Rows[i]["date"]);

                temp.Add(new Supplier(id, first_name, last_name, enter_name, enter_add, email, phone, datet));
            }

            return temp;
        }
        public static int updateHuman(Human modClient) {
            string q = DatabaseQuery.updateHumans(modClient.getId(), modClient.getEtpName(), modClient.getEtpAdress(), modClient.getEmail(), modClient.getPhoneNumb());
            return Database.setData(q);
        }

        public static int addHuman(Human c) {
            string q = tools.DatabaseQuery.addHuman(c);
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

        public static int setBikeState(Bike bk) {
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

                        string q = DatabaseQuery.getKitId_byTBike(id);
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

            string q = DatabaseQuery.getCatalogBike();
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
