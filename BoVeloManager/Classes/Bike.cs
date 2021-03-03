using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class Bike
    {
        private readonly int id;
        private readonly int id_sale;
        private readonly int id_tBike;

        private int status;
        public BikeTemplate BikeTemplate;
        

        public Bike(int status_, int id_tBike_, int id_, int id_sale_)
        {
            status = status_;
            id_tBike = id_tBike_;
            id = id_;
            id_sale = id_sale_;
            link();
        }

        private void link()
        {
            //BikeTemplate = Controler.getBikeTemplateById(id_tBike);
        }

        public int getSaleId() {
            return id;
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
            temp.price = BikeTemplate.getPrice();
            temp.id_sale = id_sale;

            return temp;
        }

        public struct displayInfo
        {
            public Bike CurBike { get; set; }
            public string state { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int price { get; set; }
            public int id_sale { get; set; }

        }

    }
}
