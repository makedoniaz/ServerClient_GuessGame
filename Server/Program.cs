using System.Net;
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

        var adress = IPAddress.Parse(ip);
        var endpoint = new IPEndPoint(adress, port);


        socket.Bind(endpoint);
        socket.Listen(backlog: 5);
        Console.WriteLine($"Server started on {ip}:{port}");

        var clientSocket = socket.Accept();

        Console.WriteLine($"{clientSocket.RemoteEndPoint} connected");
    }
}