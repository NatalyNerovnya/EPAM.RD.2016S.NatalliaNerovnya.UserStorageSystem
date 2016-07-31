﻿namespace DAL.Concrete
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class UserIdIterator : IUserIdIterator
    {
        private IEnumerator<int> iterator;

        public UserIdIterator()
        {
            this.iterator = this.MakeGenerator();
        }

        public int GetUserId()
        {
            if (this.iterator.MoveNext())
            {
                return this.iterator.Current;
            }

            return -1;
        }

        public IEnumerator<int> MakeGenerator(int initialValue = 0)
        {
            int i = initialValue;
            while (true)
            {
                while (!IsPrime(i))
                {
                    i++;
                }

                yield return i;

                checked
                {
                    i++;
                }
            }
        }

        #region Private Methods
        private static bool IsPrime(int n)
        {
            if (n >= int.MaxValue)
            {
                throw new ArgumentException();
            }

            if (n < 2)
            {
                return false;
            }

            int sqrt = (int)Math.Pow(n, 0.5);
            for (int i = 2; i <= sqrt; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
