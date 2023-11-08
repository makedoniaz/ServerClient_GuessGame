using Shared.Tcp.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Tcp.Client;

public class MyTcpClient : TcpConnection
{
    public MyTcpClient(Socket clientSocket) : base(clientSocket) { }

    public MyTcpClient() : base() { }

    public void Connect(string ip, ushort port) => _socket.Connect(ip, port);

    public void SendMessage(string message) => _socket.Send(Encoding.UTF8.GetBytes(message));

    public Task<int> SendMessageAsync(string message) => _socket.SendAsync(Encoding.UTF8.GetBytes(message));

    public string ReceiveMessage()
    {
        var buffer = new byte[1024];
        _socket.Receive(buffer);

        return Encoding.UTF8.GetString(buffer);
    }

    public override string ToString()
    {
        return $"{_socket.RemoteEndPoint}";
    }
}
