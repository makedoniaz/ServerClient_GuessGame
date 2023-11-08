using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Program
{
    static string ip = "127.0.0.1";
    static short port = 8000;

    private static void Main()
    {
        var randNumGenerator = new Random();
        int numToGuess = randNumGenerator.Next(0, 101);

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
        Console.WriteLine($"Number to guess: {numToGuess}");

        clientSocket.Send(Encoding.UTF8.GetBytes("I came up with a number between 0 and 100.\nYour goal to guess this number withing 5 tries...\n"));

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