using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ServerAvalonia.TestFromOld
{
    public class Test
    {

        static byte[] bytes = new byte[1024];           //1024 байт
        static byte[] bytesForImage = new byte[1024000];//+- мегабайт

        public static void MainServer()
        {
            SocketToo();
        }


        private static void SocketToo()
        {
            int port = 11000;
            IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Loopback);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);
                // Начинаем слушать соединения
                while (true)
                {
                    Socket handler = sListener.Accept();
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);
                    // Программа приостанавливается, ожидая входящее соединение
                    string data = null;
                    // Мы дождались клиента, пытающегося с нами соединиться
                    int bytesRec = handler.Receive(bytes);


                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    string reply = "";


                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    Debug.WriteLine(data);

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
