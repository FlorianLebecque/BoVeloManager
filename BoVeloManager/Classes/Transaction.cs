using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {
    public class Transaction {

        protected readonly int id;
        protected DateTime sale_date;
        protected DateTime prevision_date;
        protected string state;
        protected User seller;
        protected Client client;


        public Transaction(int id_, int id_seller, int id_client, string state_, DateTime sale_date_, DateTime prevision_date_, List<User> userList, List<Client> clientList) {

            id = id_;
            state = state_;
            sale_date = sale_date_;
            prevision_date = prevision_date_;

            foreach (User u in userList) {
                if (u.getId() == id_seller) {
                    seller = u.GetDisplayInfo().CurUser;
                }
            }

            foreach (Client c in clientList) {
                if (c.getId() == id_client) {
                    client = (Client)c.GetDisplayInfo().CurInstance;
                }
            }

        }

        public int getId() {
            return id;
        }

        public string getState() {
            return state;
        }

        public Client getClient() {
            return client;
        }

        public User getSeller() {
            return seller;
        }

        public DateTime getSaleDate() {
            return sale_date;
        }

        public DateTime getPreSaleDate() {
            return DateTime.MinValue;    
        }

    }
}
