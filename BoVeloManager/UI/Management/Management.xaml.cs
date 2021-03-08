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
using BoVeloManager.Classes;

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


        private void update_dg_userList() {
            //set the datatable as the items sources for the user datagrid

            dg_userList.ItemsSource = null;

            dg_userList.ItemsSource = Controler.Instance.GetUsersDisplayInfo(cb_sortUser.SelectedIndex); 
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
            Classes.User selectedUser = ((Classes.User.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).CurUser;
            //int userID = Convert.ToInt32(dataRowView["id"]);

            //open the dialog passing the user ID

            user.modUserWindow MUW = new user.modUserWindow(selectedUser);
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
            KitTemplate kt = ((KitTemplate.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).curKit;

            kit.modKitWindow MKW = new kit.modKitWindow(kt);
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

            KitTemplate kt = ((KitTemplate.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).curKit;

            kit.modCompatibleKitWindow MCKW = new kit.modCompatibleKitWindow(kt);

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
            dg_tKitList.ItemsSource = null;
            dg_tKitList.ItemsSource = Controler.Instance.getKitTemplateDisplayInfo();
        }


        #endregion

        #region Catalog

        private void bt_editCompKit_Click(object sender, RoutedEventArgs e)
        {
            //get witch row we clicked on
            CatalogBike cb = ((CatalogBike.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).CurCatBike;

            //item.modCompatibleItemWindow MCIW = new item.modCompatibleItemWindow(cb);

            //MCIW.ShowDialog();

            //update the kits datagrid
            update_dg_itemList();
        }


   
        private void bt_editItem_Click(object sender, RoutedEventArgs e) {
            //get witch row we clicked on
            CatalogBike kt = ((CatalogBike.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).CurCatBike;

            //open the dialog passing the item ID
            item.modItemWindow MUW = new item.modItemWindow(kt);
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

            dg_itemList.ItemsSource = null;

            dg_itemList.ItemsSource = Controler.Instance.getCatalogBikeDisplayInfo();

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



