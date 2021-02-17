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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoVeloManager.Commande
{
    /// <summary>
    /// Logique d'interaction pour Commande.xaml
    /// </summary>
    /// 
    public class test
    {
        public Color colors { get; set; }
        public string Name { get; set; }
    }
    public partial class Commande : Page
    {
        List<test> testlist = new List<test>();
        public Commande()
        {
            InitializeComponent();
            testlist.Add(new test() { colors = Colors.Red, Name = "Red" });
            testlist.Add(new test() { colors = Colors.Green, Name = "Green" });
            testlist.Add(new test() { colors = Colors.Gray, Name = "Gray" });
            testlist.Add(new test() { colors = Colors.Blue, Name = "Blue" });
            testlist.Add(new test() { colors = Colors.Black, Name = "Black" });
            testlist.Add(new test() { colors = Colors.Yellow, Name = "Yellow" });
            MyComboBox.ItemsSource = testlist;
            MyComboBox.SelectionChanged += MyComboBox_SelectionChanged;
        }
        void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            test test = (test)MyComboBox.SelectedItem;
            // MyGrid.Background = new SolidColorBrush(test.colors);
        }
    }
}