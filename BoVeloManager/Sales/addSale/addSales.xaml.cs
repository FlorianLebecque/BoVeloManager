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

namespace BoVeloManager.Sales {
    /// <summary>
    /// Interaction logic for addSales.xaml
    /// </summary>
    public partial class addSales : Window {
        public addSales() {
            InitializeComponent();
        }

        // Close Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Add new sale Button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
