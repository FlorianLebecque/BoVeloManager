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

namespace BoVeloManager.Management {
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window {
        public AddUserWindow() {
            InitializeComponent();
        }

        private void BTLogin_Click(object sender, RoutedEventArgs e) {
                //get the form data
            string userName = tb_userName.Text;
            string pass1 = tb_password1.Password;
            string pass2 = tb_password2.Password;
            int grade = cb_grade.SelectedIndex;

            int err = 0;

                //test the data
            if((userName.Length >= 4)&&(userName != "")){    //check if the userName is 4 caracters min, and not empty
                if((pass1 == pass2)&&(pass1 != "")&&(pass1.Length >= 4)) {
                    if(grade != -1) {

                        addUser(userName, pass1, grade);               
                        this.Close();

                    } else {
                        err = 3;
                    }
                } else {
                    err = 2;
                }
            } else {
                err = 1;
            }

            lb_error.Visibility = Visibility.Visible;
            switch (err) {
                case 1:
                    lb_error.Text = "User name invalid";
                    break;
                case 2:
                    lb_error.Text = "Password don't match or invalid";
                    break;
                case 3:
                    lb_error.Text = "You must select a grade";
                    break;
            }

        }

        private void addUser(string name,string pass,int grade) {

            string q = tools.DatabaseQuery.addUser(name,tools.md5.CreateMD5(pass),grade);

            int res = tools.Database.setData(q);

            if(res == -1) {
                MessageBox.Show("An error has occured");
            }else if (res == 1) {
                MessageBox.Show("User added");
            } else {
                MessageBox.Show("The database might be corrupted");
            }

        }

        private void BTCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
