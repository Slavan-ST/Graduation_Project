using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientAvalonia.Models.TestFromOld
{
    public class WorkingWithServer
    {
        static byte[] messageBytes = new byte[1024];        //1024 байта      массивы для отправки данных
        static byte[] imageBytes = new byte[1024000];       //+- мегабайт
        private static Socket Start()
        {
            int port = 11000;            
            IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Loopback);   //хост                   
            IPAddress ipAddr = ipHost.AddressList[0];                    //адресс
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);        //конечная точка, адрес + порт
            
            Socket sender = new Socket(
                ipAddr.AddressFamily, 
                SocketType.Stream, 
                ProtocolType.Tcp);

            sender.Connect(ipEndPoint);
            return sender;
        }
        /// <summary>
        /// Открывает соединение
        /// </summary>
        /// <param name="sender"></param>
        private static void Stop(Socket sender)
        {
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
        }
        public static void SendMessage()
        {
            string message = "Hello world!!";
            byte[] msg = Encoding.UTF8.GetBytes(message);

            Socket sender = Start();
            sender.Send(msg);                                                   // Отправляем данные через сокет
            int bytesRec = sender.Receive(messageBytes);                        //получение данных
            string otvet = Encoding.UTF8.GetString(messageBytes, 0, bytesRec);  //преобразование полученных данных в текст

            Stop(sender);
        }
    }
}
