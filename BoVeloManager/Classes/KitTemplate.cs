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

        private int bike_qtt;

        private int stock_qtt;
        private int stock_location_x;
        private int stock_location_y;

        public KitTemplate(int id_,string name_,int cat_,int price_,string prop_, int stock_qtt_, int stock_location_x_, int stock_location_y_, int bike_qtt_) {
            id = id_;
            name = name_;
            price = price_;
            cat = cat_;
            properties = prop_;
            stock_qtt = stock_qtt_;
            stock_location_x = stock_location_x_;
            stock_location_y = stock_location_y_;
            bike_qtt = bike_qtt_;
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

        public int getBikeQtt()
        {
            return bike_qtt;
        }

        public int getStockQtt()
        {
            return stock_qtt;
        }
        public void setBikeQtt(int new_bike_qtt)
        {
            bike_qtt = new_bike_qtt;
        }
        public void setStockQtt(int new_stock)
        {
            stock_qtt = new_stock;
        }
        public Dictionary<string,int> getLocation()
        {
            Dictionary<string, int> location = new Dictionary<string, int>();
            location.Add("X", stock_location_x);
            location.Add("Y", stock_location_y);
            return location;
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
            temp.stock_qtt = this.stock_qtt;
            temp.stock_location_x = this.stock_location_x;
            temp.stock_location_y = this.stock_location_y;
            temp.bike_qtt = this.getBikeQtt().ToString();

            return temp;

        }

        public class displayInfo {
            public KitTemplate curKit;
            public int id { get; set; }
            public string name { get; set; }
            public string category { get; set; }
            public string price { get; set; }
            public int priceInt { get; set; }
            public string fancyName { get; set; }
            public string properties { get; set; }
            public int stock_qtt { get; set; }
            public int stock_location_x { get; set; }
            public int stock_location_y { get; set; }
            public string bike_qtt { get; set; }
        }

    }
}
