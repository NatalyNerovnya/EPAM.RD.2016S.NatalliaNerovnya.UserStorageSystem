using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Communication
{
    [Serializable]
    public class Communicator : IDisposable
    {
        public event Action UserAdded;
        public event Action UserDeleted;
        private Sender _sender;
        private Task recieverTask;
        private CancellationTokenSource tokenSource;
        private Receiver _receiver;

        public Communicator(Sender sender, Receiver receiver)
        {
            _sender = sender;
            _receiver = receiver;
        }

        public Communicator(Sender sender) : this(sender, null) { }
        public Communicator(Receiver receiver) : this(null, receiver) { }

        public void RunReceiver()
        {
            _receiver.AcceptConnection();
            if (_receiver == null)
                return;
            tokenSource = new CancellationTokenSource();
            recieverTask = Task.Run((Action)ReceiveMessages, tokenSource.Token);

        }

        //public async void RunReceiver()
        //{
        //    await _receiver.AcceptConnection();
        //    if (_receiver == null) return;
        //    tokenSource = new CancellationTokenSource();
        //    recieverTask = Task.Run((Action)ReceiveMessages, tokenSource.Token);
        //}

        public void Connect(IEnumerable<IPEndPoint> endPoints)
        {
            if (_sender == null)
                return;
            _sender.Connect(endPoints);
        }

        public void StopReceiver()
        {
            if (tokenSource.Token.CanBeCanceled)
            {
                tokenSource.Cancel();
            }
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                if (tokenSource.IsCancellationRequested) return;
                var message = _receiver.Receive();
                if (message.IsAdd)
                {
                    OnUserAdded();
                }
                else
                {
                    OnUserDeleted();
                }
            }
        }

        public void SendAdd()
        {
            if (_sender == null)
                return;
            //////////////////////
            var id = _receiver.Receive().UserId;
            Send(new Message(true, id));
        }
        public void SendDelete()
        {
            if (_sender == null)
                return;
            /////////////////////////
            Send(new Message(false, 1));
        }

        private void Send(Message message)
        {
            _sender.Send(message);
        }

        protected virtual void OnUserDeleted()
        {
            UserDeleted?.Invoke();
        }

        protected virtual void OnUserAdded()
        {
            UserAdded?.Invoke();
        }

        public void Dispose()
        {
            _receiver?.Dispose();
            _sender?.Dispose();
        }
    }
}
