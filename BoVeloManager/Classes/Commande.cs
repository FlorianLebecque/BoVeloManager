using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {
    public class Commande : Transaction {

        private List<Commande_item> Commande_ItemsList;

        public Commande(int id_, int id_seller, int id_client, string state_, DateTime sale_date_, DateTime prevision_date_, List<Commande_item> Commande_itemList, List<User> userList, List<Supplier> clientList) : base(id_, id_seller, id_client, state_, sale_date_, prevision_date_, userList, clientList.Cast<Human>().ToList()) {

            Commande_ItemsList = Commande_itemList;

        }
        public List<Commande_item> getCommandItemList() {
            return Commande_ItemsList;
        }

        public displayInfo GetDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurCmd = this;
            temp.id = this.getId();
            temp.state = this.getState();
            temp.client = (Supplier)this.getClient();
            temp.seller = this.getSeller();
            temp.sale_date = this.getSaleDate().ToString("MM/dd/yyyy");
            temp.client_name = this.getClient().getName();
            temp.prevision_date = this.getPreSaleDate().ToString("MM/dd/yyyy");

            return temp;
        }

        public class displayInfo {
            public Commande CurCmd { get; set; }
            public Supplier client { get; set; }
            public User seller { get; set; }
            public int id { get; set; }
            public string client_name { get; set; }
            public string state { get; set; }
            public string sale_date { get; set; }
            public string prevision_date { get; set; }
        }

    }

    public class Commande_item {
        public KitTemplate kt;
        public int qnt;

        public Commande_item(KitTemplate kt_, int qnt_) {
            kt = kt_;
            qnt = qnt_;
        }
    }
}
