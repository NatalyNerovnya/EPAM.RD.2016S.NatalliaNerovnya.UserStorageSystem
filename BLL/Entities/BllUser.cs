namespace BLL.Entities
{
    using System;
    using System.Collections.Generic;
    using BLL.Interfaces;

    public enum Gender
    {
        Male = 1,
        Female
    }

    [Serializable]
    public class BllUser : IEntity
    {
        public BllUser()
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public List<BllVisa> VisaRecords { get; set; }
    }
}
