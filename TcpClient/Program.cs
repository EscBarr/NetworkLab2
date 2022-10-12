// See https://aka.ms/new-console-template for more information
using NetworkLab2;

int Port = 25565;
string IP = "127.0.0.1";
ClientTcp Client = new ClientTcp(Port, IP);
Console.WriteLine("Введите сообщение:");
string message = Console.ReadLine();
Client.SendData(message);
Console.ReadLine();