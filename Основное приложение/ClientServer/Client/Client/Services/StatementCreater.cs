

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace Client.Services
{
    public class StatementCreater
    {
        /// <summary>
        /// Создание заполненого заявления в PDF
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="patronymic"></param>
        /// <param name="phone"></param>
        /// <param name="room"></param>
        /// <param name="dateOut"></param>
        /// <param name="dateIn"></param>
        public static void CreateStatement(string name, string surname, string patronymic, string phone, string room, string dateOut, string dateIn)
        {
            using var document = new Document(new PdfDocument(new PdfWriter("helloworld-pdf.pdf")));

            var par1 = new Paragraph("right text").SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            var par2 = new Paragraph("center text").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            var par3 = new Paragraph("left text").SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);



            document.Add(par1);
            document.Add(par2);
            document.Add(par3);
        }
        /// <summary>
        /// создание пустого заявления в PDF
        /// </summary>
        public static void CreateStatementEmpty()
        {
            using var document = new Document(new PdfDocument(new PdfWriter("helloworld-pdf.pdf")));
            document.Add(new Paragraph("Hello World!"));
            //RT.ViewPDF(new Test(), "Test.pdf");
        }
    }
}
