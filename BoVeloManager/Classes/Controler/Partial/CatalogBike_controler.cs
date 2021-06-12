using BoVeloManager.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        public List<CatalogBike.displayInfo> getCatalogBikeDisplayInfo() {
            List<CatalogBike.displayInfo> temp = new List<CatalogBike.displayInfo>();

            foreach (CatalogBike cb in CatalogBikeList) {
                temp.Add(cb.GetDisplayInfo());
            }

            return temp;
        }

        public List<CatalogBike> getCatalogBike() {
            return CatalogBikeList;
        }

        public int getlastCatalogBikeId() {
            if (CatalogBikeList.Count > 0) {
                return CatalogBikeList.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public void createCatalogBike(CatalogBike cb) {
            CatalogBikeList.Add(cb);
            DatabaseClassInterface.addCatalogBike(cb);
        }

    }
}
