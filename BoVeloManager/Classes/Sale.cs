using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.Classes;
using BoVeloManager.tools;

namespace BoVeloManager.Classes {
    public class Sale : Transaction {
        
        private List<Bike> bikeList {
            get {
                return Controler.Instance.getBikesList().Where(x => x.getSaleId() == id).ToList();
            }
        }

        public Sale(int id_, int id_seller, int id_client, string state_, DateTime sale_date_, DateTime prevision_date_, List<User> userList, List<Client> clientList) : base(id_,id_seller,id_client,state_,sale_date_,prevision_date_,userList,clientList.Cast<Human>().ToList()) {

        }

        public void updateStatus() {

            
            bool result = true;
            foreach(Bike b in bikeList) {
                if(b.getState() != 2) {
                    result = false;
                    break;
                }
            }

            if (result) {
                state = "Completed";
                DatabaseClassInterface.updateSale(this);
            }

        }

        public void addbike(Bike b) {
            bikeList.Add(b);
        }

        public void setPrevSaleDate(DateTime t) {
            prevision_date = t;
            DatabaseClassInterface.updateSale(this);
        }

        public new DateTime getPreSaleDate() {

            if (bikeList.Count > 0) {
                List<DateTime> PreList = new List<DateTime>();
                foreach (Bike bike in bikeList) {
                    PreList.Add(bike.getPlannedtDate());
                }
                PreList.Sort((a, b) => a.CompareTo(b));
                prevision_date = PreList.Last();

                return prevision_date;
            } else {
                return DateTime.MinValue;
            }
            
        }

        public List<Bike> getBikeList() {
            return bikeList;
        }

        public displayInfo GetSaleDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurSale= this;
            temp.id = this.getId();
            temp.state = this.getState();
            temp.client = (Client)this.getClient();
            temp.seller = this.getSeller();
            temp.sale_date = this.getSaleDate().ToString("MM/dd/yyyy");
            if (this.getClient() != null)
            {
                temp.client_name = this.getClient().getName();
            }
            else { temp.client_name = "?"; }
            
            temp.prevision_date = this.getPreSaleDate().ToString("MM/dd/yyyy");

            return temp;
        }

        public class displayInfo {
            public Sale CurSale { get; set; }
            public Client client { get; set; }
            public User seller { get; set; }
            public int id { get; set; }
            public string client_name { get; set; }
            public string state { get; set; }
            public string sale_date { get; set; }
            public string prevision_date { get; set; }
            public string bikesStatus {
                get {
                    int nbrBikes = CurSale.getBikeList().Count();
                    int count_w = 0;
                    int count_p = 0;
                    int count_d = 0;
                    foreach(Bike b in CurSale.getBikeList()) {
                        switch (b.getState()) {
                            case 0:
                                count_w++;
                                break;
                            case 1:
                                count_p++;
                                break;
                            case 2:
                                count_d++;
                                break;
                        }
                    }
                    
                    if(nbrBikes == count_d) {
                        CurSale.updateStatus();
                    }

                    return count_w.ToString() + " - " + count_p.ToString() + " - " + count_d.ToString() + " /" + nbrBikes.ToString();
                }
            }
        }
        
        public descrInfo GetSaleDescrInfo() {
            descrInfo temp = new descrInfo();

            temp.CurSale = this;
            temp.TbikeInfoList = this.getQntTBike();
            temp.total_price = this.getTotalPrice();
            return temp;
        }

        public class descrInfo {
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
