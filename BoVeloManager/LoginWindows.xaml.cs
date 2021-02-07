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
        }

        //check login data and change windows if correct
        private void login() {

            string in_user = tb_userName.Text;
            string in_pass = tb_password.Password;

                //compute a MD5 hash of the input password
            string hash_pass = tools.md5.CreateMD5(in_pass);

                //fist get the user password
            string query = tools.DatabaseQuery.getUserPass(in_user);
            DataTable dt = tools.Database.getData(query);

                //check if we get any result
            string pass = "";
            if (dt.Rows.Count > 0) {
                pass = (string)dt.Rows[0]["psw"];
            }

            //check if password are equal
            if ((pass.ToUpper() == hash_pass.ToUpper()) && (pass != "")) {

                    //know get the user data
                query = tools.DatabaseQuery.getUserGrade(in_user);
                DataTable res = tools.Database.getData(query);
                
                    //set the data into user class
                tools.user.setGrade(Convert.ToInt32(res.Rows[0]["grade"]));
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
            } else {
                lb_error.Visibility = Visibility.Visible;
                lb_error.Text = "User or password incorrect";
            }

        }

        //event when click on the login button
        private void BTLogin_Click(object sender, RoutedEventArgs e) {
            login();
        }

        //event when enter key is pressed while the password textbox is focus
        private void tb_password_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                login();
            }
        }
    }
}
