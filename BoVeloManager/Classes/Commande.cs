using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {
    public class Commande : Transaction {

        public Commande(int id_, int id_seller, int id_client, string state_, DateTime sale_date_, DateTime prevision_date_, List<KitTemplate> kitTemplatesList, List<User> userList, List<Client> clientList) : base(id_, id_seller, id_client, state_, sale_date_, prevision_date_, userList, clientList) {



        }

        class Commande_item {
            KitTemplate kt;
            int qnt;
            
            public Commande_item(KitTemplate kt_, int qnt_) {
                kt = kt_;
                qnt = qnt_;
            }
        }

    }
}
