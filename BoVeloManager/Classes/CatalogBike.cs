﻿using System;
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
        private Image pic;
        private List<KitTemplate> kitTemplateList;
               

        public CatalogBike(int id_,string name_,int priceMul_, string pic_filename) {
            id = id_;
            name = name_;
            PriceMul = priceMul_;
            pic = Image.FromFile(pic_filename);
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

        public int getPriceMul() {
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

        public struct displayInfo {
            public CatalogBike CurCatBike { get; set; }
            public string name { get; set; }
            public string PriceMul { get; set; }
            public int id { get; set; }
            public Image pic { get; set; }
            public List<KitTemplate> kitTemplates { get; set; }

        }
    }
}
