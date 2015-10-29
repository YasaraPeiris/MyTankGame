using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace MyTankGame
{

    class Client
    {
        GameGrid grid = null;

        private NetworkStream clientStream; //Stream - outgoing
        private TcpClient client; //To talk back to the client
        private BinaryWriter writer; //To write to the clients

        private NetworkStream serverStream; //Stream - incoming        
        private TcpListener listener; //To listen to the clinets        
        public string reply = ""; //The message to be written

        int serverPort = 6000;
        int clientPort = 7000;
        String x = Console.ReadLine();
        Thread receive_thread;
        Thread send_thread;
        // private static Class1 comm = new Class1();

        public Client(GameGrid gr)
        {
            this.grid = gr;
        }

        public void startRecieve()
        {

            receive_thread = new Thread(new ThreadStart(ReceiveData));
            receive_thread.Start();
        }
        public void startSend()
        {

            send_thread = new Thread(new ThreadStart(SendCommands));
            send_thread.Start();
        }


        public void ReceiveData()
        {

            bool errorOcurred = false;
            Socket connection = null; //The socket that is listened to       
            try
            {
                //Creating listening Socket
                this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), clientPort);
                //Starts listening
                this.listener.Start();
                //Establish connection upon client request
                //      DataObject dataObj;
                while (true)
                {
                    //connection is connected socket
                    connection = listener.AcceptSocket();
                    if (connection.Connected)
                    {
                        //To read from socket create NetworkStream object associated with socket
                        this.serverStream = new NetworkStream(connection);

                        SocketAddress sockAdd = connection.RemoteEndPoint.Serialize();
                        string s = connection.RemoteEndPoint.ToString();
                        List<Byte> inputStr = new List<byte>();

                        int asw = 0;
                        while (asw != -1)
                        {
                            asw = this.serverStream.ReadByte();
                            inputStr.Add((Byte)asw);
                        }

                        reply = Encoding.UTF8.GetString(inputStr.ToArray());



                        this.serverStream.Close();
                        string ip = s.Substring(0, s.IndexOf(":"));
                        int port = 100;
                        try
                        {
                            string ss = reply.Substring(0, reply.IndexOf(";"));
                            port = Convert.ToInt32(ss);
                        }
                        catch (Exception)
                        {
                            port = 100;
                        }
                        Console.WriteLine(ip + ": " + reply);
                        //   dataObj = new DataObject(reply.Substring(0, reply.Length - 1), ip, port);
                        //String message = reply.Substring(0, reply.Length - 1);
                        // ThreadPool.QueueUserWorkItem(new WaitCallback(Program.Resolve),message);
                        grid.readServerMessage(reply);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (RECEIVING) Failed! \n " + e.Message);
                errorOcurred = true;
            }
            finally
            {
                if (connection != null)
                    if (connection.Connected)
                        connection.Close();
                if (errorOcurred)
                    this.ReceiveData();
            }
        }

        public void SendData()
        {
            //DataObject dataObj = (DataObject)stateInfo;
            //Opening the connection
            this.client = new TcpClient();

            try
            {


                this.client.Connect(IPAddress.Parse("127.0.0.1"), serverPort);

                if (this.client.Connected)
                {
                    //To write to the socket
                    this.clientStream = client.GetStream();

                    //Create objects for writing across stream
                    this.writer = new BinaryWriter(clientStream);
                    Byte[] tempStr = Encoding.ASCII.GetBytes(x);

                    //writing to the port                
                    this.writer.Write(tempStr);
                    Console.WriteLine("\t Data: " + x + " is written to " + IPAddress.Parse("127.0.0.1") + " on " + 6000);
                    this.writer.Close();
                    this.clientStream.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (WRITING) to " + IPAddress.Parse("127.0.0.1") + " on " + 6000 + "Failed! \n " + e.Message);
            }
            finally
            {
                this.client.Close();
            }
        }

        public void SendCommands()
        {
            Object t = Console.ReadKey(true).Key;
            string msg = "";
            while (grid.mytank.status)
            {
                if (t.Equals(ConsoleKey.UpArrow))
                {
                    msg = "UP#";
                }
                else if (t.Equals(ConsoleKey.DownArrow))
                {
                    msg = "DOWN#";
                }
                else if (t.Equals(ConsoleKey.LeftArrow))
                {
                    msg = "LEFT#";
                }
                else if (t.Equals(ConsoleKey.RightArrow))
                {
                    msg = "RIGHT#";
                }
                else
                {
                    msg = "wrong";
                    grid.mytank.status = false;
                }
            }
            {
                try
                {
                    this.client = new TcpClient();
                    this.client.Connect("127.0.0.1", 6000);

                    if (this.client.Connected)
                    {
                        //To write to the socket
                        NetworkStream clientStream = client.GetStream();
                        BinaryWriter writer = new BinaryWriter(clientStream);
                        //Create objects for writing across stream
                        writer = new BinaryWriter(clientStream);
                        Byte[] tempStr = Encoding.ASCII.GetBytes(msg);

                        //writing to the port                
                        writer.Write(tempStr);
                        writer.Close();
                        clientStream.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public void SendCommands2()
        {
            Console.WriteLine("inside sendcommands");
            //DataObject dataObj = (DataObject)stateInfo;
            //Opening the connection
            this.client = new TcpClient();

            try
            {
                this.client.Connect(IPAddress.Parse("127.0.0.1"), serverPort);

                if (this.client.Connected)
                {
                    Console.WriteLine("inside if");
                    //To write to the socket
                    NetworkStream stream = client.GetStream();

                    //Create objects for writing across stream
                    this.writer = new BinaryWriter(clientStream);

                    //parse user inputs
                    string msg = "";
                    while (grid.mytank.status)
                    {
                        Console.WriteLine("inside while status");
                        Object t = Console.ReadKey(true).Key;

                        if (t.Equals(ConsoleKey.UpArrow))
                        {
                            msg = "UP#";
                        }
                        else if (t.Equals(ConsoleKey.DownArrow))
                        {
                            msg = "DOWN#";
                        }
                        else if (t.Equals(ConsoleKey.LeftArrow))
                        {
                            msg = "LEFT#";
                        }
                        else if (t.Equals(ConsoleKey.RightArrow))
                        {
                            msg = "RIGHT#";
                        }
                        else
                        {
                            msg = "wrong";
                            grid.mytank.status = false;
                        }
                        Console.WriteLine(msg);
                        //Create objects for writing across stream
                        Byte[] tempStr = Encoding.ASCII.GetBytes(msg);

                        //writing to the port                
                        this.writer.Write(tempStr);


                    }
                    this.writer.Close();
                    stream.Close();


                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (WRITING) to " + IPAddress.Parse("127.0.0.1") + " on " + 6000 + "Failed! \n " + e.Message);
            }
            finally
            {
                this.client.Close();
            }
        }

    }
}
