namespace BLL.Communication
{
    using System;

    [Serializable]
    public class Message
    {
        public Message(bool isAdd, int id)
        {
            this.IsAdd = isAdd;
            this.UserId = id;
        }

        public bool IsAdd { get; set; }

        public int UserId { get; set; }
    }
}
