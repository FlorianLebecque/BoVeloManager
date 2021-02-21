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
using System.Data;

namespace BoVeloManager {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindows : Window {
        public LoginWindows() {
            InitializeComponent();

            lb_error.Visibility = Visibility.Hidden;

            tools.user.RESET();

            cb_keep.IsChecked = Properties.Settings.Default.keeplogged;

            if (Properties.Settings.Default.keeplogged) {
                tb_password.Password = Properties.Settings.Default.magicWord;
                tb_password.IsEnabled = false;
                tb_userName.Text = Properties.Settings.Default.magicWord2;
            }

        }

        //check login data and change windows if correct
        private void login(string user,string passMD5) {

            string in_user = user;
            string in_pass = passMD5;


            //check if password are equal
            if (tools.user.checkUserPassMD5(in_user,in_pass)) {

                    //know get the user data
                string query = tools.DatabaseQuery.getUserData_byName(in_user);
                DataTable res = tools.Database.getData(query);
                
                    //set the data into user class
                tools.user.setGrade(Convert.ToInt32(res.Rows[0]["grade"]));
                tools.user.setId(Convert.ToInt32(res.Rows[0]["id"]));
               
                tools.user.setUserName(in_user);

                    //hide the login windows
                this.Hide();
                tb_password.Clear();
                lb_error.Visibility = Visibility.Hidden;

                    //create and show the dashboard windows
                Dashboard dashboardWindows = new Dashboard();
                dashboardWindows.ShowDialog();

                    //wait until we close the dashboard then show the login windows
                this.Show();

                LoginWindows LW = new LoginWindows();
                LW.Show();

                this.Close();

            } else {
                lb_error.Visibility = Visibility.Visible;
                lb_error.Text = "User or password incorrect";
            }

        }

        private bool check(string user,string pass) {

            Properties.Settings.Default.keeplogged = (bool)cb_keep.IsChecked;

            if (Properties.Settings.Default.keeplogged) {
                Properties.Settings.Default.magicWord = pass;
            } else {
                Properties.Settings.Default.magicWord = "";
            }


            Properties.Settings.Default.magicWord2 = user;
            Properties.Settings.Default.Save();

            return Properties.Settings.Default.keeplogged;
        }

        private void log() {
            string user = tb_userName.Text;
            string pass = "";

            if (Properties.Settings.Default.keeplogged) {
                pass = Properties.Settings.Default.magicWord;
            } else if (tb_password.IsEnabled == true){
                pass = tools.md5.CreateMD5(tb_password.Password);
            }

            check(user, pass);
            login(user, pass);
        }

        //event when click on the login button
        private void BTLogin_Click(object sender, RoutedEventArgs e) {

            log();
        }

        //event when enter key is pressed while the password textbox is focus
        private void tb_password_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                log();
            }
        }

        private void cb_keep_Click(object sender, RoutedEventArgs e) {
            if(cb_keep.IsChecked == false) {
                Properties.Settings.Default.keeplogged = false;
                tb_password.Clear();
                tb_password.IsEnabled = true;
            } else {
                tb_password.IsEnabled = true;
            }
        }
    }
}
