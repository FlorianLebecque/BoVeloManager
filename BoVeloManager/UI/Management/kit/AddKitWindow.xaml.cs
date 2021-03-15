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
        private void BTADD_kit(object sender, RoutedEventArgs e)
        {
            //get the form data

            string kitName = kit_name.Text;
            string kitProp = kit_prop.Text;
            int kitCat = kit_cat.SelectedIndex;
            int kitPrice = Convert.ToInt32(kit_price.Text);
            int id = Controler.Instance.getLastKitTemplateId() + 1;

            KitTemplate kt = new KitTemplate(id, kitName, kitCat, kitPrice ,kitProp);
            addKit(kt);
            this.Close();

    
        }

        private void addKit(KitTemplate kt)
        {
            try {
                Controler.Instance.createKit(kt);
                
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
