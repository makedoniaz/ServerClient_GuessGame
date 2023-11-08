using System.Net.Sockets;
using System.Text;

public class Program
{
    static string ip = "127.0.0.1";
    static short port = 8000;

    private static void Main()
    {
        var clientSocket = new Socket(
            addressFamily: AddressFamily.InterNetwork,
            socketType: SocketType.Stream,
            protocolType: ProtocolType.Tcp
        );

        clientSocket.Connect(ip, port);

        byte[] buffer = new byte[1024];
        clientSocket.Receive(buffer);

        Console.WriteLine(Encoding.UTF8.GetString(buffer));

        while (true)
        {
            var num = Console.ReadLine();
            clientSocket.SendAsync(Encoding.UTF8.GetBytes(num));

            Array.Clear(buffer);
            clientSocket.Receive(buffer);

            Console.WriteLine(Encoding.UTF8.GetString(buffer));
        }
    }
}