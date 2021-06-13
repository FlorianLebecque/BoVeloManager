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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BoVeloManager.Classes;

namespace BoVeloManager.stock {
    /// <summary>
    /// Interaction logic for stock.xaml
    /// </summary>
    public partial class stock : Page {

        private static stock instance = new stock();

        Controler crtl;

        private stock() {
            InitializeComponent();
            crtl = Controler.Instance;
            init();
        }

        public void init() {
            update_dg_kitTemplateList();
            update_dg_bikeList();
        }

        private void update_dg_bikeList() {
            List<Bike> bList = Controler.Instance.getBikesList().Where(b => ((b.getSaleId() == -1 )&&( b.getState() == 2 ))).ToList();
            Dictionary<string, disp> BikeStock = new Dictionary<string, disp>();

            foreach(Bike b in bList) {

                if (BikeStock.ContainsKey(b.getBikeTempl().Key)) {
                    BikeStock[b.getBikeTempl().Key].qnt++;
                } else {
                    disp bikedisplay = new disp();
                    bikedisplay.name = b.getBikeTempl().getDisplayInfo().fullname;
                    bikedisplay.qnt = 1;
                    BikeStock.Add(b.getBikeTempl().Key, bikedisplay);
                }

            }

            dg_bikeList.ItemsSource = null;
            dg_bikeList.ItemsSource = BikeStock.Values;
        }

        class disp {
            public string name { get; set; }
            public int qnt { get; set; }
        }

        private void update_dg_kitTemplateList()
        {
            List<KitTemplate> kitTemplateList = crtl.getKitTemplateList();

            List<KitTemplate.displayInfo> temp = new List<KitTemplate.displayInfo>();

            foreach (KitTemplate kt in kitTemplateList)
            {
                if(kt.getStockLocationX() == 0 || kt.getStockLocationY() == 0)
                {
                    int[] locations = crtl.getNewLocation();
                    int stockLocactionX = locations[0];
                    int stockLocactionY = locations[1];
                    kt.setLocations(stockLocactionX, stockLocactionY);
                }
                temp.Add(kt.GetDisplayInfo());
            }

            dg_kitTemplateList.ItemsSource = temp;
        }

        private void bt_Order_Click(object sender, RoutedEventArgs e)
        {

        }

        public static stock Instance
        {
            get
            {
                return instance;
            }
        }
    }
}
