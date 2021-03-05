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

                //Quantity of the tBike
                int qnt = 1;
                // id of the tBike
                int id_tbike = 1;
                float price_mul = 1;
                string bike_name = "1";




                // all tBike tKits
                (string all_kits, float bike_price) = DisplayKits(id_tbike);


                float bikes_price = bike_price * ((float)qnt/100) * ((price_mul/100)+1);
                total_price = total_price + bikes_price;

                //bikes.Add(new BikeItem() { number_name = qnt.ToString() + "x " + bike_name, kit = all_kits, price = bikes_price.ToString("c2") });

                bikesList.ItemsSource = bikes;
            }

            total.Text = total_price.ToString("c2");
        }
        public (string , int) DisplayKits(int _id_tbike)
        {

            int kits_price = 0;
            string all_kits = "";
            string tKit_data = tools.DatabaseQuery.gettKit(_id_tbike);
            DataTable tKit_table = tools.Database.getData(tKit_data);

            foreach (DataRow tKit in tKit_table.Rows)
            {
                kits_price += Convert.ToInt32(tKit["price"]);
                string kit_name = tKit["name"].ToString();
                string kit_prop = tKit["properties"].ToString();

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
