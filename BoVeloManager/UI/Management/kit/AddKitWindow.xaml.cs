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
using System.Text.RegularExpressions;

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

            foreach (KitCategory i in Enum.GetValues(typeof(KitCategory))) {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = tools.Converter.GetCatName(i);
                cbi.Tag = i;
                kit_cat.Items.Add(cbi);
            }

        }
        private void BTADD_kit(object sender, RoutedEventArgs e)
        {
            //get the form data

            string kitName = kit_name.Text;
            string kitProp = kit_prop.Text;
            int kitCat = kit_cat.SelectedIndex;
            int kitPrice = Convert.ToInt32(kit_price.Text);
            int id = Controler.Instance.getLastKitTemplateId() + 1;
            int bike_qtt = Convert.ToInt32(kit_bike_qtt.Text);

            KitTemplate kt = new KitTemplate(id, kitName, (KitCategory)kitCat, kitPrice ,kitProp, 0, 0, 0, bike_qtt);
            addKit(kt);
            this.Close();
        }

        private void addKit(KitTemplate kt)
        {
            try {
                Controler.Instance.createKit(kt);

                tools.UI.MessageBox.Show("Kit added","Action confirmation");
            }
            catch {

                tools.UI.MessageBox.Show("An error has occured","Error");
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
