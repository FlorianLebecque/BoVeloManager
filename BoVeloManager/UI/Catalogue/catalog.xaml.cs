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
using System.Drawing;

namespace BoVeloManager.Catalogue {
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Catalog : Page {

        CatalogBike catBike;
        KitTemplate size;
        KitTemplate color;        

        public Catalog() {
            InitializeComponent();
            DisplayCatalogue();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_tBike_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("PROGRAM BUILDING ...");
        }

        private void Add_Bike_Click(object sender, RoutedEventArgs e)
        {
            BikeCat b = ((BikeCat)((System.Windows.Controls.Button)e.Source).DataContext);
            
            //List<KitTemplate> temp = new List<KitTemplate>();

            string cb = b.name;
            string s = b.size;
            string c = b.color;
            int qnt = b.qnt;

            convertInformation(cb, s, c);

            Controler.Instance.tempSale.addItems(catBike, size, color, qnt);


            //int biketemplateId = Controler.Instance.getLastBikeTemplateId() + 1;

            //BikeTemplate temp = new BikeTemplate(biketemplateId, catBike);

            //Controler.Instance.setCurrentSale(int id_, int id_seller, int id_client, string state_, DateTime sale_date_, DateTime prevision_date_, List < Bike > bikeList_, List < User > userList, List < Client > clientList);

            MessageBox.Show("Item added to basket");
        }

        private void DisplayCatalogue()
        {
            List<BikeCat> BikeCatList = new List<BikeCat>();
            List<CatalogBike.displayInfo> catalogBikesDisplayInfoList = Controler.Instance.getCatalogBikeDisplayInfo();

            foreach (CatalogBike.displayInfo temp in catalogBikesDisplayInfoList)
            {
                #region TEST
                /*
                //string name_ = "hello";
                List<string> colorList_ = new List<string>();
                List<string> sizeList_ = new List<string>();

                colorList_.Add("rouge");
                colorList_.Add("vert");

                sizeList_.Add("XL");
                sizeList_.Add("XS");
                */
                #endregion 

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

            #region TEST
            /*
            List<string> Size = new List<string>();
            List<string> Color = new List<string>();

            Size.Add("ta mere");
            Color.Add("tchoin tchoin");

            BikeCatList.Add(new BikeCat() { name = "yo", colorList = Color, sizeList = Size, pic = System.Drawing.Image.FromFile("Bike0.jpg") });
            */
            #endregion

            CatalogListView.ItemsSource = BikeCatList;
        }

        private void convertInformation(string cat, string s, string c)
        {


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
