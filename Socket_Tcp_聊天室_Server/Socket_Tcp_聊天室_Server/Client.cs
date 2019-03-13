using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Socket_Tcp_聊天室_Server
{
    class Client
    {
        private Thread t;
        private Socket clientSocket;
        private byte[] data = new byte[1024];//数据容器
        public Client(Socket s)
        {
            clientSocket = s;
            //启动一个线程处理客户端数据接收
            t = new Thread(ReceiveMessage);
            t.Start();
        }

        private void ReceiveMessage()
        {
            //一直接收客户端数据
            while (true)
            {
                //在接收之前判断是否断开
                if (clientSocket.Poll(10, SelectMode.SelectRead))
                {
                    break;//跳出循环，终止线程执行
                }
                int length = clientSocket.Receive(data);
                string message = Encoding.UTF8.GetString(data, 0, length);
                //TODO：接收到数据要把数据分发到每一个客户端
                //广播这个消息
                Program.BroadcastMessage(message);
                Console.WriteLine("收到了消息" + message);
            }
        }
        //分发消息
        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);
            Console.WriteLine("向客户端分发消息" + data);
        }
        //是否断开连接
        public bool Connected
        {
            get{ return clientSocket.Connected; }
        }
    }
}
