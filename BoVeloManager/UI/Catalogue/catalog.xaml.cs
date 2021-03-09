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

namespace BoVeloManager.Catalogue {
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Catalog : Page {
        public Catalog() {
            InitializeComponent();

            LinkComboBox();
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
            MessageBox.Show("Bike not yet added");
        }

        private void LinkComboBox()
        {
            List<string> Size = new List<string>();
            List<string> Color = new List<string>();

            List<Classes.KitTemplate> KitList = Classes.Controler.Instance.getKitTemplateList();

            foreach (Classes.KitTemplate kit in KitList)
            {
                Classes.KitTemplate.displayInfo kit_struct = kit.GetDisplayInfo();
                string kit_cat = kit_struct.category;

                if (kit_cat == "Size")
                {
                    Size.Add(kit_struct.name);
                }
                else if (kit_cat == "Color")
                {
                    Color.Add(kit_struct.name);
                }
            }

            citySize.ItemsSource = Size;
            cityColor.ItemsSource = Color;

            exploraterSize.ItemsSource = Size;
            exploraterColor.ItemsSource = Color;

            allTerrainSize.ItemsSource = Size;
            allTerrainColor.ItemsSource = Color;

            newBikeSize.ItemsSource = Size;
            newBikeColor.ItemsSource = Color;
        }
    }
}
