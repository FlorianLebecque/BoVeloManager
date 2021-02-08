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
            if(MessageBox.Show("Are you sure ?", "User deletion",MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
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
    }
}
