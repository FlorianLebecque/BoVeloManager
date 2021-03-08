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
            List<string> temp = new List<string>();
            temp.Add("Item 1");
            temp.Add("Item 2");
            temp.Add("Item 3");

            citySize.ItemsSource = temp;
            cityColor.ItemsSource = temp;

            exploraterSize.ItemsSource = temp;
            exploraterColor.ItemsSource = temp;

            allTerrainSize.ItemsSource = temp;
            allTerrainColor.ItemsSource = temp;

            newBikeSize.ItemsSource = temp;
            newBikeColor.ItemsSource = temp;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
