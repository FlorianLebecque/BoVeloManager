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
using iText.Layout.Element;
using iText.Layout.Properties;


namespace BoVeloManager.tools {
    class ExportPdf 
        {
        private string dest;
        public ExportPdf(string dest_) {
            dest = dest_;
            FileInfo file = new FileInfo(dest);
            file.Directory.Create();
        }

        public void ManipulatePdf(Sale sale) {
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(dest));
            Document doc = new Document(pdfDoc);

            var saleDesc = sale.GetSaleDisplayInfo();

            // Adding picture 
            Image img = new Image(ImageDataFactory
            .Create(@"..\Untitled.jpg"))
                .SetHeight(40)
                .SetWidth(40)
            .SetTextAlignment(TextAlignment.CENTER);

            // Adding sale infos 

            Paragraph seller = new Paragraph("Seller :" + saleDesc.seller.getName());
            Paragraph client = new Paragraph("Client :" + saleDesc.client_name);
            Paragraph entName = new Paragraph("Enterprise name :" + saleDesc.client.getEtpName());
            Paragraph entAdress = new Paragraph("Enterprise adress :" + saleDesc.client.getEtpAdress());

            // Creating Header
            Paragraph header = new Paragraph("RECIEPT")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetFontSize(20);

            // add new table

            Table table = new Table(2, false);

            Cell cell11 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Bike Name"));
            Cell cell12 = new Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Qnt"));


            Cell cell21 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("New York"));
            Cell cell22 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Albany"));

            Cell cell31 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("New Jersey"));
            Cell cell32 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Trenton"));

            Cell cell41 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("California"));
            Cell cell42 = new Cell(1, 1)
               .SetTextAlignment(TextAlignment.CENTER)
               .Add(new Paragraph("Sacramento"));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell41);
            table.AddCell(cell42);

            

            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());

            doc.Add(img);
            doc.Add(seller);
            doc.Add(client);
            doc.Add(entName);
            doc.Add(entAdress);
            doc.Add(header);
            doc.Add(ls);
            doc.Add(table);

            doc.Close();
        }
    }
}