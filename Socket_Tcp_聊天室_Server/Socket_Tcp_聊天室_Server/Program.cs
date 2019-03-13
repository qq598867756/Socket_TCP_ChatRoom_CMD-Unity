using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Socket_Tcp_聊天室_Server
{
    class Program
    {
        static List<Client> clientList = new List<Client>();

        public static void BroadcastMessage(string message)
        {
            var notConnectedList = new List<Client>();
            foreach (var client in clientList)
            {
                if (client.Connected)
                {
                    client.SendMessage(message);
                }
                else
                {
                    notConnectedList.Add(client);
                }
                foreach (var temp in notConnectedList)
                {
                    clientList.Remove(temp);
                }
            }
        }
        static void Main(string[] args)
        {
            Socket tcpServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpServer.Bind(new IPEndPoint(IPAddress.Parse("192.168.102.108"), 7788));
            tcpServer.Listen(100);
            Console.WriteLine("server running");
            while (true)
            {
                Socket clientSocket = tcpServer.Accept();
                Console.WriteLine("a client is connected!");
                Client client = new Client(clientSocket);//把与每个客户端通讯的逻辑（收发消息）放到client类里面进行处理
                clientList.Add(client);
            }
        }
    }
}
