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

            List<BikeTemplate> BikeTemplateList = Controler.Instance.getBikeTemplateList();
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
            int done = 0;
            foreach (BikeTemplate bt in BikeTemplateList)
            {
                    
                    List<KitTemplate> x = new List<KitTemplate>() {Size[indexSize], Color[indexColor]};
                    if(compare(bt.getListKit(),x) && done == 0)
                    {
                        for (int i = 0; i < Quantity; i++)
                        {
                            Bike b = new Bike(Controler.Instance.getLastBike()+1, 0, 10, indexPoste, bt, DateTime.Now, DateTime.MinValue);
                            addBike(b);
                        }
                    done = 1;
                }
            }
            if (done == 0)
            {
                for (int i = 0; i < Quantity; i++)
                {
                    BikeTemplate newbt = new BikeTemplate(Controler.Instance.getLastBikeTemplate()+1, Controler.Instance.getCatalogBike()[indexCatalog]);
                    newbt.linkKitTemplate(Size[indexSize]);
                    newbt.linkKitTemplate(Color[indexColor]);
                    Controler.Instance.createBikeTemplate(newbt);
                    Bike b = new Bike(Controler.Instance.getLastBike()+1, 0, 10, indexPoste, Controler.Instance.getBikeTemplateById(newbt.getId()), DateTime.Now, DateTime.MinValue);
                    addBike(b);
                }
            }
            this.Close();


        }

        private bool compare(List<KitTemplate> x, List<KitTemplate> y)
        {
            if ((x.Count() == y.Count()) && (x.Count()==2) &&(y.Count()==2))
            {
                if ((x[0].Equals(y[0]))&&(x[1].Equals(y[1])))
                {
                    return true;
                }
                if ((x[0].Equals(y[1])) && (x[1].Equals(y[0])))
                {
                    return true;
                }
            }
            return false;
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
