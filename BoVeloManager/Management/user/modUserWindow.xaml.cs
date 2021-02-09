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

namespace BoVeloManager.Management.user {
    /// <summary>
    /// Interaction logic for modUserWindow.xaml
    /// </summary>
    public partial class modUserWindow : Window {

        private int userId;

        public modUserWindow(int userId_) {
            InitializeComponent();
            
            //initialise the windows
            userId = userId_;
                //get the user data
            string q = tools.DatabaseQuery.getUser_by_id(userId);
            DataTable res = tools.Database.getData(q);
                //display the user data
            tb_userName.Text = (string)res.Rows[0]["user"];
            cb_grade.SelectedIndex = Convert.ToInt32(res.Rows[0]["grade"]);
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

                updateUserGrade(userId, grade);

            }else if (ch_pass.IsChecked == true) {

                string in_user = tb_userName.Text;
                string in_pass = tb_password0.Password;

                //if the first password is correct
                if (tools.user.checkUserPass(in_user, in_pass)) {

                    string new_pass_1 = tb_password1.Password;
                    string new_pass_2 = tb_password2.Password;

                    if ((new_pass_1.Length >= 4) && (new_pass_1 == new_pass_2)) {
                        string pass = tools.md5.CreateMD5(new_pass_1);
                        string q = tools.DatabaseQuery.setUserPass(userId, pass);

                        tools.Database.setData(q);
                        updateUserGrade(userId, grade);

                    } else {
                        MessageBox.Show("The new password is invalide or the old password is invalide");
                    }


                } else {
                    MessageBox.Show("The new password is invalide or the old password is invalide");
                }

            }

        }

        private void updateUserGrade(int id,int grade) {
            string q = tools.DatabaseQuery.setUserGrade(id, grade);
            int res = tools.Database.setData(q);

            if(res == -1) {
                MessageBox.Show("An error has occured");
            } else if(res == 1){
                MessageBox.Show("Done");
            } else {
                MessageBox.Show("The database is corrupted");
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
