using System;

namespace AdventOfCode_2019
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string range = "359282-820401";
            PasswordFinder finder = new PasswordFinder(range);
            Console.WriteLine(finder.ValidPasswordCount());
        }

        //public int[] cellCompete(int[] states, int days)
        //{
        //    while (days > 1)
        //    {
        //        int[] newState = new int[8];
        //        for (int i = 0; i < states.Length; i++)
        //        {
        //            if (states[] )
        //            {

        //            }
        //        }
        //    }

        //}

        public void mutate(int[] states, int pos)
        {

        }
    }
}
