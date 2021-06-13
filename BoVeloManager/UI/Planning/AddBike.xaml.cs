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
        public AddBike() {
            InitializeComponent();
            init();
        }

        private void init() {
            BikeCatalog.ItemsSource = Controler.Instance.getCatalogBikeDisplayInfo();
            setCombobox(((CatalogBike.displayInfo)BikeCatalog.SelectedItem).CurCatBike);
        }

        private void setCombobox(CatalogBike cb) {
            (BikeSize.ItemsSource, BikeColor.ItemsSource) = cb.getProperties();

        }
        

        private void BTCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();

        }

        private void BikeCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            setCombobox(((CatalogBike.displayInfo)BikeCatalog.SelectedItem).CurCatBike);

        }

        private void BT_Add_Click(object sender, RoutedEventArgs e) {

            int qnt = Convert.ToInt32(BikeQuantity.Text);

            string b_name   = ((CatalogBike.displayInfo)BikeCatalog.SelectedItem).name;
            string b_color  = BikeColor.SelectedItem.ToString();
            string b_size   = BikeSize.SelectedItem.ToString();
            CatalogBike cb  = ((CatalogBike.displayInfo)BikeCatalog.SelectedItem).CurCatBike;

            BikeBasket bb = new BikeBasket(b_name, b_color, b_size, cb,qnt);

            BikeTemplate bt = bb.CreateBikeTemplate();
            int id_bt = Controler.Instance.getLastBikeTemplateId() + 1;
            bt.setId(id_bt);
            Controler.Instance.createBikeTemplate(bt);

            for (int i = 0; i < bb.qnt; i++) {
                int bikeID = Controler.Instance.getLastBikeId() + 1;

                DateTime constr_date = TempSale.getConstrDate();
                DateTime planned_date = TempSale.getNextPrevisionDate();

                int poste = Controler.Instance.getAvailablePoste();

                Bike tempB = new Bike(bikeID, 0, -1, poste, bt, planned_date, constr_date);

                Controler.Instance.createBike(tempB);
            }

            this.Close();
        }
    }
}
