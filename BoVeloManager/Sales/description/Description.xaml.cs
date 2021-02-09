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

namespace BoVeloManager.Sales.description
{
    /// <summary>
    /// Logique d'interaction pour Description.xaml
    /// </summary>
    public partial class Description : Window
    {
        private int id_sale;
        public Description(int _id_sale)
        {
            id_sale = _id_sale;
            InitializeComponent();
            string data = tools.DatabaseQuery.getSale_by_id(id_sale);
            MessageBox.Show(data);

            seller.Text = "victor";

        }
    }
}
