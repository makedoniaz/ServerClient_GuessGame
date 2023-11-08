using System.Net.Sockets;

public class Program
{
    static string ip = "127.0.0.1";
    static short port = 8000;

    private static void Main()
    {
        var socket = new Socket(
            addressFamily: AddressFamily.InterNetwork,
            socketType: SocketType.Stream,
            protocolType: ProtocolType.Tcp
        );

        socket.Connect(ip, port);
    }
}