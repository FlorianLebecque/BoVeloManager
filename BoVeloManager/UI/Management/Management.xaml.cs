using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoVeloManager.Management {
    /// <summary>
    /// Interaction logic for Management.xaml
    /// </summary>
    public partial class Management : Page {

        bool init = false;

        public Management() {
            InitializeComponent();
            init = true;

            update_dg_userList();
            update_dg_itemList();

            set_cbtype3_content();
            cb_type3.SelectedIndex = 0;
            update_dg_kitList();
        }

        /*
         *      Code regions for the user
         */
        #region Users

        /*
            Function witch loads the users into the user datagrid
                - get users data from database
                - convert grade into poste
                    ( 0 -> Monteur)
                    ( 1 -> Vendeur) ...
                - put the users data into the datagrid
         */
        private void update_dg_userList() {
            //get the data from the db
            string q = tools.DatabaseQuery.getUsers(cb_sortUser.SelectedIndex);
            DataTable dt = tools.Database.getData(q);


            //convertion de la columns grade en poste
            DataColumn newCol = new DataColumn();
            newCol.ColumnName = "Poste";
            newCol.DataType = typeof(string);

            dt.Columns.Add(newCol);

            foreach (DataRow r in dt.Rows) {

                int g = Convert.ToInt32(r["grade"]);
                switch (g) {
                    case 0:
                        r["Poste"] = "Worker";
                        break;
                    case 1:
                        r["Poste"] = "Seller";
                        break;
                    case 2:
                        r["Poste"] = "Manager";
                        break;
                }

            }
            //we can now remove the old columns
            dt.Columns.Remove(dt.Columns["grade"]);


            //set the datatable as the items sources for the user datagrid
            dg_userList.ItemsSource = dt.DefaultView;
        }

        /*
            Function witch is trigger when the addUser btn is clicked
                - we open the adduserwindows
                - we update the user datagrid
         */
        private void bt_addUser_Click(object sender, RoutedEventArgs e) {
            //open the dialog
            AddUserWindow AUW = new AddUserWindow();
            AUW.ShowDialog();

            //update the user datagrid
            update_dg_userList();
        }


        /*
            Function witch open the edituser dialog when the edit button is click
                - Get witch user we clicked for
                - Open the dialog
                - Update the user datagrid
         */
        private void bt_editUser_Click(object sender, RoutedEventArgs e) {

            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int userID = Convert.ToInt32(dataRowView["id"]);

            //open the dialog passing the user ID
            user.modUserWindow MUW = new user.modUserWindow(userID);
            MUW.ShowDialog();

            //update the user datagrid
            update_dg_userList();

        }

        /*
            Function trigger when the delete btn is click
                - Get wich user we clicked for
                - send the query to the database
                - update the list
         */
        private void bt_delUser_Click(object sender, RoutedEventArgs e) {

            //User delete test
            if (MessageBox.Show("Are you sure ?", "User deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                //retrieve the row we click
                DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                int userID = Convert.ToInt32(dataRowView["id"]);

                //create and send the request to the db
                string q = tools.DatabaseQuery.delUser(userID);
                tools.Database.setData(q);

                //Update the list
                MessageBox.Show("User deleted");
                update_dg_userList();
            }

        }

        private void cb_sortUser_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //ICollectionView cv = CollectionViewSource.GetDefaultView(dg_userList.ItemsSource);
            if (init) {
                update_dg_userList();
            }
        }


        #endregion

        #region Kit
        private void bt_addKit_Click(object sender, RoutedEventArgs e) {
            //open the dialog
            kit.AddKitWindow AKW = new kit.AddKitWindow();
            AKW.ShowDialog();

            //update the kits datagrid
            update_dg_kitList();
        }
        private void bt_editKit_Click(object sender, RoutedEventArgs e) {
            
            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int id = Convert.ToInt32(dataRowView["id"]);

            kit.modKitWindow MKW = new kit.modKitWindow(id);
            MKW.ShowDialog();

            //update the kits datagrid
            update_dg_kitList();
        }

        private void btn_delKit_Click(object sender, RoutedEventArgs e) {
            //Kit delete test
            if (MessageBox.Show("Are you sure ?", "Kit deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                //retrieve the row we click
                DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                int kitID = Convert.ToInt32(dataRowView["id"]);

                //create and send the request to the db
                string q = tools.DatabaseQuery.delKit(kitID);
                tools.Database.setData(q);

                //Update the list
                MessageBox.Show("Kit deleted");
                update_dg_kitList();
            }
        }

        private void bt_editCompatibleKit_Click(object sender, RoutedEventArgs e) {

            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int id = Convert.ToInt32(dataRowView["id"]);

            kit.modCompatibleKitWindow MCKW = new kit.modCompatibleKitWindow(id);

            MCKW.ShowDialog();

            //update the kits datagrid
            update_dg_kitList();
        }
        private void cb_type3_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            update_dg_kitList();
        }

        /*
        private void update_dg_kitList_bis() {

        }


            int cat = cb_type2.SelectedIndex;
            string q;
            //get the data from the db
            if (cat == 0)
            {
                q = tools.DatabaseQuery.getKits();
            }
            else
            {
                q = tools.DatabaseQuery.getKit_by_category(cat);
            }
            DataTable dt = tools.Database.getData(q);

            

            //convertion de la columns grade en poste
            DataColumn newCol = new DataColumn();
            newCol.ColumnName = "cat";
            newCol.DataType = typeof(string);
            dt.Columns.Add(newCol);
            foreach (DataRow r in dt.Rows) {

                int g = Convert.ToInt32(r["category"]);
                switch (g) {
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
            //we can now remove the old columns
            dt.Columns.Remove(dt.Columns["category"]);

            //set the datatable dt as the items sources for the user datagrid
            dg_tKitList.ItemsSource = dt.DefaultView;

        }
        */

        private void set_cbtype3_content() {
            //set the datatable cb_t as the item sources for the combobox content
            string q_cb = tools.DatabaseQuery.getItem();
            DataTable cb_t = tools.Database.getData(q_cb);
            cb_type3.ItemsSource = cb_t.DefaultView;
            //Add 'show all' row to cb_t
            DataRow newRow = cb_t.NewRow();
            cb_t.Rows.InsertAt(newRow, 0);
            cb_t.Rows[0]["name"] = "Show all";
        }


        private void update_dg_kitList() {
            int item = cb_type3.SelectedIndex;

            string q;
            DataTable dt;
            //get the data from the db
            if (item <= 0) {
                q = tools.DatabaseQuery.getKits();
                dt = tools.Database.getData(q);
                Console.WriteLine(q);
                Console.WriteLine(item);
            } else {
                //item selectionned
                string q_cb = tools.DatabaseQuery.getItem();
                DataTable cb_t = tools.Database.getData(q_cb);
                item -= 1;


                //dt2 = list of associated kit id with the selectionned item
                string req = tools.DatabaseQuery.getCompatibleKitId_with_categoryId(Convert.ToInt32(cb_t.Rows[item]["id"]));
                DataTable dt2 = tools.Database.getData(req);

                //dt created table from the associated id in dt2
                dt = new DataTable();
                
                DataColumn id_Col = new DataColumn();
                id_Col.ColumnName = "id";
                id_Col.DataType = typeof(int);
                dt.Columns.Add(id_Col);

                DataColumn name_Col = new DataColumn();
                name_Col.ColumnName = "name";
                name_Col.DataType = typeof(string);
                dt.Columns.Add(name_Col);

                DataColumn properties_Col = new DataColumn();
                properties_Col.ColumnName = "properties";
                properties_Col.DataType = typeof(string);
                dt.Columns.Add(properties_Col);

                DataColumn category_Col = new DataColumn();
                category_Col.ColumnName = "category";
                category_Col.DataType = typeof(string);
                dt.Columns.Add(category_Col);
                
                //add every compatible kit to dt
                foreach (DataRow r in dt2.Rows) {
                    string q_k = tools.DatabaseQuery.getKit_by_id(Convert.ToInt32(r["id_tKit"]));
                    DataTable n_q_k = tools.Database.getData(q_k);
                    DataRow newRow = dt.NewRow();
                    newRow["id"] = n_q_k.Rows[0]["id"];
                    newRow["name"] = n_q_k.Rows[0]["name"];
                    newRow["properties"] = n_q_k.Rows[0]["properties"];
                    newRow["category"] = n_q_k.Rows[0]["category"];

                    dt.Rows.InsertAt(newRow, 0);

                }
            }

            //convertion de la columns grade en poste
            DataColumn newCol = new DataColumn();
            newCol.ColumnName = "cat";
            newCol.DataType = typeof(string);
            dt.Columns.Add(newCol);
            foreach (DataRow r in dt.Rows) {

                int g = Convert.ToInt32(r["category"]);
                switch (g) {
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

            //set the datatable dt as the items sources for the user datagrid
            dg_tKitList.ItemsSource = dt.DefaultView;
        }


        #endregion


        #region Items

        private void bt_editCompKit_Click(object sender, RoutedEventArgs e)
        {
            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int id = Convert.ToInt32(dataRowView["id"]);

            //kit.modCompatibleKitWindow MCKW = new kit.modCompatibleKitWindow(id);
            item.modCompatibleItemWindow MCIW = new item.modCompatibleItemWindow(id);

            MCIW.ShowDialog();

            //update the kits datagrid
            update_dg_itemList();
        }


   
        private void bt_editItem_Click(object sender, RoutedEventArgs e) {
            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int itemID = Convert.ToInt32(dataRowView["id"]);

            //open the dialog passing the item ID
            item.modItemWindow MUW = new item.modItemWindow(itemID);
            MUW.ShowDialog();

            //update the item datagrid
            update_dg_itemList();
        }


        private void bt_delItem_Click(object sender, RoutedEventArgs e) {
            //Item delete test
            if (MessageBox.Show("Are you sure ?", "Item deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                //retrieve the row we click
                DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
                int itemID = Convert.ToInt32(dataRowView["id"]);

                //create and send the request to the db
                string q = tools.DatabaseQuery.delItem(itemID);
                tools.Database.setData(q);

                //Update the list
                MessageBox.Show("Item deleted");
                update_dg_itemList();
            }
        }

        private void bt_addItem_Click(object sender, RoutedEventArgs e) {
 
            item.AddItemWindow AIW = new item.AddItemWindow();

            AIW.ShowDialog();

            //update the kits datagrid
            update_dg_itemList();

        }

        /*
            Function witch loads the items into the TabItem datagrid
                - get items data from database

                - put the users data into the datagrid
            */
        private void update_dg_itemList() {
            //get the data from the db
            string q = tools.DatabaseQuery.getItem();
            DataTable dt = tools.Database.getData(q);


            //convertion de la columns grade en poste
            DataColumn newCol = new DataColumn();
            newCol.ColumnName = "Name";
            newCol.DataType = typeof(string);

            dt.Columns.Add(newCol);

            

            //set the datatable as the items sources for the user datagrid
            dg_itemList.ItemsSource = dt.DefaultView;
        }



        #endregion

        private void TabablzControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void check_tutar_Checked(object sender, RoutedEventArgs e)
        {
          

        }
    }
} 



