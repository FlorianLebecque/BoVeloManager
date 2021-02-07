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

            userId = userId_;

            string q = tools.DatabaseQuery.getUser_by_id(userId);
            DataTable res = tools.Database.getData(q);

            tb_userName.Text = (string)res.Rows[0]["user"];
            cb_grade.SelectedIndex = Convert.ToInt32(res.Rows[0]["grade"]);
        }

        private void BTCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void BT_update_Click(object sender, RoutedEventArgs e) {
            int grade = cb_grade.SelectedIndex;

            if (ch_pass.IsChecked == false) {

                updateUserGrade(userId, grade);



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
    }
}
