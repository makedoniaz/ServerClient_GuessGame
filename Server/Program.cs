using Server.Services;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    static string ip = "127.0.0.1";
    static ushort port = 8000;

    private static async Task Main()
    {
        var tcpServer = new TcpServer(ip, port);

        var randNumGenerator = new Random();
        int numToGuess = randNumGenerator.Next(0, 101);

        tcpServer.OpenConnection();
        Console.WriteLine($"Server started on {ip}:{port}");

        while (true)
        {
            var clientSocket = tcpServer.AcceptClient();
            Console.WriteLine($"{clientSocket.RemoteEndPoint} connected");
            Console.WriteLine($"Number to guess: {numToGuess}");

            ThreadPool.QueueUserWorkItem(async (socket) =>
            {
                var rules = "I came up with a number between 0 and 100.\nYour goal to guess this number withing 5 tries...\n";
                TcpServer.SendMessage(socket, rules);

                while (true)
                {
                    var buffer = new byte[1024];
                    socket.Receive(buffer);

                    int.TryParse(Encoding.UTF8.GetString(buffer), out int resNum);

                    Console.WriteLine(resNum);

                    if (resNum == numToGuess)
                    {
                        await TcpServer.SendMessageAsync(socket, "You won!");
                        break;
                    }

                    else
                        await TcpServer.SendMessageAsync(socket, "Wrong answer!");
                }
            }, clientSocket, false);
        }
    }
}