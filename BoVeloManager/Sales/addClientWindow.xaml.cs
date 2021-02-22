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

namespace BoVeloManager.Sales
{
    /// <summary>
    /// Interaction logic for addSales.xaml
    /// </summary>
    public partial class addClientWindow : Window
    {
        public addClientWindow()
        {
            InitializeComponent();
        }

        private void BTLogin_Click(object sender, RoutedEventArgs e)
        {
            String firstName = tb_firstName.Text;
            String lastName = tb_lastName.Text;
            String enterpriseName = tb_enterpriseName.Text;
            String enterpriseAdress = tb_enterpriseAdress.Text;
            String email = tb_email.Text;
            String phoneNumber = tb_phoneNumber.Text;
            DateTime myDateTime = DateTime.Now;

            addClient(firstName, lastName, enterpriseName, enterpriseAdress, email, phoneNumber, myDateTime);
            this.Close();

        }
            

        private void addClient(string firstName, string lastName, string enterpriseName, string enterpriseAdress, string  email, string phoneNumber, DateTime date)
        {

            string q = tools.DatabaseQuery.addClient(firstName, lastName, enterpriseName, enterpriseAdress, email, phoneNumber, date);

            int res = tools.Database.setData(q);

            if (res == -1)
            {
                MessageBox.Show("An error has occured");
            }
            else if (res == 1)
            {
                MessageBox.Show("Client added");
            }
            else
            {
                MessageBox.Show("The database might be corrupted");
            }

        }

        private void BTCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
