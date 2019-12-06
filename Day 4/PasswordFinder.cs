using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode_2019
{
    public class PasswordFinder
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public PasswordFinder(string range)
        {
            Min = Int32.Parse(range.Split('-')[0]);
            Max = Int32.Parse(range.Split('-')[1]);
        }

        public int ValidPasswordCount()
        {
            int count = 0;
            for (int i = 0; i < Max - Min; i++)
            {
                string password = (Min + i).ToString();
                if (HasTwoAdjacentDigits(password) && IsIncreasing(password) && HasGroupCountOf2(password))
                {
                    count++;
                }
            }
            return count;
        }

        private bool HasTwoAdjacentDigits(string password)
        {

            for (int i = 0; i < password.Length; i++)
            {
                char prior = ' ';
                char after = ' ';
                if (i != 0)
                {
                    prior = password[i - 1];
                }
                if (i != password.Length - 1)
                {
                    after = password[i + 1];
                }
                if (password[i] == prior || password[i] == after)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsIncreasing(string password)
        {
            for (int i = 0; i < password.Length; i++)
            {
                int prior = 0;
                int after = 0;
                if (i != 0)
                {
                    prior = Int32.Parse(password[i - 1].ToString());
                }
                if (i != password.Length - 1)
                {
                    after = Int32.Parse(password[i + 1].ToString());
                }
                if (Int32.Parse(password[i].ToString()) < prior)
                {
                    return false;
                }
            }
            return true;
        }

        private bool HasGroupCountOf2(string password)
        {
            var counts = password.GroupBy(e => e).ToDictionary(g => g.Key, g => g.Count());
            foreach (var item in counts)
            {
                if (item.Value == 2)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
