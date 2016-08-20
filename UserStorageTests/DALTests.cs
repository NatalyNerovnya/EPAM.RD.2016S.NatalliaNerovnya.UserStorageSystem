using System.Collections;
using System.Collections.Generic;
using DAL.Concrete;
using DAL.Entities;

namespace UserStorageTests
{
    using DAL;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
     
    [TestClass]
    public class DALTests
    {
        [TestMethod]
        public void UserIdItteratorTests_MakeIterator()
        {
            int[] primeNumbers = {2, 3, 5, 7};
            var userIterator = new UserIdIterator();
            int [] result = new int[4];

            var iterator = userIterator.MakeGenerator();
            for (int i = 0; i < 4; i++)
            {
                iterator.MoveNext();
                result[i] = iterator.Current;
            }

            CollectionAssert.AreEqual(primeNumbers, result);
        }

        [TestMethod]
        public void UserIdItteratorTests_GetUserId()
        {
            int[] primeNumbers = {2, 3, 5, 7, 11, 13, 17};
            var userIterator = new UserIdIterator();
            int[] result = new int[7];

            for (int i = 0; i < 7; i++)
            {
                result[i] = userIterator.GetUserId();
            }

            CollectionAssert.AreEqual(primeNumbers, result);
        }

        [TestMethod]
        public void ValidatorTests_NotFool()
        {
            bool result = true;
            var val = new Validator();

            Assert.AreEqual(result, val.Validate(user));
        }

        [TestMethod]
        public void ValidatorTests_FoolSecondName()
        {
            bool result = false;
            var val = new Validator();

            Assert.AreEqual(result, val.Validate(MmmmFool));
        }

        [TestMethod]
        public void ValidatorTests_FoolFirstName()
        {
            bool result = false;
            var val = new Validator();
            

            Assert.AreEqual(result, val.Validate(FoolAaa));
        }

        [TestMethod]
        public void RepositoryTests_AddFool()
        {
            int result = -1;
            
            Assert.AreEqual(result, repo.Add(MmmmFool));
        }

        [TestMethod]
        public void RepositoryTests_AddUser()
        {
            int result = 2;

            Assert.AreEqual(result, repo.Add(user));
        }

        [TestMethod]
        public void RepositoryTests_DeleteUser()
        {
            var us1 = new User() {FirstName = "Natalia", LastName = "Nerovnya"};
            var us2 = new User() {FirstName = "nata"};
            var us3 = new User() {FirstName = "Nataly"};
            var result = new UserRepository();
            result.Add(us1);
            result.Add(us2);
            result.Add(us3);

            repo.Add(us1);
            repo.Add(us2);
            repo.Add(us3);
            repo.Add(user);

            repo.Delete(user);

            CollectionAssert.AreEqual((ICollection)result.GetAll(), (ICollection)repo.GetAll());
        }

        private UserRepository repo = new UserRepository();
        private User MmmmFool = new User()
        {
            FirstName = "Mmmm",
            LastName = "Fool",
            Id = 1
        };
        private User FoolAaa = new User()
        {
            FirstName = "Fool",
            LastName = "Aaa",
            Id = 1
        };
        private static User user = new User()
        {
            FirstName = "Nata",
            LastName = "Nero",
            Id = 1
        };

        private IEnumerable<User> users = new User[]{new User() {FirstName = "Natalia", LastName = "Nerovnya"},
            new User() {FirstName = "nata"},
            new User() {FirstName = "Nataly"}, user };
    }
}
