namespace BLL.Entities
{
    using System;

    [Serializable]
    public struct BllVisa
    {
        public string Country { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
