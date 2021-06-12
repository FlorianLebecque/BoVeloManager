using BoVeloManager.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {
    public class KitTemplate {

        private readonly int id;
        private string name;
        private KitCategory cat;
        private int price;
        private string properties;

        private int bike_qtt;

        private int stock_qtt;
        private int stock_location_x;
        private int stock_location_y;

        public KitTemplate(int id_,string name_,KitCategory cat_,int price_,string prop_, int stock_qtt_, int stock_location_x_, int stock_location_y_, int bike_qtt_) {
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

        public KitCategory getCategory() {
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

        public int getStockLocationX()
        {
            return stock_location_x;
        }

        public int getStockLocationY()
        {
            return stock_location_y;
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
            DatabaseClassInterface.updateKitTemplate(this);
        }

        public void setPrice(int p) {
           price = p;
           DatabaseClassInterface.updateKitTemplate(this);
        }

        public void setProperties(string p) {
            properties = p;
            DatabaseClassInterface.updateKitTemplate(this);
        }

        public void setCategory(KitCategory c) {
            cat = c;
            DatabaseClassInterface.updateKitTemplate(this);
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
            temp.category = tools.Converter.GetCatName(cat);
            temp.price = (((float)this.getPrice())/100).ToString("c2");
            temp.priceInt = getPrice();
            temp.properties = this.getProperties();
            temp.fancyName = getPropkitString();
            temp.stock_qtt = this.getStockQtt().ToString();
            temp.stock_location_x = this.getStockLocationX().ToString();
            temp.stock_location_y = this.getStockLocationY().ToString();
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
            public string stock_qtt { get; set; }
            public string stock_location_x { get; set; }
            public string stock_location_y { get; set; }
            public string bike_qtt { get; set; }
        }

    }
}
