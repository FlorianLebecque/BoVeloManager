using System;
using System.Collections.Generic;
using System.Data.Odbc;
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

namespace BoVeloManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

            //check login data and change windows if correct
        private void BTLogin_Click(object sender, RoutedEventArgs e) {

            Dashboard dashboardWindows = new Dashboard();

            //fist get the user password
            string pass = (string)tools.database.getData("SELECT `psw` FROM `bv_user` WHERE `user` = '" + tb_userName.Text + "'");
            
            //quick check if password are equal
            if((pass == tb_password.Password)&&(pass != "")) {
                this.Hide();
                dashboardWindows.ShowDialog();
                this.Show();
            }
            
            /*
                    TO DO
                - secure password (md5 at least)
                - check if user exist
                - create error handling
            
             */

            
            
        }
    }
}
