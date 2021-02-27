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
using System.Text.RegularExpressions;
using BoVeloManager.Classes;

namespace BoVeloManager.Sales {
    /// <summary>
    /// Interaction logic for addSales.xaml
    /// </summary>
    public partial class addSales : Window {

        #region Dictionnary - List

        Dictionary<int, string> ModelDic     = new Dictionary<int, string>();

        Dictionary<int, string> FrameDic     = new Dictionary<int, string>();
        Dictionary<int, string> WheelsDic    = new Dictionary<int, string>();
        Dictionary<int, string> BrakesDic    = new Dictionary<int, string>();
        Dictionary<int, string> SaddleDic    = new Dictionary<int, string>();
        Dictionary<int, string> HandlebarDic = new Dictionary<int, string>();

        Dictionary<int, string> AddonsDic    = new Dictionary<int, string>();

        Dictionary<int, string> ClientDic    = new Dictionary<int, string>();


        List<string> FrameList      = new List<string>();
        List<string> WheelsList     = new List<string>();
        List<string> BrakesList     = new List<string>();
        List<string> SaddleList     = new List<string>();
        List<string> HandlebarList  = new List<string>();
        List<string> ClientList     = new List<string>();
        List<string> AddonsList     = new List<string>();
        List<string> ModelList       = new List<string>();

        #endregion

        tools.Sales sale = new tools.Sales();

        controler crtl;

        public addSales() {

            InitializeComponent();

            crtl = controler.Instance;

            importData();            
        }

        // Add new sale Button
        private void Button_add_new(object sender, RoutedEventArgs e)
        {
            tools.Article article = new tools.Article();

            article.model = (string)Model.SelectedItem;

            article.id_kit_frame = FrameDic.FirstOrDefault(x => x.Value == (string)frame.SelectedItem).Key;
            article.id_kit_wheels = WheelsDic.FirstOrDefault(x => x.Value == (string)wheels.SelectedItem).Key;
            article.id_kit_brakes = BrakesDic.FirstOrDefault(x => x.Value == (string)brakes.SelectedItem).Key;
            article.id_kit_saddle = SaddleDic.FirstOrDefault(x => x.Value == (string)saddle.SelectedItem).Key;
            article.id_kit_handlebar = HandlebarDic.FirstOrDefault(x => x.Value == (string)handlebar.SelectedItem).Key;

            article.id_kit_addons = AddonsDic.FirstOrDefault(x => x.Value == (string)addons.SelectedItem).Key;

            article.quantity = int.Parse((string)quantity.Text);

            sale.add_article_to_sale(article);

            UpdateDisplayResume();
        }

        private void UpdateDisplayResume()
        {
            string res = "";
            foreach (tools.Article article in sale.Sale)
            {
                article.setKit();

                string l0 = "Model of bike : " + article.model + "\n";
                string l1 = "   - Frame : " + article.frame + "\n";
                string l2 = "   - wheels : " + article.wheels + "\n";
                string l3 = "   - brakes : " + article.brakes + "\n";
                string l4 = "   - saddle : " + article.saddle + "\n";
                string l5 = "   - handlebar : " + article.handlebar + "\n";
                string l6 = "Quantity : " + article.quantity + "\n";

                string res_art = l0 + l1 + l2 + l3 + l4 + l5 + l6;

                res += res_art;
            }
            resume.Text = res;
        }

        // Close Button
        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

        private void BT_Send_sale_to_db(object sender, RoutedEventArgs e)
        {
            //get the form data
            int id_client = ClientDic.FirstOrDefault(x => x.Value == (string)client.SelectedItem).Key;
            int id_seller = crtl.getCurrentUser().getId();

            DateTime prevision_date = new DateTime(2018, 04, 02);
            //DateTime current_day = new DateTime(0,0,0);

            addSale(id_client, id_seller, prevision_date, prevision_date);

            //this.Close();
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

            this.Close();
        }


        #region Form
        // complete ComboBox from database
        public void importData()
        {
            getFrame();
            getWheels();
            getBrakes();
            getSaddle();
            getHandlebar();
            getClients();
            getModel();
        }
        public void getClients()
        {
            string clients_data = tools.DatabaseQuery.getClients();
            DataTable clients_table = tools.Database.getData(clients_data);

            // import data      
            foreach (DataRow row in clients_table.Rows)
            {
                int id_client = Convert.ToInt32(row["id"]);

                string client_fisrtname = row["first_name"].ToString();
                string client_lastname = row["last_name"].ToString();

                ClientDic.Add(id_client, client_fisrtname + " " + client_lastname);
                ClientList.Add(client_fisrtname + " " + client_lastname);
            }
        }
        public void getModel()
        {
            string model_data = tools.DatabaseQuery.getModel();
            DataTable model_table = tools.Database.getData(model_data);

            foreach (DataRow row in model_table.Rows)
            {
                ModelDic.Add(Convert.ToInt32(row["id"]), row["name"].ToString());
                ModelList.Add(row["name"].ToString());
            }
        }
        public void getFrame()
        {
            string frame_data = tools.DatabaseQuery.getFrameKit();
            DataTable frame_table = tools.Database.getData(frame_data);

            addKitToListAndDic(FrameList, FrameDic, frame_table);
        }
        public void getWheels()
        {
            string wheels_data = tools.DatabaseQuery.getWheelsKit();
            DataTable wheels_table = tools.Database.getData(wheels_data);

            addKitToListAndDic(WheelsList, WheelsDic, wheels_table);
        }
        public void getBrakes()
        {
            string brakes_data = tools.DatabaseQuery.getBrakesKit();
            DataTable brakes_table = tools.Database.getData(brakes_data);

            addKitToListAndDic(BrakesList, BrakesDic, brakes_table);
        }
        public void getSaddle()
        {
            string saddle_data = tools.DatabaseQuery.getSaddleKit();
            DataTable saddle_table = tools.Database.getData(saddle_data);

            addKitToListAndDic(SaddleList, SaddleDic, saddle_table);
        }
        public void getHandlebar()
        {
            string handlebar_data = tools.DatabaseQuery.getHandlebarKit();
            DataTable handlebar_table = tools.Database.getData(handlebar_data);

            addKitToListAndDic(HandlebarList, HandlebarDic, handlebar_table);
        }
        public void getAddons()
        {
            string addons_data = tools.DatabaseQuery.getHandlebarKit();
            DataTable addons_table = tools.Database.getData(addons_data);

            addKitToListAndDic(AddonsList, AddonsDic, addons_table);
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
        private void Model_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = ModelList;
        }
        private void addons_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = AddonsList;
        }

        private void addKitToListAndDic(List<string> List, Dictionary<int, string> Dictionary, DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["properties"].ToString() != "")
                {
                    string recap = row["name"].ToString() + " - " + row["properties"].ToString();

                    Dictionary.Add(Convert.ToInt32(row["id"]), recap);
                    List.Add(row["name"].ToString() + " - " + row["properties"].ToString());
                }
                else
                {
                    Dictionary.Add(Convert.ToInt32(row["id"]), row["name"].ToString());
                    List.Add(row["name"].ToString());
                }
            }            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        
    }
}
