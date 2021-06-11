using System;
using System.Collections.Generic;
using System.Data;
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
using BoVeloManager.Classes;

namespace BoVeloManager.stock {
    /// <summary>
    /// Interaction logic for stock.xaml
    /// </summary>
    public partial class stock : Page {

        private static stock instance = new stock();

        Controler crtl;

        private stock() {
            InitializeComponent();

            update_dg_kitTemplateList();

        }

        private void update_dg_kitTemplateList()
        {
            dg_kitTemplateList.ItemsSource = crtl.getKitTemplateList();
        }

    }
}
