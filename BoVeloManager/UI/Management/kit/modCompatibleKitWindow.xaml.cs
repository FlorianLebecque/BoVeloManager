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

namespace BoVeloManager.Management.kit
{
    /// <summary>
    /// Logique d'interaction pour modCompatibleKitWindow.xaml
    /// </summary>
    public partial class modCompatibleKitWindow : Window
    {
        private KitTemplate kt;


        public modCompatibleKitWindow(KitTemplate kt_) {
            InitializeComponent();

            // initialise the windows
            kt = kt_;


            //display the kit data
            tb_kitName.Text = kt.getName();

            set_lb_selectCompIt_content();
        }

        private void BTCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void BT_update_Click(object sender, RoutedEventArgs e) {

                //get the list of all bike
            List<selectedBike> listCBike = (List<selectedBike>) lb_selectCompBike.ItemsSource;

            foreach (selectedBike scb in listCBike) {
                CatalogBike cb = scb.curCBike;

                //check if checked or not
                if(scb.itemChecked == true) {   //we want the kit

                        //we do not have the kit (do nothing if already here
                    if (!cb.getKitTemplateList().Contains(kt)) {
                        cb.linkKitTemplate(kt);
                        tools.DatabaseClassInterface.linkKTCB(cb, kt);
                    }

                } else {                        //we don't want the kit

                        //we have the kit -> we need to remove it (if not do nothing)
                    if (cb.getKitTemplateList().Contains(kt)) {
                        cb.unlinkKitTemplate(kt);
                        tools.DatabaseClassInterface.unlinkKTCB(cb, kt);
                    }


                }

            }


            MessageBox.Show("Updated");
            this.Close();


        }

        private void set_lb_selectCompIt_content() {
            List<CatalogBike> CatalogBikeList = Controler.Instance.getCatalogBike();
            List<selectedBike> temp = new List<selectedBike>();

            int index = 0;

            foreach(CatalogBike cb in CatalogBikeList) {
                selectedBike scb = new selectedBike();
                scb.itemChecked = cb.getKitTemplateList().Contains(kt);
                scb.name = cb.getName();
                scb.curCBike = cb;
                scb.id = index;
                temp.Add(scb);

                index++;

            }

            lb_selectCompBike.ItemsSource = temp;
        }

        private class selectedBike {
            public CatalogBike curCBike;
            public int id { get; set; }
            public string name { get; set; }
            public bool itemChecked { get; set; }

        }
    }
}
