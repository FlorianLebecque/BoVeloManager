using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BoVeloManager.Classes
{
    public class Bike
    {
        private int id_sale;
        private int id;
        private int status;
        private int Poste;
        private BikeTemplate BikeTemplate;
        private DateTime PlannedDate;
        private DateTime ConstrucDate;

        public Bike(int id_,int status_, int id_sale_,int Poste_, BikeTemplate bt_, DateTime planned_date_, DateTime constr_date_) {
            id = id_;
            status = status_;
            BikeTemplate = bt_;
            id_sale = id_sale_;
            Poste = Poste_;
            PlannedDate = planned_date_;
            ConstrucDate = constr_date_;
        }


        public int getId() {
            return id;
        }

        public int getSaleId() {
            return id_sale;
        }

        public void setSaleId(int id) {
            id_sale = id;
        }

        public float getPrice() {
            return this.BikeTemplate.getPrice();
        }

        public DateTime getPlannedtDate() {
            return PlannedDate;
        }

        public void setPlannedDate(DateTime dt) {
            PlannedDate = dt;
        }

        public BikeTemplate getBikeTempl() {
            return BikeTemplate;
        }

        public int getState() {
            return status;
        }

        public int getPoste() {
            return Poste;
        }

        public void setPoste(int p) {
            Poste = p;
        }

        public void setState(int s) {
            status = s;
        }

        public void getConstructionDate(DateTime dt) {
            ConstrucDate = dt;
        }

        public DateTime getConstructionDate()
        {
            if(ConstrucDate != null){
                return ConstrucDate;
            }
            else {
                return DateTime.MinValue;
            }
            
        }

        public displayInfo GetDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurBike = this;

            string state = "";
            switch (status)
            {
                case 0:
                    state = "Waiting";
                    temp.color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#8c6363"));
                    break;
                case 1:
                    state = "In progress";
                    temp.color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#637B8C"));
                    break;
                case 2:
                    state = "Done";
                    temp.color = (SolidColorBrush)(new BrushConverter().ConvertFrom("#678c63"));
                    break;
            }

            temp.state = state;

            temp.name = BikeTemplate.getName();
            temp.id = getId();
            temp.priceMul = BikeTemplate.getCat().getPriceMulDiv();
            temp.id_sale = this.getSaleId();
            temp.PlannedDate = this.getPlannedtDate().ToString("dd/MM/yyyy");
            temp.price = ((float)BikeTemplate.getPrice()/100).ToString("c2");
            temp.title = "#" + getId().ToString() + " - " + BikeTemplate.getName();
            
            DateTime dt = getConstructionDate();

            return temp;
        }

        public class displayInfo {
            public Bike CurBike { get; set; }
            public string price  { get; set; }
            public string PlannedDate { get; set; }
            public string ConstrucDate { get; set; }
            public string state { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public float priceMul { get; set; }
            public int id_sale { get; set; }
            public string title { get; set; }
            public Brush color { get; set; }
        }

    }
}
