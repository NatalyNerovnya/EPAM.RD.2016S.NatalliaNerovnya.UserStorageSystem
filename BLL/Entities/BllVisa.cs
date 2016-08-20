namespace BLL.Entities
{
    using System;

    /// <summary>
    /// Visa entity for bll level
    /// </summary>
    [Serializable]
    public struct BllVisa
    {
        public string Country { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
