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
            
            BindComboBoxCat();
        }

        private void BTLogin_Click(object sender, RoutedEventArgs e)
        {
            int indexCatalog = BikeCatalog.SelectedIndex;
            int Quantity = Convert.ToInt32(BikeQuantity.Text);
            //int id = Controler.Instance.getLastBikeTemplate() + 1;
            //Bike(int id_,int status_, int id_sale_,int Poste_, BikeTemplate bt_, DateTime planned_date_, DateTime constr_date_)
            //Bike b = new Bike(id, kitName, kitCat, kitPrice, kitProp);
            //addBike(kt);
            this.Close();


        }

        private void addBike(Bike bike)
        {
            try
            {
                Controler.Instance.createBike(bike);
            }
            catch
            {
                MessageBox.Show("An error has occured");
            }
        }



        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BindComboBox()
        {
            int indexCatalog = BikeCatalog.SelectedIndex;
            int idCatalogue = Controler.Instance.getCatalogBike()[indexCatalog].getId();
            List<string> Size = new List<string>();
            List<string> Color = new List<string>();

            //List<KitTemplate> KitList = Controler.Instance.getKitTemplateList();
            List<KitTemplate> KitList = Controler.Instance.getCatalogBike()[indexCatalog].getKitTemplateList();

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

        private void BindComboBoxCat()
        {
            List<string> Cat = new List<string>();

            List<CatalogBike> CatList = Controler.Instance.getCatalogBike();

            foreach (CatalogBike c in CatList)
            {
                Cat.Add(c.getName());
            }

            BikeCatalog.ItemsSource = Cat;

        }

        private void BikeCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindComboBox();
        }
    }
}
