using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoVeloManager.Classes;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace BoVeloManager.tools {
    class ExportPdf 
        {
        

        public static void ManipulatePdf(Sale sale) {

            string dest;

            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "Pdf Files|*.pdf";

            DialogResult DR = SFD.ShowDialog();

            if (DR == DialogResult.OK) {

                dest = SFD.FileName;

            } else if(DR == DialogResult.Cancel){
                return;
            } else {
                return;
            }

            FileInfo file = new FileInfo(dest);
            file.Directory.Create();

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            var saleDesc = sale.GetSaleDisplayInfo();

            Table Headertable = new Table(2, false)
                .UseAllAvailableWidth();

            // Adding picture 
            MemoryStream ms = new MemoryStream();
            tools.SVGRender.render(2000,2000).Save(ms, ImageFormat.Png);
            byte[] bmpBytes = ms.ToArray();

            ImageData imgD = ImageDataFactory.Create(bmpBytes);
            Image img = new Image(imgD);
            img.SetHeight(80);
            img.SetWidth(80);
            

            // Adding sale infos 

            Paragraph info = new Paragraph("Seller :  " + saleDesc.seller.getName() + "\n" + "Client :  " + saleDesc.client_name + "\n" + "Enterprise name :  " + saleDesc.client.getEtpName() + "\n" + "Enterprise adress :  " + saleDesc.client.getEtpAdress())
                .SetBorder(new SolidBorder(1))
                .SetFontSize(13)
                .SetPaddingLeft(5);


            Cell cell_img = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.LEFT)
               .SetWidth(81)
               .Add(img);

            Cell cell_parag = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.WHITE)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(info);

            Headertable.AddCell(cell_img);
            Headertable.AddCell(cell_parag);


            // Creating Header
            Paragraph header = new Paragraph("RECIEPT")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(20);

            // add new table

            Table table = new Table(4, false)
                .UseAllAvailableWidth();

            Cell cell_qnt = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("Qnt"));
            Cell cell_name = new Cell(1, 1)
              .SetBackgroundColor(ColorConstants.GRAY)
              .SetTextAlignment(TextAlignment.LEFT)
              .Add(new Paragraph("Bike Name"));
            Cell cell_Kits = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.LEFT)
               .Add(new Paragraph("Kits"));
            Cell cell_price = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.RIGHT)
               .Add(new Paragraph("Price"));


            List<Cell> cells = new List<Cell>();

            var TbikeDesc = sale.GetSaleDescrInfo();
            foreach (Sale.TbikeInfo tBike in TbikeDesc.TbikeInfoList) {

                Cell cell1 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .SetWidth(40)
                   .Add(new Paragraph(tBike.qnt.ToString()+ "x"));

                Cell cell2 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .Add(new Paragraph(tBike.CurTempl.getName()));

                Cell cell3 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.LEFT)
                   .Add(new Paragraph(tBike.CurTempl.getPropkitString()));

                Cell cell4 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.RIGHT)
                   .Add(new Paragraph(((tBike.CurTempl.getPrice()* tBike.qnt)/100).ToString("c2")));

                cells.Add(cell1);
                cells.Add(cell2);
                cells.Add(cell3);
                cells.Add(cell4);
            }

            Cell cell_total_text = new Cell(1, 2)
               .SetBackgroundColor(ColorConstants.WHITE)
               .SetTextAlignment(TextAlignment.RIGHT)
               .Add(new Paragraph("Total Price   "));

            Cell cell_total = new Cell(1, 2)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.RIGHT)
               .Add(new Paragraph((sale.getTotalPrice()/100).ToString("c2")).SetFontColor(ColorConstants.WHITE));

            Cell cell_total_text_tva = new Cell(1, 2)
                .SetBackgroundColor(ColorConstants.WHITE)
                .SetTextAlignment(TextAlignment.RIGHT)
                .Add(new Paragraph("Total Price TVA (21%)   "));

            Cell cell_total_tva = new Cell(1, 2)
                .SetBackgroundColor(ColorConstants.GRAY)
                .SetTextAlignment(TextAlignment.RIGHT)
                .Add(new Paragraph(((sale.getTotalPrice()*1.21)/100).ToString("c2")).SetFontColor(ColorConstants.WHITE));

            table.AddCell(cell_qnt);
            table.AddCell(cell_name);
            table.AddCell(cell_Kits);
            table.AddCell(cell_price);

            foreach (Cell tempCell in cells) {
                table.AddCell(tempCell);
            }

            table.AddCell(cell_total_text);
            table.AddCell(cell_total);
            table.AddCell(cell_total_text_tva);
            table.AddCell(cell_total_tva);


            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());

            doc.Add(Headertable);
            doc.Add(header);
            doc.Add(ls);
            doc.Add(table);
            doc.Close();
            ProcessStartInfo startInfo = new ProcessStartInfo(dest);
            Process.Start(startInfo);
        }

        public static string showDialog() {

            FolderBrowserDialog browser = new FolderBrowserDialog();
            string tempPath = "";

            if (browser.ShowDialog() == DialogResult.OK) {
                tempPath = browser.SelectedPath; // prints path
            }
            return tempPath;
        }
    }
}