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

        DateTime date;
        DateTime prevision_date;

        private Sale sale;

        private BikeTemplate tempBikeTemplate;

        public TempSale()
        {

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
            }

            addBikeTemplateToBasket(qnt, tBike);
        }

        // Verification existence BikeTemplate dans la db
        private bool existBikeTemplate(CatalogBike catBike, KitTemplate size, KitTemplate color, List<BikeTemplate> tBike_list)
        {
            List<KitTemplate> listKit = new List<KitTemplate>();
            listKit.Add(size);
            listKit.Add(color);

            foreach (BikeTemplate bikeTemplate in tBike_list)
            {
                if (bikeTemplate.getCat() == catBike && bikeTemplate.getListKit() == listKit)
                {
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
                //int qnt_ = basket.FirstOrDefault(x => x.Value == tBike).Key;
                //qnt_ += qnt;

                foreach (KeyValuePair<BikeTemplate, int> kvp in basket)
                {
                    if (kvp.Key == tBike)
                    {
                        int qnt_ = kvp.Value;
                        BikeTemplate tBike_ = kvp.Key;

                        basket.Remove(tBike);
                        basket.Add(tBike_, qnt_ + qnt);
                    }
                }
            }
            else
            {
                basket.Add(tBike, qnt);
            }
        }
        public void saveSale()
        {

            // Bike bike = new Bike();

            createSale();

            /*
            foreach (Bike bike in BikeList)
            {
                addBikeToSale(bike);
            }
            */
            

            drainTempSale();
        }

        // creation vente
        private void createSale() { }
        // ajout un par un des velos et les rattacher a la vente par son id
        private void addBikeToSale(Bike bike) { }

        public void drainTempSale()
        {
            Controler.Instance.tempSale = new TempSale();
        }

    }
}
