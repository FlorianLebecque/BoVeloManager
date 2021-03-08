using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class Bike
    {
 
        private readonly int id_sale;

        private int status;
        private BikeTemplate BikeTemplate;
        

        public Bike(int status_, int id_sale_, BikeTemplate bt_)
        {
            status = status_;
            BikeTemplate = bt_;
            id_sale = id_sale_;
            link();
        }

        private void link()
        {
            //BikeTemplate = Controler.getBikeTemplateById(id_tBike);
        }

        public int getSaleId() {
            return id_sale;
        }

        public BikeTemplate getBikeTempl() {
            return BikeTemplate;
        }

        public displayInfo GetDisplayInfo()
        {
            displayInfo temp = new displayInfo();

            temp.CurBike = this;

            string state = "";
            switch (status)
            {
                case 2:
                    state = "Waiting";
                    break;
                case 1:
                    state = "In progress";
                    break;
                case 0:
                    state = "Done";
                    break;
            }

            temp.state = state;

            temp.name = BikeTemplate.getName();
            temp.id = BikeTemplate.getId();
            temp.priceMul = BikeTemplate.getPriceMul();
            temp.id_sale = id_sale;

            return temp;
        }

        public struct displayInfo
        {
            public Bike CurBike { get; set; }
            public string state { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int priceMul { get; set; }
            public int id_sale { get; set; }

        }

    }
}
