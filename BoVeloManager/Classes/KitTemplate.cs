using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {
    public class KitTemplate {

        private readonly int id;
        private string name;
        private int cat;
        private int price;
        private string properties;


        public KitTemplate(int id_,string name_,int cat_,int price_,string prop_) {
            id = id_;
            name = name_;
            price = price_;
            cat = cat_;
            properties = prop_;
        }

        public string getName() {
            return name;
        }

        public int getId() {
            return id;
        }

        public int getPrice() {
            return price;
        }

        public string getProperties() {
            return properties;
        }

        public int getCategory() {
            return cat;
        }

        public void setName(string n) {
            name = n;
        }

        public void setPrice(int p) {
           price = p;
        }

        public void setProperties(string p) {
            properties = p;
        }

        public void setCategory(int c) {
            cat = c;
        }

        public string getPropkitString() {
            if (this.getProperties().Length == 0) {
                return "● " + this.getName();
            } else {
                return  "● " + this.getName() + " [" + this.getProperties() + "]";
            }
        }

        public displayInfo GetDisplayInfo() {

            displayInfo temp = new displayInfo();

            temp.curKit = this;
            temp.id = this.getId();
            temp.name = this.getName();

            switch (cat) {
                case 0:
                    temp.category = "Size";
                    break;
                case 1:
                    temp.category = "Color";
                    break;
                default:
                    temp.category = "Unkown";
                    break;
            }

            temp.price = (((float)this.getPrice())/100).ToString("c2");
            temp.priceInt = getPrice();
            temp.properties = this.getProperties();
            temp.fancyName = getPropkitString();

            return temp;

        }

        public struct displayInfo {
            public KitTemplate curKit;
            public int id { get; set; }
            public string name { get; set; }
            public string category { get; set; }
            public string price { get; set; }
            public int priceInt { get; set; }
            public string fancyName { get; set; }
            public string properties { get; set; }
        }

    }
}
