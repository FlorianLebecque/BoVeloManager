using System;
using System.Collections.Generic;
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
using BoVeloManager.Classes;

namespace BoVeloManager.UI.Catalogue.Confirmation
{
    /// <summary>
    /// Interaction logic for Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        DateTime Today = DateTime.Now;

        public Confirmation()
        {
            InitializeComponent();
            displayDescription();
            displayDetails();
        }

        #region display description
        private void displayDescription()
        {
            cb_client.ItemsSource = getClientInfo(Controler.Instance.getClientList());
            seller.Text = Controler.Instance.getCurrentUser().getName();
            sale_date.Text = Today.ToString();
        }
        private List<string> getClientInfo(List<Client> cList)
        {
            List<string> temp = new List<string>();
            foreach (Client c in cList)
            {
                string cInfo = c.getName();
                temp.Add(cInfo);
            }
            return temp;
        }
        private Client findClient(string c_name)
        {
            Client c = Controler.Instance.getClientList().Find(x => x.getName() == c_name);
            return c;
        }       

        private void cb_client_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            enterprise.Text = findClient(cb_client.SelectedItem.ToString()).getEtpName();
            Controler.Instance.tempSale.setClient(findClient(cb_client.SelectedItem.ToString()));
        }

        #endregion

        #region display details

        Dictionary<string, BikeBasket> Basket = Controler.Instance.tempSale.Basket;
        int tot  = 0;

        private void displayDetails() {
            bikesList.ItemsSource = null;
            bikesList.ItemsSource = Basket.Values;

            foreach(BikeBasket bc in Basket.Values) {
                tot += bc.price;
            }

            total.Text = ((float)tot/100).ToString("c2");
        }

        #endregion

        private void bt_save_Click(object sender, RoutedEventArgs e)
        {
            if (Controler.Instance.tempSale.RemoveStockKit())
            {
                Controler.Instance.tempSale.saveSale();
                BoVeloManager.Sales.Sales.Instance.init();
                stock.stock.Instance.init();
            }
            else
            {
                Dictionary<KitTemplate, int> MissingKits = Controler.Instance.tempSale.getMissingKits();
                // boite de dialogue : Il manque du matériel !! SHIT SHIT SHIT
                string alert = "Missing Kits : \n";
                foreach (KeyValuePair<KitTemplate, int> kvp in MissingKits)
                {
                    string L0 = kvp.Value.ToString() + " " + kvp.Key.getFullName() + "\n";
                    alert += L0;
                }
                tools.UI.MessageBox.Show(alert, "Alert");
            }
            this.Close();
        }

        private void bt_close_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e) {
            BikeBasket b = ((BikeBasket)((System.Windows.Controls.Button)e.Source).DataContext);
            Controler.Instance.tempSale.removeBikeBasket(b);

            tot = 0;
            displayDetails();
        }

    }
}
