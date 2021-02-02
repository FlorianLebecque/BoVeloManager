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



namespace BoVeloManager {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window {
        public Dashboard() {
            InitializeComponent();
        }

        /*
         * Function added to every button on the side bar
         *      Their goal is to set the page into the frame (center object of the window)
        */
        private void BTPage_Click(object sender, RoutedEventArgs e) {
            Button senderButton = (Button)sender;   //get the button that trigger the event

            String btnTag = senderButton.Tag.ToString();

                //clear the content
            frame.Content = null;
            frame.NavigationService.RemoveBackEntry();

            switch (btnTag) {
                case "Catalog":
                    frame.Content = new Catalogue.Catalog();
                    break;
            }


        }
    }
}
