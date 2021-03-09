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
using BoVeloManager.Classes;

namespace BoVeloManager.Management.item
{
    /// <summary>
    /// Logique d'interaction pour modCompatibleItemWindow.xaml
    /// </summary>
    /// 

    public partial class modCompatibleItemWindow : Window
    {

        //private int kitId;
        private CatalogBike bc;

        private List<int> updateRequestList = new List<int>{} ;
        private List<string> updateRequestList_add_or_del = new List<string> { };


        DataTable dt_item;
        public modCompatibleItemWindow(CatalogBike bc_)
        {
            InitializeComponent();

            // initialise the windows
            bc = bc_;

            //display the kit data
            tb_BikeName.Text = bc.getName();
            tb_title.Text = "Edit compatible kits with " + bc.getName() + "bike" ; 

            set_dg_selCompatibleKit_content();
        }


        private void BTCancel_Click(object sender, RoutedEventArgs e){
            this.Close();
        }

        private void BT_update_Click(object sender, RoutedEventArgs e){
            List<selectKit> sktList = (List<selectKit>)dg_selCompatibleKit.ItemsSource;

            foreach(selectKit skt in sktList) {

                
                if (skt.itemChecked) { //we want to add the kit

                    //if we don't have it -> add it (do nothing if we already have it)
                    if (!bc.getKitTemplateList().Contains(skt.curKit)) {
                        bc.linkKitTemplate(skt.curKit);
                        tools.DatabaseClassInterface.linkKTCB(bc, skt.curKit);
                    }

                } else { //we want to remove the kit

                        //if we have it -> remove it (do nothing if not)
                    if (bc.getKitTemplateList().Contains(skt.curKit)) {
                        bc.unlinkKitTemplate(skt.curKit);
                        tools.DatabaseClassInterface.unlinkKTCB(bc, skt.curKit);
                    }

                }

            }

            MessageBox.Show("updated");
            this.Close();
        }

        private void set_dg_selCompatibleKit_content() {

            List<KitTemplate> ktList = Controler.Instance.getKitTemplateList();

            List<selectKit> temp = new List<selectKit>();

            foreach(KitTemplate kt in ktList) {

                selectKit sk = new selectKit();
                sk.name = kt.getName();
                sk.price = kt.GetDisplayInfo().price;
                sk.prop = kt.getProperties();
                sk.cat = kt.GetDisplayInfo().category;
                sk.curKit = kt;
                sk.itemChecked = (bc.getKitTemplateList().Contains(kt));

                temp.Add(sk);
            }


            dg_selCompatibleKit.ItemsSource = temp;
        }    
    
        private class selectKit {

            public KitTemplate curKit;

            public string name { get; set; }
            public string price { get; set; }
            public string prop { get; set; }
            public string cat { get; set; }

            public bool itemChecked { get; set; }

        }
    
    }
}
