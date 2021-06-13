using BoVeloManager.tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.Classes {

    public partial class Controler {

        public void createBikeTemplate(BikeTemplate t) {
            DatabaseClassInterface.addBikeTemplate(t);

            if (!bikeTemplateList.ContainsKey(t.Key)) {
                bikeTemplateList.Add(t.Key, t);
            }

            
        }

        public void link_kit_to_tbike(BikeTemplate bt, KitTemplate kt) {
            DatabaseClassInterface.link_kit_to_tbike(bt, kt);
        }

        public BikeTemplate getBikeTemplateById(int id_tBike) {
            foreach (BikeTemplate bt in bikeTemplateList.Values) {

                if (bt.getId() == id_tBike) {
                    return bt;
                }

            }
            return null;
        }

        public int getLastBikeTemplateId() {
            if (bikeTemplateList.Count > 0) {
                return bikeTemplateList.Values.Select(x => x.getId()).Max();
            }
            return 0;
        }

        public Dictionary<string, BikeTemplate> getBikeTemplateList() {
            return bikeTemplateList;
        }

    }
}
