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

            bikeList = new List<Bike>();

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
            List<DateTime> PreList = new List<DateTime>();
            foreach (Bike bike in bikeList) {
                PreList.Add(bike.getPlannedtDate());
            }
            PreList.Sort((a, b) => a.CompareTo(b));
            prevision_date = PreList.Last();
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
            temp.sale_date = this.getSaleDate().ToString("MM/dd/yyyy");
            temp.client_name = this.getClient().getName();
            temp.prevision_date = this.getPreSaleDate().ToString("MM/dd/yyyy");

            return temp;
        }

        public struct displayInfo {
            public Sale CurSale { get; set; }
            public Client client { get; set; }
            public User seller { get; set; }
            public int id { get; set; }
            public string client_name { get; set; }
            public string state { get; set; }
            public string sale_date { get; set; }
            public string prevision_date { get; set; }
        }
        
        public descrInfo GetSaleDescrInfo() {
            descrInfo temp = new descrInfo();

            temp.CurSale = this;
            temp.TbikeInfoList = this.getQntTBike();
            temp.total_price = this.getTotalPrice();
            return temp;
        }

        public struct descrInfo {
            public Sale CurSale { get; set; }
            public List<TbikeInfo> TbikeInfoList;
            public float total_price { get; set; }
        }

        public List<TbikeInfo> getQntTBike() {
            List<TbikeInfo> TempTbikeInfoList = new List<TbikeInfo>();

            foreach (Bike bike in getBikeList()) {
                TbikeInfo contain = new TbikeInfo();
                contain.init = false;
                foreach (TbikeInfo tBI in TempTbikeInfoList) {
                    if (tBI.CurTempl == bike.getBikeTempl()) {
                        contain = tBI;
                    } 
                }
                if (contain.init) {
                    contain.qnt++;
                }
                else {
                    contain.init = true;
                    contain.qnt = 1;
                    contain.CurTempl = bike.getBikeTempl();
                    TempTbikeInfoList.Add(contain);
                }
                
            }
            return TempTbikeInfoList;
        }

        public float getTotalPrice() {
            float total_price = 0;

            foreach (Bike bike in bikeList) {
                total_price += bike.getPrice();
            }
            return total_price;
        }

        public class TbikeInfo {
            public BikeTemplate CurTempl { get; set; }
            public int qnt { get; set; }
            public bool init { get; set; }
        }

    }
}
