using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.tools;

namespace BoVeloManager.Classes
{
    public partial class Controler {

        private static Controler instance = new Controler();

        private User loggedUser;

        private List<User> userList;
        private List<Client> clientList;
        private List<Supplier> supplierList;
        private List<Bike> bikeList;
        private List<CatalogBike> CatalogBikeList;
        private List<BikeTemplate> bikeTemplateList;
        private List<Sale> saleList;
        private List<KitTemplate> kitTemplateList;
        private List<Commande> CommandeList;
        public TempSale tempSale;

        private Controler() {
            LOAD_USERS();
            LOAD_CLIENTS();
            LOAD_SUPPLIERS();
            LOAD_KITEMP();
            LOAD_CATALOG();
            LOAD_BIKETEMP();
            LOAD_BIKES();
            LOAD_SALES();
            LOAD_COMMANDES();
            
            tempSale = new TempSale();
        }

        public static Controler Instance {
            get {
                return instance;
            }
        }


        public void LOAD_USERS() {
            userList = DatabaseClassInterface.getUsers();
        }

        public void LOAD_CLIENTS() {
            clientList = DatabaseClassInterface.getClients();
        }
        public void LOAD_SUPPLIERS() {
            supplierList = DatabaseClassInterface.getSupplier();
        }

        public void LOAD_KITEMP() {
            kitTemplateList = DatabaseClassInterface.getKitTemplates();
        }

        public void LOAD_CATALOG() {
            CatalogBikeList = DatabaseClassInterface.getCatalogBikes(kitTemplateList);
        }

        public void LOAD_BIKETEMP() {
            bikeTemplateList = DatabaseClassInterface.getBikeTemplates(CatalogBikeList, kitTemplateList);
        }

        public void LOAD_BIKES() {
            bikeList = DatabaseClassInterface.getBikes(bikeTemplateList);
        }

        public void LOAD_SALES() {
            saleList = DatabaseClassInterface.getSales(bikeList, userList, clientList);
        }

        public void LOAD_COMMANDES() {
            CommandeList = DatabaseClassInterface.getCommandes(kitTemplateList, userList, supplierList);
        }

        public void resync(List<string> tableList) {
                //we need to resync
            if(tableList.Count > 0) {

                foreach(string s in tableList) {
                    switch (s) {
                        case "bv_bike":
                            LOAD_BIKES();
                            break;
                        case "bv_cat_tKit":
                            LOAD_CATALOG();
                            break;
                        case "bv_catalog":
                            LOAD_CATALOG();
                            break;
                        case "bv_human":
                            LOAD_CLIENTS();
                            LOAD_SUPPLIERS();
                            break;
                        case "bv_sale":
                            LOAD_SALES();
                            break;
                        case "bv_tBike_tKit":
                            LOAD_BIKETEMP();
                            break;
                        case "bv_type_bike":
                            LOAD_BIKETEMP();
                            break;
                        case "bv_type_kit":
                            LOAD_KITEMP();
                            break;
                        case "bv_user":
                            LOAD_USERS();
                            break;
                    }
                }
            }

        }

    }
}
