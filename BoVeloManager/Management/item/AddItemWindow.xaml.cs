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

namespace BoVeloManager.Management.item
{
    /// <summary>
    /// Logique d'interaction pour AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        public AddItemWindow()
        {
            InitializeComponent();
        }

        private void BTAddItem_Click(object sender, RoutedEventArgs e)
        {
            string itemName = tb_itemName.Text;
            int err = 0;
            if ((itemName.Length >= 2) && (itemName != ""))
            {    //check if the itemName is 2 caracters min, and not empty
                addItem(itemName);
                this.Close();

            }
            else { err = 1; }
            //lb_error.Visibility = Visibility.Visible;
            switch (err)
            {
                case 1:
                    //lb_error.Text = "Item name invalid";
                    break;
            }
        }

        private void addItem(string name)
        {
            string q = tools.DatabaseQuery.addItem(name);
            int res = tools.Database.setData(q);
            if (res == -1)
            {
                MessageBox.Show("An error has occured");
            }
            else if (res == 1)
            {
                MessageBox.Show("Item added");
            }
            else
            {
                MessageBox.Show("The database might be corrupted");
            }
        }

            private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
