using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.Classes;

namespace BoVeloManager.Classes {
    public class Sale {

        private readonly int id;
        private DateTime sale_date;
        private DateTime prevision_date;
        private string state;
        private User seller;
        private Client client;
        private List<Bike> bikeList;
        

        public Sale(int id_, int id_seller, int id_client, string state_, DateTime sale_date_, DateTime prevision_date_, List<Bike> bikeList_, List<User> userList, List<Client> clientList) {
            id = id_;
            state = state_;
            sale_date = sale_date_;
            prevision_date = prevision_date_;


            foreach (Bike b in bikeList_) {
                if (b.getSaleId() == id_) {
                    bikeList.Add(b);
                }
            }

            foreach (User u in userList) {
                if (u.getId() == id_seller) {
                    seller = u.GetDisplayInfo().CurUser;
                }
            }

            foreach (Client c in clientList) {
                if (c.getId() == id_client) {
                    client = c.GetDisplayInfo().CurClient;
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
            return prevision_date;
        }
        public List<Bike> getBikeList() {
            return bikeList;
        }
        public displayInfo GetSaleDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurSale= this;
            temp.id = this.getId();
            temp.state = this.getState();
            temp.client = this.getClient();
            temp.seller = this.getSeller();
            temp.sale_date = this.getSaleDate().ToString("yyyy-MM-dd");

            return temp;
        }

        public struct displayInfo {
            public Sale CurSale { get; set; }
            public Client client { get; set; }
            public User seller { get; set; }
            public int id { get; set; }
            public string state { get; set; }
            public string sale_date { get; set; }
            public string prevision_date { get; set; }
        }
    }
}
