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

        public description(Bike bk_) {
            InitializeComponent();
            bk = bk_;

            tb_name.Text = bk.getBikeTempl().getName();
            cb_state.SelectedIndex = bk.getState();
            tb_pld_date.Text = bk.GetDisplayInfo().ConstDate;

            List<KitTemplate.displayInfo> listKit = new List<KitTemplate.displayInfo>();
            foreach (KitTemplate kt in bk.getBikeTempl().getListKit()) {
                listKit.Add(kt.GetDisplayInfo());
            }

            lv_kitList.ItemsSource = listKit;


        }
    }
}
