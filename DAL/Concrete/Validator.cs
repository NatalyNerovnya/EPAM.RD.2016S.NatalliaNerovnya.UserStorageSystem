namespace DAL.Concrete
{
    using System;
    using Entities;
    using Interfaces;

    public class Validator : IValidator
    {
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
