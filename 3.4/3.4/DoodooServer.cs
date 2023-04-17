using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _3._4_
{
    internal class DoodooServer
    {
        private TcpListener myServer;
        private bool isRunning;

        public DoodooServer(int port)
        {
            myServer = new TcpListener(System.Net.IPAddress.Any, port);
            myServer.Start();
            isRunning = true;
            Console.WriteLine("Server running.");
            ServerLoop();
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server waiting for connection.");
            while (isRunning)
            {
                TcpClient client = myServer.AcceptTcpClient();
                ClientLoop(client);
            }
        }

        private void ClientLoop(TcpClient client)
        {

            try
            {
                StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
                StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);

                writer.WriteLine("Byl jsi pripojen");
                writer.WriteLine("v");
                writer.Flush();
                bool clientConnect = true;
                string? data = null;
                string? dataRecive = null;



                while (clientConnect)
                {
                    data = reader.ReadLine();
                    data = data.ToLower();

                    
                    writer.WriteLine("------------------------");
                    writer.Flush();

                    switch (data)
                    {
                        case "exit":
                            clientConnect = false;

                            break;

                        case "date":
                            writer.WriteLine(DateTime.Now);
                            writer.Flush();
                            break;

                        case "help":
                            writer.WriteLine("|\ndate => Prints current date and time.");
                            writer.WriteLine("|\nipconfig => prints the adress of the server");
                            writer.Flush();
                            break;

                        case "ipconfig":
                            writer.WriteLine("127.0.0.1");
                            writer.Flush();
                            break;

                        default:
                            writer.WriteLine("Command not known.");
                            writer.Flush();
                            break;
                    }
                    writer.WriteLine("v");
                    writer.Flush();


                }
                writer.WriteLine("Byl jsi odpojen");
                writer.Flush();
                Console.WriteLine("Client disconnected.");
            }
            catch(Exception e)
            {
                ServerLoop();
            }

           
        }
    }
}
