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
using System.Text.RegularExpressions;


namespace BoVeloManager.UI.Commande.description {
    /// <summary>
    /// Logique d'interaction pour AddCommand.xaml
    /// </summary>
    public partial class AddCommand : Window {

        DateTime Today = DateTime.Now;
        List<Commande_DisplayItem> cdi_list = new List<Commande_DisplayItem>();

        public AddCommand() {

            InitializeComponent();
            DisplayDescription();
        }



        #region display description

        private void DisplayDescription() {


            cb_supplier.ItemsSource = getSupllierInfo(Controler.Instance.getSupplierList());
            cb_addkit.ItemsSource = getKitInfo(Controler.Instance.getKitTemplateList());
            user.Text = Controler.Instance.getCurrentUser().getName();
            sale_date.Text = Today.ToString();
            BikeQuantity.Text = "1";
        }
        private List<string> getSupllierInfo(List<Supplier> sList) {
            List<string> temp = new List<string>();
            foreach (Supplier s in sList) {
                string cInfo = s.getName();
                temp.Add(cInfo);
            }
            return temp;
        }
        private List<string> getKitInfo(List<KitTemplate> kList) {
            List<string> temp = new List<string>();
            foreach (KitTemplate k in kList) {
                string kInfo =k.getName() +" "+ k.getProperties();
                temp.Add(kInfo);
            }
            return temp;
        }
        private Supplier findSupplier(string s_name) {
            Supplier s = Controler.Instance.getSupplierList().Find(x => x.getName() == s_name);
            return s;
        }
        private KitTemplate findkit(string k_name) {
            KitTemplate k = Controler.Instance.getKitTemplateList().Find(x => (x.getName()+" " +x.getProperties()) == k_name);
            return k;
        }

        private void cb_client_SelectionChanged(object sender, SelectionChangedEventArgs e) { 
            enterprise.Text = findSupplier(cb_supplier.SelectedItem.ToString()).getEtpName();
 
        }


        #endregion

        #region display details

        private class Commande_DisplayItem {
            public string KitName { get; set; }
            public string qnt { get; set; }
            public string price { get; set; }

            public Commande_DisplayItem(string kitname_, string qnt_, string price_) {
                KitName = kitname_;
                qnt = qnt_;
                price = price_ + "€";
            }

        }
        #endregion
        private void bt_add_Click(object sender, RoutedEventArgs e) {

            KitTemplate kt = findkit(cb_addkit.SelectedItem.ToString());
            cdi_list.Add(new Commande_DisplayItem(kt.getName().ToString(), BikeQuantity.Text, (kt.getPrice() * int.Parse(BikeQuantity.Text)).ToString()));

            kitList.ItemsSource = cdi_list;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void bt_save_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void bt_delete_Click(object sender, RoutedEventArgs e) {

        }
        private void bt_close_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }


    }
}
