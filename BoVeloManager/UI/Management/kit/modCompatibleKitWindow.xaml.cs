using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BoVeloManager.Management.kit
{
    /// <summary>
    /// Logique d'interaction pour modCompatibleKitWindow.xaml
    /// </summary>
    public partial class modCompatibleKitWindow : Window
    {
        private int kitId;
        DataTable dt_item;



        public modCompatibleKitWindow(int kitId_)
        {
            InitializeComponent();

            // initialise the windows
            kitId = kitId_;

            //get the kit data
            string q = tools.DatabaseQuery.getKit_by_id(kitId);
            DataTable res = tools.Database.getData(q);

            //display the kit data
            tb_kitName.Text = (string)res.Rows[0]["name"];

            set_lb_selectCompIt_content();



        }

        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BT_update_Click(object sender, RoutedEventArgs e)
        {
            

            //dt contient tous les id d'item pour lesquels dt_item["itemChecked"] valait true au depart
            string q = tools.DatabaseQuery.getCompatibleCatId_with_KitId(kitId);
            DataTable dt = tools.Database.getData(q);


            Console.WriteLine("@@@@@@@");
          
            foreach (DataRowView lbi in lb_selectCompIt.Items)
            {
                Console.WriteLine(lbi.ToString());
                Console.WriteLine(lbi["name"]);
                Console.WriteLine(lbi["itemChecked"]);



            }

            foreach (DataRowView dataR in lb_selectCompIt.Items)
            {
                if (String.Compare(Convert.ToString(dataR["itemChecked"]), "True") == 0)
                {
                    bool tog = false;
                    //verifier qu'on a une ligne avec kitId et dataR["id"] dans bv_cat_tKit (on doit l'avoir, sinon l'ajouter)
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToInt32(row["id_cat"]) == Convert.ToInt32(dataR["id"]))
                        {
                            tog = true;
                            Console.WriteLine("ID Trouvé");
                            break;
                        }
                    }

                    if (tog.CompareTo(false) == 0)
                    {
                        //Ajouter une ligne dans bv_cat_tKit avec kitId et dataR["id"]
                        string request = tools.DatabaseQuery.addCompatibleKit(Convert.ToInt32(dataR["id"]), kitId);
                        //envoyer request
                        int res = tools.Database.setData(request);
                        if (res == -1)
                        {
                            MessageBox.Show("An error has occured");
                        }
                        else if (res == 1)
                        {

                        }
                        else
                        {
                            MessageBox.Show("The database might be corrupted");
                        }
                    }
                }
                else
                {
                    bool tog = false;
                    //vérifier qu'on a pas la ligne dans bv cat tkit (on doit pas l'avoir, la retirer si on l'a)
                    foreach (DataRow row in dt.Rows)
                    {
                        if (Convert.ToInt32(row["id_cat"]) == Convert.ToInt32(dataR["id"]))
                        {
                            tog = true;
                            Console.WriteLine("ID Trouvé");
                            break;
                        }
                    }

                    if (tog.CompareTo(true) == 0)
                    {
                        //Retirer la ligne dans bv_cat_tKit avec kitId et dataR["id"]
                        string request = tools.DatabaseQuery.delCompatibleKit(Convert.ToInt32(dataR["id"]), kitId);
                        //envoyer request
                        int res = tools.Database.setData(request);
                        if (res == -1)
                        {
                            MessageBox.Show("An error has occured");
                        }
                        else if (res == 1)
                        {

                        }
                        else
                        {
                            MessageBox.Show("The database might be corrupted");
                        }
                    }

                }

            }
    


            this.Close();



        }

        private void set_lb_selectCompIt_content()
        {
            //get item compatible id in dt2
            string req = tools.DatabaseQuery.getCompatibleCatId_with_KitId(kitId);
            DataTable dt = tools.Database.getData(req);

            //get all items to show them in select box
            string req_it = tools.DatabaseQuery.getItem();
            dt_item = tools.Database.getData(req_it);

            //ajouter a dt_item une row "itemChecked" true/false
            DataColumn itChecked_col = new DataColumn();
            itChecked_col.ColumnName = "itemChecked";
            itChecked_col.DataType = typeof(string);
            dt_item.Columns.Add(itChecked_col);

            //set if item_checked should be true for each item
            foreach (DataRow r in dt_item.Rows)
            {
                //regarder si r["id"] se trouve dans dt2
                //si oui: r["itemChecked"] = True;
                //si non: =False
            
                foreach (DataRow dr in dt.Rows)
                {
             
                    if (Convert.ToInt32(r["id"]) == Convert.ToInt32(dr["id_cat"])) 
                    {
                        r["itemChecked"] = "True";
                        break;
                    }
                }
                Console.WriteLine(r["itemChecked"]);
                if (Convert.ToString(r["itemChecked"]) != "True")
                {
                    r["itemChecked"] = "False";
                }
                

            }

            lb_selectCompIt.ItemsSource = dt_item.DefaultView;
            

        }
    }
}
