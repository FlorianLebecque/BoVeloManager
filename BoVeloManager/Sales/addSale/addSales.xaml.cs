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
            string frame_data = tools.DatabaseQuery.getFrameKit();
            DataTable frame_table = tools.Database.getData(frame_data);

            foreach (DataRow row in frame_table.Rows)
            {
                
            }
        }
        public void getWheels()
        {
            string wheels_data = tools.DatabaseQuery.getFrameKit();
            DataTable wheels_table = tools.Database.getData(wheels_data);

            foreach (DataRow row in wheels_table.Rows)
            {

            }
        }
        public void getBrakes()
        {
            string brakes_data = tools.DatabaseQuery.getFrameKit();
            DataTable brakes_table = tools.Database.getData(brakes_data);

            
        }
        public void getSaddle()
        {
            string saddle_data = tools.DatabaseQuery.getFrameKit();
            DataTable saddle_table = tools.Database.getData(saddle_data);

            foreach (DataRow row in saddle_table.Rows)
            {

            }
        }
        public void getHandlebar()
        {
            string handlebar_data = tools.DatabaseQuery.getFrameKit();
            DataTable handlebar_table = tools.Database.getData(handlebar_data);

            foreach (DataRow row in handlebar_table.Rows)
            {

            }
        }
        private void client_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = ClientList;
        }

        private void frame_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = FrameList;
        }

        private void wheels_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = WheelsList;
        }

        private void brakes_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = BrakesList;
        }

        private void saddle_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = SaddleList;
        }

        private void handlebar_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = HandlebarList;
        }
        private void addKitToList(List<string> List, DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["properties"].ToString() != "")
                {
                    List.Add(row["name"].ToString() + " - " + row["properties"].ToString());
                }
                else
                {
                    List.Add(row["name"].ToString());
                }
            }            
        }
    }
}
