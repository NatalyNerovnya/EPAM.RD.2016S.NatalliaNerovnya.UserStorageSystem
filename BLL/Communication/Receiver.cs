namespace BLL.Communication
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// class-receiver for communication
    /// </summary>
    [Serializable]
    public class Receiver : IDisposable
    {
        private Socket listener;
        private Socket reciever;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ipAddress">ip adress of receiver</param>
        /// <param name="port">Port</param>
        public Receiver(IPAddress ipAddress, int port)
        {
            this.IpEndPoint = new IPEndPoint(ipAddress, port);
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.listener.Bind(this.IpEndPoint);
            this.listener.Listen(1);
        }

        public int UserId { get; set; }

        public IPEndPoint IpEndPoint { get; set; }

        /// <summary>
        /// Take lisstener
        /// </summary>
        public void AcceptConnection()
        {
            this.reciever = this.listener.Accept();
        }

        /// <summary>
        /// Desserialize message(receive it)
        /// </summary>
        /// <returns>Messge</returns>
        public Message Receive()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Message message;

            using (var networkStream = new NetworkStream(this.reciever, false))
            {
                message = (Message)formatter.Deserialize(networkStream);
            }

            return message;
        }

        public void Dispose()
        {
            this.reciever?.Close();
            this.listener?.Close();
        }
    }
}
