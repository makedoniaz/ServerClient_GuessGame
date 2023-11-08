using Shared.Tcp.Client;
using Shared.Tcp.Server;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    static string ip = "127.0.0.1";
    static ushort port = 8000;

    private static void Main()
    {
        var tcpServer = new MyTcpServer(ip, port);

        var randNumGenerator = new Random();
        int numToGuess = randNumGenerator.Next(0, 101);

        tcpServer.OpenConnection();
        Console.WriteLine($"Server started on {ip}:{port}");

        while (true)
        {
            var clientSocket = tcpServer.AcceptClient();
            Console.WriteLine($"{clientSocket} connected");


            ThreadPool.QueueUserWorkItem<MyTcpClient>(async (socket) =>
            {
                int numToGuess = randNumGenerator.Next(0, 101);
                Console.WriteLine($"{clientSocket} number to guess: {numToGuess}");

                try
                {
                    var rules = "I came up with a number between 0 and 100.\nYour goal to guess this number withing 5 tries...\n";
                    socket.SendMessage(rules);

                    while (true)
                    {
                        var numStr = socket.ReceiveMessage();

                        int.TryParse(numStr, out int resNum);

                        Console.WriteLine(resNum);

                        if (resNum == numToGuess)
                        {
                            await socket.SendMessageAsync("You won!");
                            break;
                        }

                        else
                            await socket.SendMessageAsync("Wrong answer!");
                    }
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"{socket} disconnected");
                }
            }, clientSocket, false);
        }
    }
}