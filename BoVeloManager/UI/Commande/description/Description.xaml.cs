using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BoVeloManager.Classes;
using BoVeloManager.tools;

namespace BoVeloManager.UI.Commande.description {
    /// <summary>
    /// Logique d'interaction pour Description.xaml
    /// </summary>
    public partial class Description : Window {
        private Classes.Commande cmd;

        List<Commande_DisplayItem> cdi_list = new List<Commande_DisplayItem>();

        public Description(Classes.Commande commande_) {
            cmd = commande_;

            InitializeComponent();
            DisplayDescription();
            DisplayKits();
        }

        public void DisplayDescription() {

            // Description Infos 
            seller.Text = cmd.getSeller().getName();
            user.Text = cmd.getClient().getName();
            enterprise.Text = cmd.getClient().getEtpName();
            sale_date.Text = cmd.GetDisplayInfo().sale_date;
            status.Text = cmd.GetDisplayInfo().state;
        }

        

        public void DisplayKits() {
            foreach (Commande_item Ci in cmd.getCommandItemList()) {
                cdi_list.Add(new Commande_DisplayItem(Ci.kt.getName().ToString(), Ci.qnt.ToString(), (Ci.kt.getPrice() * Ci.qnt).ToString()));
            }
            kitList.ItemsSource = cdi_list;

        }

        private class Commande_DisplayItem {
            public string KitName { get; set; }
            public string qnt { get; set; }
            public string price { get; set; }

            public Commande_DisplayItem(string kitname_,string qnt_,string price_) {
                KitName = kitname_;
                qnt = qnt_;
                price = price_ + "€";
            }

        }

        private void BT_close_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

    }
}
