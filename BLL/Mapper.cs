namespace BLL
{
    using System.Linq;
    using DAL.Entities;
    using Entities;

    /// <summary>
    /// mappers for bll and dal entities
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// mapper from bll to dal entity
        /// </summary>
        /// <param name="user">User in bll</param>
        /// <returns>User in dal</returns>
        public static User ToDalUser(this BllUser user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = (DAL.Entities.Gender)user.Gender,
                BirthDate = user.BirthDate,
                VisaRecords = user.VisaRecords?.Select(v => v.ToDalVisa()).ToList()
            };
        }

        /// <summary>
        /// mapper from dal to bll user
        /// </summary>
        /// <param name="user">User in dal</param>
        /// <returns>User in bll</returns>
        public static BllUser ToBllUser(this User user)
        {
            return new BllUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = (Entities.Gender)user.Gender,
                BirthDate = user.BirthDate,
                VisaRecords = user.VisaRecords?.Select(v => v.ToBllVisa()).ToList()
            };
        }

        /// <summary>
        /// Mapper for bll and dal visa
        /// </summary>
        /// <param name="visa">Visa entity in bll</param>
        /// <returns>Visa entity in dal</returns>
        public static Visa ToDalVisa(this BllVisa visa)
        {
            return new Visa
            {
                Country = visa.Country,
                End = visa.End,
                Start = visa.Start
            };
        }

        /// <summary>
        /// Mapper for bll and dal visa
        /// </summary>
        /// <param name="visa">Visa entity in dal</param>
        /// <returns>Visa entity in bll</returns>
        public static BllVisa ToBllVisa(this Visa visa)
        {
            return new BllVisa
            {
                Country = visa.Country,
                End = visa.End,
                Start = visa.Start
            };
        }
    }
}
