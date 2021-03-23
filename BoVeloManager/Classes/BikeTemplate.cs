using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.tools.UI;

namespace BoVeloManager.Classes
{
    public class BikeTemplate
    {
        private readonly CatalogBike catalogBike;
        private readonly int id;
        private List<KitTemplate> KitTemplList;

        public BikeTemplate(int Id, CatalogBike catalogBike_){
            catalogBike = catalogBike_;
            id = Id;
            KitTemplList = new List<KitTemplate>();
        }

        public string getName(){
            return catalogBike.getName();
        }

        public CatalogBike getCat() {
            return catalogBike;
        }
        public float getPrice(){
            float price = 0;
            foreach (KitTemplate kit in KitTemplList) {
                price += kit.getPrice();
            }
            
            price = price * (1 + getCat().getPriceMulDiv());
            return price/100;
        }

        public List<KitTemplate> getListKit() {
            return KitTemplList;
        }

        public int getId(){
            return id;
        }

        public void linkKitTemplate(KitTemplate kt){
            KitTemplList.Add(kt);
        }
        public void unlinkKitTemplate(KitTemplate kt){
            KitTemplList.Remove(kt);
        }

        public string getPropkitString() {
            string propKit = "";
            foreach (KitTemplate kit in getListKit()) {
                propKit += kit.getPropkitString() + "\n";
            }
            return propKit;
        }
        public displayInfo getDisplayInfo()
        {
            displayInfo dI = new displayInfo();
            dI.id = getId();
            dI.price = (this.getPrice()/100).ToString("c2");
            dI.priceMul = (this.getCat().getPriceMulDiv()).ToString("c2");
            dI.name = getName();
            dI.BikeTemp = this;
            dI.propKit = getPropkitString();
            return dI;
        }

        public class displayInfo
        {
            public BikeTemplate BikeTemp;
            public List<KitTemplate> KitTemplList;
            public string propKit { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string price { get; set; }
            public string priceMul { get; set; }
        }
        
    }
}
