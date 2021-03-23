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
using BoVeloManager.Classes;
using BoVeloManager.Sales;

namespace BoVeloManager.UI.Sales.Client {
    /// <summary>
    /// Logique d'interaction pour EditClientWindow.xaml
    /// </summary>
    public partial class EditHumanWindow : Window {
        private Classes.Human human;
        public EditHumanWindow(Classes.Human human_) {
            InitializeComponent();
            human = human_;
            tb_enterprise_name.Text = human.getEtpName();
            tb_enterprise_adress.Text = human.getEtpAdress();
            tb_email.Text = human.getEmail();
            tb_phoneNum.Text = human.getPhoneNumb();
        }

        private void BT_cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void BT_Update_Click(object sender, RoutedEventArgs e) {

            string entrepriseName = tb_enterprise_name.Text;
            string entrepriseAdress = tb_enterprise_adress.Text;
            string email = tb_email.Text;
            string phoneNumber = tb_phoneNum.Text;


            editHuman(entrepriseName, entrepriseAdress, email, phoneNumber);
            this.Close();

        }
        private void editHuman(string entreprise_name, string entreprise_adress, string email, string phone_num) {

            human.setEditHuman(entreprise_name, entreprise_adress, email, phone_num);
            tools.DatabaseClassInterface.updateHuman(human);
        }
    }
}
