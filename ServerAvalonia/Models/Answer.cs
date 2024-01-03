using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Models
{
    /// <summary>
    /// Запрос серверу/ответ клиенту
    /// </summary>
    public class Answer
    {
        public Answer(string header, byte[] content)
        {
            Header = header;
            Content = content;
        }

        //статус ответа
        public string Status { get; set; } = "";     //error? error!

        public string Header { get;} = "";
        //тип содержимого
        public string ContentType { get; set; } = "";  //whf?!
        public byte[]? Content { get;}//ну.. content!

        public int LengthHeader { get;} = 0;
        public int LengthContent { get;} = 0;
    }
}
