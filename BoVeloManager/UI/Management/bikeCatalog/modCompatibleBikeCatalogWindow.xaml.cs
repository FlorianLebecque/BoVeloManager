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

namespace BoVeloManager.Management.item
{
    /// <summary>
    /// Logique d'interaction pour modCompatibleItemWindow.xaml
    /// </summary>
    /// 

    public partial class modCompatibleItemWindow : Window
    {

        //private int kitId;
        private int cat_id;

        private List<int> updateRequestList = new List<int>{} ;
        private List<string> updateRequestList_add_or_del = new List<string> { };


        DataTable dt_item;
        public modCompatibleItemWindow(int catId_)
        {
            InitializeComponent();

            // initialise the windows
            cat_id = catId_;

            //get the kit data
            //string q = tools.DatabaseQuery.getKit_by_id(kitId);
            //string q = tools.DatabaseQuery.getItem_by_id(kitId);
            string q = tools.DatabaseQuery.getItem_by_id(cat_id);

            DataTable res = tools.Database.getData(q);

            //display the kit data
            tb_kitName.Text = (string)res.Rows[0]["name"];
            tb_title.Text = "Edit compatible kits with " + (string)res.Rows[0]["name"] + "bike" ; 

            set_dg_selCompatibleKit_content();


        }


        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BT_update_Click(object sender, RoutedEventArgs e)
        {
            /*

            //dt contient tous les id d'item pour lesquels dt_item["itemChecked"] valait true au depart
            //string q = tools.DatabaseQuery.getCompatibleCatId_with_KitId(kitId);
            //string q = tools.DatabaseQuery.getCompatibleKitId_with_categoryId(kitId);
            string q = tools.DatabaseQuery.getCompatibleKitId_with_categoryId(cat_id);


            DataTable dt = tools.Database.getData(q);


            Console.WriteLine("@@@@@@@");

            foreach (DataRowView lbi in dg_selCompatibleKit.Items)
            {
                Console.WriteLine(lbi.ToString());
                Console.WriteLine(lbi["name"]);
                Console.WriteLine(lbi["itemChecked"]);



            }

            foreach (DataRowView dataR in dg_selCompatibleKit.Items)
            {
                if (String.Compare(Convert.ToString(dataR["itemChecked"]), "True") == 0)
                {
                    bool tog = false;
                    //verifier qu'on a une ligne avec kitId et dataR["id"] dans bv_cat_tKit (on doit l'avoir, sinon l'ajouter)
                    foreach (DataRow row in dt.Rows)
                    {



                        //if (Convert.ToInt32(row["id_cat"]) == Convert.ToInt32(dataR["id"]))
                        if (Convert.ToInt32(row["id_tKit"]) == Convert.ToInt32(dataR["id"]))
                        {



                            tog = true;
                            Console.WriteLine("ID Trouvé");
                            break;
                        }
                    }

                    if (tog.CompareTo(false) == 0)
                    {
                        //Ajouter une ligne dans bv_cat_tKit avec kitId et dataR["id"]


                        //string request = tools.DatabaseQuery.addCompatibleKit(Convert.ToInt32(dataR["id"]), kitId);
                        //string request = tools.DatabaseQuery.addCompatibleKit(kitId, Convert.ToInt32(dataR["id"]));
                        string request = tools.DatabaseQuery.addCompatibleKit(cat_id, Convert.ToInt32(dataR["id"]));

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


                        //if (Convert.ToInt32(row["id_cat"]) == Convert.ToInt32(dataR["id"]))
                        if (Convert.ToInt32(row["id_tKit"]) == Convert.ToInt32(dataR["id"]))
                        {


                            tog = true;
                            Console.WriteLine("ID Trouvé");
                            break;
                        }
                    }

                    if (tog.CompareTo(true) == 0)
                    {



                        //Retirer la ligne dans bv_cat_tKit avec kitId et dataR["id"]
                        //string request = tools.DatabaseQuery.delCompatibleKit(Convert.ToInt32(dataR["id"]), kitId);
                        //string request = tools.DatabaseQuery.delCompatibleKit(kitId, Convert.ToInt32(dataR["id"]));
                        string request = tools.DatabaseQuery.delCompatibleKit(cat_id, Convert.ToInt32(dataR["id"]));


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

            */

            for (int i=0; i<(updateRequestList.Count); i++)
            {
                int KITid = updateRequestList[i];

                if (updateRequestList_add_or_del[i] == "add")
                {
                    

                    //if (linkExist_in_bv_cat_tKit(kitId, KITid) == false)
                    if (linkExist_in_bv_cat_tKit(cat_id, KITid) == false)
                    {
                        //string request = tools.DatabaseQuery.addCompatibleKit(kitId, KITid);  //kitId correspond a id_cat, modifier + tard
                        //envoyer request
                        string request = tools.DatabaseQuery.addCompatibleKit(cat_id, KITid);

                        int res = tools.Database.setData(request);

                        if (res == -1)
                        {
                            MessageBox.Show("An error has occured");
                        }
                        else if (res == 1)
                        {
                            Console.WriteLine("Kit added");
                        }
                        else
                        {
                            MessageBox.Show("The database might be corrupted");
                        }
                    }

                }
                else if (updateRequestList_add_or_del[i] == "del")
                {
                    //if (linkExist_in_bv_cat_tKit(kitId, KITid) == true)
                    if (linkExist_in_bv_cat_tKit(cat_id, KITid) == true)
                    {
                        //string request = tools.DatabaseQuery.delCompatibleKit(kitId, KITid);  //kitId correspond a id_cat, modifier + tard
                        //envoyer request
                        string request = tools.DatabaseQuery.delCompatibleKit(cat_id, KITid);

                        int res = tools.Database.setData(request);

                        if (res == -1)
                        {
                            MessageBox.Show("An error has occured");
                        }
                        else if (res == 1)
                        {
                            Console.WriteLine("Kit removed");
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

        private void set_dg_selCompatibleKit_content()
        {
            //get item compatible id in dt2
            //string req = tools.DatabaseQuery.getCompatibleCatId_with_KitId(kitId);
            //string req = tools.DatabaseQuery.getCompatibleKitId_with_categoryId(kitId);
            string req = tools.DatabaseQuery.getKit_by_catalogBikeId(cat_id);

            DataTable dt = tools.Database.getData(req);

            //get all items to show them in select box
            //string req_it = tools.DatabaseQuery.getItem();
            string req_it = tools.DatabaseQuery.getKits();
            //string req_it = tools.DatabaseQuery.getKits_maxId(15);


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

                    //if (Convert.ToInt32(r["id"]) == Convert.ToInt32(dr["id_cat"]))
                    if (Convert.ToInt32(r["id"]) == Convert.ToInt32(dr["id_tKit"]))
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

            DataColumn newCol = new DataColumn();
            newCol.ColumnName = "cat";
            newCol.DataType = typeof(string);
            dt_item.Columns.Add(newCol);
            foreach (DataRow r in dt_item.Rows)
            {

                int g = Convert.ToInt32(r["category"]);
                switch (g)
                {
                    case 0:
                        r["cat"] = "Frame";
                        break;
                    case 1:
                        r["cat"] = "Wheels";
                        break;
                    case 2:
                        r["cat"] = "Brake";
                        break;
                    case 3:
                        r["cat"] = "Saddle";
                        break;
                    case 4:
                        r["cat"] = "Handlebar";
                        break;
                    case 5:
                        r["cat"] = "Addons";
                        break;
                }
            }


            //dg_tKitList.ItemsSource = dt_item.DefaultView;
            dg_selCompatibleKit.ItemsSource = dt_item.DefaultView;
            //lb_properties.ItemsSource = dt_item.DefaultView;
            //dg_tKitList.ItemsSource = dt_item.DefaultView;

        }

        

        private bool linkExist_in_bv_cat_tKit(int cat_id, int kit_id)
        {
            bool linkExist = false;
            //dt has all kit id linked with the gived cat_id
            string req = tools.DatabaseQuery.getKit_by_catalogBikeId(cat_id);
            DataTable dt = tools.Database.getData(req);

            foreach (DataRow r in dt.Rows)
            {
                if (Convert.ToInt32(r["id_tKit"]) == Convert.ToInt32(kit_id))
                {
                    linkExist = true;
                    break;
                }
            }

            return linkExist;
        }
      
        private void check_tutar_Checked(object sender, RoutedEventArgs e)
        {
            
            Console.WriteLine(sender.ToString());

            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.CheckBox)e.Source).DataContext;
            int KITid = Convert.ToInt32(dataRowView["id"]);

            Console.WriteLine(KITid);
            Console.WriteLine("Kit id : " + Convert.ToString(KITid));
            //Console.WriteLine("Cat id : " + Convert.ToString(kitId));
            Console.WriteLine("Cat id : " + Convert.ToString(cat_id));

            updateRequestList.Add(KITid);
            updateRequestList_add_or_del.Add("add");

            /*


            //if (linkExist_in_bv_cat_tKit(kitId, KITid) == false)
            if (linkExist_in_bv_cat_tKit(cat_id, KITid) == false)
            {
                //string request = tools.DatabaseQuery.addCompatibleKit(kitId, KITid);  //kitId correspond a id_cat, modifier + tard
                //envoyer request
                string request = tools.DatabaseQuery.addCompatibleKit(cat_id, KITid);

                int res = tools.Database.setData(request);

                if (res == -1)
                {
                    MessageBox.Show("An error has occured");
                }
                else if (res == 1)
                {
                    Console.WriteLine("Kit added");
                }
                else
                {
                    MessageBox.Show("The database might be corrupted");
                }
            }
            */
            

        }

        private void check_tutar_Unchecked(object sender, RoutedEventArgs e)
        {
            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.CheckBox)e.Source).DataContext;
            int KITid = Convert.ToInt32(dataRowView["id"]);

            Console.WriteLine("Kit id : " + Convert.ToString(KITid));
            //Console.WriteLine("Cat id : " + Convert.ToString(kitId));
            Console.WriteLine("Cat id : " + Convert.ToString(cat_id));

            updateRequestList.Add(KITid);
            updateRequestList_add_or_del.Add("del");


            /*
            //if (linkExist_in_bv_cat_tKit(kitId, KITid) == true)
            if (linkExist_in_bv_cat_tKit(cat_id, KITid) == true)
            {
                //string request = tools.DatabaseQuery.delCompatibleKit(kitId, KITid);  //kitId correspond a id_cat, modifier + tard
                                                                                      //envoyer request
                string request = tools.DatabaseQuery.delCompatibleKit(cat_id, KITid);

                int res = tools.Database.setData(request);

                if (res == -1)
                {
                    MessageBox.Show("An error has occured");
                }
                else if (res == 1)
                {
                    Console.WriteLine("Kit removed");
                }
                else
                {
                    MessageBox.Show("The database might be corrupted");
                }
            }
            */


           

        }
    }
}
