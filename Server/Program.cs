using Server.Services;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    static string ip = "127.0.0.1";
    static ushort port = 8000;

    private static void Main()
    {
        var tcpServer = new TcpServer(ip, port);

        var randNumGenerator = new Random();
        int numToGuess = randNumGenerator.Next(0, 101);

        tcpServer.OpenConnection();
        Console.WriteLine($"Server started on {ip}:{port}");

        var clientSocket = tcpServer.AcceptClient();
        Console.WriteLine($"{clientSocket.RemoteEndPoint} connected");
        Console.WriteLine($"Number to guess: {numToGuess}");

        var rules = "I came up with a number between 0 and 100.\nYour goal to guess this number withing 5 tries...\n";
        tcpServer.SendMessage(clientSocket, rules);

        while (true)
        {
            var buffer = new byte[1024];
            clientSocket.Receive(buffer);

            int.TryParse(Encoding.UTF8.GetString(buffer), out int resNum);

            if (resNum == numToGuess)
            {
                clientSocket.Send(Encoding.UTF8.GetBytes("You won!"));
                break;
            }

            else
                clientSocket.Send(Encoding.UTF8.GetBytes("Wrong answer!"));
        }
    }
}