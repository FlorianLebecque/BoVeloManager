using BoVeloManager.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {
    partial class DatabaseClassInterface {

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

                temp.Add(new Client(id, first_name, last_name, enter_name, enter_add, email, phone, datet));
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

        public static int addHuman(Human c, int fct) {
            string q = tools.DatabaseQuery.addHuman(c, fct);
            return Database.setData(q);
        }

    }
}
