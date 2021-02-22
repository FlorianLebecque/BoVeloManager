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

namespace BoVeloManager.Management.kit
{
    /// <summary>
    /// Logique d'interaction pour AddKitWindow.xaml
    /// </summary>
    public partial class AddKitWindow : Window
    {
        public AddKitWindow()
        {
            InitializeComponent();
        }
        private void BTLogin_Click(object sender, RoutedEventArgs e)
        {
            //get the form data
            string kitName = kit_name.Text;
            string kitProp = kit_prop.Text;
            string kitCat = kit_cat.SelectedIndex.ToString();


            addKit(kitName, kitProp, kitCat);
            this.Close();

    
        }

        private void addKit(string name, string prop, string cat)
        {
            try {
                string q = tools.DatabaseQuery.addKit(name, prop, cat);
                int res = tools.Database.setData(q);

                MessageBox.Show("Kit added");
            }
            catch { 

                MessageBox.Show("An error has occured");
            }
        }

        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
