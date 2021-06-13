using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    // Rassemble tous les composants nécaissaires a l'encodage de la vente
    public class TempSale
    {
        private Client client;

        int saleID;

        private List<Bike> bikeList;
        private BikeTemplate tempBikeTemplate;
        private List<BikeTemplate> newBikeTemplates;        

        public Dictionary<string, BikeBasket> Basket = new Dictionary<string, BikeBasket>();
        private Dictionary<KitTemplate, int> Missing_Kit;

        public TempSale() {
            bikeList = new List<Bike>();
            newBikeTemplates = new List<BikeTemplate>();
        }
        public Dictionary<KitTemplate, int> getMissingKits()
        {
            return Missing_Kit;
        }

        public void setBikeBasket(BikeBasket bc) {

            string key = bc.name + bc.size + bc.color;

            if (Basket.ContainsKey(key)) {
                Basket[key] = new BikeBasket(bc);
            } else {
                Basket.Add(key, new BikeBasket(bc));
            }
        }

        public void removeBikeBasket(BikeBasket b) {
            string key = b.name + b.size + b.color;
            if (Basket.ContainsValue(b)) {
                Basket.Remove(key);
            }
        }

        public void setClient(Client c) {
            client = c;
        }

        private Dictionary<KitTemplate, int> getAllKit()
        {
            Dictionary<KitTemplate, int> AllKitTemplate = new Dictionary<KitTemplate, int>();       // tous les nombres de kit dans la commande

            foreach (BikeBasket bb in Basket.Values)
            {
                int nbr_bike = bb.qnt;
                foreach (KitTemplate tK in bb.CreateBikeTemplate().getListKit())
                {
                    if (AllKitTemplate.ContainsKey(tK))
                    {
                        AllKitTemplate[tK] += nbr_bike * tK.getBikeQtt();
                    }
                    else
                    {
                        AllKitTemplate.Add(tK, nbr_bike * tK.getBikeQtt());
                    }
                }
            }
            return AllKitTemplate;
        }
        // Verification si tous les kits sont disponnibles
        private bool isKitAvailable()
        {
            Missing_Kit = new Dictionary<KitTemplate, int>();

            foreach (KeyValuePair<KitTemplate, int> kvp in getAllKit())
            {
                if (kvp.Value >= kvp.Key.getStockQtt())
                {
                    int nbr_mk = kvp.Value - kvp.Key.getStockQtt();
                    Missing_Kit.Add(kvp.Key, nbr_mk);
                }
            }
            if (Missing_Kit.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool RemoveStockKit()
        {
            if (isKitAvailable())
            {
                foreach (KeyValuePair<KitTemplate, int> kvp in getAllKit())
                {
                    int new_stock = kvp.Key.getStockQtt() - kvp.Value * kvp.Key.getBikeQtt();
                    kvp.Key.setStockQtt(new_stock);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void saveSale()
        {
            saleID = Controler.Instance.getLastSaleId() + 1;
           
            int sellerID = Controler.Instance.getCurrentUser().getId();
            int clientID = client.getId();

            DateTime sale_date = DateTime.Now;
            DateTime prevision_date = getNextPrevisionDate();   //date de début de constrution

            List<User> userList = Controler.Instance.getUserList();
            List<Client> clientList = Controler.Instance.getClientList();


            Sale sale = new Sale(saleID, sellerID, clientID, "Open", sale_date, prevision_date, bikeList, userList, clientList);
            Controler.Instance.createSale(sale);


            foreach(BikeBasket b in Basket.Values) {
                BikeTemplate bt = b.CreateBikeTemplate();
                int id_bt = Controler.Instance.getLastBikeTemplateId() + 1;
                bt.setId(id_bt);

                Controler.Instance.createBikeTemplate(bt);

                for (int i = 0; i < b.qnt; i++) {
                    int bikeID = Controler.Instance.getLastBikeId() + 1;

                    DateTime constr_date = getConstrDate();
                    DateTime planned_date = getNextPrevisionDate();

                    int poste = Controler.Instance.getAvailablePoste();

                    Bike tempB = new Bike(bikeID, 0, saleID, poste, bt, planned_date, constr_date);

                    sale.addbike(tempB);
                    Controler.Instance.createBike(tempB);
                }

            }

            Bike bk = sale.getBikeList().Last();
            sale.setPrevSaleDate(bk.getPlannedtDate());

            drainTempSale();
        }

            //get the first available date
        private DateTime getNextPrevisionDate() {
            return Controler.Instance.getFirstAvailableDay();
        }

        private DateTime getConstrDate() {
            return DateTime.Now;
        }

        public void drainTempSale() {
            Controler.Instance.tempSale = new TempSale();
        }

    }

    public class BikeBasket {
        public string pic { get; set; }
        public string name { get; set; }
        public List<string> sizeList { get; set; }
        public List<string> colorList { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public int qnt { get; set; } = 1;
        public CatalogBike curbike { get; set; }
        public int price {
            get { return this.CreateBikeTemplate().getPrice() * qnt; }
        }
        public string pricestr {
            get { return ((float)price / 100).ToString("c2"); }
        }
        public string displayName {
            get { return name + " " + size + " " + color; }
        }

        public BikeBasket(string name_, List<string> sizes, List<string> colors, CatalogBike cb) {
            name = name_;
            sizeList = sizes;
            colorList = colors;
            curbike = cb;
        }

        public BikeBasket(BikeBasket bk) {
            pic  = bk.pic;
            name = bk.name;
            sizeList = bk.sizeList;
            colorList = bk.colorList;
            size = bk.size;
            color = bk.color;
            qnt = bk.qnt;
            curbike = bk.curbike;
        }

        public BikeTemplate CreateBikeTemplate() {
            BikeTemplate bt = new BikeTemplate(-1, curbike);

            //pour chaque category de kit
            foreach (KitCategory i in Enum.GetValues(typeof(KitCategory))) {
                List<KitTemplate> kt_list = curbike.getKitTemplateList().Where(x => x.getCategory() == i).ToList();  //list des kits correspondant à la cat

                //si il y a que un kit -> on l'ajoute
                if (kt_list.Count == 1) {
                    bt.linkKitTemplate(kt_list[0]);
                } else if (kt_list.Count > 1) {   //si plusieur -> ajoute le kit qui correspond au couleur et en taille              
                    foreach (KitTemplate kt in kt_list) {

                        string[] props = kt.getProperties().Split('|');
                        if (props.Count() == 2) {
                            if ((props[0] == size) && (props[1] == color)) {
                                bt.linkKitTemplate(kt);
                            }
                        } else {
                            if ((props[0] == color) || (props[0] == size)) {
                                bt.linkKitTemplate(kt);
                            }
                        }
                    }


                }

            }

            return bt;
        }

    }

}
