namespace BLL.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// class-sender for communication
    /// </summary>
    [Serializable]
    public class Sender : IDisposable
    {
        private IList<Socket> sockets = new List<Socket>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ipEndPoints">Collection of ip end points</param>
        public void Connect(IEnumerable<IPEndPoint> ipEndPoints)
        {
            foreach (var ipEndPoint in ipEndPoints)
            {
                var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
                this.sockets.Add(socket);
            }
        }

        /// <summary>
        /// Creation and connection to socket
        /// </summary>
        /// <param name="ipEndPoint"></param>
        public void Connect(IPEndPoint ipEndPoint)
        {
            var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEndPoint);
            this.sockets.Add(socket);
        }

        /// <summary>
        /// Send message (serialize)
        /// </summary>
        /// <param name="message">Message to be send</param>
        public void Send(Message message)
        {
            foreach (var socket in this.sockets)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (NetworkStream networkStream = new NetworkStream(socket, false))
                {
                    formatter.Serialize(networkStream, message);
                }
            }
        }

        public void Dispose()
        {
            foreach (var socket in this.sockets)
            {
                socket?.Shutdown(SocketShutdown.Both);
                socket?.Close();
            }
        }
    }
}
