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

namespace BoVeloManager.tools.UI {
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window {

        private static MessageBoxResult res;

        private MessageBox(string title,string message,bool need_can) {
            InitializeComponent();

            if (need_can) {
                bt_cancel.Visibility = Visibility.Visible;
            }

            this.Title = title;
            tb_message.Text = message;
        }

        public static void Show(string message,string title) {
            MessageBox mb = new MessageBox(title, message,false);
            mb.ShowDialog();
        }

        public static MessageBoxResult Show(string message, string title, MessageBoxButton bt) {

            res = MessageBoxResult.None;

            MessageBox mb = new MessageBox(title, message,true);
            mb.ShowDialog();

            return res;
        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            res = MessageBoxResult.Yes;
            Close();
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e) {
            res = MessageBoxResult.No;
            Close();
        }
    }
}
