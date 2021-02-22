using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace BoVeloManager {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window {


        bool isOpen = false;

        public Dashboard() {
            InitializeComponent();

            frame.Content = new Catalogue.Catalog();

            //status bar log
            lb_user.Text = tools.user.getUserName(); 
        }


        


        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e) {
            isOpen = true;

            ButtonOpenMenu.Visibility = Visibility.Hidden;
            ButtonCloseMenu.Visibility = Visibility.Visible;
            lb_user.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e) {
            isOpen = false;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Hidden;
            lb_user.Visibility = Visibility.Hidden;
        }

        /*
         * Function added to every button on the side bar
         *      Their goal is to set the page into the frame (center object of the window)
        */
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (isOpen) {
                var sb = this.Resources["CloseMenu"] as System.Windows.Media.Animation.Storyboard;
                sb.Begin();
            }

            isOpen = false;
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Hidden;
            lb_user.Visibility = Visibility.Hidden;

            //clear the content
            frame.Content = null;
            frame.NavigationService.RemoveBackEntry();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Tag.ToString()) {
                case "Catalog":
                    frame.Content = new Catalogue.Catalog();
                    break;
                case "Command":
                    frame.Content = new Commande.Commande();
                    break;
                case "Stock":
                    frame.Content = new stock.stock();
                    break;
                case "Management":
                    frame.Content = new Management.Management();
                    break;
                case "Sales":
                    frame.Content = new Sales.Sales();
                    break;

            }
        }
    }
}
