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

namespace BoVeloManager.Sales.description
{
    /// <summary>
    /// Logique d'interaction pour Description.xaml
    /// </summary>
    public partial class Description : Window
    {
        private int id_sale;
        public Description(int _id_sale)
        {
            id_sale = _id_sale;

            InitializeComponent();
            DisplayDescription(id_sale);
            DisplayBikes(id_sale);

        }

        public void DisplayDescription(int _id_sale) {
                                                                                                                                                        
            //Import all sale data
            string sale_data = tools.DatabaseQuery.getSale_by_id(_id_sale);
            DataTable dt = tools.Database.getData(sale_data);
                                                                
            // Description Infos 
            seller.Text = (string)dt.Rows[0]["user"];
            client.Text = (string)dt.Rows[0]["Client"];
            enterprise.Text = (string)dt.Rows[0]["enterprise_name"];
            sale_date.Text = ((DateTime)dt.Rows[0]["date"]).ToString("yyyy-MM-dd");
        }
        public void DisplayBikes(int _id_sale)
        {
            // import all bike from sale
            string sale_bikes_data = tools.DatabaseQuery.gettBikes_by_sale(_id_sale);
            DataTable sale_bikes_table = tools.Database.getData(sale_bikes_data);

            //Create new bike list 
            List<BikeItem> bikes = new List<BikeItem>();

            float total_price = 0;


            foreach (DataRow tBike in sale_bikes_table.Rows)
            {

                //Quantity of the tBike
                int qnt = Convert.ToInt32(tBike["qnt"]);
                // id of the tBike
                int id_tbike = Convert.ToInt32(tBike["id_tbike"]);
                float price_mul = (float)Convert.ToDouble(tBike["PriceMul"]);
                string bike_name = (string)tBike["name"];

                // Find the name of the tBike
                string tBike_data = tools.DatabaseQuery.gettBike(id_tbike);
                DataTable tBike_table = tools.Database.getData(tBike_data);

                


                // all tBike tKits
                (string all_kits, float bike_price) = DisplayKits(id_tbike);


                float bikes_price = bike_price * ((float)qnt/100) * ((price_mul/100)+1);
                total_price = total_price + bikes_price;

                bikes.Add(new BikeItem() { number_name = qnt.ToString() + "x " + bike_name, kit = all_kits, price = bikes_price.ToString("c2") });

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
        public class BikeItem
        {
            public string number_name { get; set; }
            public string kit { get; set; }
            public string price { get; set; }
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
