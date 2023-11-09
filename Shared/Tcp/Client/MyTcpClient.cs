using Shared.Tcp.Base;
using Shared.Tcp.Base.Message;
using Shared.Tcp.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Tcp.Client;

public class MyTcpClient : TcpConnection
{
    public MyTcpClient(Socket clientSocket) : base(clientSocket) { }

    public MyTcpClient() : base() { }

    public void Connect(string ip, ushort port) => _socket.Connect(ip, port);

    public void SendMessage(MessageWithConnectionStatus message)
    {
        var json = JsonSerializer.Serialize(message);
        _socket.Send(Encoding.UTF8.GetBytes(json));
    }

    public MessageWithConnectionStatus ReceiveMessage()
    {
        var buffer = new byte[1024];
        _socket.Receive(buffer);

        var json = Encoding.UTF8.GetString(buffer);
        json = json.Replace("\0", string.Empty);
        var message = JsonSerializer.Deserialize<MessageWithConnectionStatus>(json);

        return message ?? throw new Exception("Invalid message!");
    }

    public override string ToString()
    {
        return $"{_socket.RemoteEndPoint}";
    }
}
