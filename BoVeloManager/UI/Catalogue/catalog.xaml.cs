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

        static Catalog instance;

        private Catalog() {
            InitializeComponent();
            DisplayCatalogue();
        }

        public static Catalog Instance {
            get {
                return instance;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_Bike_Click(object sender, RoutedEventArgs e)
        {
            BikeCat b = ((BikeCat)((System.Windows.Controls.Button)e.Source).DataContext);

            string cb = b.name;
            string s = b.size;
            string c = b.color;
            int qnt = b.qnt;

            // Find Object - catch pas pris en compte car find met la valeur pas default si il ne trouve rien
            try
            {
                CatalogBike catBike = Controler.Instance.getCatalogBike().Find(x => x.getName() == cb);
                KitTemplate size = Controler.Instance.getKitTemplateList().Find(x => x.getName() == s);
                KitTemplate color = Controler.Instance.getKitTemplateList().Find(x => x.getName() == c);

                Console.WriteLine("Tout est ok");

                Controler.Instance.tempSale.addItems(catBike, size, color, qnt);
            }
            catch
            {
                Console.WriteLine("erreur");
                MessageBox.Show("Information missing");
                Add_Bike_Click(sender, e);
            }           

            //convertInformation(cb, s, c);

            
            MessageBox.Show("Item added to basket");
        }

        private void DisplayCatalogue()
        {
            List<BikeCat> BikeCatList = new List<BikeCat>();
            List<CatalogBike.displayInfo> catalogBikesDisplayInfoList = Controler.Instance.getCatalogBikeDisplayInfo();

            foreach (CatalogBike.displayInfo temp in catalogBikesDisplayInfoList)
            {                
                string name_ = temp.name;

                // Sorte kitTemplate
                List<KitTemplate> kitList = temp.kitTemplates;
                List<string> sizeList_ = new List<string>();
                List<string> colorList_ = new List<string>();

                foreach (KitTemplate kit in kitList)
                {
                    KitTemplate.displayInfo kit_struct = kit.GetDisplayInfo();
                    if (kit_struct.category == "Color")
                    {
                        colorList_.Add(kit_struct.name);
                    }
                    else if (kit_struct.category == "Size")
                    {
                        sizeList_.Add(kit_struct.name);
                    }
                }

                string pic_ = temp.pic;

                BikeCatList.Add(new BikeCat() { name = name_, colorList = colorList_, sizeList = sizeList_, pic = pic_});
            }

            CatalogListView.ItemsSource = BikeCatList;
        }

        // Inutile
        private void convertInformation(string cat, string s, string c)
        {
            //Client client = Controler.Instance.getClientList().Find(x => x.getName() == name);



            // retrouve le kit couleur et le kit taille parmis la liste de l'ensemble des kits
            foreach (KitTemplate kit in Controler.Instance.getKitTemplateList())
            {
                if (kit.getName() == s)
                {
                    size = kit;
                }
                if (kit.getName() == c)
                {
                    color = kit;
                }
            }

            // retrouve le catalogBike parmis la liste des catalogBike
            foreach (CatalogBike cbike in Controler.Instance.getCatalogBike())
            {
                if (cbike.getName() == cat)
                {
                    catBike = cbike;
                }
            }
        }

        private void bt_confirmation_Click(object sender, RoutedEventArgs e)
        {

            #region affichage console
            Console.WriteLine("###################");
            foreach (BikeTemplate tBike in Controler.Instance.GetBikeTemplateList())
            {
                Console.WriteLine("------------");
                Console.WriteLine("Name : " + tBike.getName());
                foreach (KitTemplate kit in tBike.getListKit())
                {
                    Console.WriteLine("kit : " + kit.getName());
                }
            }
            Console.WriteLine("###################");
            #endregion

            Confirmation CW = new Confirmation();
            CW.Show();  

        }

        public class BikeCat
        {
            public string pic { get; set; }
            public string name { get; set; }
            public List<string> sizeList { get; set; }
            public List<string> colorList { get; set; }
            public string size { get; set; }
            public string color { get; set; }
            public int qnt { get; set; }
        }

        
    }
}
