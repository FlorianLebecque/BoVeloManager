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

        private void BTLogin_Click(object sender, RoutedEventArgs a)
        {
            int indexCatalog = BikeCatalog.SelectedIndex;
            int indexSize = BikeSize.SelectedIndex;
            int indexColor = BikeColor.SelectedIndex;
            int indexPoste = Poste.SelectedIndex;
            int Quantity = Convert.ToInt32(BikeQuantity.Text);
            //int id = Controler.Instance.getLastBikeTemplate() + 1;
            //Bike(int id_,int status_, int id_sale_,int Poste_, BikeTemplate bt_, DateTime planned_date_, DateTime constr_date_)
            //Bike b = new Bike(id, kitName, kitCat, kitPrice, kitProp);
            //addBike(kt);
            List<BikeTemplate> BikeTemplateList = Controler.Instance.getBikeTemplateList();
            foreach (BikeTemplate bt in BikeTemplateList)
            {
                if (bt.getCat().Equals(Controler.Instance.getCatalogBike()[indexCatalog]))
                {
                    List<KitTemplate> Size = new List<KitTemplate>();
                    List<KitTemplate> Color = new List<KitTemplate>();

                    List<KitTemplate> KitList = Controler.Instance.getCatalogBike()[indexCatalog].getKitTemplateList();

                    foreach (KitTemplate kit in KitList)
                    {
                        KitTemplate.displayInfo kit_struct = kit.GetDisplayInfo();
                        string kit_cat = kit_struct.category;

                        if (kit_cat == "Size")
                        {
                            Size.Add(kit);
                        }
                        else if (kit_cat == "Color")
                        {
                            Color.Add(kit);
                        }

                    }
                    List<KitTemplate> x = new List<KitTemplate>() {Size[indexSize], Color[indexColor]};
                    //if (Enumerable.SequenceEqual(bt.getCat().getKitTemplateList().OrderBy(e => e), x)) //Bike template exists //ERROR
                    if (true)
                    {
                        Bike b = new Bike(12345, 0, 12345, indexPoste, bt, DateTime.Now, DateTime.Now);
                        addBike(b);
                    }
                    else //Bike template doesn t exist
                    {
                        BikeTemplate newbt = new BikeTemplate(Controler.Instance.getLastBikeTemplate() + 1, Controler.Instance.getCatalogBike()[indexCatalog]);
                        Bike b = new Bike(12345, 0, 12345, indexPoste, newbt, DateTime.Now, DateTime.Now);
                        addBike(b);
                    }
                }
            }
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
