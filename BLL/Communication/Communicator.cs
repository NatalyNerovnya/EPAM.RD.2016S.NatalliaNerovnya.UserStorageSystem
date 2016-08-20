namespace BLL.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// class for communication between master and slaves
    /// </summary>
    [Serializable]
    public class Communicator : IDisposable
    {
        private Sender sender;
        private Task recieverTask;
        private CancellationTokenSource tokenSource;
        private Receiver receiver;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sender">who send message</param>
        /// <param name="receiver">for whom message is sent</param>
        public Communicator(Sender sender, Receiver receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }

        /// <summary>
        /// Constructor for sender only
        /// </summary>
        /// <param name="sender">sender of the email</param>
        public Communicator(Sender sender) : this(sender, null)
        {
        }

        /// <summary>
        /// Constructor for receiver
        /// </summary>
        /// <param name="receiver">receiver of the message</param>
        public Communicator(Receiver receiver) : this(null, receiver)
        {
        }

        public event Action UserAdded;

        public event Action UserDeleted;

        /// <summary>
        /// Begin listenning
        /// </summary>
        public void RunReceiver()
        {
            this.receiver.AcceptConnection();
            if (this.receiver == null)
            {
                return;
            }

            this.tokenSource = new CancellationTokenSource();
            this.recieverTask = Task.Run((Action)this.ReceiveMessages, this.tokenSource.Token);
        }

        /// <summary>
        /// Connection to the sender
        /// </summary>
        /// <param name="endPoints">Ienumerable ipendPoints</param>
        public void Connect(IEnumerable<IPEndPoint> endPoints)
        {
            if (this.sender == null)
            {
                return;
            }

           this.sender.Connect(endPoints);
        }

        /// <summary>
        /// Stop listening
        /// </summary>
        public void StopReceiver()
        {
            if (this.tokenSource.Token.CanBeCanceled)
            {
                this.tokenSource.Cancel();
            }
        }

        /// <summary>
        /// Send message about adding user
        /// </summary>
       public void SendAdd()
        {
            if (this.sender == null)
            {
                return;
            }

            var id = this.receiver.Receive().UserId;
            this.Send(new Message(true, id));
        }

        /// <summary>
        /// Send message about deleting user
        /// </summary>
        public void SendDelete()
        {
            if (this.sender == null)
            {
                return;
            }

            this.Send(new Message(false, 1));
        }
        
        public void Dispose()
        {
           this.receiver?.Dispose();
            this.sender?.Dispose();
        }

        protected virtual void OnUserDeleted()
        {
            this.UserDeleted?.Invoke();
        }

        protected virtual void OnUserAdded()
        {
            this.UserAdded?.Invoke();
        }

        private void Send(Message message)
        {
            this.sender.Send(message);
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                if (this.tokenSource.IsCancellationRequested)
                {
                    return;
                }

                var message = this.receiver.Receive();
                if (message.IsAdd)
                {
                    this.OnUserAdded();
                }
                else
                {
                    this.OnUserDeleted();
                }
            }
        }
    }
}