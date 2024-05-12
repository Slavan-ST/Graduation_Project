

using Helper.Models.Main;
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
        public static void CreateStatement(Student student, string dateOut, string dateIn)
        {
            var font = PdfFontFactory.CreateFont($"{Environment.CurrentDirectory}\\Fonts\\timesnewromanpsmt.ttf", "Identity-H");
            
            using var document = new Document(new PdfDocument(new PdfWriter("заявление.pdf")));
            document.SetFont(font);
            document.SetFontSize(14);

            string roomNum = (student.Room == null)? 20.ToString() : student.Room.Number;
            string ageCategory = (student.Age >= 18) ? "совершеннолетний" : "несовершеннолетний";

            var par1 = new Paragraph(
                new iText.Layout.Element.Text(

                    "Зам директора по ВР" + Environment.NewLine +
                    "О.А. Крапп" + Environment.NewLine +
                    "студента" + Environment.NewLine +
                    $"{student.Surname} {student.Name} {student.Patronymic} комн {roomNum}" + Environment.NewLine +
                    $"{ageCategory}" + Environment.NewLine)

                ).SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT);

            Paragraph par2;

            if (student.Age >= 18)
            {
                par2 = new Paragraph(

                    $"Прошу отпустить с {dateOut} по {dateOut} по адресу: {student.Address}" + Environment.NewLine +
                    $"{student.RepresentativeSurname} {student.RepresentativeName} {student.RepresentativePatronymic}: {student.RepresentativePhone}" +
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine
                ).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            }
            else
            {
                par2 = new Paragraph(

                    $"Прошу отпустить с {dateOut} по {dateOut} по адресу: {student.Address}" + Environment.NewLine +
                    $"Мой номер: {student.Phone}" +
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine +
                    Environment.NewLine
                ).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
            }
            Paragraph par3 = new Paragraph(
                
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
