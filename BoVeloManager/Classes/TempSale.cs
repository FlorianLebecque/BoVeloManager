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
        private Dictionary<BikeTemplate, int> basket = new Dictionary<BikeTemplate, int>();

        private Client client;
        private User seller;

        DateTime sale_date;
        DateTime prevision_date;

        private List<Bike> bikeList;
        private BikeTemplate tempBikeTemplate;

        public TempSale() 
        {
            seller = Controler.Instance.getCurrentUser();

        }
        public void setClient(Client c)
        {
            client = c;
        }

        public Dictionary<BikeTemplate, int> getBasket()
        {
            return basket;
        }

        public void addItems(CatalogBike catBike, KitTemplate size, KitTemplate color, int qnt) 
        {
            BikeTemplate tBike;
            if (existBikeTemplate(catBike, size, color, Controler.Instance.GetBikeTemplateList()))
            {
                tBike = tempBikeTemplate;
            }
            else
            {
                int tBike_id = Controler.Instance.getLastBikeTemplateId() + 1;
                tBike = new BikeTemplate(tBike_id, catBike);
                tBike.linkKitTemplate(size);
                tBike.linkKitTemplate(color);
                Controler.Instance.GetBikeTemplateList().Add(tBike);
            }

            addBikeTemplateToBasket(qnt, tBike);

            #region affichage console
            foreach (KeyValuePair<BikeTemplate, int> kvp in basket)
            {
                Console.WriteLine("------------");
                Console.WriteLine("Name : " + kvp.Key.getName());
                foreach (KitTemplate kit in kvp.Key.getListKit())
                {
                    Console.WriteLine("kit : " + kit.getName());
                }
                Console.WriteLine("Quantity : " + kvp.Value);                
            }
            #endregion
        }

        // Verification existence BikeTemplate dans la db
        private bool existBikeTemplate(CatalogBike catBike, KitTemplate size, KitTemplate color, List<BikeTemplate> tBike_list)
        {
            List<KitTemplate> listKit = new List<KitTemplate>();
            listKit.Add(size);
            listKit.Add(color);

            foreach (BikeTemplate bikeTemplate in tBike_list)
            {
                //x.All(y.Contains)
                //if (bikeTemplate.getCat().getName() == catBike.getName() && bikeTemplate.getListKit() == listKit)

                if (bikeTemplate.getCat().getName() == catBike.getName() && bikeTemplate.getListKit().All(listKit.Contains) && listKit.All(bikeTemplate.getListKit().Contains))
                {
                    Console.WriteLine("le bike template ajouté existe dans la base de donnée");
                    tempBikeTemplate = bikeTemplate;
                    return true;
                }
            }

            return false;
        }

        // Verification existence BikeTemplate dans le panier (basket)
        private void addBikeTemplateToBasket(int qnt, BikeTemplate tBike)
        {
            if (basket.ContainsKey(tBike))
            {
                Console.WriteLine("basket contains this bike template");

                int qnt_ = default;
                BikeTemplate tBike_ = default;

                foreach (KeyValuePair<BikeTemplate, int> kvp in basket)
                {                    
                    if (kvp.Key == tBike)
                    {
                        Console.WriteLine("bike template founded");
                        qnt_ = kvp.Value;
                        tBike_ = kvp.Key;                                                
                    }
                }

                basket.Remove(tBike);
                basket.Add(tBike_, qnt_ + qnt);
            }
            else
            {
                basket.Add(tBike, qnt);
            }
        }
        public void saveSale()
        {
            int saleID = Controler.Instance.getLastSaleId() + 1;
            int sellerID = seller.getId();
            int clientID = client.getId();
            DateTime sale_date = DateTime.Now;
            DateTime prevision_date = DateTime.Now;
           


            Sale s = new Sale(saleID, sellerID, clientID, "open", sale_date, prevision_date, bikeList);
            Controler.Instance.createSale(s);
            
            

            drainTempSale();
        }

        // creation vente
        private void createSale() { }
        // ajout un par un des velos et les rattacher a la vente par son id
        private void addBasketToSale(Dictionary<BikeTemplate, int> basket)
        {
            foreach (KeyValuePair<BikeTemplate, int> kvp in basket)
            {
                for (int i = 0 ; i < kvp.Value ; i++)
                {
                    int bikeID = Controler.Instance.getL
                    Bike b = new Bike();
                }
            }        
        }

        public void drainTempSale()
        {
            Controler.Instance.tempSale = new TempSale();
        }

    }
}
