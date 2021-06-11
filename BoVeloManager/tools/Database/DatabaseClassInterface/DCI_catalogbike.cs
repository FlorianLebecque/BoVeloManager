using BoVeloManager.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {

    partial class DatabaseClassInterface {

        public static List<CatalogBike> getCatalogBikes(List<KitTemplate> ktList) {

            string q = DatabaseQuery.getCatalogBike();
            DataTable dt = Database.getData(q);

            //convert all the user into a user object
            List<CatalogBike> temp = new List<CatalogBike>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string name = (string)dt.Rows[i]["name"];
                int PriceMul = Convert.ToInt32(dt.Rows[i]["PriceMul"]);
                string pic_filename = (string)dt.Rows[i]["picture"];

                string query = DatabaseQuery.getKit_by_catalogBikeId(id);
                DataTable kdt = Database.getData(query);

                CatalogBike cb = new CatalogBike(id, name, PriceMul, pic_filename);

                for (int j = 0; j < kdt.Rows.Count; j++) {
                    foreach (KitTemplate kt in ktList) {
                        if (kt.getId() == Convert.ToInt32(kdt.Rows[j]["id_tKit"])) {
                            cb.linkKitTemplate(kt);
                        }
                    }
                }


                temp.Add(cb);
            }

            return temp;


        }

        public static int updateCatalogBike(CatalogBike kt) {
            string q = DatabaseQuery.updateCatalogBike(kt);
            return Database.setData(q);
        }

        public static int linkKTCB(CatalogBike cb, KitTemplate kt) {
            string q = DatabaseQuery.addCompatibleKit(cb.getId(), kt.getId());
            return Database.setData(q);
        }

        public static int unlinkKTCB(CatalogBike cb, KitTemplate kt) {
            string q = DatabaseQuery.delCompatibleKit(cb.getId(), kt.getId());
            return Database.setData(q);
        }

        public static int addCatalogBike(CatalogBike cb) {
            string q = DatabaseQuery.addCatalogBike(cb);
            return Database.setData(q);
        }

    }
}
