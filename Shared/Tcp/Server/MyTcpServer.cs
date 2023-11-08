using Shared.Tcp.Base;
using Shared.Tcp.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Tcp.Server;

public class MyTcpServer : TcpConnection, IDisposable
{
    private IPEndPoint _endpoint;
    private IEnumerable<MyTcpClient> _clients;

    public EndPoint EndPoint => _endpoint;

    public MyTcpServer(string ip, ushort port) : base()
    {
        var adress = IPAddress.Parse(ip);
        _endpoint = new IPEndPoint(adress, port);

        _clients = new List<MyTcpClient>();
    }

    public void OpenConnection(int backlog = 5)
    {
        _socket.Bind(_endpoint);
        _socket.Listen(backlog);
    }

    public MyTcpClient AcceptClient() => new MyTcpClient(_socket.Accept());
}
