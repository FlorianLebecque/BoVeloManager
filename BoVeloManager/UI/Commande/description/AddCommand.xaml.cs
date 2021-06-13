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
        Classes.Commande cmd;
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
            public int intprice { get; set; }

            public Commande_DisplayItem(string kitname_, string qnt_, string price_, int intprice_) {
                KitName = kitname_;
                qnt = qnt_;
                price = price_;
                intprice = intprice_;
            }

        }
        #endregion
        private void bt_add_Click(object sender, RoutedEventArgs e) {

            KitTemplate kt = findkit(cb_addkit.SelectedItem.ToString());

            List<Commande_DisplayItem> check = cdi_list.Where(x => x.KitName == kt.getName().ToString() + " " + kt.getProperties().ToString()).ToList();

            if (check.Count() == 0) {
                    cdi_list.Add(new Commande_DisplayItem(
                        kt.getName().ToString() + " " + kt.getProperties().ToString(),
                        BikeQuantity.Text + "x",
                        ((float)(kt.getPrice() * int.Parse(BikeQuantity.Text)) / 100).ToString("c2"),
                        kt.getPrice() * int.Parse(BikeQuantity.Text)
                    )
                );
            }
            else {
                check[0].qnt = BikeQuantity.Text + "x";
                check[0].price = ((float)(kt.getPrice() * int.Parse(BikeQuantity.Text)) / 100).ToString("c2");
                check[0].intprice = kt.getPrice() * int.Parse(BikeQuantity.Text);

            }


            total.Text = (((float)totalCalcul()/100)).ToString("c2");
            kitList.ItemsSource = null;
            kitList.ItemsSource = cdi_list;
        }
        private int totalCalcul() {
            int total = 0;
            foreach (Commande_DisplayItem di in cdi_list) {
                total += di.intprice;
            }
            return total;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void bt_save_Click(object sender, RoutedEventArgs e) {
            List<Commande_item> Commande_itemList = new List<Commande_item>();
            foreach (Commande_DisplayItem de in cdi_list) {
                KitTemplate kt = findkit(de.KitName);
                int qnt = int.Parse(de.qnt.Replace("x",""));
                Commande_itemList.Add(new Commande_item(kt, qnt));
            }

            int id = Controler.Instance.getlastCommandeId() + 1;
            Supplier sup = findSupplier(cb_supplier.SelectedItem.ToString());
            User user = Controler.Instance.getCurrentUser();

            Classes.Commande cmd = new Classes.Commande(id, user.getId(), sup.getId(), "Open", Today, DateTime.Now.AddDays(Properties.Settings.Default.Cmd_delay), Commande_itemList, Controler.Instance.getUserList(), Controler.Instance.getSupplierList());

                Controler.Instance.Addcommande(cmd);
            this.Close();
        }
        private void bt_delete_Click(object sender, RoutedEventArgs e) {

        }
        private void bt_close_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
