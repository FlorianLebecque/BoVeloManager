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
using System.Windows.Shapes;
using BoVeloManager.Classes;

namespace BoVeloManager.UI.Planning
{
    /// <summary>
    /// Interaction logic for AddBike.xaml
    /// </summary>
    public partial class AddBike : Window
    {
        public AddBike()
        {
            InitializeComponent();
            BindComboBox();
        }

        private void BTLogin_Click(object sender, RoutedEventArgs e)
        {
           // Bike newBike = new Bike();
            //Controler.createBike(newBike);

        }

        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BindComboBox()
        {
            List<string> Size = new List<string>();
            List<string> Color = new List<string>();

            List<KitTemplate> KitList = Controler.Instance.getKitTemplateList();

            foreach (KitTemplate kit in KitList)
            {
                KitTemplate.displayInfo kit_struct = kit.GetDisplayInfo();
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

            BikeSize.ItemsSource = Size;
            BikeColor.ItemsSource = Color;

        }

    }
}
