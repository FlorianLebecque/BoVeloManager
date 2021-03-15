﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class Bike
    {
 
        private readonly int id_sale;
        private readonly int id;
        private int status;
        private int Poste;
        private BikeTemplate BikeTemplate;
        private DateTime PlannedDate;
        private DateTime CreationDate;

        public Bike(int id_,int status_, int id_sale_,int Poste_, BikeTemplate bt_, DateTime constr_date_) {
            id = id_;
            status = status_;
            BikeTemplate = bt_;
            id_sale = id_sale_;
            Poste = Poste_;
            PlannedDate = constr_date_;
            link();
        }

        private void link() {
            //BikeTemplate = Controler.getBikeTemplateById(id_tBike);
        }

        public int getId() {
            return id;
        }

        public int getSaleId() {
            return id_sale;
        }

        public float getPrice() {
            float bike_price = 0;
            float priceMul = ((float)(this.getBikeTempl().getCat().getPriceMul())) / 100;
            foreach (KitTemplate kit in this.getBikeTempl().getListKit()) {
                bike_price += kit.getPrice();
            }
            bike_price = bike_price + bike_price * priceMul;
            return bike_price/100;
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

        public displayInfo GetDisplayInfo() {
            displayInfo temp = new displayInfo();

            temp.CurBike = this;

            string state = "";
            switch (status)
            {
                case 0:
                    state = "Waiting";
                    break;
                case 1:
                    state = "In progress";
                    break;
                case 2:
                    state = "Done";
                    break;
            }

            temp.state = state;

            temp.name = BikeTemplate.getName();
            temp.id = BikeTemplate.getId();
            temp.priceMul = BikeTemplate.getCat().getPriceMul();
            temp.id_sale = this.getSaleId();
            temp.ConstDate = this.getPlannedtDate().ToString("dd/MM/yyyy");
            temp.price = this.getPrice().ToString("c2");

            return temp;
        }

        public struct displayInfo {
            public Bike CurBike { get; set; }
            public string price  { get; set; }
            public string ConstDate { get; set; }
            public string state { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int priceMul { get; set; }
            public int id_sale { get; set; }
        }

    }
}
