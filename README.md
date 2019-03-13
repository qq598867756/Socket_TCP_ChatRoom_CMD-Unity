# Socket_TCP_ChatRoom_CMD-Unity
本示例是Socket Tcp 局域网通讯 
服务端俩脚本一个Client类，一个Program类；客户端是Unity项目
下载文件后首先将所有脚本里的ip改为本机ip，Windows可通过ipconfig命令查看本机ip，linux通过ifconfig
服务端两个脚本可在命令行直接编译
例如：csc /out:D:\test\Socket_Tcp_Server.dll /t:library D:\test\Client.cs D:\test\Program.cs直接生成Socket_Tcp_Server.dll文件
然后csc /out:D:\test\Program.exe /R:D:\test\Socket_Tcp_Server.dll D:\test\Program.cs 直接生成Program.exe文件
（“D:\test\”是文件路径；“Socket_Tcp_Server”是两个脚本的命名空间）
首先运行服务端，然后打开unity项目运行就可以通讯了
