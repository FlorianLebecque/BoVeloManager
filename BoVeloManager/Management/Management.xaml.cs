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

        private void update_dg_userList() {
            string q = tools.DatabaseQuery.getUsers();
            DataTable dt = tools.Database.getData(q);

            DataColumn newCol = new DataColumn();
            newCol.ColumnName = "Poste";
            newCol.DataType = typeof(string);
            
            dt.Columns.Add(newCol);

            foreach(DataRow r in dt.Rows) {
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

            dt.Columns.Remove(dt.Columns[1]);

            dg_userList.ItemsSource = dt.DefaultView;
        }

        private void bt_addUser_Click(object sender, RoutedEventArgs e) {
            AddUserWindow AUW = new AddUserWindow();
            AUW.ShowDialog();
            update_dg_userList();
        }
    }
}
