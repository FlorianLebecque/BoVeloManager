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

        List<string> FrameList = new List<string>();
        List<string> WheelsList = new List<string>();
        List<string> BrakesList = new List<string>();
        List<string> SaddleList = new List<string>();
        List<string> HandlebarList = new List<string>();
        List<string> ClientList = new List<string>();

        public addSales() {

            InitializeComponent();
            importData();




            DisplayResume();            
        }

        private void DisplayResume()
        {
            
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

        // complete ComboBox from database
        public void importData()
        {
            getFrame();
            getWheels();
            getBrakes();
            getSaddle();
            getHandlebar();
            getClients();
        }
        public void getClients()
        {
            string clients_data = tools.DatabaseQuery.getClients();
            DataTable clients_table = tools.Database.getData(clients_data);

            // import data      
            foreach (DataRow row in clients_table.Rows)
            {
                string client_fisrtname = row["first_name"].ToString();
                string client_lastname = row["last_name"].ToString();

                ClientList.Add(client_fisrtname + " " + client_lastname);
            }
        }
        public void getFrame()
        {

        }
        public void getWheels()
        {

        }
        public void getBrakes()
        {

        }
        public void getSaddle()
        {

        }
        public void getHandlebar()
        {

        }
        private void client_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = ClientList;
            combo.SelectedIndex = 0;
        }

        private void frame_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void wheels_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void brakes_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void saddle_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void handlebar_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
