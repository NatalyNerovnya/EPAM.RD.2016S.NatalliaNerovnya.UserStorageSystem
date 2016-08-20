namespace DAL.Concrete
{
    using System;
    using Entities;
    using Interfaces;

    /// <summary>
    /// Validator
    /// </summary>
    public class Validator : IValidator
    {
        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="user">user to be validated</param>
        /// <returns>False if th user is fool</returns>
        public bool Validate(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            return user.FirstName != "Fool" && user.LastName != "Fool";
        }
    }
}
