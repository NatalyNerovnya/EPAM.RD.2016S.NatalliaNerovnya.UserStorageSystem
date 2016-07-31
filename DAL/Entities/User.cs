namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    [Serializable]
    public class User : IEntity
    {
        public User()
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }

        public List<Visa> VisaRecords { get; set; }
    }
}
