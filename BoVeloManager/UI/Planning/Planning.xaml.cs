using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace BoVeloManager.UI.Planning {
    /// <summary>
    /// Interaction logic for Planning.xaml
    /// </summary>
    public partial class Planning : Page {


        private int nbrWeek;

        public Planning() {
            InitializeComponent();

            nbrWeek = (new GregorianCalendar(GregorianCalendarTypes.Localized).GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday));

            init();
        }

        private void init() {
            lb_nbrWeek.Text = "Week : " + nbrWeek.ToString();


            List<Bike.displayInfo> bk_dpiList = Controler.Instance.GetBikeDisplayInfo();

            //sort them to only get the one for the week


        }

        private void bt_nextWeek_Click(object sender, RoutedEventArgs e) {
            nbrWeek++;
            init();
        }

        private void bt_lastWeek_Click(object sender, RoutedEventArgs e) {
            nbrWeek--;
            init();
        }
    }
}
