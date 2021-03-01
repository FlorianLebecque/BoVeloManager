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
using BoVeloManager.Classes;

namespace BoVeloManager.Sales
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Sales : Page
    {

        Controler crtl;

        public Sales()
        {
            InitializeComponent();

            crtl = Controler.Instance;

            update_dg_salesList();
            update_dg_clientList();

        }

        private void update_dg_salesList()
        {
            dg_salesList.ItemsSource = crtl.GetClientDisplayInfo();
        }
        
        private void bt_showDescription_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)((System.Windows.Controls.Button)e.Source).DataContext;
            int saleID = Convert.ToInt32(dataRowView["id"]);

            description.Description DW = new description.Description(saleID);
            DW.Show();
        }

        private void update_dg_clientList()
        {
            dg_clientList.ItemsSource = crtl.GetClientDisplayInfo();
        }

        private void bt_addClient_Click(object sender, RoutedEventArgs e)
        {
            Client.AddClientWindow ACW = new Client.AddClientWindow();
            ACW.ShowDialog();

            update_dg_clientList();
        }

        private void bt_editClient_Click(object sender, RoutedEventArgs e){
            
        }

        private void bt_addSale_Click(object sender, RoutedEventArgs e)
        {
            addSales NewSale = new addSales();
            NewSale.ShowDialog();
        }
    }
}
