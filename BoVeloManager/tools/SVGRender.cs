using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BoVeloManager.tools {
    class SVGRender {

        public static Bitmap render(int w,int h) {
            var byteArray = Properties.Resources.icon_svg;

            XmlDocument xmlDoc = new XmlDocument();

            using (MemoryStream memStream = new MemoryStream(Properties.Resources.icon_svg)) {

                xmlDoc.Load(memStream);
            }

            using (var stream = new MemoryStream(byteArray)) {
                var svgDocument = SvgDocument.Open(xmlDoc);
                var bitmap = svgDocument.Draw(w,h);
                return bitmap;
            }
        }

    }
}
