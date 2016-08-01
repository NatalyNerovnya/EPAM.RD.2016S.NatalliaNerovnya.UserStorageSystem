namespace BLL.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization.Formatters.Binary;

    [Serializable]
    public class Sender : IDisposable
    {
        private IList<Socket> sockets = new List<Socket>();

        public void Connect(IEnumerable<IPEndPoint> ipEndPoints)
        {
            foreach (var ipEndPoint in ipEndPoints)
            {
                var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
                this.sockets.Add(socket);
            }
        }

        public void Connect(IPEndPoint ipEndPoint)
        {
            var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipEndPoint);
            this.sockets.Add(socket);
        }

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

            Console.WriteLine("Message is sent!");
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
