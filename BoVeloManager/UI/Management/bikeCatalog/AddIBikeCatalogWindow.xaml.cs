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

namespace BoVeloManager.Management.item
{
    /// <summary>
    /// Logique d'interaction pour AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void BTAddItem_Click(object sender, RoutedEventArgs e)
        {
            string itemName = tb_itemName.Text;
            int err = 0;
            if ((itemName.Length >= 2) && (itemName != ""))
            {    //check if the itemName is 2 caracters min, and not empty
                addItem(itemName, (int)sl_pricemul.Value);
                this.Close();

            }
            else { err = 1; }
            //lb_error.Visibility = Visibility.Visible;
            switch (err)
            {
                case 1:
                    //lb_error.Text = "Item name invalid";
                    break;
            }
        }

        private void addItem(string name,int priceMul)
        {

            int id = Controler.Instance.getlastCatalogBikeId();

            CatalogBike cb = new CatalogBike(id,name, priceMul);
            Controler.Instance.createCatalogBike(cb);

            
        }

            private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            lb_pricemul.Content = "Price " + (sl_pricemul.Value/100).ToString("P");
        }
    }
}
