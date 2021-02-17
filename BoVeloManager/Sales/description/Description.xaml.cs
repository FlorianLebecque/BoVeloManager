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

            string sale_data = tools.DatabaseQuery.getSale_by_id(id_sale);
            DataTable dt = tools.Database.getData(sale_data);

            // Description Infos 
            seller.Text = (string)dt.Rows[0]["user"];
            client.Text = (string)dt.Rows[0]["Client"];
            enterprise.Text = (string)dt.Rows[0]["enterprise_name"];
            sale_date.Text = dt.Rows[0]["date"].ToString();
            int total_price = 0;


            string sale_bikes_data = tools.DatabaseQuery.gettBikes_by_sale(id_sale);
            DataTable sale_bikes_table = tools.Database.getData(sale_bikes_data);

            List<BikeItem> bikes = new List<BikeItem>();
            foreach (DataRow tBike in sale_bikes_table.Rows) {

                //Quantity of the tBike
                int qnt = Convert.ToInt32(tBike["qnt"]);
                // id of the tBike
                int id_tbike = Convert.ToInt32(tBike["id_tbike"]);

                // Find the name of the tBike
                string tBike_data = tools.DatabaseQuery.gettBike(id_tbike);
                DataTable tBike_table = tools.Database.getData(tBike_data);
                string bike_name = (string)tBike_table.Rows[0]["name"];
                int bike_price = Convert.ToInt32(tBike_table.Rows[0]["price"]);

                // all tBike tKits
                string all_kits = "";
                string tKit_data = tools.DatabaseQuery.gettKit(id_tbike);
                DataTable tKit_table = tools.Database.getData(tKit_data);
                foreach (DataRow tKit in tKit_table.Rows)
                {
                    string kit_name = tKit["name"].ToString();
                    string kit_prop = tKit["properties"].ToString();
                    all_kits = all_kits + "● " + kit_name + " (" + kit_prop + ") \n";

                }

                int bikes_price = bike_price * qnt;
                total_price = total_price + bikes_price;

                bikes.Add(new BikeItem() { number_name = qnt.ToString()+"x "+ bike_name, kit = all_kits, price = bikes_price });

                bikesList.ItemsSource = bikes;
            }

            total.Text = total_price.ToString();
        }
        public class BikeItem
        {
            public string number_name { get; set; }
            public string kit { get; set; }
            public int price { get; set; }
        }
    }
}
