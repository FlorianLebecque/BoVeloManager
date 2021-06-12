using BoVeloManager.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {

    partial class DatabaseClassInterface {

        public static List<KitTemplate> getKitTemplates() {

            //get the user query and data from the database
            string query = DatabaseQuery.getKits();
            DataTable dt = tools.Database.getData(query);

            //convert all the user into a user object
            List<KitTemplate> temp = new List<KitTemplate>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                string name = (string)dt.Rows[i]["name"];
                int cat = Convert.ToInt32(dt.Rows[i]["category"]);
                int p = Convert.ToInt32(dt.Rows[i]["Price"]);

                string prop;
                if (dt.Rows[i]["properties"] != DBNull.Value) {
                    prop = (string)dt.Rows[i]["properties"];
                } else {
                    prop = "";
                }

                int stock_qtt = Convert.ToInt32(dt.Rows[i]["stock_qtt"]);
                int stock_location_x = Convert.ToInt32(dt.Rows[i]["location_x"]);
                int stock_location_y = Convert.ToInt32(dt.Rows[i]["location_y"]);
                int bike_qtt = Convert.ToInt32(dt.Rows[i]["bike_qtt"]);

                temp.Add(new KitTemplate(id, name, cat, p, prop, stock_qtt, stock_location_x, stock_location_y, bike_qtt));
                
            }

            return temp;
        }

        public static int addKitTemplate(KitTemplate kt) {
            string q = DatabaseQuery.addKit(kt.getId(), kt.getName(), kt.getProperties(), kt.getPrice(), kt.getCategory());
            return Database.setData(q);
        }

        public static int updateKitTemplate(KitTemplate kt) {
            string q = DatabaseQuery.updateKitTemplate(kt.getId(), kt.getName(), kt.getCategory(), kt.getPrice(), kt.getProperties());
            return Database.setData(q);
        }

    }
}
