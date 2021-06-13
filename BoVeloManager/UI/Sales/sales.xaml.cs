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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BoVeloManager.Classes;


namespace BoVeloManager.Sales
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Sales : Page
    {

        private static Sales instance = new Sales();

        Controler crtl;
        Brush tb_searchBackGround;

        private Sales()
        {
            InitializeComponent();

            crtl = Controler.Instance;
            tb_searchBackGround = tb_search.Background;


            init();
        }

        public void init() {
            update_dg_salesList();
            update_dg_clientList();
        }


       



        public static Sales Instance {
            get {
                return instance;
            }
        }




        private void update_dg_salesList()
        {
            dg_salesList.ItemsSource = crtl.GetSaleDisplayInfo();
        }
        
        private void update_dg_salesList_search(string t)
        {
            dg_salesList.ItemsSource = crtl.GetSaleDisplayInfo_search(t);
            
        }
        
        private void update_dg_clientList() 
        {
            //dg_clientList.ItemsSource = null;
            dg_clientList.ItemsSource = crtl.GetClientDisplayInfo();
        }

        private void update_dg_clientList_search(string t)
        {
            dg_clientList.ItemsSource = crtl.GetClientDisplayInfo_search(t);
        }



        private void bt_showDescription_Click(object sender, RoutedEventArgs e)
        {
            Sale s = ((Sale.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).CurSale;
            

            description.Description DW = new description.Description(s);
            DW.Show();
        }


        
        private void bt_addClient_Click(object sender, RoutedEventArgs e)
        {
            Client.AddHumanWindow ACW = new Client.AddHumanWindow(0);
            ACW.ShowDialog();

            update_dg_clientList();
        }

        private void bt_editClient_Click(object sender, RoutedEventArgs e){

            Classes.Client c = (Classes.Client)((Classes.Client.displayInfo)((System.Windows.Controls.Button)e.Source).DataContext).CurInstance;
            BoVeloManager.UI.Sales.Client.EditHumanWindow ECW = new UI.Sales.Client.EditHumanWindow(c);
            ECW.ShowDialog();
            update_dg_clientList();

        }

        private void bt_addSale_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            

            if (tb_search.Text == "")
            {
                // Use the brush 'tb_searchBackGround' to paint the button's background.
                tb_search.Background = tb_searchBackGround;
                update_dg_salesList();
            }
            else
            {
                tb_search.Background = null;
                update_dg_salesList_search(tb_search.Text);
            }


        }

        private void tb_searchClient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_searchClient.Text == "")
            {
                // Use the brush 'tb_searchBackGround' to paint the button's background.
                tb_searchClient.Background = tb_searchBackGround;
                update_dg_clientList();
            }
            else
            {
                tb_searchClient.Background = null;
                update_dg_clientList_search(tb_searchClient.Text);
            }

        }
    }
}
