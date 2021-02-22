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
using System.Data;

namespace BoVeloManager.Sales {
    /// <summary>
    /// Interaction logic for addSales.xaml
    /// </summary>
    public partial class addSales : Window {

        List<string> ClientList = new List<string>();

        public addSales() {

            InitializeComponent();

            ClientList.Add("monsieur l'embrouille");
            ClientList.Add("Monsieur bigboss");

            DisplayResume();            
        }
        
        public List<string> getClientList { get { return ClientList; } }

        private void DisplayResume()
        {
            string resumeText = "";

            for (int i = 0 ; i < ClientList.Count ; i ++)
            {
                resumeText += ClientList[i];
            }

            resume.Text = resumeText;
        }

        // Close Button
        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Add new sale Button
        private void Button_add_new(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void BT_Send_sale_to_db(object sender, RoutedEventArgs e)
        {
            //get the form data
            int id_client;
            int id_seller;

            DateTime prevision_date = new DateTime(0, 0, 0);
            DateTime current_day = DateTime.Today;

            //addSale(id_client, id_seller, prevision_date, current_day);
            this.Close();
        }

        private void addSale(int id_client, int id_seller, DateTime prevision_date, DateTime date)
        {
            try
            {
                string q = tools.DatabaseQuery.addSale(id_client, id_seller, prevision_date, date);
                int res = tools.Database.setData(q);

                MessageBox.Show("Sale added");
            }
            catch
            {

                MessageBox.Show("An error has occured");
            }
            
        }
    }
}
