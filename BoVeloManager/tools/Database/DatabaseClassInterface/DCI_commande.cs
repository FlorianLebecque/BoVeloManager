using BoVeloManager.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {

    partial class DatabaseClassInterface {

        public static List<Commande> getCommandes(List<KitTemplate> kitTemplatesList, List<User> userList, List<Client> clientList) {

            //get the user query and data from the database
            string query = DatabaseQuery.getCommande();
            DataTable st = tools.Database.getData(query);

            //convert all the user into a user object
            List<Commande> temp = new List<Commande>();
            for (int i = 0; i < st.Rows.Count; i++) {
                int id = Convert.ToInt32(st.Rows[i]["id"]);
                int id_client = Convert.ToInt32(st.Rows[i]["id_human"]);
                int id_seller = Convert.ToInt32(st.Rows[i]["id_seller"]);
                string state = (string)st.Rows[i]["state"];
                DateTime prevision_date = DateTime.Today;//DateTime.Parse((string)dt.Rows[i]["date"]);
                DateTime date = DateTime.Today;//DateTime.Parse((string)dt.Rows[i]["date"]);


                string q = DatabaseQuery.getCommandeItems(id);
                DataTable cmdit_dt = Database.



                temp.Add(new Commande(id, id_seller, id_client, state, date, prevision_date,, userList, clientList));
            }

            return temp;
        }

        public static int addCommande(Commande cd) {
            string q = tools.DatabaseQuery.addCommande(cd);
            return Database.setData(q);
        }


    }
}
