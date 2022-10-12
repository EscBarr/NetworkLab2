using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace NetworkLab2
{

    public class ClientTcp
    {
        int Port;
        string IP;
        Socket Connection;
        public ClientTcp(int port, string iP)
        {
            Port = port;
            IP = iP;
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(iP), port);
                Connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Connection.Connect(ipPoint);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SendData(string message)
        {
            try
            {
                
                byte[] data = Encoding.Unicode.GetBytes(message);
                Connection.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = Connection.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (Connection.Available > 0);
               
               
                // закрываем сокет
                Connection.Shutdown(SocketShutdown.Both);
                Connection.Close();
                Console.WriteLine("ответ сервера: " + builder.ToString());
             
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }
    }
}
