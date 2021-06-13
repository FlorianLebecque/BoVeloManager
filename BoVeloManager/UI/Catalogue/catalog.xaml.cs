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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using BoVeloManager.Classes;
using BoVeloManager.UI.Catalogue.Confirmation;
using System.Drawing;


namespace BoVeloManager.Catalogue {
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Catalog : Page {

        CatalogBike catBike;
        KitTemplate size;
        KitTemplate color;

        static Catalog instance = new Catalog();
        
        private Catalog() {
            InitializeComponent();
            init();
        }

        public void init() {
            DisplayCatalogue();
        }

        public static Catalog Instance {
            get {
                return instance;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e){
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_Bike_Click(object sender, RoutedEventArgs e){
            BikeBasket b = ((BikeBasket)((System.Windows.Controls.Button)e.Source).DataContext);
            if (b.qnt == 1)
            {
                tools.UI.MessageBox.Show("Bike added to basket", "Bike Added");
                Controler.Instance.tempSale.setBikeBasket(b);
            }
            else if (b.qnt == 0)
            {
                tools.UI.MessageBox.Show("Bike quantity is 0", "Error");
            }
            else
            {
                tools.UI.MessageBox.Show("Bikes added to basket", "Bikes Added");
                Controler.Instance.tempSale.setBikeBasket(b);
            }            
        }

        private void DisplayCatalogue(){
            List<BikeBasket> BikeCatList = new List<BikeBasket>();
            List<CatalogBike.displayInfo> catalogBikesDisplayInfoList = Controler.Instance.getCatalogBikeDisplayInfo();

            foreach (CatalogBike.displayInfo temp in catalogBikesDisplayInfoList){                

                List<string> sizeList = new List<string>();
                List<string> colorList = new List<string>();

                // Pour chaque kittemplate de categorie 0
                foreach(KitTemplate kt in temp.kitTemplates.Where(x => x.getCategory() == KitCategory.frame).ToList()) {
                    string[] prop = kt.getProperties().Split('|');
                    if (!sizeList.Contains(prop[0])) {
                        sizeList.Add(prop[0]);
                    }
                    if (!colorList.Contains(prop[1])) {
                        colorList.Add(prop[1]);
                    } 
                    
                }
                BikeBasket bb = new BikeBasket(temp.name, sizeList, colorList, temp.CurCatBike);
                bb.pic = temp.pic;
                BikeCatList.Add(bb);
                
            }

            CatalogListView.ItemsSource = BikeCatList;
        }

        private void bt_confirmation_Click(object sender, RoutedEventArgs e) {
            Confirmation CW = new Confirmation();
            CW.Show();  
        }

    }
}
