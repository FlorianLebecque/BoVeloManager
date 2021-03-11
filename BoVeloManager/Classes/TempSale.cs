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
        private int nb_x;
        private int nb_y;
        private int nb_z;

        private BikeTemplate x;
        private BikeTemplate y;
        private BikeTemplate z;

        private Client client;
        private User seller;

        DateTime date;
        DateTime prevision_date;

        private Sale sale;

        public TempSale()
        {

        }

        

        public void addItem(CatalogBike catBike, KitTemplate size, KitTemplate color, int qnt) 
        {
            


        }

        public void saveSale()
        {


            drainTempSale();
        }

        public void drainTempSale()
        {
            Controler.Instance.tempSale = new TempSale();
        }

    }
}
