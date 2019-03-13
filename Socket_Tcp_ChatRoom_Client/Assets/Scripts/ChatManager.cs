using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour {
    public string ipaddress = "192.168.102.108";
    public int port = 7788;

    public InputField inputField;
    public Text chatLabel;

    private Socket clientSocket;
    private Thread t;
    private byte[] data = new byte[1024];//数据容器
    private string message = "";//消息容器

    // Use this for initialization
    void Start () {
        ConnectToServer();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (message != null && message != "")
        {
            chatLabel.text += "\n" + message;
            message = "";//清空消息
        }
		
	}
    void ConnectToServer()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //发起连接
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ipaddress), port));
        //创建一个新的线程用来接收消息
        t = new Thread(ReceiveMessage);
        t.Start();
    }
    //这个线程方法用来循环接收消息
    void ReceiveMessage()
    {
        while (true)
        {
            if (clientSocket.Connected == false)
                break;
            int length = clientSocket.Receive(data);
            message = Encoding.UTF8.GetString(data, 0, length);
            
        }
    }
    //发送消息
    void SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        clientSocket.Send(data);
    }
    //按下发送按钮
    public void OnSendButtonClick()
    {
        string value = inputField.text;
        SendMessage(value);
        inputField.text = "";
    }
    void OnDestroy()
    {
        clientSocket.Shutdown(SocketShutdown.Both);
        clientSocket.Close();//关闭连接
    }
  
}
