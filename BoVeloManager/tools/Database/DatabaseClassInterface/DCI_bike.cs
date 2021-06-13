using BoVeloManager.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVeloManager.tools {

    partial class DatabaseClassInterface {

        public static int addBike(Bike b) {
            string q = tools.DatabaseQuery.addBike(b);
            return Database.setData(q);
        }

        public static List<Bike> getBikes(Dictionary<string, BikeTemplate> btList) {
            string query = DatabaseQuery.getBike();
            DataTable dt = tools.Database.getData(query);

            List<Bike> temp = new List<Bike>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                int id = Convert.ToInt32(dt.Rows[i]["id"]);
                int id_tBike = Convert.ToInt32(dt.Rows[i]["id_tBike"]);

                int id_sale;

                if (dt.Rows[i]["id_sale"] != DBNull.Value) {
                    id_sale = Convert.ToInt32(dt.Rows[i]["id_sale"]);
                } else {
                    id_sale = -1;
                }

                int state = Convert.ToInt32(dt.Rows[i]["state"]);
                int poste = Convert.ToInt32(dt.Rows[i]["poste"]);

                DateTime planned_date = (DateTime)dt.Rows[i]["planne_cDate"];
                DateTime Constr_date;
                if (dt.Rows[i]["create_Date"] != DBNull.Value) {
                    Constr_date = (DateTime)dt.Rows[i]["create_Date"];
                } else {
                    Constr_date = DateTime.MinValue;
                }

                foreach (BikeTemplate bt in btList.Values) {
                    if (bt.getId() == id_tBike) {
                        temp.Add(new Bike(id, state, id_sale, poste, bt, planned_date, Constr_date));
                    }
                }

            }

            return temp;
        }

        public static int updateBike(Bike bk) {
            string q = DatabaseQuery.updateBike(bk);
            return Database.setData(q);
        }

        public static int setBikeState(Bike bk) {
            string q = DatabaseQuery.updateBike(bk);
            return Database.setData(q);
        }

    }
}
