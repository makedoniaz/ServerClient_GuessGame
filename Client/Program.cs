using Shared.Tcp.Client;
using Shared.Tcp.ConnectionStatus;
using Shared.Tcp.Message;
using System.Net.Sockets;
using System.Text;

public class Program
{
    static string ip = "127.0.0.1";
    static ushort port = 8000;

    private static async Task Main()
    {
        var client = new MyTcpClient();

        client.Connect(ip, port);

        var rulesMsg = client.ReceiveMessage();
        Console.WriteLine(rulesMsg.Text);
        try
        {
            while (true)
            {
                var num = Console.ReadLine();
                client.SendMessage(new MessageWithConnectionStatus(num, TcpConnectionStatus.ConnectionProceeds));

                var response = client.ReceiveMessage();
                Console.WriteLine(response.Text);

                if (response.Status == TcpConnectionStatus.ConnectionEnded)
                    Environment.Exit(0);
            }
        }
        catch (SocketException)
        {
            Console.WriteLine($"{ip}:{port} connection lost");
        }
    }
}