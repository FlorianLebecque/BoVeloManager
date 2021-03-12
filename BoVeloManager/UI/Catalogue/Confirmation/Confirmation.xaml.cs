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
        }

        #endregion

        #region display details

        Dictionary<BikeTemplate, int> basket = Controler.Instance.tempSale.getBasket();
        float tot;

        private void displayDetails()
        {
            // list of BikeItem for display
            List<BikeItem> bil = new List<BikeItem>();
            // get total price
            

            foreach (KeyValuePair<BikeTemplate, int> kvp in basket)
            {
                // create BikeItem 
                BikeTemplate tBike_ = kvp.Key;
                int qnt = kvp.Value;

                string qnt_name_ = qnt.ToString() + "x " + tBike_.getName();
                string string_kits_ = tBike_.getPropkitString();
                float p = tBike_.getPrice() * qnt / 100;
                string price_ = ((tBike_.getPrice()*qnt)/100).ToString() + "$";
                tot += p;

                bil.Add(new BikeItem() { tbike = tBike_ ,qnt_name = qnt_name_, string_kits = string_kits_, price = price_ });
            }

            bikesList.ItemsSource = bil;

            total.Text = tot.ToString() + "$";
        }

        private class BikeItem
        {
            public BikeTemplate tbike { get; set; }
            public string qnt_name { get; set; }
            public string string_kits { get; set; }
            public string price { get; set; }
        }

        #endregion

        private void bt_save_Click(object sender, RoutedEventArgs e)
        {

        }
        private void bt_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            BikeItem b = ((BikeItem)((System.Windows.Controls.Button)e.Source).DataContext);
            Controler.Instance.tempSale.getBasket().Remove(b.tbike);

            tot = 0;

            displayDetails();
        }
    }
}
