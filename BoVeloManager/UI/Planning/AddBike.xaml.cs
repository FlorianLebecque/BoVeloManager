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
        }

        private void BTLogin_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
