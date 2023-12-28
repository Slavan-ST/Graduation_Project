using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace ServerAvalonia.Models
{
    public class Server
    {
        TcpListener Listener;
        public Server(int port)
        {
            //создаём слушиватель
            Listener = new TcpListener(System.Net.IPAddress.Any,port);
            Listener.Start();
            //принимаем новых клиентов
            while (true)
            {
                //для каждого нового клиента  создаём новый поток
                //так сервер будет работать в многопотоке
                //потоков может быть неограниченное кол-во, но справится ли сервер
                TcpClient client = Listener.AcceptTcpClient();
                Thread thread = new Thread(new ParameterizedThreadStart(ClientThread));
                thread.Start(client);
            }

        }
        //при завершении работы сервера
        ~Server()
        {
            //если слушатель найден
            if (Listener != null)
            {
                Listener.Stop();
            }
        }
        //метод созддания потока
        static void ClientThread(Object? StateInfo)
        {
            new Client((TcpClient)StateInfo!);
        }
    }
}
