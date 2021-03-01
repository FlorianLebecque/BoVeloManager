using System;
using System.Collections.Generic;
using System.Data;
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

namespace BoVeloManager.Management.user {
    /// <summary>
    /// Interaction logic for modUserWindow.xaml
    /// </summary>
    public partial class modUserWindow : Window {

        private Classes.User mod_user;

        public modUserWindow(Classes.User user_) {
            InitializeComponent();

            mod_user = user_;

            //display the user data
            tb_userName.Text = mod_user.getName();
            cb_grade.SelectedIndex = mod_user.getGrade();
        }


        /*
            Function trigger on the click event of the cancel button
         */
        private void BTCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }


        /*
            Function trigger on the click event of the cancel button
         */
        private void BT_update_Click(object sender, RoutedEventArgs e) {
            int grade = cb_grade.SelectedIndex;

            if (ch_pass.IsChecked == false) {

                updateUserGrade(grade);

            }else if (ch_pass.IsChecked == true) {

                string in_user = tb_userName.Text;
                string in_pass = tools.md5.CreateMD5(tb_password0.Password);

                //if the first password is correct
                if (mod_user.checkPass(in_pass)) {

                    string new_pass_1 = tb_password1.Password;
                    string new_pass_2 = tb_password2.Password;

                    if ((new_pass_1.Length >= 4) && (new_pass_1 == new_pass_2)) {
                        string pass = tools.md5.CreateMD5(new_pass_1);

                        mod_user.setHashPass(pass);
                        mod_user.setGrade(grade);

                        int res = tools.DatabaseClassInterface.updateUser(mod_user);

                    } else {
                        MessageBox.Show("The new password is invalide or the old password is invalide");
                    }


                } else {
                    MessageBox.Show("The new password is invalide or the old password is invalide");
                }

            }

        }

        private void updateUserGrade(int grade) {

            mod_user.setGrade(grade);

            int res = tools.DatabaseClassInterface.updateUser(mod_user);

            if(res == 1) {
                MessageBox.Show("User updated");
            } else {
                MessageBox.Show("Something wrong happend");
            }

            this.Close();

        }

        private void ch_pass_Click(object sender, RoutedEventArgs e) {

            tb_password0.IsEnabled = (bool)ch_pass.IsChecked;
            tb_password1.IsEnabled = (bool)ch_pass.IsChecked;
            tb_password2.IsEnabled = (bool)ch_pass.IsChecked;

        }
    }
}
