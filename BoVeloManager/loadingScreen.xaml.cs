using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BoVeloManager.Classes;

namespace BoVeloManager {
    /// <summary>
    /// Interaction logic for loadingScreen.xaml
    /// </summary>
    public partial class loadingScreen : Window {
        public loadingScreen() {
            InitializeComponent();

            ImageBrush myBrush = new ImageBrush();
            var bitmap = tools.SVGRender.render(200,200);
            var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            bitmap.Dispose();

            spash.Source = bitmapSource;
        }

        private void Window_ContentRendered(object sender, EventArgs e) {
            Controler a = Controler.Instance;
            pgb.Value += 1;
            Catalogue.Catalog b = Catalogue.Catalog.Instance;
            pgb.Value += 1;
            UI.Planning.Planning c = UI.Planning.Planning.Instance;
            pgb.Value += 1;
            Management.Management d = Management.Management.Instance;
            pgb.Value += 1;
            Sales.Sales f = Sales.Sales.Instance;
            pgb.Value += 1;

            LoginWindows LW = new LoginWindows();
            this.Hide();
            LW.ShowDialog();
            this.Close();
        }
    }
}
