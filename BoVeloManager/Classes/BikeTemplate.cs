using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes
{
    public class BikeTemplate
    {
        private readonly CatalogBike catalogBike;
        private readonly int price;
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
            return price;
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
            dI.price = this.getPrice().ToString("P");
            dI.priceMul = (((float)getCat().getPriceMul()) / 100).ToString("P");
            dI.name = getName();
            dI.BikeTemp = this;
            dI.propKit = getPropkitString();
            return dI;
        }

        public struct displayInfo
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
