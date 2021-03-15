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

using BoVeloManager.Classes;

namespace BoVeloManager.Management.item
{
    /// <summary>
    /// Logique d'interaction pour modItemWindow.xaml
    /// </summary>
    public partial class modItemWindow : Window
    {
        private CatalogBike kt;
        public modItemWindow(CatalogBike kt_)
        {
            InitializeComponent();

            //initialize the windows
            kt = kt_;


            //diplay the item data
            tb_newItemName.Text = kt.getName();
            sl_pricemul.Value = kt.getPriceMul();
            lb_pricemul.Content = "Price " + (sl_pricemul.Value / 100).ToString("P");
        }

        private void BT_update_Click(object sender, RoutedEventArgs e)
        {
            
            string newItemName = tb_newItemName.Text;
            if ((newItemName.Length >= 2) && (newItemName != ""))
            {
                //string q = tools.DatabaseQuery.setItemName(itemId, newItemName);
                int price = (int)sl_pricemul.Value;
                //tools.Database.setData(q);
                updateItemName(newItemName,price);
            }
            else
            {
                tools.UI.MessageBox.Show("The new name is invalid", "Error");
            }
        }

        private void updateItemName(string name,int priceMul)
        {

            kt.setName(name);
            kt.setPriceMul(priceMul);

            tools.DatabaseClassInterface.updateCatalogBike(kt);

            this.Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            lb_pricemul.Content = "Price " + (sl_pricemul.Value / 100).ToString("P");
        }

        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
