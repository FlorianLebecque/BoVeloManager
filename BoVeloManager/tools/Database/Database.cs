using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using BoVeloManager.Classes;

namespace BoVeloManager.tools {

    class Database {

        private static Dictionary<string, string> tableCheck = new Dictionary<string, string>();

        private static MySqlConnection MSCon;

        public static DataTable getData(string query) {
            checkConnection();

            int nbrTry = 0;

            DataTable dt = new DataTable();

            while (nbrTry < Properties.Settings.Default.MAX_DB_TRY) {
                try {
                    MySqlCommand cmd = MSCon.CreateCommand();
                    cmd.CommandText = query;

                    MySqlDataAdapter DataRow = new MySqlDataAdapter(cmd);
                    DataRow.Fill(dt);

                    return dt;
                } catch {
                    nbrTry++;
                }

            }

            throw new Exception("Can't get data from database, code 1");
        }

        public static int setData(string query) {
            checkConnection();

            int nbrTry = 0;

            while (nbrTry < Properties.Settings.Default.MAX_DB_TRY) {
                try {

                    

                    MySqlCommand cmd = MSCon.CreateCommand();
                    cmd.CommandText = query;

                    int nbr = cmd.ExecuteNonQuery();

                    if (Properties.Settings.Default.SYNC_ENABLE) {
                        Controler.Instance.resync(checkTable());
                    }       

                    return nbr;
                } catch {
                    nbrTry++; 
                    if(nbrTry % 5 == 0) {
                        tools.UI.MessageBox.Show("Trying to reconnect", "Error");
                        connectToDB();
                    }

                }
            }

            throw new Exception("Can't set data to database, code 2");

        }

        private static void checkConnection() {

            if ((MSCon == null) || (MSCon.State == ConnectionState.Closed) || (MSCon.State == ConnectionState.Broken)) {
                int nbrTry = 0;

                while (nbrTry < 5) {
                    try {
                        connectToDB();
                        return;
                    } catch {
                        nbrTry++;
                    }
                }
                throw new Exception("Could not connect to database, code 0");
            }
        }

        private static void connectToDB() {
            MySqlConnectionStringBuilder connBuilder;

            if (Properties.Settings.Default.DBProd) {
                connBuilder = new MySqlConnectionStringBuilder {
                    { "Database", Properties.Settings.Default.DBBase },
                    { "Data Source", Properties.Settings.Default.DBHost },
                    { "User Id", Properties.Settings.Default.DBUser },
                    { "Password", Properties.Settings.Default.DBPass }
                };
            } else {
                connBuilder = new MySqlConnectionStringBuilder {
                    { "Database", Properties.Settings.Default.DBBase2 },
                    { "Data Source", Properties.Settings.Default.DBHost },
                    { "User Id", Properties.Settings.Default.DBUser2 },
                    { "Password", Properties.Settings.Default.DBPass2 }
                };
            }

            MSCon = new MySqlConnection(connBuilder.ConnectionString);

            MSCon.Open();
        }
            
        private static List<string> checkTable() {
            string q = DatabaseQuery.getTable();
            DataTable dt = getData(q);

            List<string> changed_table = new List<string>();

            for(int i = 0; i < dt.Rows.Count; i++) {
                string[] temp = new string[2];
                temp[0] = (string)dt.Rows[i]["Tables_in_"+Properties.Settings.Default.DBUser];
                string q2 = DatabaseQuery.getCheckSum_tableQuery(temp[0]);
                DataTable dt2 = getData(q2);
                temp[1] = Convert.ToInt64(dt2.Rows[0]["Checksum"]).ToString();

                if (tableCheck.ContainsKey(temp[0])){
                    if(tableCheck[temp[0]] != temp[1]) {
                        changed_table.Add(temp[0]);
                    }
                } else {
                    tableCheck.Add(temp[0], temp[1]);
                }

            }

            return changed_table;

        }

    }

}
