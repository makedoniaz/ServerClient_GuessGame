using Shared.Tcp.Client;
using System.Net.Sockets;
using System.Text;

public class Program
{
    static string ip = "127.0.0.1";
    static ushort port = 8000;

    private static void Main()
    {
        var client = new MyTcpClient();

        client.Connect(ip, port);

        var rulesStr = client.ReceiveMessage();
        Console.WriteLine(rulesStr);

        while (true)
        {
            var num = Console.ReadLine();
            client.SendMessageAsync(num);

            var response = client.ReceiveMessage();
            Console.WriteLine(response);
        }
    }
}