using BoVeloManager.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {

    partial class DatabaseClassInterface {

        public static int addBikeTemplate(BikeTemplate t) {
            string q = tools.DatabaseQuery.addBikeTemplate(t);
            Database.setData(q);

            foreach(KitTemplate kt in t.getListKit()) {
                link_kit_to_tbike(t, kt);
            }

            return 0;
        }

        public static int link_kit_to_tbike(BikeTemplate bt, KitTemplate kt) {
            string q = tools.DatabaseQuery.link_kit_to_tbike(bt, kt);
            return Database.setData(q);
        }

        public static Dictionary<string,BikeTemplate> getBikeTemplates(List<CatalogBike> cbList, List<KitTemplate> ktList) {
            string query = DatabaseQuery.getTBike();
            DataTable dt = tools.Database.getData(query);
            Dictionary<string, BikeTemplate> Btemp = new Dictionary<string, BikeTemplate>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                int id_cat = Convert.ToInt32(dt.Rows[i]["id_cat"]);

                foreach (CatalogBike cb in cbList) {
                    if (cb.getId() == id_cat) {

                        BikeTemplate bt = new BikeTemplate(id, cb);

                        string q = DatabaseQuery.getKitId_byTBike(id);
                        DataTable kdt = Database.getData(q);

                        for (int j = 0; j < kdt.Rows.Count; j++) {
                            int idK = Convert.ToInt32(kdt.Rows[j]["id"]);

                            foreach (KitTemplate kt in ktList) {
                                if (kt.getId() == idK) {
                                    bt.linkKitTemplate(kt);
                                }
                            }

                        }

                        if (!Btemp.ContainsKey(bt.Key)) {
                            Btemp.Add(bt.Key, bt);
                        }
                    }
                }
            }

            return Btemp; 
        }
    }
}
