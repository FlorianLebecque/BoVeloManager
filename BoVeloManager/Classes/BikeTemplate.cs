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
        private readonly int priceMul;
        private readonly int id;
        private List<KitTemplate> KitTemplList;

        public BikeTemplate(int Id, CatalogBike catalogBike_, int Price ){
            catalogBike = catalogBike_;
            priceMul = Price;
            id = Id;
            KitTemplList = new List<KitTemplate>();
        }

        public string getName(){
            return catalogBike.getName();
        }

        public int getPriceMul(){
            return priceMul;
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

        public displayInfo getDisplayInfo()
        {
            displayInfo dI = new displayInfo();
            dI.id = getId();
            dI.priceMul = (((float)getPriceMul())/100).ToString("P");
            dI.name = getName();
            dI.BikeTemp = this;
            return dI;
        }

        public struct displayInfo
        {
            public BikeTemplate BikeTemp;
            public List<KitTemplate> KitTemplList;
            public int id { get; set; }
            public string name { get; set; }
            public string priceMul { get; set; }
        }
        
    }
}
