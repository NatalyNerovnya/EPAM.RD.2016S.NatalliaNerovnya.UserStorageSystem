namespace BLL.Communication
{
    using System;

    /// <summary>
    /// Message for communication
    /// </summary>
    [Serializable]
    public class Message
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isAdd">Add - true, delete - false</param>
        /// <param name="id">users' id</param>
        public Message(bool isAdd, int id)
        {
            this.IsAdd = isAdd;
            this.UserId = id;
        }

        public bool IsAdd { get; set; }

        public int UserId { get; set; }
    }
}
