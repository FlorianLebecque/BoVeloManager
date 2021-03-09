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
    public partial class EditClientWindow : Window {
        private Classes.Client client;
        public EditClientWindow(Classes.Client client_) {
            InitializeComponent();
            client = client_;
            tb_first_name.Text = client.getName();
            tb_enterprise_name.Text = client.getEtpName();
            tb_enterprise_adress.Text = client.getEtpAdress();
            tb_email.Text = client.getEmail();
            tb_phoneNum.Text = client.getPhoneNumb();
        }

        private void BT_cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void BT_Update_Click(object sender, RoutedEventArgs e) {

            string firstName = tb_first_name.Text;
            string lastName = tb_last_name.Text;
            string entrepriseName = tb_enterprise_name.Text;
            string entrepriseAdress = tb_enterprise_adress.Text;
            string email = tb_email.Text;
            string phoneNumber = tb_phoneNum.Text;


            editClient(firstName, lastName, entrepriseName, entrepriseAdress, email, phoneNumber);
            this.Close();

        }
        private void editClient(string first_name, string last_name, string entreprise_name, string entreprise_adress, string email, string phone_num) {

            client.setEditClient(entreprise_name, entreprise_adress, email, phone_num);
            tools.DatabaseClassInterface.updateClient(client);
        }
    }
}
