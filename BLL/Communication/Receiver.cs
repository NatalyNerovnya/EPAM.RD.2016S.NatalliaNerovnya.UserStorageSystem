namespace BLL.Communication
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;

    [Serializable]
    public class Receiver : IDisposable
    {
        private Socket listener;
        private Socket reciever;

        public Receiver(IPAddress ipAddress, int port)
        {
            this.IpEndPoint = new IPEndPoint(ipAddress, port);
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.listener.Bind(this.IpEndPoint);
            this.listener.Listen(1);
        }

        public int UserId { get; set; }

        public IPEndPoint IpEndPoint { get; set; }

        public void AcceptConnection()
        {
            ////Console.WriteLine("Wait Connection");
            this.reciever = this.listener.Accept();
            ////Console.WriteLine("Connection accepted");
        }

        ////public Task AcceptConnection()
        ////{
        ////    return Task.Run(() =>
        ////    {
        ////        //Console.WriteLine("Wait Connection");
        ////        reciever = listener.Accept();
        ////        //Console.WriteLine("Connection accepted");
        ////    });

        ////}

        public Message Receive()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Message message;

            using (var networkStream = new NetworkStream(this.reciever, false))
            {
                message = (Message)formatter.Deserialize(networkStream);
            }

            Console.WriteLine("User {0} :  {1}!", message.UserId, message.IsAdd == true ? "add new" : "delete");
            return message;
        }

        public void Dispose()
        {
            this.reciever?.Close();
            this.listener?.Close();
        }
    }
}
