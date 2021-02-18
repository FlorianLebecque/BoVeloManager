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

namespace BoVeloManager.Sales
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Sales : Page
    {
        public Sales()
        {
            InitializeComponent();

            update_dg_salesList();
        }
        
        private void update_dg_salesList()
        {
            string q = tools.DatabaseQuery.getSales();
            DataTable dt = tools.Database.getData(q);

            dg_salesList.ItemsSource = dt.DefaultView;
        }
        private void bt_showDescription_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int saleID = Convert.ToInt32(dataRowView["id"]);

            description.Description DW = new description.Description(saleID);
            DW.Show();
        }

        private void bt_addSale_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
