namespace BLL.Communication
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    [Serializable]
    public class Communicator : IDisposable
    {
        private Sender sender;
        private Task recieverTask;
        private CancellationTokenSource tokenSource;
        private Receiver receiver;

        public Communicator(Sender sender, Receiver receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }

        public Communicator(Sender sender) : this(sender, null)
        {
        }

        public Communicator(Receiver receiver) : this(null, receiver)
        {
        }

        public event Action UserAdded;

        public event Action UserDeleted;

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

        public void Connect(IEnumerable<IPEndPoint> endPoints)
        {
            if (this.sender == null)
            {
                return;
            }

           this.sender.Connect(endPoints);
        }

        public void StopReceiver()
        {
            if (this.tokenSource.Token.CanBeCanceled)
            {
                this.tokenSource.Cancel();
            }
        }

       public void SendAdd()
        {
            if (this.sender == null)
            {
                return;
            }

            var id = this.receiver.Receive().UserId;
            this.Send(new Message(true, id));
        }

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