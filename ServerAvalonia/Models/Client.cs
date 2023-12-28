using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerAvalonia.Models
{
    public class Client
    {
        //конструктор, будет передавать клиента о принятого TcpListener
        public Client(TcpClient client)
        {
            //текст, отправляемый клиентк
            //string textRequest = "<html><body><h1>It works!</h1></body></html>";

            //сам запрос: сервер, заголовки, контент
            //string request = "HTTP/1.1 200 OK\nContent-type: text/html\nContent-Length:" + textRequest.Length.ToString() + "\n\n" + textRequest;

            string request = "hello world!";

            //переводим запрос в массив байт
            byte[] buffer = Encoding.ASCII.GetBytes(request);

            //отправляем севреру                        клиенту
            client.GetStream().Read(buffer, 0, buffer.Length);



            client.Close();
        }
    }
}
