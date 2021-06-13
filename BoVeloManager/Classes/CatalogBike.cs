using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoVeloManager.Classes {
    public class CatalogBike {

        private readonly int id;
        private string name;
        private int PriceMul;
        private string pic;
        private List<KitTemplate> kitTemplateList;
               

        public CatalogBike(int id_,string name_,int priceMul_, string pic_filename) {
            id = id_;
            name = name_;
            PriceMul = priceMul_;
            pic = pic_filename;
            kitTemplateList = new List<KitTemplate>();
        }

        public string getName() {
            return name;
        }

        public void setName(string n) {
            name = n;
        }

        public int getId() {
            return id;
        }

        public float getPriceMul() {
            return PriceMul;
        }

        public int getPriceMulDiv() {
            return PriceMul;
        }

        public void setPriceMul(int p) {
            PriceMul = p;
        }

        public void linkKitTemplate(KitTemplate kt) {
            kitTemplateList.Add(kt);
            
        }

        public void unlinkKitTemplate(KitTemplate kt) {
            kitTemplateList.Remove(kt);
            
        }

        public (List<string>,List<string>) getProperties() {
            List<string> sizeList = new List<string>();
            List<string> colorList = new List<string>();

            // Pour chaque kittemplate de categorie 0
            foreach (KitTemplate kt in kitTemplateList.Where(x => x.getCategory() == KitCategory.frame).ToList()) {
                string[] prop = kt.getProperties().Split('|');
                if (!sizeList.Contains(prop[0])) {
                    sizeList.Add(prop[0]);
                }
                if (!colorList.Contains(prop[1])) {
                    colorList.Add(prop[1]);
                }

            }

            return (sizeList, colorList);
        }


        public List<KitTemplate> getKitTemplateList() {
            return kitTemplateList;
        }

        public displayInfo GetDisplayInfo() {

            displayInfo temp = new displayInfo();

            temp.CurCatBike = this;
            temp.name = this.getName();
            temp.PriceMul = (((float)PriceMul)/100).ToString("p");
            temp.id = this.getId();
            temp.pic = this.pic;

            temp.kitTemplates = this.getKitTemplateList();

            return temp;
        }

        public class displayInfo {
            public CatalogBike CurCatBike { get; set; }
            public string name { get; set; }
            public string PriceMul { get; set; }
            public int id { get; set; }
            public string pic { get; set; }
            public List<KitTemplate> kitTemplates { get; set; }

        }
    }
}
