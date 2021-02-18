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
    /// Logique d'interaction pour showAssociatedKitsWithItemWindow.xaml
    /// </summary>
    public partial class showAssociatedKitsWithItemWindow : Window
    {
        private int itemId;
        public showAssociatedKitsWithItemWindow(int itemId_)
        {
            InitializeComponent();

            //initialize the windows
            itemId = itemId_;
            //get the item data
            string q = tools.DatabaseQuery.getItem_by_id(itemId);
            DataTable res = tools.Database.getData(q);

            //diplay the kit category
            tb_kitCategory.Text = (string)res.Rows[0]["name"];
            

            update_dg_associatedKitsList();

        }

        private void bt_addAssociatedKit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BUILDING PROGRAM ...");

            //open the dialog
            kit.AddKitWindow AKW = new kit.AddKitWindow();
            AKW.ShowDialog();

            //update the kits datagrid
            update_dg_associatedKitsList();

        }

        private void btnEditAssociatedKit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BUILDING PROGRAM ...");
        }

        private void btnDeleteAssociatedKit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BUILDING PROGRAM ...");
        }
        private void update_dg_associatedKitsList()
        {
            //get the data from the db
            string q = tools.DatabaseQuery.getKits();
            //string q = tools.DatabaseQuery.getKit_by_category(0);
            DataTable dt = tools.Database.getData(q);

            //convertion de la columns grade en poste
            DataColumn newCol = new DataColumn();
            newCol.ColumnName = "Name";
            newCol.DataType = typeof(string);

            dt.Columns.Add(newCol);



            //set the datatable as the items sources for the user datagrid
            dg_associatedKitsList.ItemsSource = dt.DefaultView;
        }
    }
}
