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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BoVeloManager.UI.Commande {
    /// <summary>
    /// Interaction logic for Command.xaml
    /// </summary>
    /// 

    public partial class Command : Page {

        private static Command instance = new Command();

        public Command() {
            InitializeComponent();
        }

        public static Command Instance {
            get {
                return instance;
            }
        }

    }

    
}
