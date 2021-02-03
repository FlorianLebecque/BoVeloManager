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
    public partial class LoginWindows : Window {
        public LoginWindows() {
            InitializeComponent();
        }

        //check login data and change windows if correct
        private void login() {

            string in_user = tb_userName.Text;
            string in_pass = tb_password.Password;


            string hash_pass = tools.md5.CreateMD5(in_pass);

            //fist get the user password
            string query = tools.DatabaseQuery.getUserPass(in_user);
            string pass = (string)tools.Database.getData(query)[0][0];

            //quick check if password are equal
            if ((pass.ToUpper() == hash_pass.ToUpper()) && (pass != "")) {

                    //know get the user data
                query = tools.DatabaseQuery.getUserGrade(in_user);
                List<object[]> res = tools.Database.getData(query);
                
                    //set the data into user class
                tools.user.setGrade(Convert.ToInt32(res[0][0]));
                tools.user.setUserName(in_user);

                    //hide the login windows
                this.Hide();
                tb_password.Clear();
                    
                    //create and show the dashboard windows
                Dashboard dashboardWindows = new Dashboard();
                dashboardWindows.ShowDialog();

                    //wait until we close the dashboard then show the login windows
                this.Show();
            }
        }

        private void BTLogin_Click(object sender, RoutedEventArgs e) {

            login();
            
            /*
                    TO DO
                - create error handling
             */

        }

        

        private void tb_password_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                login();
            }
        }
    }
}
