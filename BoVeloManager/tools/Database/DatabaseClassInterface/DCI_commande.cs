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

                List<Commande_item> ci_list = new List<Commande_item>();

                //get commande items
                string q = DatabaseQuery.getCommandeItems(id);
                DataTable cmdit_dt = Database.getData(q);
                for(int j = 0; i < cmdit_dt.Rows.Count; j++) {

                    int id_kit = Convert.ToInt32(cmdit_dt.Rows[i]["id_type_kit"]);
                    int qnt = Convert.ToInt32(cmdit_dt.Rows[i]["qnt"]);

                    KitTemplate kt = kitTemplatesList.Where(x => x.getId() == id_kit).ToList()[0];

                    Commande_item ci = new Commande_item(kt, qnt);

                    ci_list.Add(ci);
                }


                temp.Add(new Commande(id, id_seller, id_client, state, date, prevision_date, ci_list, userList, clientList));
            }

            return temp;
        }

        public static int addCommande(Commande cd) {
            string q = tools.DatabaseQuery.addCommande(cd);
            return Database.setData(q);
        }


    }
}
