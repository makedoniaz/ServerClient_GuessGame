using System.Net.Sockets;
using System.Net;

namespace Shared.Tcp.Base;

public abstract class TcpConnection : IDisposable
{
    protected Socket _socket;

    public TcpConnection()
    {
        _socket = new Socket(
            addressFamily: AddressFamily.InterNetwork,
            socketType: SocketType.Stream,
            protocolType: ProtocolType.Tcp
        );
    }

    public TcpConnection(Socket socket)
    {
        _socket = socket;
    }

    public void Dispose()
    {
        _socket.Close();
        _socket.Dispose();
    }
}
