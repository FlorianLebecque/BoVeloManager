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
            DisplayTBikes(sale);

        }

        public void DisplayDescription(Sale sale) {
                                                  
            // Description Infos 
            seller.Text = sale.getSeller().getName();
            client.Text = sale.getClient().getName();
            enterprise.Text = sale.getClient().getEtpName();
            sale_date.Text = sale.GetSaleDisplayInfo().sale_date;
        }
        public void DisplayTBikes(Sale sale)
        {
            //Create new bike list 
            List<BikeItem> bikesListItems = new List<BikeItem>();

            var TbikeDesc = sale.GetSaleDescrInfo();
            foreach (Sale.TbikeInfo tBike in TbikeDesc.TbikeInfoList)
            {

                //Quantity of the tBike
                float qnt = (float)tBike.qnt;
                string bike_name = tBike.CurTempl.getName();
                List<KitTemplate> kitList = tBike.CurTempl.getListKit();

                // all tBike tKits
                DisplayKits(kitList);

                string string_kits_ = tBike.CurTempl.getPropkitString();
                float Tbike_price = tBike.CurTempl.getPrice();

                string qnt_name_ = qnt.ToString() + "x " + bike_name;
                string price_ = ((Tbike_price*qnt)/100).ToString("c2");
                bikesListItems.Add(new BikeItem() { qnt_name = qnt_name_ , string_kits = string_kits_, price = price_ });
            }

            bikesList.ItemsSource = bikesListItems;
            total.Text = (sale.getTotalPrice()/100).ToString("c2");
        }
        public void DisplayKits(List<KitTemplate> kitList)
        {
            foreach (KitTemplate kit in kitList)
            {
                var kitDisp = kit.GetDisplayInfo();
                string kit_name = kitDisp.name;
                string kit_prop = kitDisp.properties;
            }
        }

        public class BikeItem {
            public string qnt_name { get; set; }
            public string string_kits { get; set; }
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
