

using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.IO;
using System.Text;
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
        /// <param name="address"></param>
        /// <param name="repName"></param>
        /// <param name="repSurname"></param>
        /// <param name="repPatronymic"></param>
        /// <param name="repPhone"></param>
        public static void CreateStatement(
            string name, 
            string surname, 
            string patronymic, 
            string phone, 
            string room, 
            string dateOut,
            string dateIn, 
            string address,
            string repName, 
            string repSurname, 
            string repPatronymic, 
            string repPhone,
            string ageCategory = "несовершеннолетний")
        {
            var font = PdfFontFactory.CreateFont($"{Environment.CurrentDirectory}\\Fonts\\timesnewromanpsmt.ttf", "Identity-H");
            
            using var document = new Document(new PdfDocument(new PdfWriter("заявление.pdf")));
            document.SetFont(font);
            document.SetFontSize(14);


            var par1 = new Paragraph(
                new iText.Layout.Element.Text(

                    "Зам директора по ВР" + Environment.NewLine +
                    "О.А. Крапп" + Environment.NewLine +
                    "студента" + Environment.NewLine +
                    $"{surname} {name} {patronymic} комн {room}" + Environment.NewLine +
                    $"{ageCategory}" + Environment.NewLine)

                ).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            var par2 = new Paragraph(

                    $"Прошу отпустить с {dateOut} по {dateOut} по адресу {address}" + Environment.NewLine +
                    $"{repSurname} {repName} {repPatronymic}: {repPhone}" +
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine



                ).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            var par3 = new Paragraph(
                
                    $"{dateOut}"


                ).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);



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
