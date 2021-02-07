using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BoVeloManager.tools {
    class colorPicker {

        public static string getColor() {
            ColorDialog CD = new ColorDialog();

            CD.ShowDialog();

            return CD.Color.Name;
        }

    }
}
