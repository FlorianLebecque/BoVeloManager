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

namespace BoVeloManager.Sales.description
{
    /// <summary>
    /// Logique d'interaction pour Description.xaml
    /// </summary>
    public partial class Description : Window
    {
        private Sale sale;
        public Description(Sale sale_)
        {
            sale = sale_;

            InitializeComponent();
            DisplayDescription(sale);
            DisplayBikes(sale);

        }

        public void DisplayDescription(Sale sale) {
                                                  
            // Description Infos 
            seller.Text = sale.getSeller().getName();
            client.Text = sale.getClient().getName();
            enterprise.Text = sale.getClient().getEtpName();
            sale_date.Text = sale.GetSaleDisplayInfo().sale_date;
        }
        public void DisplayBikes(Sale sale)
        {


            //Create new bike list 
            List<Bike> bikes = sale.getBikeList();

            float total_price = 0;


            foreach (Bike bike in bikes)
            {
                var bikeDesc = bike.GetDisplayInfo();

                //Quantity of the tBike
                int qnt = 1;
                // id of the tBike
                int id_tbike = bikeDesc.id;
                float price_mul = bikeDesc.priceMul;
                string bike_name = bikeDesc.name;
                List<KitTemplate> kitList = bike.getBikeTempl().getListKit();

                // all tBike tKits
                (string all_kits, float bike_price) = DisplayKits(kitList);

                float bikes_price = bike_price * ((float)qnt/100) * ((price_mul/100)+1);
                total_price = total_price + bikes_price;

                string number_name = qnt.ToString() + "x " + bike_name;
                string kit = all_kits;
                string price = bikes_price.ToString("c2");

                bikesList.ItemsSource = number_name;
                bikesList.ItemsSource = kit;
                bikesList.ItemsSource = price;
            }

            total.Text = total_price.ToString("c2");
        }
        public (string , int) DisplayKits(List<KitTemplate> kitList)
        {

            int kits_price = 0;
            string all_kits = "";

            foreach (KitTemplate kit in kitList)
            {
                var kitDisp = kit.GetDisplayInfo();
                kits_price += kitDisp.priceInt;
                string kit_name = kitDisp.name;
                string kit_prop = kitDisp.properties;

                if (kit_prop.Length == 0) {
                    all_kits = all_kits + "● " + kit_name +"\n";
                }
                else {
                    all_kits = all_kits + "● " + kit_name + " [" + kit_prop + "] \n";
                } 
            }
            return (all_kits, kits_price);
        }


        private void BT_export_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BT_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
