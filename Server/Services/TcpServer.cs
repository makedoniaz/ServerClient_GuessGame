using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services;

public class TcpServer
{
    private Socket _socket;
    private IPEndPoint _endpoint;

    public EndPoint EndPoint => _endpoint;

    public TcpServer(string ip, ushort port)
    {
        _socket = new Socket(
            addressFamily: AddressFamily.InterNetwork,
            socketType: SocketType.Stream,
            protocolType: ProtocolType.Tcp
        );

        var adress = IPAddress.Parse(ip);
        _endpoint = new IPEndPoint(adress, port);
    }

    public void OpenConnection(int backlog = 5)
    {
        _socket.Bind(_endpoint);
        _socket.Listen(backlog);
    }

    public Socket AcceptClient() => _socket.Accept();

    public void SendMessage(Socket receiver, string message) => receiver.Send(Encoding.UTF8.GetBytes(message));

    public void SendMessageAsync(Socket receiver, string message) => receiver.SendAsync(Encoding.UTF8.GetBytes(message));
}
