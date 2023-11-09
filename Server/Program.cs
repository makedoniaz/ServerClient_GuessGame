using Shared.Tcp.Client;
using Shared.Tcp.ConnectionStatus;
using Shared.Tcp.Message;
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

    static uint amountOfTries = 5;

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


            ThreadPool.QueueUserWorkItem<MyTcpClient>((socket) =>
            {
                int numToGuess = randNumGenerator.Next(0, 101);
                Console.WriteLine($"{clientSocket} number to guess: {numToGuess}");

                try
                {
                    var rules = "I came up with a number between 0 and 100.\nYour goal to guess this number withing 5 tries...\n";
                    socket.SendMessage(new MessageWithConnectionStatus(rules, TcpConnectionStatus.ConnectionProceeds));
                    while (true)
                    {
                        var response = socket.ReceiveMessage();

                        int.TryParse(response.Text, out int resNum);

                        Console.WriteLine($"{socket}: {resNum}");

                        if (resNum == numToGuess)
                        {
                            socket.SendMessage(new MessageWithConnectionStatus("You won!", TcpConnectionStatus.ConnectionEnded));
                            break;
                        }
                        else if (amountOfTries == 0)
                            socket.SendMessage(new MessageWithConnectionStatus("You lost!", TcpConnectionStatus.ConnectionEnded));
                        else
                            socket.SendMessage(new MessageWithConnectionStatus("Wrong answer!", TcpConnectionStatus.ConnectionProceeds));

                        amountOfTries--;
                    }

                    Console.WriteLine($"{socket} disconnected from server!");
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"{socket} disconnected");
                }
            }, clientSocket, false);
        }
    }
}