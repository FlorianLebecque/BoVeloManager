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

namespace BoVeloManager.UI.Planning.description {
    /// <summary>
    /// Interaction logic for description.xaml
    /// </summary>
    public partial class description : Window {

        private Bike bk;
        private bool has_init = false;

        public description(Bike bk_) {
            InitializeComponent();
            has_init = true;
            bk = bk_;

            init();

        }

        public void init() {
            
            int state = bk.getState();

            if (state == 2) {
                cb_state.IsEnabled = false;
                dp_pld_date.IsEnabled = false;
                cb_poste.IsEnabled = false;
            }

            tb_name.Text = bk.getBikeTempl().getName();
            cb_state.SelectedIndex = state;
            dp_pld_date.SelectedDate = bk.getPlannedtDate();
            tb_price.Text = bk.GetDisplayInfo().price;
            cb_poste.SelectedIndex = bk.getPoste();

            List<KitTemplate.displayInfo> listKit = new List<KitTemplate.displayInfo>();
            foreach (KitTemplate kt in bk.getBikeTempl().getListKit()) {
                listKit.Add(kt.GetDisplayInfo());
            }

            lv_kitList.ItemsSource = listKit;
        }

        private void BT_close_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void cb_poste_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (has_init) {
                bk.setPoste(cb_poste.SelectedIndex);
                tools.DatabaseClassInterface.updateBike(bk);
                init();
            }
        }

        private void tb_pld_date_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {
            DatePicker dp = (DatePicker)sender;

            if (dp.SelectedDate != null) {

                DateTime sel = (DateTime)dp.SelectedDate;

                if (sel.Date >= DateTime.Now.Date) {
                    bk.setPlannedDate(sel);
                    tools.DatabaseClassInterface.updateBike(bk);
                    init();
                } else {
                    MessageBox.Show("You can't choose this date");
                    dp.SelectedDate = bk.getPlannedtDate();
                }
            }
        }
    }
}
