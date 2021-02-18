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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoVeloManager.Management {
    /// <summary>
    /// Interaction logic for Management.xaml
    /// </summary>
    public partial class Management : Page {
        public Management() {
            InitializeComponent();

            update_dg_userList();
            update_dg_kitList();
            update_dg_itemList();
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
            string q = tools.DatabaseQuery.getUsers();
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
                        r["Poste"] = "Monteur";
                        break;
                    case 1:
                        r["Poste"] = "Vendeur";
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


        #endregion

        #region Kit
        private void bt_addKit_Click(object sender, RoutedEventArgs e)
        {
            //open the dialog
            kit.AddKitWindow AKW = new kit.AddKitWindow();
            AKW.ShowDialog();

            //update the kits datagrid
            update_dg_kitList();
        }
        private void bt_editKit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BUILDING PROGRAM ...");
        }



        private void update_dg_kitList() {
            //get the data from the db
            string q = tools.DatabaseQuery.getKits();
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



            //set the datatable as the items sources for the user datagrid
            dg_tKitList.ItemsSource = dt.DefaultView;
        }

        #endregion

        #region Item
        private void bt_editItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BUILDING PROGRAM ...");
            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int itemID = Convert.ToInt32(dataRowView["id"]);

            //open the dialog passing the item ID
            item.modItemWindow MUW = new item.modItemWindow(itemID);
            MUW.ShowDialog();

            //update the item datagrid
            update_dg_itemList();

        }

        private void bt_showKits_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BUILDING PROGRAM ...");

            //get witch row we clicked on
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int itemID = Convert.ToInt32(dataRowView["id"]);

            //open the dialog passing the item ID
            item.showAssociatedKitsWithItemWindow SAKWIW = new item.showAssociatedKitsWithItemWindow(itemID);
            SAKWIW.ShowDialog();


            //update the item datagrid
            update_dg_itemList();
        }




        private void bt_delItem_Click(object sender, RoutedEventArgs e)
        {
            //Item delete test
            if (MessageBox.Show("Are you sure ?", "Item deletion", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
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
        private void bt_addItem_Click(object sender, RoutedEventArgs e)
        {
            item.AddItemWindow AIW = new item.AddItemWindow();
            AIW.ShowDialog();

            update_dg_itemList();
            MessageBox.Show("BUILDING PROGRAM ...");
        }
        
        

        /*
            Function witch loads the items into the TabItem datagrid
                - get items data from database
                
                - put the users data into the datagrid
         */
        private void update_dg_itemList()
        {
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
    }
}
//#endregion

