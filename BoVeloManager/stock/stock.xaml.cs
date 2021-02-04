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

namespace BoVeloManager.stock {
    /// <summary>
    /// Interaction logic for stock.xaml
    /// </summary>
    public partial class stock : Page {
        public stock() {
            InitializeComponent();

            string q = tools.DatabaseQuery.getUsers();

            DataTable dt = tools.Database.getDataTable(q);

            

            dg_grid.ItemsSource = dt.DefaultView;


        }
    }
}
